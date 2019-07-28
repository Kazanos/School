using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slasher
{
    class Boss : Creature
    {
        public Boss(int x, int y) : base(x, y)
        {
            Speed = (0.8 + 0.4 * Game.Difficulty) * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 300;
            Size = 6 - Game.Difficulty;
            Damage = 2;
            Shotspeed = 1 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 250;
            Shotsize = 1 + 0.25 * Game.Difficulty;
            Hp = 20 + 10 * Game.Difficulty;
            Attackspeed = 1.25 + 0.25 * Game.Difficulty;
            Type = "boss";
        }
    }
}
