using Pelotas.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelotas
{
    public class Pie
    {
        public int PosX, PosY;
        public int Size;
        
        public Pie(int posX, int posY, int size)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Size = size;
        }

        public int x
        {
            set { PosX = value; }
        }

        public int y
        {
            set { PosY = value; }
        }

        public void Dibujo(Graphics g)
        {
            Bitmap imgbitmap = new Bitmap(Resource1.fuego);
            using (Image resizedImage = resizeImage(imgbitmap, new Size(Size, Size)))
            {
                g.DrawImage(resizedImage, PosX, PosY);
            }
            
        }

        
        }
    }
}
