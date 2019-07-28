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
        static Bitmap bmp;
        static Graphics g;
        static Game game;
        static Random random = new Random();
        static bool running = true;
        static bool won = false;

        internal static Game Game { get => game; set => game = value; }
        public static int Resx { get => resx; set => resx = value; }
        public static int Resy { get => resy; set => resy = value; }
        public static int TileWidth { get => tileWidth; set => tileWidth = value; }
        public static int TileHeight { get => tileHeight; set => tileHeight = value; }
        public static int Xoffset { get => xoffset; set => xoffset = value; }
        public static int Yoffset { get => yoffset; set => yoffset = value; }
        public static Graphics G { get => g; set => g = value; }
        public static Random Random { get => random; set => random = value; }
        public static bool Running { get => running; set => running = value; }
        public static bool Won { get => won; set => won = value; }

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(resx, resy);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);

            game = new Game();
            game.Players.Add(new Player());
            timer1.Interval = 30;
            timer1.Start();
        }

        public static void Reset()
        {
            game = new Game();
            game.Players.Add(new Player());
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
            if (!running & won)
            {
                Font f = new Font("Verdana", 60);
                Brush b = Brushes.Green;
                Form1.G.DrawString("You Won", f, b, Form1.Resx / 2, Form1.Resy / 2);
                timer1.Stop();
            }
            pictureBox1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //pohyb prveho hraca
                case Keys.W: game.Players[0].Direction = new Tuple<int, int>(game.Players[0].Direction.Item1, -1); break;
                case Keys.A: game.Players[0].Direction = new Tuple<int, int>(-1, game.Players[0].Direction.Item2); break;
                case Keys.S: game.Players[0].Direction = new Tuple<int, int>(game.Players[0].Direction.Item1, 1); break;
                case Keys.D: game.Players[0].Direction = new Tuple<int, int>(1, game.Players[0].Direction.Item2); break;
                //strielanie prveho hraca
                case Keys.I: game.Players[0].Shotdirection = new Tuple<int, int>(game.Players[0].Shotdirection.Item1, -1); break;
                case Keys.J: game.Players[0].Shotdirection = new Tuple<int, int>(-1, game.Players[0].Shotdirection.Item2); break;
                case Keys.K: game.Players[0].Shotdirection = new Tuple<int, int>(game.Players[0].Shotdirection.Item1, 1); break;
                case Keys.L: game.Players[0].Shotdirection = new Tuple<int, int>(1, game.Players[0].Shotdirection.Item2); break;
                //nakupovanie statov
                case Keys.D1:
                    foreach (Player p in game.Players)
                    {
                        if (p.Shards >= 3)
                        {
                            p.Shards -= 3;
                            p.Dmg += 0.1;
                        }
                    }
                    break;
                case Keys.D2:
                    foreach (Player p in game.Players)
                    {
                        if (p.Shards >= 4)
                        {
                            p.Shards -= 4;
                            p.Hp += 1;
                            p.Size += 0.1;
                        }
                    }
                    break;
                case Keys.D3:
                    foreach (Player p in game.Players)
                    {
                        if (p.Shards >= 5)
                        {
                            p.Shards -= 5;
                            p.Attackspeed += 0.2;
                            p.Shotsize -= 0.1;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //rusenie pohybu prveho hraca, viacerych zatial neimplementujem
                case Keys.W:
                    if (game.Players[0].Direction.Item2 == -1)
                    {
                        game.Players[0].Direction = new Tuple<int, int>(game.Players[0].Direction.Item1, 0);
                    }
                    break;
                case Keys.A:
                    if (game.Players[0].Direction.Item1 == -1)
                    {
                        game.Players[0].Direction = new Tuple<int, int>(0, game.Players[0].Direction.Item2);
                    }
                    break;
                case Keys.S:
                    if (game.Players[0].Direction.Item2 == 1)
                    {
                        game.Players[0].Direction = new Tuple<int, int>(game.Players[0].Direction.Item1, 0);
                    }
                    break;
                case Keys.D:
                    if (game.Players[0].Direction.Item1 == 1)
                    {
                        game.Players[0].Direction = new Tuple<int, int>(0, game.Players[0].Direction.Item2);
                    }
                    break;
                //rusenie strielania prveho hraca
                case Keys.I:
                    if (game.Players[0].Shotdirection.Item2 == -1)
                    {
                        game.Players[0].Shotdirection = new Tuple<int, int>(game.Players[0].Shotdirection.Item1, 0);
                    }
                    break;
                case Keys.J:
                    if (game.Players[0].Shotdirection.Item1 == -1)
                    {
                        game.Players[0].Shotdirection = new Tuple<int, int>(0, game.Players[0].Shotdirection.Item2);
                    }
                    break;
                case Keys.K:
                    if (game.Players[0].Shotdirection.Item2 == 1)
                    {
                        game.Players[0].Shotdirection = new Tuple<int, int>(game.Players[0].Shotdirection.Item1, 0);
                    }
                    break;
                case Keys.L:
                    if (game.Players[0].Shotdirection.Item1 == 1)
                    {
                        game.Players[0].Shotdirection = new Tuple<int, int>(0, game.Players[0].Shotdirection.Item2);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
