using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnblockWebMarks
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }       

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            string path = this.txtFolder.Text.Trim();

            if (!string.IsNullOrEmpty(path))
            {
                this.folderBrowserDialog1.SelectedPath = path;
            }

            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtFolder.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string path = this.txtFolder.Text.Trim();

            if (!Directory.Exists(path))
            {
                MessageBox.Show($@"The path ""{ path }"" is not exists");
                return;
            }

            this.Execute();
        }

        private void Execute()
        {
            string path = this.txtFolder.Text;

            string fileName = "cmd.exe";

            var commands = new string[] { "powershell.exe", $@"dir ""{path}"" -Recurse | Unblock-File"};

            ProcessHelper.ExecuteCommands(fileName, "", commands, this.Process_ErrorDataReceived);

            MessageBox.Show("Webmarks have been removed.");
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                MessageBox.Show(e.Data);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }      

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            this.txtFolder.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }
    }
}
