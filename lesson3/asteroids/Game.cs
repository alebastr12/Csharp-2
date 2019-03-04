using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.IO;

namespace asteroids
{
    delegate void LogInto(string message);
    class Game
    {
        private static LogInto myDelegate;

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static List<BaseObject> _asteroids;
        public static List<BaseObject> _stars;
        public static List<BaseObject> _bullets;
        public static Medicine medic;
        private static Random rnd;
        private static int medicTimeout;
        private static int r = 5; //Скорость движения элементов
        private static int score = 0;
        private static Ship _ship;
        private static Timer timer;

        static Game()
        {
            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
            rnd = new Random();
        }
        /// <summary>
        /// Метод иницализирует объекты и их начальные состояния
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
            myDelegate += LogIntoConsole;
            myDelegate += LogIntoFile;
            medicTimeout = rnd.Next(50, 1000); //Случайная величина до пояыления аптечки
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
            myDelegate?.Invoke("Проведена инициализация...");
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.ControlKey)
            {
                _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(20, 0), new Size(4, 1)));
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();

        }

        /// <summary>
        /// Обработчик события таймера. Вызывает обновление состояний объектов и их перерисовку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (medic == null | medic?.isScreenOut() ?? false) //выдаем аптечку через случайные промежутки времени
            {
                if (medicTimeout != 0)
                {
                    medicTimeout--;
                }
                else
                {
                    medicTimeout = rnd.Next(50, 1000);
                    medic = new Medicine(new Point(Width, rnd.Next(0, Height)), new Point(rnd.Next(10, 30), r), new Size(18, 18));
                }
            }
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
            _bullets = new List<BaseObject>();
            for (int i = 0; i < 10; i++)
            {
                _asteroids.Add(new aster(new Point(Game.Width, rnd.Next(0,Game.Height)), new Point(-rnd.Next(4,12), r), new Size()));
            }
            for (int i = 0; i < 50; i++)
            {
                _stars.Add(new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-i, 0), new Size(3, 3)));
            }
            //_bullet = new Bullet(new Point(0, rnd.Next(0,Game.Height-10)), new Point(5, 0), new Size(6, 2));
                   
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
            foreach (BaseObject obj in _bullets)
                obj.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString($"Эергия:{_ship.Energy} Очки: {score}", 
                    new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular), Brushes.White, 0, 0);
            }
            medic?.Draw();
            Buffer.Render();
        }
        /// <summary>
        /// В методе вызываются методы update для всех членов _objs
        /// </summary>
        public static void Update()
        {
            int count;
            medic?.Update();
            foreach (BaseObject obj in _stars)
                obj.Update();
            _bullets.RemoveAll((e)=>(e as Bullet).isScreenOut()); //Удаляем элементы за пределами экрана
            for (int i = 0; i < _asteroids.Count; i++)
            {
                _asteroids[i].Update();
                count = _bullets.Count;
                for (int j = 0; j < count; j++)
                {
                    if (_asteroids[i].Collision(_bullets[j]))
                    {
                        score++;
                        myDelegate?.Invoke("Столкновение пули с астеройдом...");
                        SystemSounds.Hand.Play();
                        _bullets.Remove(_bullets[j]);
                        count--;
                        j--;
                        _asteroids[i] = new aster(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-rnd.Next(4, 12), r), new Size());
                    }
                }
                if (medic?.Collision(_ship) ?? false)
                {
                    medic = null;
                    SystemSounds.Beep.Play();
                    _ship.EnergyHigh(10);
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                _ship?.EnergyLow(rnd.Next(1, 10));
                myDelegate?.Invoke("Столкновение корабля с астеройдом...");
                SystemSounds.Asterisk.Play();
            }
            foreach (var item in _bullets)
            {
                item.Update();
            }
        }
        public static void Finish()
        {
            timer.Stop();
            myDelegate?.Invoke("конец игры...");
            Buffer.Graphics.DrawString("Конец игры", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Graphics.DrawString($"Ваши очки {score}", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Regular), Brushes.White, 200, 200);
            Buffer.Render();
        }
        /// <summary>
        /// Метод для вывода информации в лог файл
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void LogIntoFile(string message) {
            using(StreamWriter log = new StreamWriter("data.log", true, Encoding.UTF8))
            {
                DateTime now = DateTime.Now;
                log.WriteLine($"{now.ToShortDateString()} {now.ToShortTimeString()} - {message}");
            }
        }
        /// <summary>
        /// Методдля вывода информации в консоль
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void LogIntoConsole(string message)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"{now.ToShortDateString()} {now.ToShortTimeString()} - {message}");
        }

    }
}
