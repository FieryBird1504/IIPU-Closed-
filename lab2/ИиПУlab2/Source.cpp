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

void getMemoryInfo() {
	string path;
	_ULARGE_INTEGER diskSpace;
	_ULARGE_INTEGER freeSpace;

	diskSpace.QuadPart = 0;
	freeSpace.QuadPart = 0;

	_ULARGE_INTEGER totalDiskSpace;
	_ULARGE_INTEGER totalFreeSpace;
	totalDiskSpace.QuadPart = 0;
	totalFreeSpace.QuadPart = 0;

	unsigned long int logicalDrivesCount = GetLogicalDrives();
	cout.setf(ios::left);
	cout << setw(8) << "Disk"
		<< setw(16) << "Total space[Mb]"
		<< setw(16) << "Free space[Mb]"
		<< setw(16) << "Busy space[%]"
		<< setw(16) << "Driver type"
		<< endl;

	for (char var = 'A'; var < 'Z'; var++) {
		if ((logicalDrivesCount >> var - 65) & 1) {
			path = var;
			path.append(":\\");
			GetDiskFreeSpaceEx(path.c_str(), 0, &diskSpace, &freeSpace);
			diskSpace.QuadPart = diskSpace.QuadPart / (bThousand * bThousand);
			freeSpace.QuadPart = freeSpace.QuadPart / (bThousand * bThousand);

			cout << setw(8) << var
				<< setw(16) << diskSpace.QuadPart
				<< setw(16) << freeSpace.QuadPart
				<< setw(16) << setprecision(3) << 100.0 - (double)freeSpace.QuadPart / (double)diskSpace.QuadPart * Hundred
				<< setw(16) << driveType[GetDriveType(path.c_str())]
				<< endl;
			if (GetDriveType(path.c_str()) == 3) {
				totalDiskSpace.QuadPart += diskSpace.QuadPart;
				totalFreeSpace.QuadPart += freeSpace.QuadPart;
			}
		}
	}

	void getDeviceInfo(HANDLE diskHandle, STORAGE_PROPERTY_QUERY storageProtertyQuery) {
		STORAGE_DEVICE_DESCRIPTOR* deviceDescriptor = (STORAGE_DEVICE_DESCRIPTOR*)calloc(bThousand, 1);
		deviceDescriptor->Size = bThousand;

		if (!DeviceIoControl(diskHandle, IOCTL_STORAGE_QUERY_PROPERTY, &storageProtertyQuery, sizeof(storageProtertyQuery), deviceDescriptor, bThousand, NULL, 0)) {
			printf("%d", GetLastError());
			CloseHandle(diskHandle);
			exit(-1);
		}
	}

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