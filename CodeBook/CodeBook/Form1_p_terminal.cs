using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Management.Automation;
using System.Text;
using System.Windows.Forms;

namespace CodeBook
{
    partial class   Form1
    {
        private void button1_Click(object sender, EventArgs e)
        {
            //  consoleControl1.Clear();
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(txtInput.Text);
                //     powerShell.AddCommand("Out-String");
                Collection<PSObject> PSOutput = powerShell.Invoke();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (PSObject pSObject in PSOutput)
                    stringBuilder.AppendLine(pSObject.ToString());
                consoleControl1.Text = stringBuilder.ToString();
            }
        }

        public void ExecuteGitBashCommand(string fileName, string command, string workingDir)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName, "-c \" " + command + " \"")
                {
                    WorkingDirectory = workingDir,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = Process.Start(processStartInfo);
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                var exitCode = process.ExitCode;

                process.Close();

                consoleControl1.Text = output;
            }
            catch (Exception ex)
            {
                consoleControl1.Text += ex.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\Program Files\Git\bin";
            string command = txtInput.Text;
            string workingDir = @"C:\workspace\Test";
            ExecuteGitBashCommand(fileName, command, workingDir);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        string workingDir = @"c:/";
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (txtInput.Text != "")
            {
                //consoleControl1.ClearOutput();
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + txtInput.Text;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = workingDir;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                ProcessStartInfo processStartInfo = process.StartInfo;
                //// Create the readers and writers
                //var     inputWriter = process.StandardInput;
                //var outputReader = TextReader.Synchronized(process.StandardOutput);
                //var errorReader = TextReader.Synchronized(process.StandardError);

                // Run the output and error workers

                //      process.Start();
                //ThreadStart ths = new ThreadStart(() =>
                //{
                //    process.Start();
                //    string q = "";
                //    while (!process.HasExited)
                //    {

                consoleControl1.ShowDiagnostics = true;
                consoleControl1.StartProcess(processStartInfo);
                //consoleControl1.StartProcess("cmd.exe", "/C " + txtInput.Text);

                //AppendTextBox(process.StandardOutput.ReadToEnd());
                //   consoleControl1.WriteOutput(process.StandardOutput.ReadLine() , Color.Yellow);
                //     txtOutput.Text = consoleControl1.InternalRichTextBox;
                //  txtOutput.Text += process.StandardOutput.ReadToEnd();
                //    }

                //});
                //Thread th = new Thread(ths);
                //th.Start();


                //      txtOutput.Text = q;
                //      MessageBox.Show(q);
            }

        }
        //void outputWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    // Keep working until we're told to cancel
        //    while (outputWorker.CancellationPending == false)
        //    {
        //        // Just keep on reading the buffer
        //        int count = 0;
        //        char[] buffer = new char[1024];
        //        do
        //        {
        //            // Create a builder
        //            StringBuilder builder = new StringBuilder();

        //            // Read and append data
        //            count = outputReader.Read(buffer, 0, 1024);
        //            builder.Append(buffer, 0, count);

        //            // Report the progress
        //            outputWorker.ReportProgress(
        //                  0, new OutputEvent() { Output = builder.ToString() });
        //        } while (count > 0);
        //    }
        //}
        public void AppendTextBox(string value)
        {
            if (consoleControl1.InvokeRequired)
            {
                consoleControl1.Invoke(new Action(() =>
                {
                    // .. do some "work" in here ...
                    consoleControl1.WriteOutput(value, Color.Yellow);
                }));

                //   this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            // textBox1.Text += value;

        }
        //oepn foler
        private void button22_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  

            if (result == DialogResult.OK)
            {
                workingDir = folderBrowserDialog1.SelectedPath;
                textBox3.Text = ">> " + workingDir;
                Environment.SpecialFolder root = folderBrowserDialog1.RootFolder;
            }
        }
        public void updateWorkinDir()
        {
            textBox3.Text = ">> " + workingDir;
        }

    }
}
