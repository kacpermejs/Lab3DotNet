using System.Diagnostics;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        Renderer R;

        int Xpic;
        int Ypic;

        Stopwatch stopwatch = new Stopwatch();
        public TimeSpan t;

        public Form1()
        {
            InitializeComponent();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //get mouse (x, y)
            Point P = PointToScreen(new Point(pictureBox1.Bounds.Left, pictureBox1.Bounds.Top));
            
            Xpic = Cursor.Position.X - P.X - (pictureBox1.ClientSize.Width-pictureBox1.Image.Width)/2;
            Ypic = Cursor.Position.Y - P.Y - (pictureBox1.ClientSize.Height - pictureBox1.Image.Height) / 2;
            Console.WriteLine(Xpic + " " + Ypic);
            
            
            //convert to local coordinates
            double xScale = Math.Min(pictureBox1.Width, pictureBox1.Height); ;
            double yScale = Math.Min(pictureBox1.Width, pictureBox1.Height); ;

            double x = (Xpic / xScale) * (R.fracBRx - R.fracTLx) + R.fracTLx - R.offsetX;
            double y = (Ypic / yScale) * (R.fracBRy - R.fracTLy) + R.fracTLy - R.offsetY;

            //center on the coordinates
            R.CenterToPoint(x, y);
            Drawing();

        }

        public async void Drawing()
        {
            stopwatch.Reset();
            stopwatch.Start();
            int size = Math.Min(pictureBox1.Width, pictureBox1.Height);
            //R.Draw2(size, size);
            R.DrawThreaded(size, size, 8);
            pictureBox1.Image = R.bmp;
            t = stopwatch.Elapsed;
            TimeLabel.Text = "Elapsed time: " + t.TotalSeconds.ToString() + " seconds\n"
                           + "Iterations: " + R.maxIterations
                           + "\n(" + Xpic + ", " + Ypic + ")";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            R = new Renderer();
            //R.Draw(pictureBox1.Width, pictureBox1.Height, 1);
            Drawing();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.E)
            {
                R.Zoom(0.8);
                Drawing();
            }
            else if(e.KeyCode == Keys.Q)
            {
                R.Zoom(1.2);
                Drawing();
            }
            else if(e.KeyCode == Keys.W)
            {
                //increase iterations
                R.maxIterations += 64;
                Drawing();
                
            }
            else if (e.KeyCode == Keys.S)
            {
                //decrease iterations
                if (R.maxIterations > 64)
                {
                    R.maxIterations -= 64;
                    Drawing();
                }
                    
            }
        }

        private void TimeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}