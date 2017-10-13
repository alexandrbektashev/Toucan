using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toucan
{
    
    public partial class Form1 : Form
    {
        public static Bitmap _graph;
        Graphics gr;

        public List<string> output;

        /* 
        на этот раз я буду все тупа пихать в класс формы 
        и не заморачиваться с разделением все на разные функциональные типы

        ...ведь я прогер-натурал.....
        */

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            надо сделать загрзку графика по ссылке
            я сказал, что она есть, но на самом деле загрузил картинку сам))))))))


            это, кстати, на ссылка на наш график
            http://cliware.meteo.ru/webchart/timeser/27612/SYNOPRUS/TempDP/2000/1200?colors=255,255,255;255,255,255;0,0,0;0,0,0;255,0,0;0,255,0;0,0,0&dates=2017-01-01,2017-01-10
            
            в этой ссылке куча параметров, с наличием которых приложение станет круче и умнее

          
            */

            _graph = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = ".\\";
            openFileDialog1.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2; 
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open an image file";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try //"я взял защиту, дорогая..."
                {
                    _graph = new Bitmap(openFileDialog1.FileName);
                    btnMake.Enabled = true;

                    gr = this.panel1.CreateGraphics();
                    gr.DrawImage(_graph, 0, 0);

                    /*
                     * это очень круто,
                     когда изображение не помещается на панель
                     надо сделать крутой скроллинг, как в супер крутых приложениях

                    */
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            



            /*
              я еще не придумал архитектуру приложения
             */
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            //перепилить это говно

            output = new List<string>();


            //параметры изображения
            const int left = 73, right = 51, top = 10, bot = 70,
                scaleWidth = 94, scaleHeight = 31,
                imageWidth = 2000, imageHeight = 1200; 

            //параметры масштаба графика 
            const int stepDay = 20, stepHour = 20, stepDegree = 1;
            const int depth = 150; 

            //обрезаем ненужное
            int naturalHeight = imageHeight - top - bot;
            int naturalWidth = imageWidth - left - right;
            _graph = CropImage(_graph, new Rectangle(left, top, naturalWidth , naturalHeight ));
            gr.DrawImage(_graph, 0, 0);

            //начинаем разделять
            int countOfSteps = stepDay * stepHour;
            for (int i = 0; i < countOfSteps; i++)
            {
                int currentWidth = naturalWidth * i / countOfSteps;

                int cm = 255, xm = 0, ym = 0;

                for (int j = 0; j < naturalHeight; j++)
                {

                    Color pix = _graph.GetPixel(currentWidth, j);

                    //debugging

                    int curr = (pix.R + pix.G + pix.B)/ 3;


                    if ( curr < cm )
                    {
                        cm = curr;
                        xm = currentWidth;
                        ym = j;
                    }
                   
                }
                output.Add(string.Format("{0};{1}", xm, ym));
                SetVerticalLine(xm, ym, Color.Red);

                gr.DrawImage(_graph, 0, 0);
            }


           


        }



        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // Метод обрезки изображения
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

      

        public static void SetVerticalLine(int x, int y, Color c)
        {
            for (int i = 0; i < y; i++)
                _graph.SetPixel(x, i, c);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //Katzen, здесь надо сделать сохранение битмапа через savefiledialog
          //shneil, zoldat

        }

        
    }
}
