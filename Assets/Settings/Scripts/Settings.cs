/*******************************************************
 * Copyright (C) 2017 Doron Weiss  - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of unity license.
 * 
 * See https://abnormalcreativity.wixsite.com/home for more info
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dweiss {
	[System.Serializable]
	public class Settings : ASettings {

        [Header("-- EASY Penalty --")]
        public float easyStartTransparency = 0.0f;
        public float easyEndTransparency = 0.5f;
        public float easyStartEyePatch = 0f;
        public float easyEndEyePatch = 0f;

        [Header("-- MEDIUM Penalty --")]
        public float medStartTrasparency = 0.5f;
        public float medEndTransparency = 0.75f;
        public float medStartEyePatch = 0f;
        public float medEndEyePatch = 0.5f;

        [Header("-- HARD Penalty --")]
        public float hardStartTrasparency = 0.75f;
        public float hardEndTransparency = 1.01f;
        public float hardStartEyePatch = 0.5f;
        public float hardEndEyePatch = 1.01f;

        [Header("-- Penalize these tags --")]
        public bool PlayerTag   = false;
        public bool EnemyTag    = false;
        public bool BadItemTag  = false;
        public bool GoodItemTag = false;
        

        private new void Awake()
        {
			base.Awake ();
            SetupSingelton();
        }


        #region  Singelton
        public static Settings _instance;
        public static Settings Instance { get { return _instance; } }
        private void SetupSingelton()
        {
            if (_instance != null)
            {
                Debug.LogError("Error in settings. Multiple singeltons exists: " + _instance.name + " and now " + this.name);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion



    }
}