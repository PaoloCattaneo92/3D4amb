using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB.Scripts.Utils
{
    [Serializable]
    public class UserSettings
    {
        public PlayerID playerID;
        public Eye healthyEye;
        public GameDifficulty defaultDifficulty;

        public UserSettings()
        {
            playerID = new PlayerID("New Player");
            healthyEye = Eye.LEFT;
            defaultDifficulty = GameDifficulty.MEDIUM;
        }

        public UserSettings(PlayerID playerID, Eye healthyEye, GameDifficulty defaultDifficulty)
        {
            this.playerID = playerID;
            this.healthyEye = healthyEye;
            this.defaultDifficulty = defaultDifficulty;
        }
    }
}
