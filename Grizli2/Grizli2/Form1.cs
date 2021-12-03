using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Grizli2
{
    public partial class Form1 : Form
    { 
        public const int mapsize = 15; // размер поля
        public const int picsize = 20; // размер клетки в пикселях 
        public int Xr, Yr; // переменнные текущего положения LG
        public int Xfut, Yfut; // цель маленького грызлика
        public int GXr, GYr; // положение большого Грызля
        public int n = 0; // Gryzlik's try № 
        public int foo = 0;
        public Boolean gameoverflag = false; // флаг конца игры
        public PictureBox LGG; // маленький грызлик
        public PictureBox BGG; // Большой Грызль
        public int detectiondistance = 4; // расстояние шума маленького грызлика большим Грызлем
        List<int> WhiteSquaresX = new List<int>();
        List<int> WhiteSquaresY = new List<int>();
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
            spawnBG();
            LGselect();
            timer1.Tick += new EventHandler(update);
            timer1.Interval = 100;
            timer1.Start();
            textBox1.Text = ("In progress c:");
        }

        private void update(object sender, EventArgs eventArgs)
        {
            if (gameoverflag == false)
            {
                if (Xfut == Xr & Yfut == Yr)
                {
                    LGselect();

                }
                else
                {
                    LGmove();
                }

                if (Math.Abs(Xr - GXr) <= detectiondistance || Math.Abs(Yr - GYr) <= detectiondistance)
                {
                    BGcatch();
                }
                else
                {
                    BGrandommove();
                }

            }
        }

        private void spawnBG()
        {
            Random x = new Random();
            GXr = x.Next(0, 15);
            GYr = x.Next(0, 15);
            PictureBox BG = new PictureBox();
            BG.Location = new Point(posposX[GXr, GYr], posposY[GXr, GYr]); // положение клеток на форме по пикселям
            BG.Size = new Size(picsize, picsize); // размеры клетки
            BG.BackColor = Color.Red;
            this.Controls.Add(BG);
            BG.BringToFront();
            BGG = BG;
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
            LGG = LG;
           
        }

        private void BGrandommove()
        {
            Random x = new Random();
            int Z = x.Next(8);

            switch(Z)
            {
                case 0:
                    GXr = GXr + 1;
                    if(GXr >= 15)
                    {
                        GXr = 14;
                            }
                    break;
                case 1:
                    GXr = GXr - 1;
                    if (GXr < 0)
                    {
                        GXr = 0;
                    }
                    break;
                case 2:
                    GYr = GYr + 1;
                    if (GYr >= 15)
                    {
                        GYr = 14;
                    }
                    break;
                case 3:
                    GYr = GYr - 1;
                    if (GYr < 0)
                    {
                        GYr = 0;
                    }
                    break;
                case 4:
                    GXr = GXr + 1;
                    GYr = GYr + 1;
                    if (GXr >= 15)
                    {
                        GXr = 14;
                    }
                    if (GYr >= 15)
                    {
                        GYr = 14;
                    }
                    break;
                case 5:
                    GXr = GXr - 1;
                    GYr = GYr + 1;
                    if (GXr < 0)
                    {
                        GXr = 0;
                    }
                    if (GYr >= 15)
                    {
                        GYr = 14;
                    }
                    break;
                case 6:
                    GXr = GXr + 1;
                    GYr = GYr - 1;
                    if (GXr >= 15)
                    {
                        GXr = 14;
                    }
                    if (GYr < 0)
                    {
                        GYr = 0;
                    }
                    break;
                case 7:
                    GXr = GXr - 1;
                    GYr = GYr - 1;
                    if (GXr < 0)
                    {
                        GXr = 0;
                    }
                    if (GYr < 0)
                    {
                        GYr = 0;
                    }
                    break;
                default:

                    break;

            }

            BGG.Location = new Point(posposX[GXr, GYr], posposY[GXr, GYr]);

        }

        private void newspawn()
        {
            Random x = new Random();
            Xr = x.Next(0, 15);
            Yr = x.Next(0, 15);
            LGG.Location = new Point(posposX[Xr, Yr], posposY[Xr, Yr]); // положение клеток на форме по пикселям
            box[Xr, Yr].BackColor = Color.Yellow;         
        }

        private void newspawnBG()
        {
            Random x = new Random();
            GXr = x.Next(0, 15);
            GYr = x.Next(0, 15);
            BGG.Location = new Point(posposX[Xr, Yr], posposY[Xr, Yr]);

        }

        private void BGcatch()
        {
            bool flagsteps = false;
            int countersteps = 0; // for steps restriction
            if(flagsteps == true)
            {
                countersteps--;
                if(countersteps == 0)
                {
                    flagsteps = false;
                }
            }
            if(Xr > GXr)
            {
                if(Xr - GXr > 1 & !flagsteps)
                {
                    GXr = GXr + 2;
                    countersteps++;
                    if(countersteps == 2)
                    {
                        flagsteps = true;
                    }
                }
                else
                {
                    GXr++;
                }
            }
            if(Xr < GXr)
            {
                if(GXr - Xr > 1 & !flagsteps)
                {
                    GXr = GXr - 2;
                    countersteps++;
                    if(countersteps == 2)
                    {
                        flagsteps = true;
                    }
                }
                else
                {
                    GXr--;
                }
            }
            if (Yr > GYr)
            {
                if (Yr - GYr > 1 & !flagsteps)
                {
                    GYr = GYr + 2;
                    countersteps++;
                    if (countersteps == 2)
                    {
                        flagsteps = true;
                    }
                }
                else
                {
                    GYr++;
                }
            }
            if (Yr < GYr)
            {
                if (GYr - Yr > 1 & !flagsteps)
                {
                    GYr = GYr - 2;
                    countersteps++;
                    if (countersteps == 2)
                    {
                        flagsteps = true;
                    }
                }
                else
                {
                    GYr--;
                }
            }
            BGG.Location = new Point(posposX[GXr, GYr], posposY[GXr, GYr]);
            if (GXr == Xr & GYr == Yr)
            { 
                n++;
                textBox2.Text = Convert.ToString(n);               
                newspawn();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {  if (foo != 1)
            {
                timer1.Stop();
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        box[i, j].BackColor = Color.White;
                    }
                }
                n = 0;
                newspawn();
                newspawnBG();
                timer1.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foo++;
            if(foo == 1)
            {
                gameoverflag = true;
                textBox1.Text = ("Paused");
            }
            if(foo == 2)
            {
                foo = 0;
                textBox1.Text = ("In progress c:");
                gameoverflag = false;
            }
            
        }

        private void LGselect()
        {
            WhiteSquaresX.Clear();
            WhiteSquaresY.Clear();
            for (int i = 0; i < 15; i++)
            { for (int j = 0; j < 15; j++)
                { if (box[i, j].BackColor == Color.White)
                    {
                        WhiteSquaresX.Add(i);
                        WhiteSquaresY.Add(j);
                        
                    }
                }
            }
            int Length = 0;
            Length = WhiteSquaresX.Count();
            if(Length == 0)
            { 
                textBox1.Text = ("GG");
                gameoverflag = true;
            }
            else
            {
                Random xx = new Random();
                int NEXT = xx.Next(Length);
                Xfut = WhiteSquaresX[NEXT];
                Yfut = WhiteSquaresY[NEXT];

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LGmove()
        {
            if(Xfut > Xr)
            {
                Xr = Xr + 1;
            }
            if(Xfut < Xr)
            {
                Xr = Xr - 1;
            }
            if(Yfut > Yr)
            {
                Yr = Yr + 1; 
            }
            if(Yfut < Yr)
            {
                Yr = Yr - 1;
            }
            LGG.Location = new Point(posposX[Xr, Yr], posposY[Xr, Yr]);
            box[Xr, Yr].BackColor = Color.Yellow;
            
         
        }
        
    }
}
