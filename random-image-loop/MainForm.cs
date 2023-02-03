using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace random_image_loop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            pictureBoxBG.SizeMode = PictureBoxSizeMode.StretchImage;
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            string[] favs = Enumerable.Range(0, 10).Select(_=>Path.Combine(dir, $"image-{_}.png")).ToArray();
            _task = WaitSomeTime(favs, 100);
        }
        Task _task;
        Random rnd = new Random();
        public async Task WaitSomeTime(String[] favs, int time)
        {
            while (true)
            {
                List<Image> trash= new List<Image>();
                favs = favs.OrderBy(item => rnd.Next()).ToArray();
                foreach (string fav in favs)
                {
                    await Task.Delay(time);
                    if(pictureBoxBG.Image != null)
                    {
                        trash.Add(pictureBoxBG.Image);
                    }
                    Image img = new Bitmap(fav);
                    pictureBoxBG.Image = img;
                }
                for (int i = 0; i < trash.Count; i++)
                {
                    trash[i].Dispose();
                    trash[i] = null;
                }
                GC.Collect();
            }
        }
    }
}
