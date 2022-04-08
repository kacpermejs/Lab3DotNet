using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
    public class Renderer
    {
        public Bitmap bmp;
        double RM0 = 0;
        double IM0 = 0;
        double TopToBotomValue = 4;
        
        int maxIterations = 1000;

        public Renderer()
        {
            bmp = new Bitmap(10, 10);
        }

        public async void CenterToPixel(int X, int Y)
        {

        }

        public async void Draw(int pixelsX, int pixelsY, double Zoom)
        {
            
            Bitmap bm = new Bitmap(pixelsX, pixelsY);
            double offsetLeft = (double)(pixelsX / 2);
            double offsetDown = (double)(pixelsY / 2);

            for (int x = 0; x < pixelsX; x++)
            {
                for (int y = 0; y < pixelsY; y++)
                {
                    double a = (double)(x - offsetLeft) / (double)(pixelsX / (4/Zoom));
                    double b = (double)(y - offsetDown) / (double)(pixelsY / (4/Zoom));

                    
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);

                    int it = 0;
                    do
                    {
                        it++;
                        z.Square();
                        z += c;

                        if (z.Mod() > 2.0)
                            break;

                    } while (it < maxIterations);
                    if (it < maxIterations)
                        bm.SetPixel(x, y, Color.FromArgb(150, (it % 32) * 7, (it % 16) * 7, (it % 128) * 2));//(it % 32) * 7, (it % 16) * 7, (it % 128) * 2)
                    else
                        bm.SetPixel(x, y, Color.Black);

                }
            }
            bmp = bm;

        }

    }
}
