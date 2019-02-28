using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace asteroids
{
    class Ship : BaseObject
    {
        /// <summary>
        /// Свойство определяющее количество жизней корабля
        /// </summary>
        public int Energy { get; private set; }

        public static event Action MessageDie;

        public Ship(Point pos, Point dir, Size size):base(pos,dir,size)
        {
            Energy = 100;
        }
        /// <summary>
        /// Метод нанесения урона
        /// </summary>
        /// <param name="n">количество урона</param>
        public void EnergyLow(int n)
        {
            Energy -= n;
            if (Energy < 0) Die();
        }
        /// <summary>
        /// Лечение корабля
        /// </summary>
        /// <param name="n">На сколько лечить</param>
        public void EnergyHigh(int n)
        {
            Energy += n;
        }
        /// <summary>
        /// отрисовка корабля
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            
        }
        /// <summary>
        /// Движение корабля вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        /// <summary>
        /// Движение корабля вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        /// <summary>
        /// Уничтожение корабля
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }

    }
}
