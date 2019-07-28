using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slasher
{
    class Item
    {
        double dmg = 0;
        double speed = 0;
        double size = 0;
        double shotspeed = 0;
        double shotsize = 0;
        double attackspeed = 0;
        int hp = 0;
        string type = "";
        int gold = 0;
        int x;
        int y;

        public Item(int newx, int newy)
        {
            x = newx;
            y = newy;
        }

        public virtual void Draw()
        {
        }


        public double Dmg { get => dmg; set => dmg = value; }
        public double Speed { get => speed; set => speed = value; }
        public double Size { get => size; set => size = value; }
        public double Shotspeed { get => shotspeed; set => shotspeed = value; }
        public double Shotsize { get => shotsize; set => shotsize = value; }
        public double Attackspeed { get => attackspeed; set => attackspeed = value; }
        public int Hp { get => hp; set => hp = value; }
        public string Type { get => type; set => type = value; }
        public int Gold { get => gold; set => gold = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
