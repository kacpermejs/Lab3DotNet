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
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            R = new Renderer();
            R.Draw(pictureBox1.Width, pictureBox1.Height, 1);
            pictureBox1.Image = R.bmp;
        }
    }
}