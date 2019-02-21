using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
        public static List<BaseObject> _objs;
        
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
            _objs = new List<BaseObject>(50);
            for (int i = 0; i < 25; i++)
            {
                _objs.Add(new BaseObject(new Point(600, i * 30), new Point(30 - i, 30 - i), new Size(i + 5, i + 5)));
            }
            for (int i = 0; i < 25; i++)
            {
                _objs.Add(new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5)));
            }
            _objs.Add(new aster(new Point(0, 0), new Point(3, 3), new Size()));
            _objs.Add(new Label("НАЧАЛО ИГРЫ", new Point(300, 100), new Size(30, 30)));
            _objs.Add(new Label("РЕКОРДЫ", new Point(300, 150), new Size(30, 30)));
            _objs.Add(new Label("ВЫХОД", new Point(300, 200), new Size(30, 30)));

        }
        /// <summary>
        /// Метод отрисовки фона и всех членов колекции _objs
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Render();
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }
        /// <summary>
        /// В методе вызываются методы update для всех членов _objs
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

    }
}
