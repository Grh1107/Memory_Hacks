#include "..\..\MemHack\MemHack\MemHack.h"
#include "..\..\MemHack\MemHack\MemhackSource.cpp"

extern "C" __declspec(dllexport) void Run_scan(void)
{
	ui_run_scan();
}
extern "C" __declspec(dllexport) bool ConditionalScan(UINT64 pid, int data_size, UINT32 start_val)
{
	return _initializeScan(pid, data_size, start_val);
}

extern "C" __declspec(dllexport) bool UnconditionalScan(UINT64 pid, int data_size)
{
	return _initializeScan(pid, data_size);
}

extern "C" __declspec(dllexport) void Increased()
{
	_Increased();
}

extern "C" __declspec(dllexport) void Decreased()
{
	_Decreased();
}

extern "C" __declspec(dllexport) bool WriteAddress(UINT32 addr, UINT32 val)
{
	return _WriteAddress(addr, val);
}

extern "C" __declspec(dllexport) unsigned int ReadAddress(UINT32 addr, UINT32 val)
{
	return _ReadAddress(addr, val);
}

extern "C" __declspec(dllexport) void Free_Scan()
{
	_Free_Scan();
}
extern "C" __declspec(dllexport) void PrintMatches()
{
	_PrintMatches();
}
extern "C" __declspec(dllexport) void MatchCount()
{
	_get_match_count();
}
extern "C" __declspec(dllexport) void ConditionEquals(UINT32 Val)
{
     _ConditionEquals(Val);
}

extern "C" __declspec(dllexport) void IncreasedBy(UINT32 val)
{
	_IncreasedBy(val);
}

extern "C" __declspec(dllexport) void DecreasedBy(UINT32 val)
{
	_DecreasedBy(val);
}
extern "C" __declspec(dllexport) void Changed()
{
	_Changed();
}
extern "C" __declspec(dllexport) void Remained()
{
	_Remained();
}
extern "C" __declspec(dllexport) void GreaterThan(UINT32 val)
{
	_GreaterThan(val);
}
extern "C" __declspec(dllexport) void LessThan(UINT32 val)
{
	_LessThan(val);
}
extern "C" __declspec(dllexport) MEMINFO* MatchResult()
{
	return _MatchResult();
}
extern "C" __declspec(dllexport) void Free_Returned_Matches()
{
	_free_returned_matches();
}
