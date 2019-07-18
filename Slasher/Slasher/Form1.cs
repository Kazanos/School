using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slasher
{
    public partial class Form1 : Form
    {

        static int resx = Screen.PrimaryScreen.Bounds.Width;
        static int resy = Screen.PrimaryScreen.Bounds.Height;

        static int tileWidth = resx / 32;
        static int tileHeight = resy / 18;
        static int xoffset = (resx - 17 * tileWidth) / 2;
        static int yoffset = (resy - 17 * tileHeight) / 2;
        Bitmap bmp;
        Graphics g;
        private static Game game;
        int x;

        internal static Game Game { get => game; set => game = value; }
        public static int Resx { get => resx; set => resx = value; }
        public static int Resy { get => resy; set => resy = value; }
        public static int TileWidth { get => tileWidth; set => tileWidth = value; }
        public static int TileHeight { get => tileHeight; set => tileHeight = value; }
        public static int Xoffset { get => xoffset; set => xoffset = value; }
        public static int Yoffset { get => yoffset; set => yoffset = value; }

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(resx, resy);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);

            game = new Game();
            game.Players.Add(new Player());
            timer1.Interval = 18;
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Move();
            game.Draw();
            foreach (Player p in game.Players)
            {
                p.Attackcharge += timer1.Interval;
            }
            foreach (Creature enemy in game.Floor[game.Current.Item1, game.Current.Item2].Enemies)
            {
                enemy.Movecharge += timer1.Interval;
                enemy.Attackcharge += timer1.Interval;
                enemy.Shoot();
            }
        }
    }
}
