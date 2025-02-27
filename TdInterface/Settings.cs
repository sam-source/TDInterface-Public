﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TdInterface
{
    public class Settings
    {
        private int _oneRProfitPercenatage;
        public bool TradeShares { get; set; }
        public int MaxShares { get; set; }
        public decimal MaxRisk { get; set; }
        public bool UseBidAskOcoCalc { get; set; }
        public bool DisableFirstTargetProfitDefault { get; set; }
        public int OneRProfitPercenatage { 
            get 
            { 
                return _oneRProfitPercenatage == 0 ? 25 : _oneRProfitPercenatage; 
            }
            set
            {
                _oneRProfitPercenatage = value;
            }
        }
        public bool MoveLimitPriceOnFill { get; set; }
        public bool ReduceStopOnClose { get; set; }
        public decimal DefaultLimitOffset { get; set; }
        public bool EnableMaxLossLimit { get; set; }
        public decimal MaxLossLimitInR { get; set; }
        public bool PreventRiskExceedMaxLoss { get; set; }
        public bool AdjustRiskNotExceedMaxLoss { get; set; }
        public double MinimumRisk { get; set; }
        public bool SendAltPrtScrOnOpen { get; set; }
        public bool ShowPnL { get;set; }
    }
}
