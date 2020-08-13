using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            IEnumerable<int> aaa = YieldDemo();
            List<int> bbb = aaa.ToList();
           //// bbb.ForEach(s => Console.WriteLine(s));
            foreach (var item in bbb)
            {

              //  MessageBox.Show(item.ToString(), "提示");

            }

        }

        public IEnumerable<int> YieldDemo()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int counter = 0;
            int result = 1;
            while (counter++ < 10)
            {
                result = result * 2;
                yield return result;
            }
            while (sw.IsRunning)
            {
                Console.WriteLine(sw.ElapsedMilliseconds);
                if (sw.ElapsedMilliseconds > 5000)
                {
                    sw.Stop();
                }
            }
            
            Console.WriteLine(sw.ElapsedMilliseconds);
        }


        public void aaa()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));
            for (int i = 0; i < 3; i++)
            {
                DataRow row = dt.NewRow();
                row["id"] = "id" + i;
                row["name"] = "name" + i;
                dt.Rows.Add(row);
            }
          
          
           
        }
    }
}
