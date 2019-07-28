using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Slasher
{
    class Creature
    {
        int x;
        int y;
        Tuple<int, int> destination;
        int vx = 0;
        int vy = 0;
        double speed;
        double size;
        int damage;
        double shotspeed;
        double shotsize;
        double hp;
        double attackspeed;
        int attackcharge = 0;
        int movecharge = 1500;
        string type;

        public Creature(int basex, int basey)
        {
            x = basex + Form1.Xoffset;
            y = basey + Form1.Yoffset;
            destination = new Tuple<int, int>(Form1.Resx / 2, Form1.Resy / 2);
            speed = 0.7 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 300;
            size = 1.3;
            damage = 1;
            shotspeed = 0.6 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 250;
            shotsize = 1;
            hp = 4 +  Game.Difficulty;
            attackspeed = 1 + 0.25 * Game.Difficulty;
            type = "regular";
        }

        //vystrelenie projektilu na najblizsieho hraca
        public void Shoot()
        {
            if (attackcharge >= 1000 / attackspeed)
            {
                double hypotenuse = double.MaxValue;
                double newhypotenuse;
                Player chosenplayer = null;
                foreach (Player p in Form1.Game.Players)
                {
                    if (p.Hp > 0)
                    {
                        newhypotenuse = Math.Sqrt(Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2));
                        if (newhypotenuse < hypotenuse)
                        {
                            hypotenuse = newhypotenuse;
                            chosenplayer = p;
                        }
                    }
                }
                if (chosenplayer != null)
                {
                    int dx = chosenplayer.X - x;
                    int dy = chosenplayer.Y - y;
                    int velx = (int)Math.Round((dx / hypotenuse) * shotspeed);
                    int vely = (int)Math.Round((dy / hypotenuse) * shotspeed);
                    Form1.Game.Projectiles.Add(new Projectile(damage, velx, vely, shotsize, x, y, "enemy"));
                }
                attackcharge = 0;
            }
        }

        //pohyb s pripadnym vyberom noveho ciela pohybu
        public void Move()
        {
            if (movecharge >= 1500)
            {
                destination = new Tuple<int, int>(Form1.Random.Next(Form1.Xoffset + Form1.TileWidth, Form1.Xoffset + 16 * Form1.TileWidth), Form1.Random.Next(Form1.Yoffset + Form1.TileHeight, Form1.Yoffset + 16 * Form1.TileHeight));
                movecharge = 0;
            }

            

            double hypotenuse = Math.Sqrt(Math.Pow(destination.Item1 - x, 2) + Math.Pow(destination.Item2 - y, 2));
            //zabranuje vzacnemu crashu, kde destinacia trafi prave poziciu kde stoji, alebo pohyb skonci presne na destinacii
            while (Math.Abs(hypotenuse) <= speed)
            {
                destination = new Tuple<int, int>(Form1.Random.Next(Form1.Xoffset + Form1.TileWidth, Form1.Xoffset + 16 * Form1.TileWidth), Form1.Random.Next(Form1.Yoffset + Form1.TileHeight, Form1.Yoffset + 16 * Form1.TileHeight));
                hypotenuse = Math.Sqrt(Math.Pow(destination.Item1 - x, 2) + Math.Pow(destination.Item2 - y, 2));
            }

            int dx = destination.Item1 - x;
            int dy = destination.Item2 - y;
            vx = (int)Math.Round((dx / hypotenuse) * speed);
            vy = (int)Math.Round((dy / hypotenuse) * speed);
            int h = (x - Form1.Xoffset) / Form1.TileWidth;
            int hnext = (x - Form1.Xoffset + vx) / Form1.TileWidth;
            int v = (y - Form1.Yoffset) / Form1.TileHeight;
            int vnext = (y - Form1.Yoffset + vy) / Form1.TileHeight;

            if (Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Field[v, hnext] == 0)
            {
                x += vx;
            }
            else movecharge += 500;
            if (Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Field[vnext, h] == 0)
            {
                y += vy;
            }
            else movecharge += 500;
        }

        //vykresli priseru
        public void Draw()
        {
            int side = (int)Math.Round(Form1.Resx * size / 100);
            Brush b = Brushes.DarkViolet;
            Rectangle r = new Rectangle(x - side, y - side, 2 * side, 2 * side);
            Form1.G.FillEllipse(b, r);
        }

        //dropne mincu alebo zivot
        public void Drop()
        {
            Random random = new Random();
            int r = random.Next(1, 10);
            if (r == 1 | r == 2)
            {
                Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Items.Add(new Items.Healthpoint(x, y));
            }
            else Form1.Game.Floor[Form1.Game.Current.Item1, Form1.Game.Current.Item2].Items.Add(new Items.Shard(x, y));
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Tuple<int, int> Destination { get => destination; set => destination = value; }
        public int Vx { get => vx; set => vx = value; }
        public int Vy { get => vy; set => vy = value; }
        public double Speed { get => speed; set => speed = value; }
        public double Size { get => size; set => size = value; }
        public int Damage { get => damage; set => damage = value; }
        public double Shotspeed { get => shotspeed; set => shotspeed = value; }
        public double Shotsize { get => shotsize; set => shotsize = value; }
        public double Hp { get => hp; set => hp = value; }
        public double Attackspeed { get => attackspeed; set => attackspeed = value; }
        public int Attackcharge { get => attackcharge; set => attackcharge = value; }
        public int Movecharge { get => movecharge; set => movecharge = value; }
        public string Type { get => type; set => type = value; }
    }
}
