using Realmlist_Changer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Realmlist_Changer
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            checkBoxDeleteCache.Checked = Settings.Default.DeleteCache;
            checkBoxLoginToChar.Checked = Settings.Default.LoginToChar;
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void SaveSettings()
        {
            Settings.Default.DeleteCache = checkBoxDeleteCache.Checked;
            Settings.Default.LoginToChar = checkBoxLoginToChar.Checked;
            Settings.Default.Save();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
