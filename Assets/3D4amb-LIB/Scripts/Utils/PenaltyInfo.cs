using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>This is created and updated by PenaltyManager</remarks>
    public class PenaltyInfo
    {
        public float PenaltyTransparency { get; set; }
        public float PenaltyEyePatch { get; set; }
        public Eye PenaltyEye { get; set; }

        public PenaltyInfo(Eye eye, float trans, float eyep)
        {
            PenaltyEye = eye;
            PenaltyTransparency = trans;
            PenaltyEyePatch = eyep;
        }

        public PenaltyInfo(PenaltyInfo o)
        {
            this.PenaltyTransparency = o.PenaltyTransparency;
            this.PenaltyEyePatch = o.PenaltyEyePatch;
            this.PenaltyEye = o.PenaltyEye;
        }

        public override string ToString()
        {
            //TODO make me with a StringBuilder
            return "[eye: " + PenaltyEye + "][trasp: " + PenaltyTransparency + "][eyep: " + PenaltyEyePatch + "]";
        }
    }
}
