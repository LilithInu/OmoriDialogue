using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OmoriDialogueParser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            /*logTextBox = new TextBox()
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Bottom
            };*/

            logTextBox.ReadOnly = false;
            logTextBox.ScrollBars = ScrollBars.Vertical;

            this.Controls.Add(logTextBox);

            Util.LogAction = LogMessage;

            if (Properties.Settings.Default.ProjectFolder != "")
                pathProject.Text = Properties.Settings.Default.ProjectFolder;

            if (Properties.Settings.Default.OutputFolder != "")
                pathOutput.Text = Properties.Settings.Default.OutputFolder;
        }

        public void LogMessage(string message)
        {
            if (logTextBox.InvokeRequired)
            {
                logTextBox.Invoke(new Action(() =>
                {
                    logTextBox.AppendText(message + Environment.NewLine);
                }));
            }
            else
            {
                logTextBox.AppendText(message + Environment.NewLine);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Process.run("W:\\OMORI Modding\\rainymari\\www_playtest", "C:\\Users\\dick\\Desktop\\Ouput");
            Process.run(pathProject.Text, pathOutput.Text);
        }

        private void logTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void browseOutput_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a folder to export dialogue to.";
                folderDialog.UseDescriptionForTitle = true;

                if (Properties.Settings.Default.OutputFolder != "")
                    folderDialog.SelectedPath = Properties.Settings.Default.OutputFolder;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.OutputFolder = folderDialog.SelectedPath;
                    Properties.Settings.Default.Save();

                    pathOutput.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void pathOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void browseProject_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select the folder containing the game files.";
                folderDialog.UseDescriptionForTitle = true;

                if(Properties.Settings.Default.ProjectFolder != "")
                    folderDialog.SelectedPath = Properties.Settings.Default.ProjectFolder;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.ProjectFolder = folderDialog.SelectedPath;
                    Properties.Settings.Default.Save();

                    pathProject.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
