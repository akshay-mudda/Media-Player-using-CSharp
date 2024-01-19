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

namespace Media_Player_using_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.ValueMember = "Path";
            listBox1.DisplayMember = "FileName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true, ValidateNames = true, Filter = "All Supported Formats|*.wmv;*.wav;*.mp3;*.mp4" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    List<Mediafile> files = new List<Mediafile>();
                    foreach (string filenames in ofd.FileNames)
                    {
                        FileInfo fInfo = new FileInfo(filenames);
                        files.Add(new Mediafile() { FileName = Path.GetFileNameWithoutExtension(fInfo.FullName), Path = fInfo.FullName });
                        listBox1.DataSource = files;
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mediafile File = listBox1.SelectedItem as Mediafile;
            if (File != null)
            {
                WMPlayer.URL = File.Path;
                WMPlayer.Ctlcontrols.play();
            }
        }

        private void TogglePlayPause()
        {
            if (WMPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                WMPlayer.Ctlcontrols.pause();
            }
            else
            {
                WMPlayer.Ctlcontrols.play();
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string githubProfileLink = "https://github.com/akshay-mudda";

            // Open the link in the default web browser
            Process.Start(githubProfileLink);
        }

        private void WMPlayer_ClickEvent_1(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            TogglePlayPause();
        }
    }
}
