using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnalogClock
{
    public partial class Form1 : Form
    {
        Bitmap bmp; // Бітмап для збереження зображення годинника
        Point center; // Центр годинника
        Timer timer; // Таймер для оновлення годинника щосекунди
        Graphics g; // Графічний об'єкт для малювання

        public Form1()
        {
            InitializeComponent();
            center = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2); // Встановлюємо центр годинника
            bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height); // Ініціалізація бітмапа з розміром форми
            timer = new Timer();
            timer.Interval = 1000; // Інтервал таймера 1000 мс (1 секунда)
            timer.Tick += Timer_Tick; // Додаємо обробник події для таймера
            timer.Start(); // Запуск таймера
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now; // Отримання поточного часу
            int second = now.Second; // Поточна секунда
            int minute = now.Minute; // Поточна хвилина
            int hour = now.Hour; // Поточна година

            // Обчислення кінцевих точок стрілок для секунд, хвилин і годин
            Point secEndPoint = CalculateSecondPoint(second, 100, center);
            Point minEndPoint = CalculateMinutePoint(minute, 80, center);
            Point hourEndPoint = CalculateHourPoint(hour % 12, minute, 60, center);

            g = Graphics.FromImage(bmp); // Отримання графічного об'єкта з бітмапа
            g.Clear(Color.White); // Очищення зображення білим кольором
            // Малювання циферблата
            g.FillEllipse(Brushes.Gray, new Rectangle(new Point(this.ClientSize.Width / 2 - 118, this.ClientSize.Height / 2 - 118), new Size(236, 236)));
            g.FillEllipse(Brushes.LightGray, new Rectangle(new Point(this.ClientSize.Width / 2 - 115, this.ClientSize.Height / 2 - 115), new Size(230, 230)));

            // Малювання поділок для секунд
            for (int i = 0; i < 60; i++)
            {
                g.DrawLine(Pens.Gray, CalculateSecondPoint(i, 101, center), CalculateSecondPoint(i, 105, center));
            }
            // Малювання поділок для годин
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(Pens.Black, CalculateHourPoint(i, 0, 101, center), CalculateHourPoint(i, 0, 110, center));
            }

            // Малювання стрілок
            g.DrawLine(Pens.Red, center, secEndPoint); // Секундна стрілка
            g.DrawLine(Pens.Black, center, minEndPoint); // Хвилинна стрілка
            g.DrawLine(Pens.Black, center, hourEndPoint); // Годинникова стрілка
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(this.ClientSize.Width / 2 - 4, this.ClientSize.Height / 2 - 4), new Size(8, 8))); // Центральна точка

            pictureBox1.Image = bmp; // Встановлення зображення на pictureBox
        }

        // Метод для обчислення кінцевої точки секундної стрілки
        private Point CalculateSecondPoint(int value, int radius, Point center)
        {
            double angle = Math.PI * value / 30 - Math.PI / 2;
            int x = (int)(center.X + radius * Math.Cos(angle));
            int y = (int)(center.Y + radius * Math.Sin(angle));
            return new Point(x, y);
        }

        // Метод для обчислення кінцевої точки хвилинної стрілки
        private Point CalculateMinutePoint(int value, int radius, Point center)
        {
            double angle = Math.PI * value / 30 - Math.PI / 2;
            int x = (int)(center.X + radius * Math.Cos(angle));
            int y = (int)(center.Y + radius * Math.Sin(angle));
            return new Point(x, y);
        }

        // Метод для обчислення кінцевої точки годинникової стрілки
        private Point CalculateHourPoint(int hour, int minute, int radius, Point center)
        {
            double angle = Math.PI * (hour * 5 + minute / 12.0) / 30 - Math.PI / 2;
            int x = (int)(center.X + radius * Math.Cos(angle));
            int y = (int)(center.Y + radius * Math.Sin(angle));
            return new Point(x, y);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bmp.Save("finalClock.bmp"); // Збереження фінального зображення годинника при закритті форми
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
