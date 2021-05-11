using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;
using System.IO;

namespace yt_downloader_2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string SelectFolder()
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директории";
            //DirDialog.SelectedPath = @"C:\";

            if (DirDialog.ShowDialog() == DialogResult.OK)
            {
                return DirDialog.SelectedPath;
            }
            return DirDialog.SelectedPath;
        }

        private void Download(string path, string url)
        {
            var youTube = YouTube.Default;

            Console.WriteLine(listBox1.SelectedIndex.ToString());
            Console.WriteLine(listBox1.Items.Count);
            
            var video = YouTube.Default.GetAllVideos(url).First(v => v.Resolution == int.Parse(listBox1.SelectedItem.ToString()));
            var audio = YouTube.Default.GetVideo(url);

            byte[] bytesvideo = video.GetBytes();
            File.WriteAllBytes(path + video.FullName, bytesvideo);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<int> allResolutuons = new List<int>();

            var videoInfos = Client.For(YouTube.Default).GetAllVideosAsync(textBox1.Text).GetAwaiter().GetResult();
            var resolutions = videoInfos.Where(j => j.AdaptiveKind == AdaptiveKind.Video).Select(j => j.Resolution);

            foreach (int resol in resolutions)
            {
                if (allResolutuons.IndexOf(resol) == -1 && resol > 240 && resol < 1080)
                {
                    allResolutuons.Add(resol);
                }
            }

            allResolutuons.Sort();
            allResolutuons.Reverse();

            foreach (int resol in allResolutuons)
            {
                Console.WriteLine(resol);
                listBox1.Items.Add(resol);
                Console.WriteLine(listBox1.Items.Count);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = SelectFolder() + "/";
            string link = textBox1.Text;

            Download(path, link);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
