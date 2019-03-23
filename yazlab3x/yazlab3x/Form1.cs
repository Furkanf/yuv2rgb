using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace yazlab3x
{

    public partial class Form1 : Form
    {
        public static byte[] array;
        public static int width= 720, height= 576, format = 0;
        public static Form2 f2 = new Form2();
        public Form1()
        {
            InitializeComponent();
        }

        private void play_buton_Click(object sender, EventArgs e)
        {   
            

            if(radio444.Checked == true)
            {
                format = 0;
            }else if (radio422.Checked == true)
            {
                format = 1;
            }else if(radio420.Checked == true)
            {
                format = 2;
            }

            if (width_text.Text.Equals("") || height_text.Text.Equals(""))
            {
                path_name.Text = "w ve h değerlerini kontrol ediniz";
            }
            else
            {

                width = Convert.ToInt32(width_text.Text);
                height = Convert.ToInt32(height_text.Text);
                path_name.Text = "";
                path_name.Text = width + "x" + height + " boyutunda dosya açılıyor";

                

                if (format == 0)
                {
                    int frame = array.Length / (width * height * 3);
                    for (int i = 0; i < frame;  i++)
                    {

                        f2.Width = width;
                        f2.Height = height;
                        yuv444(i);
                        Debug.WriteLine((i + 1) + ". kare oynatılıyor.  ");
                    }
                    
                }else if (format == 1)
                {

                    int frame = array.Length / (width * height * 2);
                    for (int i = 0; i < frame; i++)
                    {
                        f2.Width = width;
                        f2.Height = height;
                        yuv422(i);
                        Debug.WriteLine((i+1) + ". kare oynatılıyor.  ");
                    }

                }

                else if (format == 2)
                {

                    int frame = array.Length / (width * height * 3/2);
                    for (int i = 0; i < frame; i++)
                    {
                        f2.Width = width;
                        f2.Height = height;
                        yuv420(i);

                        Debug.WriteLine(i + ". kare oynatılıyor");
                    }
             }

            }

        }

        public void yuv420(int sira)
        {
            Bitmap b = new Bitmap(width, height);
            Bitmap c = new Bitmap(width, height);
            int y, u=0, v=0;
            y = sira * b.Width * b.Height * 3 / 2;

            for (int i = 0; i < b.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    int r1, g1, b1;

                    if (y % 2 == 0)
                    {
                        if(i % 2 ==0)
                        u = ((y % b.Width) / 2) + (i * b.Width / 4) + b.Width * b.Height;
                        v =  u + ((b.Width*b.Height) / 4); 
                        if(i%2==1)
                        u = ((y % b.Width) / 2) + ((i-1) * b.Width / 4) + b.Width * b.Height;
                        v = u + (b.Width * b.Height / 4); 
                    }
                    else if (y % 2 == 1)
                    {
                        int tmp = y-1;

                        if (i % 2 == 0)
                        u = ((tmp % b.Width) / 2) + (i * b.Width / 4) + b.Width * b.Height;
                        v = u + (b.Width * b.Height / 4);

                        if (i % 2 == 1)
                        u = ((tmp % b.Width) / 2) + ((i - 1) * b.Width / 4) + b.Width * b.Height;
                        v = u + (b.Width * b.Height / 4);

                    }

                    y++;

                    int yx = array[y];
                    int ux = array[u];
                    int vx = array[v];

                    r1 = (int)(yx + 1.4065 * (vx - 128));
                    g1 = (int)(yx - 0.3455 * (ux - 128) - 0.581 * (vx - 128));
                    b1 = (int)(yx + 2.032 * (ux - 128));
         
                    if (r1 > 255) r1 = 255;
                    if (r1 < 0) r1 = 0;
               
                    if (g1 > 255) g1 = 255;
                    if (g1 < 0) g1 = 0;
          
                    if (b1 > 255) b1 = 255;
                    if (b1 < 0) b1 = 0;

                    b.SetPixel(j, i, Color.FromArgb(r1, g1, b1));
                    c.SetPixel(j, i, Color.FromArgb(yx, yx, yx));
                }
            }

            f2.picBox.Height = height;
            f2.picBox.Width = width;

            f2.picBox.Image = b;
            f2.picBox.Refresh();

            if (save_check.Checked == true)
            {
                c.Save(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmpgray\frame" + (sira + 1) + ".bmp", ImageFormat.Bmp);
                f2.picBox.Image.Save(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmp\frame" + (sira + 1) + ".bmp", ImageFormat.Bmp);
            }

            if (sira == 0)
            {
                f2.Show();
            }
        }


        public void yuv444(int sira)
        {
            Bitmap b = new Bitmap(width, height);
            Bitmap c = new Bitmap(width, height);

            for (int i = 0; i < b.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    int y = i * b.Width + j + (sira*width*height*3);
                    int u = y + (b.Width * b.Height);
                    int v = y + 2 * (b.Width * b.Height);

                    int r1, g1, b1;

                    int yx = array[y];
                    int ux = array[u];
                    int vx = array[v];


                    r1 = (int)(yx + 1.4065 * (vx - 128));
                    g1 = (int)(yx - 0.3455 * (ux - 128) - 0.581 * (vx - 128));
                    b1 = (int)(yx + 2.032 * (ux - 128));

                    if (r1 > 255) r1 = 255;
                    if (r1 < 0) r1 = 0;

                    if (g1 > 255) g1 = 255;
                    if (g1 < 0) g1 = 0;

                    if (b1 > 255) b1 = 255;
                    if (b1 < 0) b1 = 0;

                    b.SetPixel(j, i, Color.FromArgb(r1, g1, b1));

                    c.SetPixel(j, i, Color.FromArgb(yx, yx, yx));

                }

            }

            f2.picBox.Height = height;
            f2.picBox.Width = width;

            f2.picBox.Image = b;
            f2.picBox.Refresh();

            

            if (save_check.Checked == true)
            {
                f2.picBox.Image.Save(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmp\frame" + (sira + 1) + ".bmp", ImageFormat.Bmp);
                c.Save(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmpgray\frame" + (sira + 1) + ".bmp", ImageFormat.Bmp);
            }

            if (sira == 0)
            {
                f2.Show();
            }
        }

        public void yuv422(int sira)
        {
            Bitmap b = new Bitmap(width, height);
            Bitmap c = new Bitmap(width, height);

            int y = sira * b.Width * b.Height * 2;
            int u = y + (b.Width * b.Height);
            int v = u + (b.Width * b.Height / 2);

            for (int i = 0; i < b.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {

                    int r1, g1, b1;

                    int yx = array[y];
                    int ux = array[u];
                    int vx = array[v];


                    r1 = (int)(yx + 1.4065 * (vx - 128));
                    g1 = (int)(yx - 0.3455 * (ux - 128) - 0.581 * (vx - 128));
                    b1 = (int)(yx + 2.032 * (ux - 128));

                    if (r1 > 255) r1 = 255;
                    if (r1 < 0) r1 = 0;

                    if (g1 > 255) g1 = 255;
                    if (g1 < 0) g1 = 0;

                    if (b1 > 255) b1 = 255;
                    if (b1 < 0) b1 = 0;

                    b.SetPixel(j, i, Color.FromArgb(r1, g1, b1));
                    c.SetPixel(j, i, Color.FromArgb(yx, yx, yx));

                    if (y % 2 == 1)
                    {
                        u++;
                        v++;
                    }
                    y++;
                }

            }

            f2.picBox.Height = height;
            f2.picBox.Width = width;

            f2.picBox.Image = b;
            f2.picBox.Refresh();

            if (save_check.Checked == true)
            {
                c.Save(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmpgray\frame" + (sira + 1) + ".bmp", ImageFormat.Bmp);
                f2.picBox.Image.Save(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmp\frame" + (sira + 1) + ".bmp", ImageFormat.Bmp);
            }

            

            if (sira == 0)
            {
                f2.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "yuv dosyası | *.yuv";

            if (ofd.ShowDialog() == DialogResult.OK) 
            {
                string filePath = ofd.FileName;
                array = File.ReadAllBytes(filePath);

                path_name.Text = Path.GetFileName(filePath) + " dosyası seçildi";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
