using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyDirectory
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sourceFolder = textBox1.Text;
            string destinationFolder = textBox2.Text;

            //check if the source and destination fields are empty.
            if (String.IsNullOrEmpty(textBox1.Text) && String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter the Source and Target destination.");
            }
            else if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter the Source destination.");
            }
            else if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter the Target destination.");
            }
            else

                try
                {
                    FileSystemWatcher watcher = new FileSystemWatcher();
                    watcher.Path = destinationFolder;
                    watcher.NotifyFilter = watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                    
                    watcher.Created += new FileSystemEventHandler(OnCreated);
                    
                    watcher.EnableRaisingEvents = true;

                    if (Directory.Exists(sourceFolder))
                    {
                        // Copy folder structure
                        foreach (string sourceSubFolder in Directory.GetDirectories(sourceFolder, "*", SearchOption.AllDirectories))
                        {
                            Directory.CreateDirectory(sourceSubFolder.Replace(sourceFolder, destinationFolder));
                        }
                        // Copy files
                        foreach (string sourceFile in Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories))
                        {
                            string destinationFile = sourceFile.Replace(sourceFolder, destinationFolder);
                            File.Copy(sourceFile, destinationFile, true);
                        }
                        MessageBox.Show("The files have been successfully copied.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }



        }
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            //*I wasn't sure how to present a visual representation so I decided to open a dialog box that lets the user know, the file had been created.

            MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);

           
        }

    }
}
