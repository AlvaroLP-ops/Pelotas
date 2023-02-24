using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Pelotas
{
    public partial class Pelotas : Form
    {
        static List<Pelota> balls;
        static Bitmap bmp;
        static Graphics g;
        static Random rand = new Random();
        static float deltaTime;

        static Pie Dapie;//objeto para pie
        public SolidBrush brushOrange = new SolidBrush(Color.FromArgb(30, 255, 128, 0)); // color fuego
        public SolidBrush brushBrown = new SolidBrush(Color.FromArgb(255, 128, 64, 0)); // color stick

        private int nBalls = 15;



        Bitmap bmpimagen = new Bitmap(Resource1.nuebe);//hacer bitmar imagen
        public Pelotas()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (PCT_CANVAS.Width == 0)
                return;

            //inicialido objeto cue
            Dapie = new Pie(MousePosition.X, MousePosition.Y, 400);

            balls = new List<Pelota>();
            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g = Graphics.FromImage(bmp);
            deltaTime = 1;
            PCT_CANVAS.Image = bmp;


            //dibujo el fuego
            Dapie.Dibujo(g);

            for (int b = 0; b < nBalls; b++)
                balls.Add(new Pelota(rand, PCT_CANVAS.Size, b, 128, Dapie));


            for (int b = nBalls / 2; b < nBalls; b++)
                balls.Add(new Pelota(rand, PCT_CANVAS.Size, b, 255, Dapie));
        }

        private void Pelotas_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Pelotas_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Transparent);

            Dapie.x = MousePosition.X - (Dapie.Size / 2);
            Dapie.y = MousePosition.Y - (Dapie.Size / 2);

            g.DrawImage(Resource1.fuego, MousePosition.X - 280, MousePosition.Y);

            // objeto para cambiar tributos imagen
            ImageAttributes atributosImagen = new ImageAttributes();


            //segun la posicion del mous
            g.FillEllipse(brushOrange, Dapie.PosX - (Dapie.Size / 2), Dapie.PosY - (Dapie.Size / 2), Dapie.Size * 2, Dapie.Size * 2);


            g.Clear(Color.Black);

            Parallel.For(0, balls.Count, b =>//ACTUALIZAMOS EN PARALELO
            {
                Pelota P;
                balls[b].Update(deltaTime, balls);
                P = balls[b];
            });

            Pelota p;
            for (int b = 0; b < balls.Count; b++)//PINTAMOS EN SECUENCIA
            {
                p = balls[b];
                g.FillEllipse(new SolidBrush(p.c), p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);
            }

            PCT_CANVAS.Invalidate();
            deltaTime += .1f;

        }



    }
}
    