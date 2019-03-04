using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace asteroids
{
    class Bullet : BaseObject
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        /// <summary>
        /// Переопределенный метод отрисовки
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Переопределенный метод обновления положения
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }
        public override bool isScreenOut()
        {
            return (Pos.X > Game.Width);
        }
        
    }

}
