namespace Realmlist_Changer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.comboBoxItems = new System.Windows.Forms.ComboBox();
            this.textBoxRealmlistFile = new System.Windows.Forms.TextBox();
            this.buttonSearchDirectory = new System.Windows.Forms.Button();
            this.buttonLaunchWow = new System.Windows.Forms.Button();
            this.textBoxWowFile = new System.Windows.Forms.TextBox();
            this.buttonSearchWowDirectory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.textBoxAccountPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelOnOrOff = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxItems
            // 
            this.comboBoxItems.FormattingEnabled = true;
            this.comboBoxItems.Location = new System.Drawing.Point(6, 18);
            this.comboBoxItems.Name = "comboBoxItems";
            this.comboBoxItems.Size = new System.Drawing.Size(254, 21);
            this.comboBoxItems.TabIndex = 6;
            this.comboBoxItems.SelectedIndexChanged += new System.EventHandler(this.comboBoxItems_SelectedIndexChanged);
            this.comboBoxItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.readonlyField_KeyPress);
            // 
            // textBoxRealmlistFile
            // 
            this.textBoxRealmlistFile.Location = new System.Drawing.Point(6, 19);
            this.textBoxRealmlistFile.Name = "textBoxRealmlistFile";
            this.textBoxRealmlistFile.Size = new System.Drawing.Size(338, 20);
            this.textBoxRealmlistFile.TabIndex = 0;
            // 
            // buttonSearchDirectory
            // 
            this.buttonSearchDirectory.Location = new System.Drawing.Point(344, 18);
            this.buttonSearchDirectory.Name = "buttonSearchDirectory";
            this.buttonSearchDirectory.Size = new System.Drawing.Size(25, 22);
            this.buttonSearchDirectory.TabIndex = 1;
            this.buttonSearchDirectory.Text = "...";
            this.buttonSearchDirectory.UseVisualStyleBackColor = true;
            this.buttonSearchDirectory.Click += new System.EventHandler(this.buttonSearchDirectory_Click);
            // 
            // buttonLaunchWow
            // 
            this.buttonLaunchWow.Location = new System.Drawing.Point(286, 181);
            this.buttonLaunchWow.Name = "buttonLaunchWow";
            this.buttonLaunchWow.Size = new System.Drawing.Size(95, 23);
            this.buttonLaunchWow.TabIndex = 10;
            this.buttonLaunchWow.Text = "Launch WoW!";
            this.buttonLaunchWow.UseVisualStyleBackColor = true;
            this.buttonLaunchWow.Click += new System.EventHandler(this.buttonLaunchWow_Click);
            // 
            // textBoxWowFile
            // 
            this.textBoxWowFile.Location = new System.Drawing.Point(6, 45);
            this.textBoxWowFile.Name = "textBoxWowFile";
            this.textBoxWowFile.Size = new System.Drawing.Size(338, 20);
            this.textBoxWowFile.TabIndex = 2;
            // 
            // buttonSearchWowDirectory
            // 
            this.buttonSearchWowDirectory.Location = new System.Drawing.Point(344, 44);
            this.buttonSearchWowDirectory.Name = "buttonSearchWowDirectory";
            this.buttonSearchWowDirectory.Size = new System.Drawing.Size(25, 22);
            this.buttonSearchWowDirectory.TabIndex = 3;
            this.buttonSearchWowDirectory.Text = "...";
            this.buttonSearchWowDirectory.UseVisualStyleBackColor = true;
            this.buttonSearchWowDirectory.Click += new System.EventHandler(this.buttonSearchWowDirectory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server status is ";
            // 
            // textBoxAccountName
            // 
            this.textBoxAccountName.Location = new System.Drawing.Point(6, 44);
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.Size = new System.Drawing.Size(181, 20);
            this.textBoxAccountName.TabIndex = 8;
            this.textBoxAccountName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.readonlyField_KeyPress);
            // 
            // textBoxAccountPassword
            // 
            this.textBoxAccountPassword.Location = new System.Drawing.Point(188, 44);
            this.textBoxAccountPassword.Name = "textBoxAccountPassword";
            this.textBoxAccountPassword.PasswordChar = '*';
            this.textBoxAccountPassword.Size = new System.Drawing.Size(181, 20);
            this.textBoxAccountPassword.TabIndex = 9;
            this.textBoxAccountPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.readonlyField_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxRealmlistFile);
            this.groupBox1.Controls.Add(this.buttonSearchDirectory);
            this.groupBox1.Controls.Add(this.textBoxWowFile);
            this.groupBox1.Controls.Add(this.buttonSearchWowDirectory);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 71);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directories";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.comboBoxItems);
            this.groupBox3.Controls.Add(this.textBoxAccountPassword);
            this.groupBox3.Controls.Add(this.textBoxAccountName);
            this.groupBox3.Location = new System.Drawing.Point(12, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(376, 71);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Realmlists";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Add/remove/edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonAddOrRemove_Click);
            // 
            // labelOnOrOff
            // 
            this.labelOnOrOff.AutoSize = true;
            this.labelOnOrOff.Location = new System.Drawing.Point(90, 186);
            this.labelOnOrOff.Name = "labelOnOrOff";
            this.labelOnOrOff.Size = new System.Drawing.Size(54, 13);
            this.labelOnOrOff.TabIndex = 14;
            this.labelOnOrOff.Text = "<nothing>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(402, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSettings,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Name = "menuItemSettings";
            this.menuItemSettings.Size = new System.Drawing.Size(152, 22);
            this.menuItemSettings.Text = "Settings";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 211);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelOnOrOff);
            this.Controls.Add(this.buttonLaunchWow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Realmlist Changer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxItems;
        private System.Windows.Forms.TextBox textBoxRealmlistFile;
        private System.Windows.Forms.Button buttonSearchDirectory;
        private System.Windows.Forms.Button buttonLaunchWow;
        private System.Windows.Forms.TextBox textBoxWowFile;
        private System.Windows.Forms.Button buttonSearchWowDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.TextBox textBoxAccountPassword;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelOnOrOff;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

