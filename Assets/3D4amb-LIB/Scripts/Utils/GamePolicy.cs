using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB
{
    /// <summary>
    /// This must be configured by the Developer
    /// (it can be done directly in Unity)
    /// </summary>
    /// <remarks>This is only for game policy (the game itself),NOT the penalty</remarks>
    public class GamePolicy
    {
        public enum Continue
        {
            LAST, RESTART
        }

        public enum SaveSession
        {
            EACH, ENDGAME
        }

        public enum DifficultyProgression
        {
            FIXED, INFINITE
        }

        public enum IncreasePenalty
        {
            BY_STEPS, MANUAL
        }

        public enum IncreaseType
        {
            STATIC, DYNAMIC
        }

        public enum IncreaseResetOnDeath
        {
            LAST, TO_ZERO, NO_RESET
        }
    }
}
