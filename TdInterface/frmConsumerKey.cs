﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TdInterface
{
    public partial class frmConsmerKey : Form
    {
        public frmConsmerKey()
        {
            InitializeComponent();
        }

        public string ConsumerKey { get; private set; }
        private void btnOk_Click(object sender, EventArgs e)
        {
            ConsumerKey = txtConsumerKey.Text;
            this.Close();
        }
    }
}
