using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Slasher.Items
{
    class Healthpoint : Item
    {
        public Healthpoint(int x, int y) : base(x, y)
        {
            Hp = 1;
            Type = "pickup";
        }

        public override void Draw()
        {
            Brush b = Brushes.DarkRed;
            Rectangle r = new Rectangle(X - Form1.Xoffset / 100, Y - Form1.Xoffset / 30, Form1.Xoffset / 50, Form1.Xoffset / 15);
            Form1.G.FillRectangle(b, r);
        }
    }
}
