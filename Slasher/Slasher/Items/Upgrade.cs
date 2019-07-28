using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slasher.Items
{
    class Upgrade : Item
    {
        public Upgrade(int x, int y) : base(x, y)
        {
            Dmg = 0.1;
            Speed = 0.03 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 200;
            Size = -0.03;
            Shotspeed = 0.1 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 200;
            Shotsize = 0.1;
            Hp = 2;
            Attackspeed = 0.1;
            Type = "pickup";
        }

        public override void Draw()
        {

        }
    }
}
