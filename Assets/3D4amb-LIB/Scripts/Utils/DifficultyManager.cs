using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB
{
    public static class DifficultyManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        /// <remarks>It can't go beyond Hard</remarks>
        public static GameDifficulty GetNext(GameDifficulty now)
        {
            switch(now)
            {
                case GameDifficulty.EASY:  return GameDifficulty.MEDIUM;
                case GameDifficulty.MEDIUM: return GameDifficulty.HARD;
                default: return GameDifficulty.HARD;
            }
        }
    }
}
