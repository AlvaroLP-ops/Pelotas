using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Pelotas
{
    public class Pelota
    {
        int index;
        Size space;
        public Color c;
        // Variables de posición
        public float x;
        public float y;

        // Variables de velocidad
        private float vx;
        private float vy;

        // Variable de radio
        public float radio;

        public int opacidad1;
        public int opacidadgeneral;

        // Constructor
        public Pelota(Random rand, Size size, int index, int opacity, Pie Dapie)
        {
            this.radio  = rand.Next(70, 100);
            this.x      = -100;
            this.y      = -100;         
            c           = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));


            this.vy = rand.Next(15, (int)radio - 50);
            this.vx = rand.Next(15, (int)radio - 50);

            this.opacidadgeneral = opacity;
            this.opacidad1 = opacity;

            this.index = index;
            space = size;
        }

        public int PosX
        {
            set { this.x = value; }
        }

        public int PosY
        {
            set { this.y = value; }
        }

        public void Collision(Pelota otraPelota)
        {
            float distancia = (float)Math.Sqrt(Math.Pow((otraPelota.x - this.x), 2) + Math.Pow((otraPelota.y - this.y), 2));

            if (distancia < (this.radio + otraPelota.radio))//ESTO SIGNIFICA COLISIÓN...
            {
                // Calculamos las velocidades finales de cada pelota en función de su masa y velocidad inicial
                float masaTotal = this.radio + otraPelota.radio;
                float masaRelativa = this.radio / masaTotal;

                float v1fx = this.vx - masaRelativa * (this.vx - otraPelota.vx) / 100;
                float v1fy = this.vy - masaRelativa * (this.vy - otraPelota.vy) / 100;

                float v2fx = otraPelota.vx - masaRelativa * (otraPelota.vx - this.vx) / 100;
                float v2fy = otraPelota.vy - masaRelativa * (otraPelota.vy - this.vy) / 100;

                // Actualizamos las velocidades de las pelotas
                this.vx = v1fx;     // -----AQUI CAMBIAMOS EL ANGULO---------
                this.vy = v1fy;     // -----AQUI CAMBIAMOS EL ANGULO--------------

                otraPelota.vx = v2fx;//-----AQUI CAMBIAMOS EL ANGULO----------------------
                otraPelota.vy = v2fy;//-----AQUI CAMBIAMOS EL ANGULO----------------------

                // Movemos las pelotas para evitar que se superpongan
                float distanciaOverlap = (this.radio + otraPelota.radio) - distancia;
                float dx = (this.x - otraPelota.x) / distancia;
                float dy = (this.y - otraPelota.y) / distancia;

                this.x += dx * distanciaOverlap / 2f;
                this.y += dy * distanciaOverlap / 2f;

                otraPelota.x -= dx * distanciaOverlap / 2f;
                otraPelota.y -= dy * distanciaOverlap / 2f;
            }
        }







            // Método para actualizar la posición de la pelota en función de su velocidad
            public void Update(Random rand, Pie emitter)
        {
            this.y -= this.vy;
            if (y <= 0 - radio)
            {
                x = rand.Next(emitter.PosX - 50, emitter.PosX + emitter.Size - ((int)radio * 2 )+ 50);
                y = emitter.PosY + (emitter.Size / 2);
                opacidadgeneral = opacidad1;
            }
        }
    }

}
