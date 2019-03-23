using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazlab3x
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            FileInfo fileInfo;
            string uzanti = ".bmp";

            foreach (string dosya in Directory.GetFiles(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmpgray"))
            {
                fileInfo = new FileInfo(dosya);
                if (fileInfo.Extension == uzanti) 
                {
                    fileInfo.Delete();
                }
            }

            foreach (string dosya in Directory.GetFiles(@"C:\Users\furkn\Desktop\yazlab 1.donem\yazlab3x\bmp"))
            {
                fileInfo = new FileInfo(dosya);
                if (fileInfo.Extension == uzanti)
                {
                    fileInfo.Delete();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
