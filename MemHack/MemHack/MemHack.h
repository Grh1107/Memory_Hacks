#include<Windows.h>
namespace MemoryScanner {

	class MemoryScan
	{
	
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
		typedef enum
	{
		COND_UNCONDITIONAL,
		COND_EQUALS,

		COND_INCREASED,
		COND_DECREASED,
	} SEARCH_CONDITION;

		 /*__declspec(dllexport) MEMBLOCK* create_scan(unsigned int pid, int data_size);
		 __declspec(dllexport) MEMBLOCK* create_memblock (HANDLE hProc, MEMORY_BASIC_INFORMATION *meminfo, int data_size);
		 __declspec(dllexport) void free_memblock (MEMBLOCK *mb);
		 __declspec(dllexport) void update_memblock (MEMBLOCK *mb, SEARCH_CONDITION condition, unsigned int val);
		 __declspec(dllexport) MEMBLOCK* create_scan(unsigned int pid, int data_size);
		 __declspec(dllexport) void free_scan (MEMBLOCK *mb_list);
		 __declspec(dllexport) void update_scan (MEMBLOCK *mb_list, SEARCH_CONDITION condition, unsigned int val);
		 __declspec(dllexport) 	void ui_run_scan(void);
		 __declspec(dllexport)void print_matches (MEMBLOCK *mb_list);*/

		 MEMBLOCK* create_scan(unsigned int pid, int data_size);
		 MEMBLOCK* create_memblock (HANDLE hProc, MEMORY_BASIC_INFORMATION *meminfo, int data_size);
		 void free_memblock (MEMBLOCK *mb);
		 void update_memblock (MEMBLOCK *mb, SEARCH_CONDITION condition, unsigned int val);
		 void free_scan (MEMBLOCK *mb_list);
		 void update_scan (MEMBLOCK *mb_list, SEARCH_CONDITION condition, unsigned int val);
		 void ui_run_scan(void);
		 void print_matches (MEMBLOCK *mb_list);
		 bool _initializeScan(DWORD pid, int data_size, unsigned start_val);
		 bool _initializeScan(DWORD pid, int data_size);
		 void _Increased();
		 void _Decreased();
		 void _WriteAddress(unsigned int addr, unsigned int val);
		 void _Free_Scan();
		 unsigned int _ReadAddress(unsigned int addr, unsigned int val);
		 void _PrintMatches();

	};
}