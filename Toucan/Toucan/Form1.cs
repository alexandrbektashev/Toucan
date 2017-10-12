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
        Graphics g;

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

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2; 
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open an image file";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try //"я взял защиту, дорогая..."
                {
                    _graph = new Bitmap(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            g = this.panel1.CreateGraphics();
            g.DrawImage(_graph, 0, 0); /* это очень круто,
            когда изображение не помещается на панель

            надо сделать крутой скроллинг, как супер крутых приложениях

            */



            /*
              я еще не придумал архитектуру приложения
             */
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
