using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Digests;

namespace md4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[] data;
        byte[] data1;

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter =
                                "Txt files |*.txt";
            openFileDialog1.Title = "Текст для хеширования";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                data = File.ReadAllBytes(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            Digests.MD4Digest d = new Digests.MD4Digest();
           d.Reset();
           d.BlockUpdate(data, 0, data.Length);
            byte[] output =new byte [16];
            d.DoFinal(output,0);

            string tmp = "";
            
            tmp = tmp + BitConverter.ToString(output);
            MessageBox.Show(tmp);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter =
                                "Txt files |*.txt";
            openFileDialog1.Title = "Текст для хеширования";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                data1 = File.ReadAllBytes(openFileDialog1.FileName);
            }
            byte [] data2=new byte [data1.Length];
            data1.CopyTo(data2, 0);
            Digests.MD4Digest d1 = new Digests.MD4Digest();
            d1.Reset();
            d1.BlockUpdate(data1, 0, data1.Length);
            byte[] output1 = new byte [16];
            d1.DoFinal(output1, 0);

            int tmp = data2[0] + 1;
            data2[0] = (byte)tmp;
            Digests.MD4Digest d2 = new Digests.MD4Digest();
            d2.Reset();
            d2.BlockUpdate(data2, 0, data2.Length);
            byte[] output2 = new byte [16];
            d2.DoFinal(output2, 0);

            byte [] result= new byte [16];
            for (int i = 0; i < output1.Length; i++)
            {
                result[i] = (byte)(output1[i] ^ output2[i]);
            }

            List<int> code = new List<int>();

            foreach (var i in result)
            {
                string BinaryCode = Convert.ToString(i, 2);
                int res = Convert.ToInt32(BinaryCode);
                code.Add(res);
            }
            string codetxt = string.Join("", code.ToArray());
            Console.WriteLine("Двоичный код исходного текста" + "\n" + codetxt);

            int[] digits1 = codetxt.ToString().Select(c => (int)char.GetNumericValue(c)).ToArray();

            int sum=0;
            for (int i = 0; i < digits1.Length; i++)
            {
                if (digits1[i] == 1)
                    sum++;
            }

            MessageBox.Show("digits_Count= " + Convert.ToString(digits1.Length) + " " + "1_Count= " + Convert.ToString(sum));         

        }
    }
}
