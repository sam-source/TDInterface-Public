﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TdInterface
{
    public partial class UserOptionsForm : Form
    {
        public Settings _settings;
        public UserOptionsForm()
        {
            InitializeComponent();
        }

        private void chkTrainingWheels_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.TradeShares = chkTrainingWheels.Checked;
            _settings.MaxRisk = Decimal.Parse(txtMaxRisk.Text);
            _settings.MaxShares = int.Parse(txtMaxShares.Text);
            _settings.UseBidAskOcoCalc = chkUseBidAskOcoCalc.Checked;
            _settings.DisableFirstTargetProfitDefault = chkDisableFirstTarget.Checked;
            _settings.OneRProfitPercenatage = int.Parse(txtOneRSharePct.Text);
            _settings.MoveLimitPriceOnFill = chkMoveLimitOnFill.Checked;
            _settings.ReduceStopOnClose = chkReduceStopOnClose.Checked;
            _settings.DefaultLimitOffset = string.IsNullOrEmpty(txtDefaultLimitOffset.Text) ? 0 : Decimal.Parse(txtDefaultLimitOffset.Text);
            _settings.EnableMaxLossLimit = chkMaxLossLimit.Checked;
            _settings.MaxLossLimitInR = _settings.EnableMaxLossLimit ? decimal.Parse(txtMaxLossLimit.Text) : 0;
            _settings.MinimumRisk = string.IsNullOrEmpty(txtMinRisk.Text) ? 0 : double.Parse(txtMinRisk.Text);
            _settings.SendAltPrtScrOnOpen = chkSendPrtScrOnOpen.Checked;
            _settings.ShowPnL = chkShowPnL.Checked;
            _settings.PreventRiskExceedMaxLoss = chkPreventExceedMaxLoss.Checked;
            _settings.AdjustRiskNotExceedMaxLoss = chkAdjustRiskForMaxLoss.Checked;

            Utility.SaveSettings(_settings);
            this.Close();
        }

        private void UserOptionsForm_Load(object sender, EventArgs e)
        {
            _settings = Utility.GetSettings();
            if(_settings == null) _settings = new Settings();

            chkTrainingWheels.Checked = _settings.TradeShares;
            txtMaxRisk.Text = _settings.MaxRisk.ToString("#.##");
            txtMaxShares.Text = _settings.MaxShares.ToString();
            chkUseBidAskOcoCalc.Checked = _settings.UseBidAskOcoCalc;
            chkDisableFirstTarget.Checked = _settings.DisableFirstTargetProfitDefault;
            txtOneRSharePct.Text = _settings.OneRProfitPercenatage.ToString();
            chkMoveLimitOnFill.Checked = _settings.MoveLimitPriceOnFill;
            chkReduceStopOnClose.Checked = _settings.ReduceStopOnClose;
            txtDefaultLimitOffset.Text = _settings.DefaultLimitOffset.ToString("#.##");
            chkMaxLossLimit.Checked = _settings.EnableMaxLossLimit;
            txtMaxLossLimit.Text = _settings.MaxLossLimitInR.ToString("#.##");
            txtMinRisk.Text = _settings.MinimumRisk.ToString("#.##");
            chkSendPrtScrOnOpen.Checked = _settings.SendAltPrtScrOnOpen;
            chkShowPnL.Checked = _settings.ShowPnL;
            chkPreventExceedMaxLoss.Checked = _settings.PreventRiskExceedMaxLoss;
            chkAdjustRiskForMaxLoss.Checked = _settings.AdjustRiskNotExceedMaxLoss;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
