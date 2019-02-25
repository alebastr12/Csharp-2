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
        protected static string fileName = @"..//../asteroid.png";
        protected Image img;
        protected RectangleF srcRect;
        public int Power { get; set; }
        public aster(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            img = Image.FromFile(fileName);
            base.Size = img.Size;
            srcRect = new Rectangle(Pos,Size);
            Power = 1;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img,srcRect);
            //Game.Buffer.Graphics.DrawImage()
        }
        public override void Update()
        {
            //Pos.X += Dir.X;
            //if (Pos.X == 0)
            //{
            //    Pos.X = Game.Width;
            //}
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width;//Dir.X = -Dir.X;
            //if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
            srcRect.X = Pos.X;
            srcRect.Y = Pos.Y;
        }
    }
}
