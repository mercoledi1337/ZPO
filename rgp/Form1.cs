using System.Runtime.InteropServices;


namespace rgp
{


    public partial class Form1 : Form
    {
        int r = 0;
        int g = 0;
        int b = 0;
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool AllocConsole();
            ProgramRGB tmp = new ProgramRGB();
            tmp.MainInterface();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            r = Int32.Parse(textBox1.Text);
            g = Int32.Parse(textBox2.Text);
            b = Int32.Parse(textBox3.Text);
            Color rgb = Color.FromArgb(r, g, b);

            panel1.BackColor = rgb;
        }

        
    }
}

