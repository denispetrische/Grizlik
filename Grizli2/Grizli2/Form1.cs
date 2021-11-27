using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grizli2
{
    public partial class Form1 : Form
    {  public const int mapsize = 15; // размер поля
        public const int picsize = 20; // размер клетки в пикселях 
        public int Xr, Yr; // переменнные координат спавна 
        public PictureBox LG; // маленький грызлик
        public int ZieleX = 0;
        public int ZieleY = 0;
        public int i, j;
        public int shifthorizontal = 971 - (picsize * mapsize / 2); // чтобы разместить поле по центру экрана по горизонтали
        public int shiftvertical = 451 - (picsize * mapsize / 2); // чтобы разместить поле по центру экрана по вертикали ( чуть выше, учитывая нижнюю полоску виндовс) 
        public PictureBox[,] box = new PictureBox[mapsize, mapsize]; // массив объектов ( клеток) 
        public int[,] posposX = new int[mapsize, mapsize]; // массив Х координат клеток
        public int[,] posposY = new int[mapsize, mapsize]; // массив У координат клеток
        public Form1()
        {
            InitializeComponent(); // инициализация формы 
            _generateMap(); // вызов метода генерации поля
            spawn();
            LGselect(); 
        }
       

        private void _generateMap() // метод создания поля клеток
        {
            for (int ii = 0; ii < mapsize; ii++)
            {
                for (int jj = 0; jj < mapsize; jj++)
                {
                    PictureBox pic = new PictureBox(); // создаём клетку как PictureBox
                    pic.Location = new Point(picsize * ii + shifthorizontal, picsize * jj + shiftvertical); // положение клеток на форме по пикселям
                    pic.Size = new Size(picsize, picsize); // размеры клетки
                    pic.BackColor = Color.White; // белый цвет клетки
                    pic.BorderStyle = BorderStyle.FixedSingle;                    // создаём границы клетки
                    pic.SendToBack();
                    this.Controls.Add(pic); // добавляем на форму клетку
                    box[ii, jj] = pic; // массив добавленных клеток
                    posposX[ii, jj] = pic.Location.X; // запись Х координаты каждой клетки
                    posposY[ii, jj] = pic.Location.Y; // запись Х координаты каждой клетки
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void spawn() // метод спавна в случайном месте маленького грызлика
        {
            Random x = new Random();
            Xr = x.Next(0, 15);
            Yr = x.Next(0, 15);
            PictureBox LG = new PictureBox();
            LG.Location = new Point(posposX[Xr, Yr], posposY[Xr, Yr]); // положение клеток на форме по пикселям
            LG.Size = new Size(picsize, picsize); // размеры клетки
            LG.BackColor = Color.Blue;
            this.Controls.Add(LG);
            LG.BringToFront();
            box[Xr, Yr].BackColor = Color.Yellow;
            i = 1;
            j = 1;
        }

        private void LGselect()
        {
            int startX = Xr - i;
            int startY = Yr - j;
            PictureBox[,] highlighted = new PictureBox[1 + 2 * i, 1 + 2 * j]; // массив для выделенной области при поиске клеток
            int[,] Xnext = new int[1 + 2 * i, 1 + 2 * j]; // массив Х координат этих клеток
            int[,] Ynext = new int[1 + 2 * i, 1 + 2 * j]; // массив Y координат этих клеток
            int[,] hlAppr = new int[1 + 2 * i, 1 + 2 * j]; // проверка цвета клеток

            for (int ii = 0; ii < 2 * i; i++)
            {
                for (int jj = 0; jj < 2 * j; jj++)
                {
                    if (startX + ii < 0 || startY + jj < 0 || startX + ii > 14 || startY + jj > 14) // проверка на выход за поле клеток
                    {
                        highlighted[ii, jj] = new PictureBox();
                        Xnext[ii, jj] = 0;
                        Ynext[ii, jj] = 0;
                    }
                    else
                    {
                        highlighted[ii, jj] = box[startX + ii, startY + jj];
                        Xnext[ii, jj] = Xr - startX + ii;
                        Ynext[ii, jj] = Yr - startY + jj;
                    }
                }
            }

            for (int ii = 0; ii < 2 * i; ii++)
            {
                for (int jj = 0; jj < 2 * j; jj++)
                {
                    if (ii == i & jj == j)
                    {
                        hlAppr[ii, jj] = 0;
                    }
                    else
                    {
                        int z = highlighted[ii, jj].BackColor == Color.White ? 1 : 0;
                        hlAppr[ii, jj] = z;
                    }
                }
            }




        }
        private void LGmove()
        {
            int X, Y;
            X = posposX[Xr + ZieleX, Yr + ZieleY];
            Y = posposY[Xr + ZieleX, Yr + ZieleY];
            LG.Location = new Point(X, Y);

        }
        
    }
}
