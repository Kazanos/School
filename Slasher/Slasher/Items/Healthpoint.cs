using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
