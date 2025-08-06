namespace SLS
{
    [System.Serializable]
    public class SaveLoadSystemCache
    {
        public int version;
        public int level = 1;
        public ScoreSaveData scoreSaveData;

        public SaveLoadSystemCache()
        {
            scoreSaveData = new ScoreSaveData();
        }

        public int GetLevelData() => level;

        internal SaveLoadSystemCache Init()
        {
            return this;
        }

        [System.Serializable]
        public class ScoreSaveData
        {
            public int MaxScore;
            public ScoreSaveData()
            {
                MaxScore = -1;
            }
        }
    }
}
