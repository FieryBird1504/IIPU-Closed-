#include <iostream>
#include <iomanip>
#include <Windows.h>
#include <WinIoCtl.h>
#include <ntddscsi.h>

using namespace std;

#define bThousand 1024
#define Hundred 100
#define BYTE_SIZE 8

char* busType[] = { "UNKNOWN", "SCSI", "ATAPI", "ATA", "ONE_TREE_NINE_FOUR", "SSA", "FIBRE", "USB", "RAID", "ISCSI", "SAS", "SATA", "SD", "MMC" };
char* driveType[] = { "UNKNOWN", "INVALID", "CARD_READER/FLASH", "HARD", "REMOTE", "CD_ROM", "RAM" };



int main() {
	STORAGE_PROPERTY_QUERY storageProtertyQuery;
	storageProtertyQuery.QueryType = PropertyStandardQuery; 
	storageProtertyQuery.PropertyId = StorageDeviceProperty;

	HANDLE diskHandle = CreateFile("//./PhysicalDrive0", GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, NULL, NULL);
	if (diskHandle == INVALID_HANDLE_VALUE) {
		cout << GetLastError();
		return -1;
	}

	getDeviceInfo(diskHandle, storageProtertyQuery);
	getAtaSupportStandarts(diskHandle);
	getMemoryInfo();

	CloseHandle(diskHandle);
	return 0;
}