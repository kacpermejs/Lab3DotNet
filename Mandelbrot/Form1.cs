namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        Renderer R;

        int Xpic;
        int Ypic;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //get mouse (x, y)
            Point P = PointToScreen(new Point(pictureBox1.Location.X, pictureBox1.Location.Y));
            
            Xpic = Cursor.Position.X - P.X;
            Ypic = Cursor.Position.Y - P.Y;
            Console.WriteLine(Xpic + " " + Ypic);
            
            
            //convert to local coordinates
            double xScale = (double)pictureBox1.Width;
            double yScale = (double)pictureBox1.Height;

            double x = (Xpic / xScale) * (R.fracBRx - R.fracTLx) + R.fracTLx - R.offsetX;
            double y = (Ypic / yScale) * (R.fracBRy - R.fracTLy) + R.fracTLy - R.offsetY;

            //center on the coordinates
            R.CenterToPoint(x, y);
            Drawing();

        }

        public async void Drawing()
        {
            int size = Math.Min(pictureBox1.Width, pictureBox1.Height);
            R.Draw2(size, size);
            pictureBox1.Image = R.bmp;
            TimeLabel.Text = "Elapsed time: " + R.t.TotalSeconds.ToString() + " seconds\n"
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