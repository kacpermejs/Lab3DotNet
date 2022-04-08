namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async void Drawing()
        {
            
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int x = 0; x < pictureBox1.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    double a = (double)(x - (pictureBox1.Width / 2)) / (double)(pictureBox1.Width / 4);
                    double b = (double)(y - (pictureBox1.Height / 2)) / (double)(pictureBox1.Height / 4);
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

                    } while (it < 100);
                    bm.SetPixel(x, y, it < 100 ? Color.White : Color.Black);

                }
            }
            pictureBox1.Image = bm;
            Console.Write(".");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Drawing();
        }
    }
}