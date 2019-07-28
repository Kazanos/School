using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Slasher
{
    class Player
    {
        int x = Form1.Resx / 2;
        int y = Form1.Resy / 2;
        Tuple<int, int> direction = new Tuple<int, int>(0, 0);
        int vx = 0;
        int vy = 0;
        double speed = 0.7 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 200;
        double size = 1;
        double dmg = 1;
        Tuple<int, int> shotdirection = new Tuple<int, int>(0, 0);
        double shotspeed = 0.8 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 200;
        double shotsize = 1;
        int hp = 6;
        double attackspeed = 2;
        int attackcharge = 0;
        List<Item> items = new List<Item>();
        int shards = 0;

        //nakresli hraca
        public void Draw()
        {
            if (hp > 0)
            {
                int side = (int)Math.Round(Form1.Resx * size / 150);
                Brush b = Brushes.White;
                Rectangle r = new Rectangle(x - side, y - side, 2 * side, 2 * side);
                Form1.G.FillEllipse(b, r);
            }
        }

        public void Move()
        {
            if (direction.Item1 == 0 | direction.Item2 == 0)
            {
                vx = (int)Math.Round(direction.Item1 * speed);
                vy = (int)Math.Round(direction.Item2 * speed);
            }
            else
            {
                vx = (int)Math.Round(direction.Item1 * speed / Math.Sqrt(2));
                vy = (int)Math.Round(direction.Item2 * speed / Math.Sqrt(2));
            }

            int h = (x - Form1.Xoffset) / Form1.TileWidth;
            int hnext = (x - Form1.Xoffset + vx) / Form1.TileWidth;
            int v = (y - Form1.Yoffset) / Form1.TileHeight;
            int vnext = (x - Form1.Yoffset + vy) / Form1.TileHeight;
            int now = Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Field[v, h];

            if (!Form1.Game.Combat)
            {
                if (now >= 4)
                {
                    switch (now % 4)
                    {
                        case 0:
                            Form1.Game.Current = new Tuple<int, int>(Form1.Game.Current.Item1, Form1.Game.Current.Item2 + 1);
                            x = (int)Math.Round(Form1.Xoffset + 1.5 * Form1.TileWidth); break;
                        case 1:
                            Form1.Game.Current = new Tuple<int, int>(Form1.Game.Current.Item1 + 1, Form1.Game.Current.Item2);
                            y = (int)Math.Round(Form1.Yoffset + 1.5 * Form1.TileHeight); break;
                        case 2:
                            Form1.Game.Current = new Tuple<int, int>(Form1.Game.Current.Item1, Form1.Game.Current.Item2 - 1);
                            x = (int)Math.Round(Form1.Xoffset + 15.5 * Form1.TileWidth); break;
                        case 3:
                            Form1.Game.Current = new Tuple<int, int>(Form1.Game.Current.Item1 - 1, Form1.Game.Current.Item2);
                            y = (int)Math.Round(Form1.Yoffset + 15.5 * Form1.TileHeight); break;
                        default:
                            break;
                    }
                    Form1.Game.Projectiles = new List<Projectile>();
                    if (!Form1.Game.Visible.Exists(f => f == Form1.Game.Current))
                    {
                        Form1.Game.Visible.Add(Form1.Game.Current);
                    }
                    if (Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies.Count != 0)
                    {
                        Form1.Game.Combat = true;
                    }
                    if (Form1.Game.Bossesdefeated == Form1.Game.BossCount)
                    {
                        if (Game.Difficulty < 5)
                        {
                            Form1.Game.Indexes = new List<Tuple<int, int>>();
                            Form1.Game.Visible = new List<Tuple<int, int>>();
                            Game.Difficulty += 1;
                            Form1.Game.GenerateFloor();
                            Form1.Game.Current = Form1.Game.Indexes[Form1.Random.Next(Form1.Game.Indexes.Count)];
                            Form1.Game.Visible.Add(Form1.Game.Current);
                            Form1.Game.Bossesdefeated = 0;
                            Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2] = new Room("rooms/r0.txt");
                            //prida sipky na miesta kde sa da presjt do vedlajsej miestnosti
                            for (int i = 0; i < Form1.Game.Indexes.Count; i++)
                            {
                                v = Form1.Game.Indexes[i].Item1;
                                h = Form1.Game.Indexes[i].Item2;
                                if (v + 1 < 9)
                                {
                                    if (Form1.Game.Floor[v + 1, h].Type != "empty")
                                    {
                                        Form1.Game.Floor[v, h].Field[16, 8] = 1 + 4 * Form1.Game.Floor[v + 1, h].Field[0, 0];
                                        Form1.Game.Floor[v + 1, h].Field[0, 8] = 3 + 4 * Form1.Game.Floor[v, h].Field[0, 0];
                                    }
                                }
                                if (v - 1 >= 0)
                                {
                                    if (Form1.Game.Floor[v - 1, h].Type != "empty")
                                    {
                                        Form1.Game.Floor[v, h].Field[0, 8] = 3 + 4 * Form1.Game.Floor[v - 1, h].Field[0, 0];
                                        Form1.Game.Floor[v - 1, h].Field[16, 8] = 1 + 4 * Form1.Game.Floor[v, h].Field[0, 0];
                                    }
                                }
                                if (h + 1 < 9)
                                {
                                    if (Form1.Game.Floor[v, h + 1].Type != "empty")
                                    {
                                        Form1.Game.Floor[v, h].Field[8, 16] = 0 + 4 * Form1.Game.Floor[v, h + 1].Field[0, 0];
                                        Form1.Game.Floor[v, h + 1].Field[8, 0] = 2 + 4 * Form1.Game.Floor[v, h].Field[0, 0];
                                    }
                                }
                                if (h - 1 >= 0)
                                {
                                    if (Form1.Game.Floor[v, h - 1].Type != "empty")
                                    {
                                        Form1.Game.Floor[v, h].Field[8, 0] = 2 + 4 * Form1.Game.Floor[v, h - 1].Field[0, 0];
                                        Form1.Game.Floor[v, h - 1].Field[8, 16] = 0 + 4 * Form1.Game.Floor[v, h].Field[0, 0];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            h = (x - Form1.Xoffset) / Form1.TileWidth;
            hnext = (x - Form1.Xoffset + vx) / Form1.TileWidth;
            v = (y - Form1.Yoffset) / Form1.TileHeight;
            vnext = (y - Form1.Yoffset + vy) / Form1.TileHeight;
            if (hnext >= 0 & hnext < 17)
            {
                int next = Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Field[v, hnext];
                if (next == 0 | next > 3)
                {
                    x += vx;
                }
            }
            if (vnext >= 0 & vnext < 17)
            {
                int next = Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Field[vnext, h];
                if (next == 0 | next > 3)
                {
                    y += vy;
                }
            }
        }

        public void Shoot()
        {
            if (attackcharge >= 1000 / attackspeed)
            {
                attackcharge = 0;
                int shotvx = 0;
                int shotvy = 0;
                if (shotdirection.Item1 == 0 | shotdirection.Item2 == 0)
                {
                    shotvx = (int)Math.Round(shotdirection.Item1 * shotspeed);
                    shotvy = (int)Math.Round(shotdirection.Item2 * shotspeed);
                }
                else
                {
                    shotvx = (int)Math.Round(shotdirection.Item1 * shotspeed / Math.Sqrt(2));
                    shotvy = (int)Math.Round(shotdirection.Item2 * shotspeed / Math.Sqrt(2));
                }
                Form1.Game.Projectiles.Add(new Projectile(dmg, shotvx, shotvy, shotsize, x, y, "friendly"));
            }
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Tuple<int, int> Direction { get => direction; set => direction = value; }
        public int Vx { get => vx; set => vx = value; }
        public int Vy { get => vy; set => vy = value; }
        public double Speed { get => speed; set => speed = value; }
        public double Size { get => size; set => size = value; }
        public double Dmg { get => dmg; set => dmg = value; }
        public Tuple<int, int> Shotdirection { get => shotdirection; set => shotdirection = value; }
        public double Shotspeed { get => shotspeed; set => shotspeed = value; }
        public double Shotsize { get => shotsize; set => shotsize = value; }
        public int Hp { get => hp; set => hp = value; }
        public double Attackspeed { get => attackspeed; set => attackspeed = value; }
        public int Attackcharge { get => attackcharge; set => attackcharge = value; }
        public int Shards { get => shards; set => shards = value; }
        internal List<Item> Items { get => items; set => items = value; }
    }
}
