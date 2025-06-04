using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Buffers.Binary;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml.Linq;


namespace rgp
{


    public partial class Form1 : Form
    {
        static string path = @"D:\rgbJson.json";
        ProgramRGB tmp = new ProgramRGB();
        LEdsRGB leds = new LEdsRGB();
        List<LEdsRGB> LedsRGBs = new List<LEdsRGB>();

        static string workingDirectory = Directory.GetCurrentDirectory();
        // or: Directory.GetCurrentDirectory() gives the same result

        // This will get the current PROJECT directory
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        public Form1()
        {
            InitializeComponent();

            AllocConsole();
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool AllocConsole();

            int temp;

            
            tmp.MainInterface();
            //while (true)
            //{

            //    string t = tmp.SerialTempPublisher();
            //    temp = Int32.Parse(t.Substring(0, 2));
            //    Thread.Sleep(300);
            //    break;
            //    //Console.WriteLine(temp);
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string rgbFromJson = File.ReadAllText(path);


            List<LEdsRGB> led = JsonConvert.DeserializeObject<List<LEdsRGB>>(rgbFromJson);
            foreach (var item in led)
            {
                string label = "";
                label += item.get()[0] + ", " + item.get()[1] + ", " + item.get()[2];
                comboBox1.Items.Add(label);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string color = textBox1.Text + "&" + textBox2.Text + "&" + textBox3.Text + "&#";


            int r = Int32.Parse(textBox1.Text);
            int g = Int32.Parse(textBox2.Text);
            int b = Int32.Parse(textBox3.Text);



            Color rgb = Color.FromArgb(r, g, b);
            tmp.SerialDataSender(color);
            panel1.BackColor = rgb;
            //reszta z dzielenia pokazuje który kolor podczas przesy³ania


        }

        private void button2_Click(object sender, EventArgs e)
        {
            leds.set(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
            string label = "";
            label += leds.get()[0] + ", " + leds.get()[1] + ", " + leds.get()[2];
            comboBox1.Items.Add(label);
            comboBox1.BackColor = Color.White;
            LedsRGBs.Add(leds);
            string rgbToJson = JsonConvert.SerializeObject(LedsRGBs, Formatting.Indented);
            File.WriteAllText(path, rgbToJson);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //elements in combobox drawing

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Color color;
            //Text of your ComboBox element
            string text = ((ComboBox)sender).Items[0].ToString();
            //Check for something
           
                color = Color.Red;
           
            e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);
            e.Graphics.DrawString(text, e.Font, new SolidBrush(((ComboBox)sender).ForeColor), new Point(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }
    }
}

