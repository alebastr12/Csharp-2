using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asteroids
{/// <summary>
/// Класс изображения астеройда
/// </summary>
    class aster:BaseObject
    {
        protected static string fileName = @"..//../aster.png";
        protected Image img;

        public aster(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            img = Image.FromFile(fileName);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos);
        }
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X >= Game.Width)
            {
                Pos.X = 0;
            }
            else if (Pos.Y >= Game.Height)
            {
                Pos.Y = 0;
            }
        }
    }
}
