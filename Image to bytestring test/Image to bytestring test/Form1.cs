using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace Image_to_bytestring_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
                File.WriteAllText(Directory.GetCurrentDirectory() + "/varbinoutput.txt", image2varbin(pictureBox1.Image));
        }

        string image2varbin(Image image)
        {
            byte[] streamOutput;
            string output = "";
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                streamOutput = ms.ToArray();
            }
            foreach (byte b in streamOutput)
            {
                string number = Convert.ToString(Convert.ToInt32(b));
                while (number.Length < 3)
                    number = "0" + number;
                output += number;
            }
            return output;
        }

        Image varbin2image(string varbin)
        {
            byte[] streamInput = new byte[varbin.Length / 3];
            for (int i = 0; i < streamInput.Length; i++)
            {
                int input = Convert.ToInt32(Convert.ToString(varbin[0]) + Convert.ToString(varbin[1]) + Convert.ToString(varbin[2]));
                streamInput[i] = (byte)input;
                varbin = varbin.Remove(0, 3);
            }
            return (Bitmap)new ImageConverter().ConvertFrom(streamInput);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *png;";
            if (open.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = new Bitmap(open.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = File.ReadAllText(Directory.GetCurrentDirectory() + "/varbinoutput.txt");
            pictureBox1.Image = varbin2image(input);
        }
    }
}
