using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Buffers.Binary;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace rgp
{
    public partial class Form1 : Form
    {
        static string path = Directory.GetCurrentDirectory();

        int bezpiecznik = 0;
        ProgramRGB tmp = new ProgramRGB();
        List<LEdsRGB> LedsRGBs = new List<LEdsRGB>();
        List<DayTemp> temperatura = new List<DayTemp>();

        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool AllocConsole();

            label2.Text = "";
            tmp.MainInterface();
            Task.Factory.StartNew(() =>
            {
            while (bezpiecznik == 0)
                {
                    tmp.SerialTempPublisher();
                    string t = tmp.SerialTempPublisher();
                    label1.Invoke(new Action(delegate ()
                    {
                        if (t.Equals("999"))
                        {
                            label1.Text = "arduino nie pod³¹czone";
                            bezpiecznik = 1;
                        } else 
                        {
                            bezpiecznik = 0;
                            label1.Text = t;
                        }

                       
                    }));
                    Task.Delay(1000);
                    try
                    {
                        if (Int32.Parse(t.Substring(0, 2)) >= 10 && bezpiecznik == 0)
                        {

                            DayTemp temp = new DayTemp();
                           
                            temp.Temp = Int32.Parse(t.Substring(0, 2));

                            label2.Invoke(new Action(delegate ()
                            {
                                temp.Temp = Int32.Parse(t.Substring(0, 2));
                                if (temp.Temp >= 22)
                                    label2.Text = "za gor¹co!";
                                else
                                    label2.Text = "temperatura w porz¹dku";
                                temperatura.Add(temp);
                            }));
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string rgbFromJson = File.ReadAllText(path + "\\rgbJson.json");

            label3.Text = "";
            List<LEdsRGB> led = JsonConvert.DeserializeObject<List<LEdsRGB>>(rgbFromJson);
            if (led != null)
            {
                foreach (var item in led)
                {
                    string label = "";
                    label += item.get()[0] + ", " + item.get()[1] + ", " + item.get()[2];
                    comboBox1.Items.Add(label);
                    LedsRGBs.Add(item);
                }
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
            Regex rex = new Regex(@"^(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])$");
            if (rex.IsMatch(textBox1.Text))
            {
                int r = Int32.Parse(textBox1.Text);
                int g = Int32.Parse(textBox2.Text);
                int b = Int32.Parse(textBox3.Text);
                
                Color rgb = Color.FromArgb(r, g, b);
                panel1.BackColor = rgb;
                if (bezpiecznik == 0)
                    tmp.SerialDataSender(color);
                textBox1.ForeColor = Color.Black;
            }
            else
            {
                textBox1.ForeColor = Color.Red;
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LEdsRGB leds = new LEdsRGB();
            leds.set(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
            string label = "";
            label += leds.get()[0] + ", " + leds.get()[1] + ", " + leds.get()[2];
            comboBox1.Items.Add(label);
            comboBox1.BackColor = Color.White;
            LedsRGBs.Add(leds);
            string rgbToJson = JsonConvert.SerializeObject(LedsRGBs, Formatting.Indented);
            File.WriteAllText(path + "\\rgbJson.json", rgbToJson);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;
            string rgbFromComboBox = selectedItem.ToString();
            string[] tmpRgb = rgbFromComboBox.Split(',');
            Color rgb = Color.FromArgb(Int32.Parse(tmpRgb[0]), Int32.Parse(tmpRgb[1]), Int32.Parse(tmpRgb[2]));
            panel1.BackColor = rgb;
            textBox1.Text = tmpRgb[0];
            textBox2.Text = tmpRgb[1];
            textBox3.Text = tmpRgb[2];
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
        //    e.DrawBackground();
        //    Color color;
        //    string text = ((ComboBox)sender).Items[0].ToString();

        //    color = Color.Red;

        //    e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);
        //    e.Graphics.DrawString(text, e.Font, new SolidBrush(((ComboBox)sender).ForeColor), new Point(e.Bounds.X, e.Bounds.Y));
        //    e.DrawFocusRectangle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (temperatura.Count != 0)
            {
                int t2132 = temperatura[0].Temp;
                List<DayTemp> max = temperatura.Where(x => x.Temp >= t2132).ToList();
                max.Sort();
                label3.Text = max[0].Temp.ToString();
            }
        }
    }
}

