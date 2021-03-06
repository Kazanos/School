﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Slasher
{
    class DrawingFunctions
    {
        //nakresli stenu
        public static void DrawBlock(int x, int y, Color color)
        {
            Pen p = new Pen(color);
            Rectangle r = new Rectangle(x, y, Form1.TileWidth, Form1.TileHeight);
            Form1.G.DrawRectangle(p, r);
            Form1.G.DrawLine(p, x, y, x + Form1.TileWidth, y + Form1.TileHeight);
            Form1.G.DrawLine(p, x, y + Form1.TileHeight, x + Form1.TileWidth, y);
        }

        //nakresli sipku
        public static void DrawDoor(int x, int y, int dx, int dy, Color color)
        {
            Pen p = new Pen(color);
            Form1.G.DrawLine(p, (int)Math.Round(x - dx * Form1.TileWidth * 0.35), (int)Math.Round(y - dy * Form1.TileHeight * 0.35), (int)Math.Round(x + dx * Form1.TileWidth * 0.35), (int)Math.Round(y + dy * Form1.TileHeight * 0.35));
            if (dx != 0)
            {
                Form1.G.DrawLine(p, (int)Math.Round(x + 0.5 * dx * Form1.TileWidth * 0.35), (int)Math.Round(y + 0.5 * Form1.TileHeight * 0.35), (int)Math.Round(x + dx * Form1.TileWidth * 0.35), (int)Math.Round(y + dy * Form1.TileHeight * 0.35));
                Form1.G.DrawLine(p, (int)Math.Round(x + 0.5 * dx * Form1.TileWidth * 0.35), (int)Math.Round(y - 0.5 * Form1.TileHeight * 0.35), (int)Math.Round(x + dx * Form1.TileWidth * 0.35), (int)Math.Round(y + dy * Form1.TileHeight * 0.35));
            }
            else if (dy != 0)
            {
                Form1.G.DrawLine(p, (int)Math.Round(x + 0.5 * Form1.TileWidth * 0.35), (int)Math.Round(y + 0.5 * dy * Form1.TileHeight * 0.35), (int)Math.Round(x + dx * Form1.TileWidth * 0.35), (int)Math.Round(y + dy * Form1.TileHeight * 0.35));
                Form1.G.DrawLine(p, (int)Math.Round(x - 0.5 * Form1.TileWidth * 0.35), (int)Math.Round(y + 0.5 * dy * Form1.TileHeight * 0.35), (int)Math.Round(x + dx * Form1.TileWidth * 0.35), (int)Math.Round(y + dy * Form1.TileHeight * 0.35));
            }
        }

        //nakresli minimapu
        public static void DrawMinimap()
        {
            Rectangle r;
            Pen p;
            foreach (Tuple<int,int> i in Form1.Game.Visible)
            {
                r = new Rectangle((Form1.Xoffset / 12) * (i.Item2 + 1) + 1, (Form1.Xoffset / 12) * (i.Item1 + 1) + 1, (Form1.Xoffset / 12) - 2, (Form1.Xoffset / 12) - 2);
                if (Form1.Game.Floor[i.Item1,i.Item2].Type == "room")
                {
                    p = new Pen(Color.Lime);
                    Form1.G.DrawRectangle(p, r);
                }
                else if (Form1.Game.Floor[i.Item1, i.Item2].Type == "special")
                {
                    p = new Pen(Color.Yellow);
                    Form1.G.DrawRectangle(p, r);
                }
            }

            int xside = Form1.Resx / 320;
            int yside = Form1.Resy / 180;
            Brush b = Brushes.White;
            r = new Rectangle((Form1.Xoffset / 12) * (Form1.Game.Current.Item2 + 1) + xside, (Form1.Xoffset / 12) * (Form1.Game.Current.Item1 + 1) + yside, (Form1.Xoffset / 12) - 2 * xside, (Form1.Xoffset / 12) - 2 * yside);
            Form1.G.FillEllipse(b, r);
        }

        //nakresli info o hracoch
        public static void DrawStats()
        {
            Brush b = Brushes.DarkRed;
            Rectangle r;
            for (int j = 0; j < Form1.Game.Players.Count; j++)
            {
                for (int i = 0; i < Form1.Game.Players[j].Hp; i++)
                {
                    r = new Rectangle(Form1.Xoffset + 17 * Form1.TileWidth + 3 * (Form1.Xoffset / 45) + (Form1.Xoffset / 45) * (2 * (i % 10)), j * Form1.Xoffset / 12 + Form1.Yoffset + (i / 10) * (Form1.Xoffset / 12) + 1, Form1.Xoffset / 45, Form1.Xoffset / 12 - 2);
                    Form1.G.FillRectangle(b, r);
                }
                DrawShard(Form1.Xoffset + 17 * Form1.TileWidth + 25 * (Form1.Xoffset / 45), j * Form1.Xoffset / 12 + Form1.Yoffset * 3 / 2);
                Font f = new Font("Verdana", 20);
                b = Brushes.White;
                Form1.G.DrawString(Form1.Game.Players[j].Shards.ToString(), f, b, Form1.Xoffset + 17 * Form1.TileWidth + 27 * (Form1.Xoffset / 45), j * Form1.Xoffset / 12 + Form1.Yoffset);
            }

        }

        //nakresli shard (peniaze)
        public static void DrawShard(int x, int y)
        {
            Pen p = new Pen(Color.Aqua);
            Point[] points = new Point[] {new Point(x, y), new Point(x, y - Form1.Resy/75), new Point(x + Form1.Resx/200, y), new Point(x, y + Form1.Resy/75), new Point(x - Form1.Resx /200, y) };
            Form1.G.DrawPolygon(p, points);
        }

        //nakresli ovladanie
        public static void DrawControls()
        {
            Font f = new Font("Verdana", Form1.Resx / 200);
            Brush b = Brushes.White;
            Form1.G.DrawString("WASD - Movement", f, b, Form1.Resx * 31 / 40, Form1.Resy * 24 / 40);
            Form1.G.DrawString("IJKL - Shooting", f, b, Form1.Resx * 31 / 40, Form1.Resy * 25 / 40);
            Form1.G.DrawString("1 - +0.1 DMG for 3 Shards", f, b, Form1.Resx * 31 / 40, Form1.Resy * 26 / 40);
            Form1.G.DrawString("2 - +1 HP and +0.1 SIZE for 4 Shards", f, b, Form1.Resx * 31 / 40, Form1.Resy * 27 / 40);
            Form1.G.DrawString("3 - +0.2 AS and -0.1 SHOTSIZE for 5 Shards", f, b, Form1.Resx * 31 / 40, Form1.Resy * 28 / 40);
        }
    }
}
