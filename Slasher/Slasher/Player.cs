using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slasher
{
    class Player
    {
        int x = Form1.Resx / 2;
        int y = Form1.Resy / 2;
        Tuple<int, int> direction = new Tuple<int, int>(0, 0);
        int vx = 0;
        int vy = 0;
        double speed = 0.7 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 200;
        double size = 1;
        int dmg = 1;
        Tuple<int, int> shotDirection = new Tuple<int, int>(0, 0);
        double shotspeed = 0.8 * Math.Sqrt(Math.Pow(Form1.Resx, 2) + Math.Pow(Form1.Resy, 2)) / 200;
        double shotsize = 1;
        int hp = 6;
        double attackspeed = 2;
        int attackcharge = 0;
        List<Item> items = new List<Item>();
        int gold = 0;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Tuple<int, int> Direction { get => direction; set => direction = value; }
        public int Vx { get => vx; set => vx = value; }
        public int Vy { get => vy; set => vy = value; }
        public double Speed { get => speed; set => speed = value; }
        public double Size { get => size; set => size = value; }
        public int Dmg { get => dmg; set => dmg = value; }
        public Tuple<int, int> ShotDirection { get => shotDirection; set => shotDirection = value; }
        public double Shotspeed { get => shotspeed; set => shotspeed = value; }
        public double Shotsize { get => shotsize; set => shotsize = value; }
        public int Hp { get => hp; set => hp = value; }
        public double Attackspeed { get => attackspeed; set => attackspeed = value; }
        public int Attackcharge { get => attackcharge; set => attackcharge = value; }
        public int Gold { get => gold; set => gold = value; }
        internal List<Item> Items { get => items; set => items = value; }
    }
}
