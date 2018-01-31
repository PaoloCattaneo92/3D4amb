using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._3D4amb_LIB
{
    [Serializable]
    public class PlayerID
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This must be unique</remarks>
        public string PlayerName;
        public int IdAvatar;


        public PlayerID(string name/*, Gender g*/)
        {
            PlayerName = name;
        }

        public bool Equals(PlayerID o)
        {
            return this.PlayerName == o.PlayerName;
        }
    }
}

