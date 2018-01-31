using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB
{
    [Serializable]
    public class SessionResult/*<S> where S : IComparable */
    {
        public PlayerID Player;
        public DateTime Date;
        public GameDifficulty DifficultyStart;
        public GameDifficulty DifficultyEnd;
        public /*S*/ int Score;

        public SessionResult(PlayerID player, GameDifficulty start, GameDifficulty end, int score)
        {
            this.Player = player;
            this.Date = new DateTime();
            this.DifficultyStart = start;
            this.DifficultyEnd = end;
            this.Score = score;
        }

        public int CompareTo(SessionResult o)
        {
            return this.Score.CompareTo(o.Score);
        }

    }
}
