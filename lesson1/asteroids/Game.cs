using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace asteroids
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static List<BaseObject> _asteroids;
        public static List<BaseObject> _stars;
        private static Random rnd = new Random();
        private static int r = 5; //Скорость движения элементов
        private static Bullet _bullet;

        static Game()
        {
            
        }
        public static void Init(Form form)
        {
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // Создаем объект (поверхность рисования) и связываем его с формой
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            if (Width>1000 || Width<0 || Height>1000 || Height < 0)
            {
                throw new ArgumentOutOfRangeException($"Недопустимые значения ширины или высоты окна.");
            }
            Load();
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
        }
        /// <summary>
        /// Обработчик события таймера. Вызывает обновление состояний объектов и их перерисовку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Загрузка экземпляров в коллекцию
        /// </summary>
        public static void Load()
        {
            _asteroids = new List<BaseObject>();
            _stars = new List<BaseObject>();
            for (int i = 0; i < 10; i++)
            {
                _asteroids.Add(new aster(new Point(Game.Width, rnd.Next(0,Game.Height)), new Point(-rnd.Next(1,8), r), new Size()));
            }
            for (int i = 0; i < 50; i++)
            {
                _stars.Add(new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-i, 0), new Size(3, 3)));
            }
            _bullet = new Bullet(new Point(0, rnd.Next(0,Game.Height-10)), new Point(5, 0), new Size(6, 2));
            

        }
        /// <summary>
        /// Метод отрисовки фона и всех членов колекции _objs
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Render();
            foreach (BaseObject obj in _asteroids)
                obj.Draw();
            foreach (BaseObject obj in _stars)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }
        /// <summary>
        /// В методе вызываются методы update для всех членов _objs
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _stars)
                obj.Update();
            for (int i = 0; i < _asteroids.Count; i++)
            {
                _asteroids[i].Update();
                if (_asteroids[i].Collision(_bullet))
                {
                    SystemSounds.Hand.Play();
                    _bullet = new Bullet(new Point(0, rnd.Next(0, Game.Height-10)), new Point(5, 0), new Size(6, 2));
                    _asteroids[i] = new aster(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-rnd.Next(1, 8), r), new Size());
                }
            }
            _bullet.Update();
        }

    }
}
