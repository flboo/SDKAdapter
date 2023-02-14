namespace TGCenterSdk.Api
{
    public class Day1Retention {

        public enum RetentionTypes {
            Hour = 1,
            NatureDay = 2
        }
        
        public RetentionTypes RetentionType { get; private set; }
        public int StartCount  { get; private set; }
        public int EndCount  { get; private set; }

        public Day1Retention(RetentionTypes retentionType, int startCount, int endCount) {
            this.RetentionType = retentionType;
            this.StartCount = startCount;
            this.EndCount = endCount;
        }
    }
}