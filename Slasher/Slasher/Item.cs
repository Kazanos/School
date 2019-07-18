using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slasher
{
    class Item
    {
        int dmg = 0;
        int speed = 0;
        int size = 0;
        int shotspeed = 0;
        int shotsize = 0;
        int attackspeed = 0;
        int hp = 0;
        string type = "";
        int gold = 0;

        public int Dmg { get => dmg; set => dmg = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Size { get => size; set => size = value; }
        public int Shotspeed { get => shotspeed; set => shotspeed = value; }
        public int Shotsize { get => shotsize; set => shotsize = value; }
        public int Attackspeed { get => attackspeed; set => attackspeed = value; }
        public int Hp { get => hp; set => hp = value; }
        public string Type { get => type; set => type = value; }
        public int Gold { get => gold; set => gold = value; }
    }
}
