using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using F1Speed.Core;
using log4net;

namespace F1Speed
{
    public partial class OptionsForm : Form
    {
        private static ILog logger = Logger.Create();

        public OptionsForm()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            AllowExternalConnectionsCheckBox.Checked = F1SpeedSettings.AllowConnectionsFromOtherMachines;
            LogPacketDataCheckBox.Checked = F1SpeedSettings.LogPacketData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                F1SpeedSettings.AllowConnectionsFromOtherMachines = AllowExternalConnectionsCheckBox.Checked;
                F1SpeedSettings.LogPacketData = LogPacketDataCheckBox.Checked;
                F1SpeedSettings.Save();
                this.Close();
                MessageBox.Show("Restart F1 Speed for settings changes to take effect", "Settings", MessageBoxButtons.OK);
            } catch (Exception ex)
            {
                logger.Error("Failed to update Configuration", ex);                
            }

        }
    }
}
