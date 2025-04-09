using System;

namespace LudumDare57.DataSaving
{
    [Serializable]
    public class SaveData
    {
        public float gas;
        public int tankSegmentCount, money, debt;
        public FishCatchCount[] fishCatchCounts;
    }

    [Serializable]
    public class FishCatchCount
    {
        public string fishName;
        public int count;
    }
}