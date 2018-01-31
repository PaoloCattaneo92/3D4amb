using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dweiss;
using System;
using Assets._3D4amb_LIB.Scripts.Utils;

namespace Assets._3D4amb_LIB
{
    public class PenaltyManager : MonoBehaviour
    {
        /// <summary>
        /// This needs to be init at the beginning
        /// It uses this Difficulty for everything
        /// </summary>
        public GameDifficulty Difficulty;


        /// <summary>
        /// Set this in the inspector
        /// </summary>
        /// <remarks>The one to penalize</remarks>
        public Eye HealthyEye;

        /// <summary>
        /// This is how many steps the game do between the min to the max penalty
        /// (for each difficulty)
        /// </summary>
        /// <remarks>If the game is procedural and can go beyond the game go up in difficulty
        /// to the max of max difficulty</remarks>
        public int Steps;
        
        [Header("Game Policies")]
        /// <summary>
        /// 
        /// </summary>
        public GamePolicy.Continue PolicyContinue;
        public GamePolicy.DifficultyProgression PolicyDifficultyProgression;
        public GamePolicy.SaveSession PolicySaveSession;
        public GamePolicy.IncreasePenalty PolicyIncreasePenalty;
        public GamePolicy.IncreaseType PolicyIncreaseType;
        public GamePolicy.IncreaseResetOnDeath PolicyResetOnDeath;

        [Header("Static Penalty")]
        public float StaticTrasp;
        public float StaticEyePatch;
        

        public PenaltyInfo PenaltyInfoNow;
        public PenaltyInfo PenaltyInfoLast;

        public Dictionary<GameDifficulty, PenaltyInfo> StartPenalties;
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Set the maximum a little more than 1 (1.01f works). This because of roundings</remarks>
        public Dictionary<GameDifficulty, PenaltyInfo> EndPenalties;
        public Dictionary<string, bool> Penalizables;

        /// <summary>
        /// Ctor
        /// </summary>
        void Awake()
        {
            Settings s = Settings.Instance;
            UserSettings userSettings = GameObject.Find("PrefManager").GetComponent<PrefManager>().userSettings;
            HealthyEye = userSettings.healthyEye;
            Difficulty = userSettings.defaultDifficulty;

            switch (PolicyIncreaseType)
            {
                case (GamePolicy.IncreaseType.STATIC):
                    {
                        AwakeStatic();
                        break;
                    }
                case (GamePolicy.IncreaseType.DYNAMIC):
                    {
                        AwakeDynamic();
                        break;
                    }
            }

            Penalizables = new Dictionary<string, bool>()
            {
                { "PlayerTag",  s.PlayerTag     },
                { "EnemyTag",   s.EnemyTag      },
                { "BadItemTag", s.BadItemTag    },
                { "GoodItemTag",s.GoodItemTag   },
            };
        }

        protected void AwakeDynamic()
        {
            Settings s = Settings.Instance;
            StartPenalties = new Dictionary<GameDifficulty, PenaltyInfo>()
            {
                {GameDifficulty.EASY,   new PenaltyInfo(HealthyEye,
                                        s.easyStartTransparency,
                                        s.easyStartEyePatch)},
                {GameDifficulty.MEDIUM, new PenaltyInfo(HealthyEye,
                                        s.medStartTrasparency,
                                        s.medStartEyePatch)},
                {GameDifficulty.HARD,   new PenaltyInfo(HealthyEye,
                                        s.hardStartTrasparency,
                                        s.hardStartEyePatch)   }
            };

            EndPenalties = new Dictionary<GameDifficulty, PenaltyInfo>
            {
                {GameDifficulty.EASY,   new PenaltyInfo(HealthyEye,
                                        s.easyEndTransparency,
                                        s.easyEndEyePatch)},
                {GameDifficulty.MEDIUM, new PenaltyInfo(HealthyEye,
                                        s.medEndTransparency,
                                        s.medEndEyePatch)},
                {GameDifficulty.HARD,   new PenaltyInfo(HealthyEye,
                                        s.hardEndTransparency,
                                        s.hardEndEyePatch)},
            };

            PenaltyInfoNow = new PenaltyInfo(GetStartPenalty());
        }

        protected void AwakeStatic()
        {
            PenaltyInfoNow = new PenaltyInfo(HealthyEye, StaticTrasp, StaticEyePatch);
        }

        public void ResetPenalty()
        {
            switch(PolicyResetOnDeath)
            {
                case (GamePolicy.IncreaseResetOnDeath.TO_ZERO):
                    {
                        ResetPenaltyToZero();
                        break;
                    }
                case (GamePolicy.IncreaseResetOnDeath.LAST):
                    {
                        ResetPenaltyNowTo(PenaltyInfoLast);
                        break;
                    }
                case (GamePolicy.IncreaseResetOnDeath.NO_RESET):
                    {
                        break;
                    }
            }
            Debug.Log("Penalty reset to " + PenaltyInfoNow);
            PenaltyInfoLast = new PenaltyInfo(PenaltyInfoNow);
            Debug.Log("PenaltyInfoLast: " + PenaltyInfoLast);
        }

        protected void ResetPenaltyToZero()
        {
            PenaltyInfoNow.PenaltyTransparency = 0f;
            PenaltyInfoNow.PenaltyEyePatch = 0f;
        }

        public PenaltyInfo GetStartPenalty()
        {
            return StartPenalties[Difficulty];
        }

        public PenaltyInfo GetEndPenalty()
        {
            return EndPenalties[Difficulty];
        }

     
        public void IncreasePenaltyNow()
        {
            PenaltyInfoLast = new PenaltyInfo(PenaltyInfoNow);
            //Debug.Log("PenaltyInfoLast: " + PenaltyInfoLast.ToString());
            switch(PolicyIncreasePenalty)
            {
                case GamePolicy.IncreasePenalty.BY_STEPS: IncreasePenaltyNowBySteps(); break;
                case GamePolicy.IncreasePenalty.MANUAL: IncreasePenaltyNowManual(); break;
            }
        }

        /// <summary>
        /// Dev: override this in your son class as you prefer
        /// </summary>
        /// <remarks>You can use the other methods overloadings of IncrasePenaltyNow</remarks>
        protected void IncreasePenaltyNowManual()
        {
            Debug.Log("IncreasePenaltyNowManual not implemented (maybe you are calling the wrong one)");
        }

        /// <summary>
        /// This increase the penalty of a float number based on the number of steps
        /// </summary>
        /// <remarks>Be sure that you set a number of steps. Also, the max value for both PenaltyInfo values is 1, if 
        /// your setting goes beyond for some reason it's set to 1</remarks>
        private void IncreasePenaltyNowBySteps()
        {
            float stepTrasp = (GetEndPenalty().PenaltyTransparency - GetStartPenalty().PenaltyTransparency) / Steps;
            float stepEyePatch = (GetEndPenalty().PenaltyEyePatch - GetStartPenalty().PenaltyEyePatch) / Steps;

            float finalTrasp = PenaltyInfoNow.PenaltyTransparency + stepTrasp;
            float finalEyePatch = PenaltyInfoNow.PenaltyEyePatch + stepEyePatch;

            Debug.Log("stepTrasp " + stepTrasp
                + "\nstepEyePatch " + stepEyePatch);

            if (PolicyDifficultyProgression == GamePolicy.DifficultyProgression.INFINITE)
            {
                PenaltyInfoNow.PenaltyTransparency = finalTrasp;
                PenaltyInfoNow.PenaltyEyePatch = finalEyePatch;
                if(PenaltyInfoNow.PenaltyTransparency >= EndPenalties[Difficulty].PenaltyTransparency
                    && PenaltyInfoNow.PenaltyEyePatch >= EndPenalties[Difficulty].PenaltyEyePatch)
                {
                    Difficulty = DifficultyManager.GetNext(Difficulty);
                }
            }
            else
            {
                if (finalTrasp <= GetEndPenalty().PenaltyTransparency)
                {
                    PenaltyInfoNow.PenaltyTransparency = finalTrasp;
                }
                else Debug.Log("Cant' increase transparency because of the difficulty");
                if (finalEyePatch <= GetEndPenalty().PenaltyEyePatch)
                {
                    PenaltyInfoNow.PenaltyEyePatch = finalEyePatch;
                }
                else Debug.Log("Cant' increase eyepatch because of the difficulty");
            }
            CheckAcceptableValues();
            Debug.Log("Policies.PolicyDifficultyProgression :" + PolicyDifficultyProgression);
            Debug.Log("PenaltyInfoNow: " + PenaltyInfoNow.ToString());
        }

        /// <summary>
        /// This increase the penaly from the start of the current difficulty for a
        /// given number X on total steps
        /// </summary>
        /// <remarks>This uses the logic of the number of steps so be sure to have set those.
        /// Also if X==0 it returs the StartPenalty for the difficult set.</remarks>
        /// <example>It can be used to have the penalty for level X of difficulty D</example>
        /// <param name="X"></param>
        public void IncreasePenaltyNow(int X)
        {
            for(int i=0;i<X;i++)
            {
                IncreasePenaltyNow();   //TODO this one is quite bovino but should do the trick
            }
        }

        public void CheckAcceptableValues()
        {
            //Not below 0
            if (PenaltyInfoNow.PenaltyTransparency < 0) PenaltyInfoNow.PenaltyTransparency = 0;
            if (PenaltyInfoNow.PenaltyEyePatch < 0) PenaltyInfoNow.PenaltyEyePatch = 0;
            //Not "far" beyond 1
            if (PenaltyInfoNow.PenaltyTransparency > 1) PenaltyInfoNow.PenaltyTransparency = 1;
            if (PenaltyInfoNow.PenaltyEyePatch > 1) PenaltyInfoNow.PenaltyEyePatch = 1;
        }

        /// <summary>
        /// This manually increase the PenaltyInfoNow without regards of 
        /// start and end penalty
        /// </summary>
        /// <remarks>Use this only if you don't want to increase of a fixed amount
        /// every step (you should use this always after you used this once)</remarks>
        /// <param name="added">This PenaltyInfo contains values added to the PenaltyInfoNow</param>
        public void IncreasePenaltyNow(PenaltyInfo added)
        {
            PenaltyInfoNow.PenaltyTransparency += added.PenaltyTransparency;
            PenaltyInfoNow.PenaltyEyePatch += added.PenaltyEyePatch;
            CheckAcceptableValues();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This can't go below zero (if it does it's set to 0)</remarks>
        /// <param name="subbed">This PenaltyInfo contains values subbed to the PenaltyInfoNow</param>
        public void SubPenaltyNow(PenaltyInfo subbed)
        {
            PenaltyInfoNow.PenaltyTransparency -= subbed.PenaltyTransparency;
            PenaltyInfoNow.PenaltyEyePatch -= subbed.PenaltyEyePatch;
            CheckAcceptableValues();
        }

        /// <summary>
        /// This resets the penalty now to the value passed as parameter
        /// </summary>
        /// <param name="reset"></param>
        protected void ResetPenaltyNowTo(PenaltyInfo reset)
        {
            PenaltyInfoNow = new PenaltyInfo(reset);
            CheckAcceptableValues();
        }
    }
}
