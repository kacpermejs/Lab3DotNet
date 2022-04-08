namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        Renderer R;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //get mouse (x, y)
            var point = MousePosition;
            Point P = PointToScreen(new Point(pictureBox1.Bounds.Left, pictureBox1.Bounds.Top));
            Int32 X = Cursor.Position.X - P.X;
            Int32 Y = Cursor.Position.Y - P.Y;
            Console.WriteLine(X + " " + Y);
            
            
            //convert to local coordinates
            double xScale = (double)pictureBox1.Width;
            double yScale = (double)pictureBox1.Height;

            double x = (X / xScale) * (R.fracBRx - R.fracTLx) + R.fracTLx - R.offsetX;
            double y = (Y / yScale) * (R.fracBRy - R.fracTLy) + R.fracTLy - R.offsetY;

            //center on the coordinates
            R.CenterToPoint(x, y);
            R.Draw2(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = R.bmp;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            R = new Renderer();
            //R.Draw(pictureBox1.Width, pictureBox1.Height, 1);
            R.Draw2(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = R.bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            R.Zoom(0.8);
            R.Draw2(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = R.bmp;
        }
    }
}