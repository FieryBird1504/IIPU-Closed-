namespace IMAPI2.Interop
{
    public enum BURN_MEDIA_TASK
    {
        BURN_MEDIA_TASK_FILE_SYSTEM,
        BURN_MEDIA_TASK_WRITING
    }

    public class BurnData
    {
        public string uniqueRecorderId;
        public string statusMessage;
        public string volumeName;
        public BURN_MEDIA_TASK task;

        public long elapsedTime;
        public long remainingTime;
        public long totalTime;
        public IMAPI_FORMAT2_DATA_WRITE_ACTION currentAction;
        public long startLba;
        public long sectorCount;
        public long lastReadLba;
        public long lastWrittenLba;
        public long totalSystemBuffer;
        public long usedSystemBuffer;
        public long freeSystemBuffer;
    }
}
