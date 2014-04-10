using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Realmlist_Changer.Properties;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace Realmlist_Changer
{
    public partial class MainForm : Form
    {
        private const int EM_SETCUEBANNER = 0x1501; //! Used to set placeholder text

        //! Key codes to send to the client
        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        private const uint WM_CHAR = 0x0102;
        private const int VK_RETURN = 0x0D;
        private const int VK_TAB = 0x09;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private Dictionary<string /* realmlist */, Dictionary<string /* accountName */, string /* accountPassword */>> realmlists = new Dictionary<string, Dictionary<string /* accountName */, string /* accountPassword */>>();
        private string xmlDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Realmlist-Changer\";
        private string xmlDirFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Realmlist-Changer\realmlist-changer.xml";
        private string applicationVersion = String.Empty;
        private Socket clientSocket = null;

        public Dictionary<string, Dictionary<string /* accountName */, string /* accountPassword */>> Realmlists
        {
            get { return realmlists; }
            set { realmlists = value; }
        }

        public MainForm()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            applicationVersion = "v" + version.Major + "." + version.Minor + "." + version.Build;
            Text = "Realmlist Changer " + applicationVersion + "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //! Set the placeholder text
            SendMessage(textBoxRealmlistFile.Handle, EM_SETCUEBANNER, 0, "Realmlist.wtf directory");
            SendMessage(textBoxWowFile.Handle, EM_SETCUEBANNER, 0, "Wow.exe directory");
            textBoxRealmlistFile.Text = Settings.Default.RealmlistDir;
            textBoxWowFile.Text = Settings.Default.WorldOfWarcraftDir;

            if (File.Exists(xmlDirFile))
            {
                using (StringReader stringReader = new StringReader(File.ReadAllText(xmlDirFile)))
                {
                    using (XmlTextReader reader = new XmlTextReader(stringReader))
                    {
                        string realmlist = String.Empty, accountName = String.Empty, encryptedPassword = String.Empty;

                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                switch (reader.Name)
                                {
                                    case "realm":
                                        realmlist = reader["realmlist"];
                                        break;
                                    case "accountname":
                                        accountName = reader.ReadString();
                                        break;
                                    case "accountpassword":
                                        encryptedPassword = reader.ReadString();
                                        break;
                                    case "entropy":
                                        if (String.IsNullOrWhiteSpace(encryptedPassword))
                                        {
                                            MessageBox.Show("Something went wrong while loading an account for realmlist '" + realmlist + "'.", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }

                                        string accountPassword = GetDecryptedPassword(encryptedPassword, reader.ReadString());

                                        if (!comboBoxRealmlists.Items.Contains(realmlist))
                                            comboBoxRealmlists.Items.Add(realmlist);

                                        AddAccountToRealmlist(realmlist, accountName, accountPassword);
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //! Has to be called after xml loading
            if (comboBoxRealmlists.Items.Count > 0 && Settings.Default.LastSelectedIndex == -2)
                comboBoxRealmlists.SelectedIndex = 0;
            else if (comboBoxRealmlists.Items.Count > Settings.Default.LastSelectedIndex)
                comboBoxRealmlists.SelectedIndex = Settings.Default.LastSelectedIndex;

            //! HAS to be called after setting comboBoxRealmlists's selected index
            if (comboBoxAccountName.Items.Count > 0)
                comboBoxAccountName.SelectedIndex = 0;

            if (comboBoxRealmlists.SelectedIndex == -1 || !realmlists.ContainsKey(comboBoxRealmlists.Text))
                textBoxAccountPassword.Text = String.Empty;
            else
                textBoxAccountPassword.Text = realmlists[comboBoxRealmlists.Text][comboBoxAccountName.Text];
        }

        private void buttonSearchDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Wtf files (*.wtf)|*.wtf";
            openFileDialog.FileName = "";

            if (textBoxRealmlistFile.Text != "" && Directory.Exists(textBoxRealmlistFile.Text))
                openFileDialog.InitialDirectory = textBoxRealmlistFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxRealmlistFile.Text = openFileDialog.FileName;
        }

        private void buttonLaunchWow_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxRealmlistFile.Text))
            {
                MessageBox.Show("The realmlist.wtf file could not be located!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(textBoxWowFile.Text))
            {
                MessageBox.Show("The WoW.exe file could not be located!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var outputFile = new StreamWriter(textBoxRealmlistFile.Text, false))
                outputFile.WriteLine("set realmlist " + comboBoxRealmlists.SelectedItem);

            try
            {
                //! Delete the cache folder if the settings tell us to.
                //! The reason this has its own try-catch block is because the logging in should not
                //! be stopped if the directory removing threw an exception.
                if (Settings.Default.DeleteCache)
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Path.GetDirectoryName(textBoxWowFile.Text) + @"\Cache");
                        dirInfo.Delete(true);
                    }
                    catch
                    {

                    }
                }

                Process process = Process.Start(textBoxWowFile.Text);

                //! Only attempt to login to the account page (and possibly character if set to do so in settings) if te
                //! acc info is actually given.
                if (comboBoxAccountName.SelectedIndex == -1)
                    return;

                string accountName = comboBoxAccountName.Text;
                Thread.Sleep(600);

                //! Run this code in a new thread so the main form does not freeze up.
                new Thread(() =>
                {
                    try
                    {
                        Thread.CurrentThread.IsBackground = true;

                        while (!process.WaitForInputIdle()) ;

                        Thread.Sleep(1500);

                        foreach (char accNameLetter in accountName)
                        {
                            SendMessage(process.MainWindowHandle, WM_CHAR, new IntPtr(accNameLetter), IntPtr.Zero);
                            Thread.Sleep(30);
                        }

                        //! Switch to password field
                        if (!String.IsNullOrWhiteSpace(textBoxAccountPassword.Text))
                        {
                            SendMessage(process.MainWindowHandle, WM_KEYUP, new IntPtr(VK_TAB), IntPtr.Zero);
                            SendMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_TAB), IntPtr.Zero);

                            foreach (char accPassLetter in textBoxAccountPassword.Text)
                            {
                                SendMessage(process.MainWindowHandle, WM_CHAR, new IntPtr(accPassLetter), IntPtr.Zero);
                                Thread.Sleep(30);
                            }

                            //! Login to account
                            SendMessage(process.MainWindowHandle, WM_KEYUP, new IntPtr(VK_RETURN), IntPtr.Zero);
                            SendMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);

                            //! Login to char
                            if (Settings.Default.LoginToChar)
                            {
                                Thread.Sleep(1500);
                                SendMessage(process.MainWindowHandle, WM_KEYUP, new IntPtr(VK_RETURN), IntPtr.Zero);
                                SendMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);
                            }
                        }

                        Thread.CurrentThread.Abort();
                    }
                    catch
                    {
                        Thread.CurrentThread.Abort();
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.RealmlistDir = textBoxRealmlistFile.Text;
            Settings.Default.WorldOfWarcraftDir = textBoxWowFile.Text;
            Settings.Default.LastSelectedIndex = comboBoxRealmlists.SelectedIndex;

            if (!Directory.Exists(xmlDir))
                Directory.CreateDirectory(xmlDir);

            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

            using (XmlWriter writer = XmlWriter.Create(xmlDirFile, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("realms");

                foreach (string realmlist in realmlists.Keys)
                {
                    writer.WriteStartElement("realm");
                    writer.WriteAttributeString("realmlist", realmlist);

                    foreach (KeyValuePair<string, string> accountInfo in realmlists[realmlist])
                    {
                        writer.WriteStartElement("account");
                        writer.WriteElementString("accountname", accountInfo.Key);

                        //! Encrypt the password
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] buffer = new byte[1024];
                        rng.GetBytes(buffer);
                        string salt = BitConverter.ToString(buffer);
                        rng.Dispose();
                        writer.WriteElementString("accountpassword", accountInfo.Value.Length == 0 ? String.Empty : accountInfo.Value.ToSecureString().EncryptString(Encoding.Unicode.GetBytes(salt)));
                        writer.WriteElementString("entropy", salt);

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            Settings.Default.Save();
        }

        public string GetDecryptedPassword(string encryptedPassword, string entropy)
        {
            string password = encryptedPassword;

            if (password.Length > 150)
                password = password.DecryptString(Encoding.Unicode.GetBytes(entropy)).ToInsecureString();

            return password;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (comboBoxRealmlists.SelectedIndex == -1)
            {
                MessageBox.Show("You have no item selected to remove!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            realmlists.Remove(comboBoxRealmlists.Text);
            comboBoxRealmlists.Items.Remove(comboBoxRealmlists.SelectedItem);
            comboBoxRealmlists.SelectedIndex = comboBoxRealmlists.Items.Count > 0 ? 0 : -1;

            if (comboBoxRealmlists.Items.Count == 0)
                comboBoxRealmlists.Text = String.Empty;
        }

        private void buttonAddOrRemove_Click(object sender, EventArgs e)
        {
            using (ManageRealmlistsForm ManageRealmlistsForm = new ManageRealmlistsForm())
                ManageRealmlistsForm.ShowDialog(this);
        }

        private void buttonSearchWowDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Exe files (*.exe)|*.exe";
            openFileDialog.FileName = "";

            if (textBoxWowFile.Text != "" && Directory.Exists(textBoxWowFile.Text))
                openFileDialog.InitialDirectory = textBoxWowFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxWowFile.Text = openFileDialog.FileName;
        }

        private void comboBoxRealmlists_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxAccountPassword.Text = String.Empty;

            if (comboBoxRealmlists.SelectedIndex == -1)
                return;

            string selectedItem = comboBoxRealmlists.Text;

            if (!realmlists.ContainsKey(selectedItem))
                return;

            if (clientSocket != null)
                clientSocket.Close();

            SetTextOfControl(labelOnOrOff, "<connecting...>");
            labelOnOrOff.ForeColor = Color.Black;
            labelOnOrOff.Update();

            comboBoxAccountName.Items.Clear();

            foreach (string accountName in realmlists[comboBoxRealmlists.Text].Keys)
                comboBoxAccountName.Items.Add(accountName);

            if (comboBoxAccountName.Items.Count > 0)
                comboBoxAccountName.SelectedIndex = 0;

            try
            {
                if (selectedItem != "127.0.0.1" && selectedItem != "localhost")
                {
                    IPAddress hostAddress = Dns.GetHostEntry(selectedItem).AddressList[0];

                    switch (hostAddress.AddressFamily)
                    {
                        case AddressFamily.InterNetwork:
                             clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                             break;
                         case AddressFamily.InterNetworkV6:
                             clientSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                             break;
                         default:
                             return;
                    }

                    SocketAsyncEventArgs telnetSocketAsyncEventArgs = new SocketAsyncEventArgs();
                    telnetSocketAsyncEventArgs.RemoteEndPoint = new IPEndPoint(hostAddress, 3724); //! Client port is always 3724 so this is safe
                    telnetSocketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(telnetSocketAsyncEventArgs_Completed);
                    clientSocket.ConnectAsync(telnetSocketAsyncEventArgs);
                }
                else
                    //! If server is localhost, check if worldserver is running
                    SetSelectedServerState(Process.GetProcessesByName("worldserver").Length > 0 && Process.GetProcessesByName("authserver").Length > 0);
            }
            catch (Exception)
            {
                SetSelectedServerState(false);
            }
        }

        private void telnetSocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                SetSelectedServerState(e.SocketError == SocketError.Success && e.LastOperation == SocketAsyncOperation.Connect);
            }
            catch (Exception)
            {
                SetSelectedServerState(false);
            }
        }

        private void SetSelectedServerState(bool online)
        {
            SetTextOfControl(labelOnOrOff, online ? "online" : "offline");
            labelOnOrOff.ForeColor = online ? Color.Chartreuse : Color.Red;
        }

        private delegate void SetTextOfControlDelegate(Control control, string text);

        private void SetTextOfControl(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                Invoke(new SetTextOfControlDelegate(SetTextOfControl), new object[] { control, text });
                return;
            }

            control.Text = text;
        }

        private void readonlyField_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void AddRealmlist(string realmlist, string accountName, string accountPassword)
        {
            AddAccountToRealmlist(realmlist, accountName, accountPassword);

            if (!comboBoxRealmlists.Items.Contains(realmlist))
            {
                comboBoxRealmlists.Items.Add(realmlist);
                comboBoxRealmlists.SelectedIndex = comboBoxRealmlists.Items.Count - 1; //! Also sets account info in event
            }
        }

        public void RemoveRealmlist(string realmlist)
        {
            if (!realmlists.ContainsKey(realmlist) || String.IsNullOrWhiteSpace(realmlist))
                return;

            realmlists.Remove(realmlist);

            if (comboBoxRealmlists.Items.Contains(realmlist))
                comboBoxRealmlists.Items.Remove(realmlist);

            if (comboBoxRealmlists.Items.Count == 0)
            {
                comboBoxRealmlists.Text = String.Empty;
                textBoxAccountPassword.Text = String.Empty;
            }
            else
                comboBoxRealmlists.SelectedIndex = 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Realmlist-Changer @ 2014 Discover-", "About Realmlist-Changer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
                settingsForm.ShowDialog(this);
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

        private void AddAccountToRealmlist(string realmlist, string accountName, string accountPassword)
        {
            if (realmlists.ContainsKey(realmlist))
                realmlists[realmlist].Add(accountName, accountPassword);
            else
                realmlists.Add(realmlist, new Dictionary<string, string>() { { accountName, accountPassword } });

            comboBoxAccountName.Items.Add(accountName);
        }
    }
}
