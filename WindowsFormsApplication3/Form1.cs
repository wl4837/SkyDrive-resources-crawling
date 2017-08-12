using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }
        void zhidao()//百度知道爬虫
        {
            int jmp = 1;
            int ax = 0;
            int Lan = 0;
            for (int i = 0; i < 3; i++)
            {
                WebClient wc = new WebClient();
                string html = wc.DownloadString(string.Format("https://zhidao.baidu.com/search?word={0}&ie=gbk&site=-1&sites=0&date=2&pn={0}0", textBox1.Text,i));
                //测试
                //MessageBox.Show(html);

                MatchCollection matches = Regex.Matches(html, "http[s]?://zhidao.baidu.com/question/([\\s\\S]*?).html");
               
                progressBar1.Maximum += matches.Count * 10;
                foreach (Match item in matches)
                {
                    ax++;
                    if (jmp >= 1)
                    {
                        jmp = 0;
                        Lan += baiduyun(item.Value.ToString(), "百度知道");
                    }
                    else
                    {
                        jmp++;
                    }
                    progressBar1.Value += 10;
                }
            }
            richTextBox1.Text += string.Format("共抓取百度知道"+ax+"个父地址\n共提取到"+Lan+"个百度云地址\n");
          
        }

        //正则表达式筛选模块
       string help(MatchCollection add) {
             string url = "";
            foreach (Match vim in add)
            {
                url = vim.Value.ToString();

            }
             return url;
        }
       
        //百度云爬取模块
       int baiduyun(string item,string type){
           int L = 0;
             WebClient wc = new WebClient();
             string html = wc.DownloadString(item);
             MatchCollection url = Regex.Matches(html, "http([s]{0,1})://pan.baidu.com/[a-zA-Z0-9]{1,3}/([\\s\\S\\n]{1,25})");
             foreach (Match python in url)
             {
                 MatchCollection pw = Regex.Matches(python.Value.ToString(), "([密码]{1,3})([:：]{1,1})([a-zA-z0-9]{4,4})");
                 string password = help(pw);
                 MatchCollection ul = Regex.Matches(python.Value.ToString(), "http([s]{0,1})://pan.baidu.com/[a-zA-Z0-9]{1,3}/([a-zA-Z0-9]{1,10})");
                 string urladd = help(ul);
                 ListViewItem items = new ListViewItem(type);
                 items.SubItems.Add(item);
                 items.SubItems.Add(urladd);
                 items.SubItems.Add(password);
                 listView1.Items.Add(items);
                 L++;
             }
             return L;
         }
        void tieba() //百度贴吧爬虫    
        {
            int jmp = 1;
            int ax = 0;
            int Lan = 0;
            for (int i = 0; i < 3; i++)
            {
                WebClient wc = new WebClient();
                Encoding GBK = Encoding.GetEncoding("GBK");
                string html = wc.DownloadString(string.Format("http://tieba.baidu.com/f/search/res?ie=utf-8&isnew=1&kw=&qw={0}&rn=10&un=&only_thread=0&sm=1&sd=&ed=&pn={1}", textBox1.Text,i));
                Encoding.GetEncoding("GBK").GetString(Encoding.Default.GetBytes(html));
                MatchCollection matches = Regex.Matches(html, "/p/([0-9]+)\\?pid=([0-9]+)&cid=([0-9#]+)");
                
                progressBar1.Maximum += matches.Count * 10;
                foreach (Match item in matches)
                {
                    ax++;
                    if (jmp >= 1)
                    {
                        jmp = 0;
                        //richTextBox1.Text += string.Format("https://tieba.baidu.com" + item.Value.ToString()+"\n");   //测试语句
                        Lan += baiduyun(string.Format("https://tieba.baidu.com" + item.Value.ToString()), "百度贴吧");
                    }
                    else
                    {
                        jmp++;
                    }
                    progressBar1.Value += 10;
                }
                
            }
            richTextBox1.Text += string.Format("共抓取百度贴吧" + ax + "个父地址-共提取到" + Lan + "个百度云地址\n");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 0;
            listView1.Items.Clear();//清空上次查询
            richTextBox1.Text = null;
            //zhidao();
            Thread tiebaThread = new Thread(new ThreadStart(tieba));
            tiebaThread.Start();
            Thread zhidaoThread = new Thread(new ThreadStart(zhidao));
            zhidaoThread.Start();
            //while (conut>=2)
            //{
            //    progressBar1.Maximum = id;
            //    richTextBox1.Text += id.ToString();
            //}
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {
                System.Diagnostics.Process.Start(listView1.SelectedItems[0].SubItems[2].Text.ToString());
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_AutoSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
          
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }

        private void 父级地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                System.Diagnostics.Process.Start(listView1.SelectedItems[0].SubItems[1].Text.ToString());

            }
        }

        private void 资源地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                System.Diagnostics.Process.Start(listView1.SelectedItems[0].SubItems[2].Text.ToString());

            }
        }

        private void 密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Clipboard.SetDataObject(listView1.SelectedItems[0].SubItems[3].Text.ToString());
            }
        }
    }
}
