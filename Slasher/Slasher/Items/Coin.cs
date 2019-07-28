using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slasher.Items
{
    class Coin : Item
    {
        public Coin(int x, int y) : base(x, y)
        {
            Gold = 1;
            Type = "pickup";
        }

        public override void Draw()
        {

        }
    }
}
