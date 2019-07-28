using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Slasher
{
    class Projectile
    {
        double damage;
        int vx;
        int vy;
        double basesize = Form1.Resx / 100;
        double size;
        int x;
        int y;
        string type;

        public Projectile(double dmg, int velx, int vely, double s, int newx, int newy, string t)
        {
            damage = dmg;
            vx = velx;
            vy = vely;
            size = s * basesize;
            x = newx;
            y = newy;
            type = t;
        }

        //pohne projektil a riesi kolizie
        public void Move()
        {
            int h = (x - Form1.Xoffset) / Form1.TileWidth;
            int v = (y - Form1.Yoffset) / Form1.TileHeight;
            int now = Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Field[v, h];
            x += vx;
            y += vy;

            if (now != 0)
            {
                Form1.Game.Projectiles.Remove(this);
            }
            else if (type == "enemy")
            {
                foreach (Player p in Form1.Game.Players)
                {
                    if (Math.Sqrt(Math.Pow(x - p.X, 2) + Math.Pow(y - p.Y, 2)) < (size / 2 + p.Size * Form1.Resx / 150))
                    {
                        p.Hp -= (int)damage;
                        Form1.Game.Projectiles.Remove(this);
                    }
                }
            }
            else if (type == "friendly")
            {
                //toto je celkom nechutne, no potrebujem zoznam prechadzat odzadu, cize nemozem pouzit foreach
                for (int i = Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies.Count - 1; i >= 0; i--)
                {
                    if (Math.Sqrt(Math.Pow(x - Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies[i].X, 2) + Math.Pow(y - Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies[i].Y, 2)) < (size / 2 + Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies[i].Size * Form1.Resx / 100))
                    {
                        Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies[i].Hp -= damage;
                        if (Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies[i].Hp <= 0)
                        {
                            Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies[i].Drop();
                            Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies.Remove(Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies[i]);
                            if (Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Enemies.Count == 0)
                            {
                                Form1.Game.Combat = false;
                                if (Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Type == "special")
                                {
                                    Form1.Game.Bossesdefeated += 1;
                                }
                            }
                        }
                        Form1.Game.Projectiles.Remove(this);
                    }
                }
            }
        }
        //vykresli projektil
        public void Draw()
        {
            double radius = size / 2;
            double innerradius = radius * 0.66;
            Brush b = Brushes.White;
            if (type == "friendly")
            {
                b = Brushes.Aqua;
            }
            else if (type == "enemy")
            {
                b = Brushes.DarkViolet;
            }
            Rectangle r = new Rectangle((int)Math.Round(x - innerradius), (int)Math.Round(y - innerradius), (int)Math.Round(2 * innerradius), (int)Math.Round(2 * innerradius));
            Form1.G.FillEllipse(b, r);
            r = new Rectangle((int)Math.Round(x - innerradius), (int)Math.Round(y - innerradius), (int)Math.Round(2 * innerradius), (int)Math.Round(2 * innerradius));
            b = Brushes.White;
            Form1.G.FillEllipse(b, r);
        }
    }
}
