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

        


        public int maxIterations = 64;

        public Renderer()
        {
            bmp = new Bitmap(10, 10);
            
        }

        public void CenterToPoint(double x, double y)
        {
            double x0 = (fracTLx + fracBRx) / 2;
            double y0 = (fracTLy + fracBRy) / 2;
            offsetX = -(x - x0);
            offsetY = -(y - y0);
            
        }

        public void Zoom(double factor)
        {
            
            fracTLx *= factor;
            fracTLy *= factor;
            fracBRx *= factor;
            fracBRy *= factor;
        }

        public async void DrawThreaded(int pixelsX, int pixelsY, int threadCount)
        {
            Thread[] threads = new Thread[threadCount];
            //precalculation
            bmp = new Bitmap(pixelsX, pixelsY);
            int[,] map = new int[pixelsX, pixelsY];
            double xScale = (fracBRx - fracTLx) / (double)pixelsX;
            double yScale = (fracBRy - fracTLy) / (double)pixelsY;

            int stripSize = pixelsX / threadCount;
            int diff = pixelsX - (stripSize * threadCount);

            for (int i = 0; i < threadCount; i++)
            {
                var temp = i;
                threads[i] = new Thread(() => DrawSection(stripSize, pixelsY, temp, xScale, yScale, map) );
            }
            threads[threadCount-1] = new Thread(() => DrawSection(stripSize, pixelsY, threadCount-1, xScale, yScale, map));

            for (int i = 0; i < threadCount; i++)
            {
                threads[i].Start();
            }
            for (int i = 0; i < threadCount; i++)
                threads[i].Join();
            for(int x = 0; x < pixelsX; x++)
            {
                for (int y = 0; y < pixelsY; y++)
                {
                    int it = map[x, y];
                    if (it < maxIterations)
                        bmp.SetPixel(x, y, Color.FromArgb(150, (it % 32) * 7, (it % 16) * 7, (it % 128) * 2));//(it % 32) * 7, (it % 16) * 7, (it % 128) * 2)
                                                                                                              //else if(a > 0 && b > 0)
                                                                                                              //bm.SetPixel(x, y, Color.Red);
                    else
                        bmp.SetPixel(x, y, Color.Black);
                }
            }
        }

        private async void DrawSection(int stripSize, int pixelsY, int threadNum, double xScale, double yScale, int[,] map)
        {
            

            for (int x = 0; x < stripSize; x++)
            {
                for (int y = 0; y < pixelsY; y++)
                {

                    double a = ((x + threadNum*stripSize) * xScale + fracTLx - offsetX);
                    double b = (y * yScale + fracTLy - offsetY);


                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);

                    int it = 0;
                    do
                    {
                        it++;
                        z.Square();
                        z += c;

                        if (z.ModSquared() > 4.0)//optimized (z.Mod() > 2.0)
                            break;

                    } while (it < maxIterations);
                    map[x + threadNum * stripSize, y] = it;

                }
            }
            

        }
        public void Draw2(int pixelsX, int pixelsY)
        {
            
            //Bitmap bm = new Bitmap(pixelsX, pixelsY);
            bmp = new Bitmap(pixelsX, pixelsY);
            
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

                        if (z.ModSquared() > 4.0)//optimized (z.Mod() > 2.0)
                            break;

                    } while (it < maxIterations);
                    if (it < maxIterations)
                        bmp.SetPixel(x, y, Color.FromArgb(150, (it % 32) * 7, (it % 16) * 7, (it % 128) * 2));//(it % 32) * 7, (it % 16) * 7, (it % 128) * 2)
                    //else if(a > 0 && b > 0)
                        //bm.SetPixel(x, y, Color.Red);
                    else
                        bmp.SetPixel(x, y, Color.Black);

                }
            }
            //bm.SetPixel(450, 450, Color.White);
            //bmp = bm;
            

        }
    }
}
