// MemoryScan.cpp : Defines the entry point for the console application.
//

#include <Windows.h>
#include <stdio.h>

	#define WRITABLE (PAGE_READWRITE | PAGE_WRITECOPY | PAGE_EXECUTE_READWRITE | PAGE_EXECUTE_WRITECOPY)
	#define IS_IN_SEARCH(mb, offset) (mb->searchmask[(offset)/8] & (1<<((offset)%8)))
	#define REMOVE_FROM_SEARCH(mb, offset) (mb->searchmask[(offset)/8] &= ~(1<<((offset)%8)));
	

bool bMBhasMatch = false;

	typedef struct _MEMBLOCK
	{
		HANDLE hProc;
		unsigned char *addr;
		int size;
		unsigned char *buffer;
		
		unsigned char *searchmask;
		int matches;
		int data_size;

		struct _MEMBLOCK *next;
	} MEMBLOCK;

	typedef struct _MEMINFO
	{
		UINT32 Addr;
		UINT32 val;
	} MEMINFO;

	typedef enum
	{
		COND_UNCONDITIONAL,
		COND_EQUALS,

		COND_INCREASED,
		COND_DECREASED,
		COND_INCREASED_BY,
		COND_DECREASED_BY,
		COND_CHANGED,
		COND_REMAINED,
		COND_GREATERTHAN,
		COND_LESSTHAN
	} SEARCH_CONDITION;

	
	MEMBLOCK *INITscan = NULL; 
	MEMBLOCK* LastMatch;
	MEMINFO* RetMatch;

	MEMBLOCK* create_memblock (HANDLE hProc, MEMORY_BASIC_INFORMATION *meminfo, int data_size)
	{
		MEMBLOCK *mb = (MEMBLOCK*)malloc(sizeof(MEMBLOCK));
	
		if (mb)
		{
			mb->hProc = hProc;
			mb->addr = (unsigned char*)meminfo->BaseAddress;
			mb->size = meminfo->RegionSize;
			mb->buffer = (unsigned char*)malloc(meminfo->RegionSize);
			mb->searchmask = (unsigned char*)malloc (meminfo->RegionSize/8);
			memset(mb->searchmask, 0xff, meminfo->RegionSize/8);
			mb->matches = meminfo->RegionSize;
			mb->data_size = data_size;
			mb->next = NULL;
		}

		return mb;
	}

	void free_memblock (MEMBLOCK *mb)
	{
		if(mb)
		{
			if(mb->buffer)
			{
				free (mb->buffer);
			}
			if (mb->searchmask)
			{
				free(mb->searchmask);
			}
			free(mb);
		}
	}

	// should be signed for val
	void update_memblock (MEMBLOCK *mb, SEARCH_CONDITION condition, unsigned int val)
	{
		static unsigned char tempbuf[128*1024];
		unsigned int bytes_left;
		unsigned int total_read;
		unsigned int bytes_to_read;
		unsigned int bytes_read;
		bMBhasMatch = false;
		// greater than 1?	
		if (mb->matches > 0) 
		{
			bytes_left = mb->size;
			total_read = 0;
			mb->matches = 0;

			while (bytes_left)
			{
				bytes_to_read = (bytes_left > sizeof(tempbuf)) ? sizeof(tempbuf) : bytes_left;
				ReadProcessMemory(mb->hProc, mb->addr + total_read, tempbuf, bytes_to_read, (DWORD*)&bytes_read);
		
				if(bytes_read != bytes_to_read) break;
	
				if(condition == COND_UNCONDITIONAL)
				{
					memset (mb->searchmask + (total_read/8), 0xff, bytes_read/8);
					mb->matches += bytes_read;
					bMBhasMatch = true;
				}
				else
				{
					unsigned int offset;
					for(offset = 0; offset < bytes_read; offset += mb->data_size)
					{
						if (IS_IN_SEARCH(mb, (total_read+offset)))
						{
							bool is_match = FALSE;
							unsigned int temp_val;
							unsigned int prev_val = 0;
							switch (mb->data_size)
							{
							case 1:
								temp_val = tempbuf[offset];
								prev_val = *((unsigned char*)&mb->buffer[total_read+offset]);
								break;
							case 2:
								temp_val = *((unsigned short*)&tempbuf[offset]);
								prev_val =*((unsigned short*)&mb->buffer[total_read+offset]);
								break;
							case 4:
							default:
								temp_val = *((unsigned int*)&tempbuf[offset]);
								prev_val =*((unsigned int*)&mb->buffer[total_read+offset]);
								break;

							}
							switch (condition)
							{
							case COND_EQUALS:
								is_match = (temp_val==val);
								break;
							case COND_INCREASED:
								is_match = (temp_val>prev_val);
								break;
							case COND_DECREASED:
								is_match = (temp_val<prev_val);
								break;
							case COND_CHANGED:
								is_match = (temp_val!=prev_val);
								break;
							case COND_REMAINED:
								is_match = (temp_val==prev_val);
								break;
							case COND_INCREASED_BY:
								is_match = (temp_val==(prev_val+val));
								break;
							case COND_DECREASED_BY:
								is_match = (temp_val==(prev_val-val));
								break;
							case COND_GREATERTHAN:
								is_match = ((int)temp_val > (int)val);
								break;
							case COND_LESSTHAN:
								is_match = ((int)temp_val < (int)val);
								break;
							default:
								break;
							}
							if (is_match)
							{
								mb->matches++;
								bMBhasMatch = true;
							}
							else
							{
								REMOVE_FROM_SEARCH(mb, (total_read + offset));
							}
						}
					}
				}


				memcpy(mb->buffer + total_read, tempbuf, bytes_read);

				bytes_left -= bytes_read;
				total_read += bytes_read;
			}
			mb->size = total_read;
		}
	}

	MEMBLOCK* create_scan(unsigned int pid, int data_size)
	{
		MEMBLOCK *mb_list = NULL;
		MEMORY_BASIC_INFORMATION meminfo;
		unsigned char* addr = 0;

		HANDLE hProc = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pid);

		if (hProc)
		{
			SYSTEM_INFO si;
			GetSystemInfo(&si);
		
			while(1)
			{
				if((VirtualQueryEx(hProc, addr, &meminfo, sizeof(meminfo)) == 0) || !(addr < si.lpMaximumApplicationAddress))
				{
					break;
				}
				if((meminfo.State & MEM_COMMIT) && (meminfo.Protect & WRITABLE));
				{
					MEMBLOCK *mb = create_memblock (hProc, &meminfo, data_size);
					if (mb)
					{
						mb->next = mb_list;
						mb_list = mb;
					}
				}
				addr = (unsigned char*) meminfo.BaseAddress + meminfo.RegionSize;
			}
			return mb_list;
		}
	}

	void free_scan (MEMBLOCK *mb_list)
	{
		if (mb_list != NULL)
		{
			CloseHandle(mb_list->hProc);
			while(mb_list)
			{
				MEMBLOCK *mb = mb_list;
				mb_list = mb_list->next;
				free_memblock(mb);
			}
			INITscan = NULL;
		}
	}
	//prolly signed
	void update_scan (MEMBLOCK *mb_list, SEARCH_CONDITION condition, unsigned int val)
	{
		LastMatch = NULL;
		MEMBLOCK *mb = mb_list;
		INITscan = NULL;
		while(mb)
		{
			update_memblock(mb, condition, val);
			MEMBLOCK* Temp = mb;
			mb = mb->next;
			if(bMBhasMatch)
			{
				if (LastMatch != NULL)
				{
					LastMatch->next = Temp;
				}
				else
				{
					INITscan = Temp;
				}
				LastMatch = Temp;
			}
			else
			{
				free_memblock(Temp);
				Temp = NULL;
			}
		}
		if(LastMatch)
		{
			LastMatch->next= NULL;
		}
	}

	void dump_scan_info (MEMBLOCK *mb_list)
	{
		MEMBLOCK *mb = mb_list;
	
		while(mb)
		{
			int i;
			printf("Ox%08x %d\r\n",  mb->addr, mb->size);
		
			for(i = 0; i< mb->size; i++)
			{
				printf("%02x", mb->buffer[i]);
			}
			printf ("\r\n");

			mb = mb->next;
		}
	}

	bool poke(HANDLE hProc, int data_size, unsigned int addr, unsigned int val)
	{
		if(WriteProcessMemory (hProc, (void*)addr, &val, data_size, NULL) == 0)
		{
			return false;
		}
		return true;
	}
	unsigned int peek (HANDLE hProc, int data_size, unsigned int addr)
	{
		unsigned int val = 0;
		if (ReadProcessMemory(hProc, (void*)addr, &val, data_size, NULL)==0)
		{
			printf("peek Failed\r\n");
		}
		return val;
	}

	void print_matches (MEMBLOCK *mb_list)
	{
		unsigned int offset;
		MEMBLOCK *mb = mb_list;

		while(mb)
		{
			for(offset = 0; offset < mb->size; offset += mb->data_size)
			{
				if(IS_IN_SEARCH(mb, offset))
				{
					unsigned int val = peek(mb->hProc, mb->data_size, (unsigned int)mb->addr + offset);
					printf("0x%08x: 0x%08x (%d) \r\n", mb->addr + offset, val, val);
				}
			}
			mb = mb->next;
		}
	}

	int get_match_count(MEMBLOCK *mb_list)
	{
		MEMBLOCK *mb = mb_list;
		int count = 0;

		while (mb)
		{
			count += mb->matches;
			mb = mb->next;
		}
		return count;
	}

	unsigned int str2int(char *s)
	{
		int base = 10;
		if (s[0]=='0' && s[1]=='x')
		{
			base = 16;
			s+=2;
		}
		return strtoul (s, NULL, base);
	}

	MEMBLOCK* ui_new_scan(void)
	{
		MEMBLOCK *scan = NULL;
		DWORD pid;
		int data_size;
		unsigned start_val;
		SEARCH_CONDITION start_cond;
		char s[20];

		while(1)
		{
			printf("\r\nEnter the pid: ");
			fgets (s, sizeof(s), stdin);
			pid = str2int(s);
			printf("\r\nEnter the data dize: ");
			fgets (s, sizeof(s), stdin);
			data_size = str2int(s);
			printf("\r\n Enter the start value or 'u' for unknown: ");
			fgets (s, sizeof(s), stdin);
			if (s[0] == 'u')
			{
				start_cond = COND_UNCONDITIONAL;
				start_val = 0;
			}
			else
			{
				start_cond = COND_EQUALS;
				start_val = str2int(s);
			}

			INITscan = create_scan(pid, data_size);
			if (INITscan)
				break;
		
			printf("\r\nInvalid scan");
		}
		update_scan(INITscan, start_cond, start_val);
		printf("\r\n%d matches found \r\n", get_match_count(INITscan));

		return INITscan;
	}


	void ui_poke (HANDLE hProc, int data_size)
	{
		unsigned int addr;
		unsigned int val;

		char s[20];

		printf ("Enter the address: ");
		fgets (s, sizeof(s), stdin);
		addr = str2int(s);

		printf("\r\nEnter the value: ");
		fgets (s, sizeof(s), stdin);
		val = str2int(s);
		printf("\r\n");

		poke(hProc, data_size, addr, val);
	}

	bool _initializeScan(DWORD pid, int data_size, unsigned start_val)
	{
		if(INITscan != NULL)
		{
			free_scan(INITscan);
		}
		INITscan = create_scan(pid, data_size);
		if (INITscan)
		{
			update_scan(INITscan, COND_EQUALS, start_val);
			return true;
		}
		return false;
	}

	bool _initializeScan(DWORD pid, int data_size)
	{
		if(INITscan != NULL)
		{
			free_scan(INITscan);
		}
		INITscan = create_scan(pid, data_size);
		if (INITscan)
		{
			update_scan(INITscan, COND_UNCONDITIONAL, 0);
			return true;
		}
		return false;
	}	

	void _Increased()
	{
		update_scan (INITscan, COND_INCREASED, 0);
	}

	void _IncreasedBy(UINT32 val)
	{
		update_scan (INITscan, COND_INCREASED_BY, val);
	}

	void _Decreased()
	{
		update_scan (INITscan, COND_DECREASED, 0);
	}

	void _DecreasedBy(UINT32 val)
	{
		update_scan (INITscan, COND_DECREASED_BY, val);
	}
	void _Changed()
	{
		update_scan (INITscan, COND_CHANGED, 0);
	}
	void _Remained()
	{
		update_scan (INITscan, COND_REMAINED, 0);
	}
	void _ConditionEquals(UINT32 val)
	{
		update_scan (INITscan, COND_EQUALS, val);
	}

	bool _WriteAddress(unsigned int addr, unsigned int val)
	{
		return poke(INITscan->hProc, INITscan->data_size, addr, val);
	}

	void _Free_Scan()
	{
		free_scan(INITscan);
	}
	unsigned int _ReadAddress(unsigned int addr, unsigned int val)
	{
		MEMBLOCK *mb = INITscan;
		if (ReadProcessMemory(mb->hProc, (void*)addr, &val, mb->data_size, NULL)==0)
		{
			printf("peek Failed\r\n");
		}
		return val;
	}
	void _PrintMatches()
	{
		print_matches(INITscan);
	}

	void _GreaterThan(UINT32 val)
	{
		update_scan (INITscan, COND_GREATERTHAN, val);
	}

	void _LessThan(UINT32 val)
	{
		update_scan (INITscan, COND_LESSTHAN, val);
	}

	int _get_match_count()
	{
		return get_match_count(INITscan);
	}
	
	MEMINFO* _MatchResult()
	{
		unsigned int offset;
		int x;
		MEMBLOCK *mb = INITscan;
		int Mc = _get_match_count();
		MEMINFO* Matches = (MEMINFO*)malloc(sizeof(MEMINFO)*Mc);
		if(!Matches)
		{
			return NULL;
		}
		RetMatch = Matches;

		while(mb)
		{
			for(offset = 0, x = 0; offset < mb->size; offset += mb->data_size)
			{
				if(IS_IN_SEARCH(mb, offset))
				{
					unsigned int val = peek(mb->hProc, mb->data_size, (unsigned int)mb->addr + offset);
					Matches->Addr = (unsigned int)mb->addr + offset;
					Matches->val = val;
					++Matches;

					x++;
				}
			}
			mb = mb->next;
		}
		Matches = NULL;
		return RetMatch;
	}

	void _free_returned_matches()
	{
		if(RetMatch)
		{
			free(RetMatch);
		}
		RetMatch = NULL;
	}

	void ui_run_scan(void)
	{
		unsigned int val;
		char s[20];
		MEMBLOCK* scan;

		INITscan = ui_new_scan();

		while(1)
		{
			printf ("\r\nEnter the next value or ");
			printf("\r\n[i] increased");
			printf("\r\n[d] decreased");
			printf("\r\n[m] print matches");
			printf("\r\n[p] poke address");
			printf("\r\n[n] new scan");
			printf("\r\n[q] quit");
		
			fgets(s, sizeof(s), stdin);
			printf("\r\n");
			switch (s[0])
			{
			case 'i':
				update_scan (INITscan, COND_INCREASED, 0);
				printf("%d matches found\r\n", get_match_count(INITscan));
				break;
			case 'd':
				update_scan (INITscan, COND_DECREASED, 0);
				printf("%d matches found\r\n", get_match_count(INITscan));
				break;
			case 'm':
				print_matches(INITscan);
				break;
			case 'p':
				ui_poke(INITscan->hProc, INITscan->data_size);
				break;
			case 'n':
				free_scan(INITscan);
				INITscan = ui_new_scan();
				break;
			case 'q':
				free_scan(INITscan);
				return;
			default:
				val = str2int(s);
				update_scan(INITscan, COND_EQUALS, val);
				printf("%d matches found\r\n", get_match_count(INITscan));
				break;
			}
		}

	}
