using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Realmlist_Changer
{
    public partial class ManageRealmlistsForm : Form
    {
        private Dictionary<string /* realmlist */, Dictionary<string /* accountName */, string /* accountPassword */>> realmlists = new Dictionary<string, Dictionary<string /* accountName */, string /* accountPassword */>>();

        public ManageRealmlistsForm()
        {
            InitializeComponent();
        }

        private void ManageRealmlistsForm_Load(object sender, EventArgs e)
        {
            realmlists = ((MainForm)Owner).Realmlists; //! Has to be called in Load event, otherwise Owner is NULL

            foreach (string realmlist in realmlists.Keys)
                comboBoxRealmlists.Items.Add(realmlist);

            if (comboBoxRealmlists.Items.Count > 0)
                comboBoxRealmlists.SelectedIndex = 0;

            //! Focus on the realmlis combobox
            comboBoxRealmlists.Select();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxRealmlists.SelectedIndex == -1)
            {
                MessageBox.Show("There is no item selected!", "Nothing selected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ((MainForm)Owner).RemoveRealmlist(comboBoxRealmlists.Text);
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(comboBoxRealmlists.Text) || String.IsNullOrWhiteSpace(comboBoxAccountName.Text) || String.IsNullOrWhiteSpace(textBoxAccountPassword.Text))
            {
                MessageBox.Show("All fields must be filled!", "Not all fields are filled!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (realmlists.ContainsKey(comboBoxRealmlists.Text) && realmlists[comboBoxRealmlists.Text].ContainsKey(comboBoxAccountName.Text) &&
                realmlists[comboBoxRealmlists.Text][comboBoxAccountName.Text] == textBoxAccountPassword.Text)
            {
                MessageBox.Show("This account already exists for this realmlist.", "Already exists!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ((MainForm)Owner).AddRealmlist(comboBoxRealmlists.Text, comboBoxAccountName.Text, textBoxAccountPassword.Text);
            Close();
        }

        private void ManageRealmlistsForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void comboBoxRealmlists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRealmlists.SelectedIndex == -1 || !realmlists.ContainsKey(comboBoxRealmlists.Text))
                return;

            comboBoxAccountName.Items.Clear();

            foreach (string accountName in realmlists[comboBoxRealmlists.Text].Keys)
                comboBoxAccountName.Items.Add(accountName);

            if (comboBoxAccountName.Items.Count > 0)
                comboBoxAccountName.SelectedIndex = 0;
        }

        private void comboBoxAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!realmlists.ContainsKey(comboBoxRealmlists.Text) || !realmlists[comboBoxRealmlists.Text].ContainsKey(comboBoxAccountName.Text))
            {
                MessageBox.Show("Something went wrong...", "Woops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBoxAccountPassword.Text = realmlists[comboBoxRealmlists.Text][comboBoxAccountName.Text];
        }
    }
}
