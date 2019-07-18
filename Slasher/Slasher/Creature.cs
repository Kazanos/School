using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        int hp;
        double attackspeed;
        int attackcharge = 0;
        int movecharge = 1500;
        string type;

        public Creature(int basex, int basey, int diff)
        {
            x = basex + Form1.Xoffset;
            y = basey + Form1.Yoffset;
            destination = new Tuple<int, int>(Form1.Resx / 2, Form1.Resy / 2);
            vx = 0;
            speed = 0.7 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 300;
            size = 1.3;
            damage = 1;
            shotspeed = 0.6 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 250;
            shotsize = 1;
            hp = 4;
            attackspeed = 1 + 0.25 * diff;
            type = "regular";
        }

        public void Shoot()
        {
            if (attackcharge >= 1000 / attackspeed)
            {
                
            }
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
        public int Hp { get => hp; set => hp = value; }
        public double Attackspeed { get => attackspeed; set => attackspeed = value; }
        public int Attackcharge { get => attackcharge; set => attackcharge = value; }
        public int Movecharge { get => movecharge; set => movecharge = value; }
        public string Type { get => type; set => type = value; }
    }
}
