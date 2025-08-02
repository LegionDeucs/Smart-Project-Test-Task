using System;

namespace SLS
{
    [System.Serializable]
    public class SaveLoadSystemCache
    {
        public int version;
        public int level = 1;

        public SaveLoadSystemCache()
        {
        }

        public int GetLevelData() => level;

        internal SaveLoadSystemCache Init()
        {
            return this;
        }
    }
}
