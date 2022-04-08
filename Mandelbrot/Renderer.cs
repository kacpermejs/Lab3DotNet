using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mandelbrot
{
    public class Renderer
    {
        public Bitmap bmp;
        public double fracTLx = -2;
        public double fracTLy = 2;
        public double fracBRx = 2;
        public double fracBRy = -2;
        public double offsetX = 0;
        public double offsetY = 0;


        int maxIterations = 500;

        public Renderer()
        {
            bmp = new Bitmap(10, 10);
        }

        public async void CenterToPoint(double x, double y)
        {
            double x0 = (fracTLx + fracBRx) / 2;
            double y0 = (fracTLy + fracBRy) / 2;
            offsetX = -(x - x0);
            offsetY = -(y - y0);
            
        }

        public async void Zoom(double factor)
        {
            
            fracTLx *= factor;
            fracTLy *= factor;
            fracBRx *= factor;
            fracBRy *= factor;
        }

        

    public async void Draw2(int pixelsX, int pixelsY)
        {

            Bitmap bm = new Bitmap(pixelsX, pixelsY);
            
            double xScale = (fracBRx - fracTLx) / (double)pixelsX;
            double yScale = (fracBRy - fracTLy) / (double)pixelsY;

            for (int x = 0; x < pixelsX; x++)
            {
                for (int y = 0; y < pixelsY; y++)
                {
                    
                    double a = (x * xScale + fracTLx - offsetX);
                    double b = (y * yScale + fracTLy - offsetY);
                    

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
                    //else if(a > 0 && b > 0)
                        //bm.SetPixel(x, y, Color.Red);
                    else
                        bm.SetPixel(x, y, Color.Black);

                }
            }
            //bm.SetPixel(450, 450, Color.White);
            bmp = bm;

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
