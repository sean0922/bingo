using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 賓果12._1_1_
{
    public partial class Form1 : Form
    {
        bool whowin = true;
        bool clean_trash = false;//判斷是否需要清除動態元件
        int loca = 0;//button座標
        Random rd = new Random();
        int[] tmp = new int[25];
        Button[,] btn1 = new Button[5, 5];//玩家button
        Button[,] btn2 = new Button[5, 5];//電腦button
        public Form1()
        {
            InitializeComponent();
        }
        void a()//智慧電腦
        {
            bool stopp = false;//找到後停止
            string txt = "";//傳遞數字
            int[] c = { 0, 0, 0, 0, 0 };//聰明的電腦cul major-
            int[] r = { 0, 0, 0, 0, 0 };//聰明的電腦row major|
            int lef=0;//聰明的電腦左斜 major\
            int right = 0;//聰明的電腦右斜 major/
            int tmpc=0, tmpr=0;//比大小
            int placec = 0, placer = 0, tmpplace = 0; ;//位置的值
            for (int i=0;i<25;i++)
            {
                if (btn2[i/5, i%5].Text == "click") { r[i / 5] += 1; }//直的
                if (btn2[i % 5, i /5].Text == "click") { c[i / 5] += 1; }//橫的
                if ((btn2[i / 5, i % 5].Text == "click") && (i == 0 || i == 6 || i == 12 || i == 18 || i == 24))
                {
                    lef += 1;
                }
                if ((btn2[i / 5, i % 5].Text == "click") && (i == 4 || i == 8 || i == 12 || i == 16 || i == 20))
                {
                    right += 1;
                    //label5.Text = right.ToString();
                }
            }
            for(int i=0;i<5;i++)
            {
                if(c[i]>tmpc&&c[i]<5)
                {
                    tmpc = c[i];
                    placec = i;
                }
                if (r[i] > tmpr&&r[i]<5)
                {
                    tmpr = r[i];
                    placer = i;
                }
            }
            label5.Text = "c"+tmpc.ToString() +"p"+placec.ToString()+ "\r\n" + "r"+tmpr.ToString() + "p" + placer.ToString() + "\r\n"+"lef"+lef.ToString() + "\r\n" + "right"+right.ToString();
            if (tmpc > tmpr && tmpc > lef && tmpc > right && lef != 5 && right != 5) { tmpplace = 0; }
            if (tmpr > tmpc && tmpr > lef && tmpr > right && lef != 5 && right != 5) { tmpplace = 1; }
            if (lef > tmpc && lef > tmpr && lef > right && lef != 5 && right != 5) { tmpplace = 2; }
            if (right > tmpr && right > lef && right > tmpc && lef != 5 && right != 5) { tmpplace = 3; }
            for(int i=0;i<5; i++)
            {
                if (tmpplace == 0&&btn2[i, placec].Text!="click"&&stopp==false)
                {
                    txt = btn2[i, placec].Text;
                    aa(txt);
                    btn2[i, placec].Text = "click";
                    btn2[i, placec].BackColor = Color.Blue;
                    stopp = true;
                }
                if (tmpplace == 1 && btn2[placer, i].Text != "click" && stopp == false)
                {
                    txt = btn2[placer, i].Text;
                    aa(txt);
                    btn2[placer, i].Text = "click";
                    btn2[placer, i].BackColor = Color.Blue;
                    stopp = true;
                }
                if(tmpplace==2&&btn2[i,i].Text!="click" && stopp == false)
                {
                    txt = btn2[i, i].Text;
                    aa(txt);
                    btn2[i, i].Text = "click";
                    btn2[i, i].BackColor = Color.Blue;
                    stopp = true;
                }
                if (tmpplace == 3 && btn2[i, 4-i].Text != "click" && stopp == false)
                {
                    txt = btn2[i, 4 - i].Text;
                    aa(txt);
                    btn2[i, 4-i].Text = "click";
                    btn2[i, 4-i].BackColor = Color.Blue;
                    stopp = true;
                }
            }
            label6.Text = tmpplace.ToString();
        }
        void aa(string tt)
        {
            for (int i = 0; i < 25; i++)
            {
                if (btn1[i / 5, i % 5].Text ==tt)
                {
                    btn1[i / 5, i % 5].Text = "click";
                    btn1[i / 5, i % 5].BackColor = Color.Blue;
                    btn1[i / 5, i % 5].Enabled = false;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            whowin = true;
            button1.Enabled = false;
            label3.Text = "0";
            label4.Text = "結果";
            removeplace(btn1);
            removeplace(btn2);
            loca = 50;
            setplace(btn1, loca);
            loca = 400;
            setplace(btn2, loca);
            for (int i = 0; i < 25; i++)
            {
                btn1[i / 5, i % 5].Enabled = true;
                btn2[i / 5, i % 5].Enabled = false;
            }
        }
        void Buttons_Click(object sender, EventArgs e)//動態按鈕判斷
        {
            for (int i = 0; i < 25; i++)
            {
                if (btn2[i / 5, i % 5].Text == ((Button)sender).Text)
                {
                    btn2[i / 5, i % 5].Text = "click";
                    btn2[i / 5, i % 5].BackColor = Color.Green;
                    btn2[i / 5, i % 5].Enabled = false;
                    //label5.Text = (i / 5).ToString() + "," + (i % 5).ToString();//顯示座標
                }
            }
           ((Button)sender).BackColor = Color.Green;
            ((Button)sender).Enabled = false;
            ((Button)sender).Text = "click";
            timer1.Enabled = true;
        }
        void check_line()//判斷是否連線
        {
            int player_totalline = 0;//判斷玩家連線
            int computer_totalline = 0;//判斷電腦連線
            for (int i = 0; i < 5; i++)//判斷
            {
                if ((btn1[i, 0].Text == "click") && (btn1[i, 1].Text == "click") && (btn1[i, 2].Text == "click") && (btn1[i, 3].Text == "click") && (btn1[i, 4].Text == "click"))
                {
                    btn1[i, 0].BackColor = Color.Red;
                    btn1[i, 1].BackColor = Color.Red;
                    btn1[i, 2].BackColor = Color.Red;
                    btn1[i, 3].BackColor = Color.Red;
                    btn1[i, 4].BackColor = Color.Red;
                    player_totalline += 1;
                    label3.Text = player_totalline.ToString();
                }
                if ((btn2[i, 0].Text == "click") && (btn2[i, 1].Text == "click") && (btn2[i, 2].Text == "click") && (btn2[i, 3].Text == "click") && (btn2[i, 4].Text == "click"))
                {
                    btn2[i, 0].BackColor = Color.Red;
                    btn2[i, 1].BackColor = Color.Red;
                    btn2[i, 2].BackColor = Color.Red;
                    btn2[i, 3].BackColor = Color.Red;
                    btn2[i, 4].BackColor = Color.Red;
                    computer_totalline += 1;
                    label3.Text = computer_totalline.ToString();
                }
                if ((btn1[0, i].Text == "click") && (btn1[1, i].Text == "click") && (btn1[2, i].Text == "click") && (btn1[3, i].Text == "click") && (btn1[4, i].Text == "click"))
                {
                    btn1[0, i].BackColor = Color.Red;
                    btn1[1, i].BackColor = Color.Red;
                    btn1[2, i].BackColor = Color.Red;
                    btn1[3, i].BackColor = Color.Red;
                    btn1[4, i].BackColor = Color.Red;
                    player_totalline += 1;
                    label3.Text = player_totalline.ToString();
                }
                if ((btn2[0, i].Text == "click") && (btn2[1, i].Text == "click") && (btn2[2, i].Text == "click") && (btn2[3, i].Text == "click") && (btn2[4, i].Text == "click"))
                {
                    btn2[0, i].BackColor = Color.Red;
                    btn2[1, i].BackColor = Color.Red;
                    btn2[2, i].BackColor = Color.Red;
                    btn2[3, i].BackColor = Color.Red;
                    btn2[4, i].BackColor = Color.Red;
                    computer_totalline += 1;
                    label3.Text = computer_totalline.ToString();
                }
            }
            if ((btn1[0, 0].Text == "click") && (btn1[1, 1].Text == "click") && (btn1[2, 2].Text == "click") && (btn1[3, 3].Text == "click") && (btn1[4, 4].Text == "click"))
            {
                btn1[0, 0].BackColor = Color.Red;
                btn1[1, 1].BackColor = Color.Red;
                btn1[2, 2].BackColor = Color.Red;
                btn1[3, 3].BackColor = Color.Red;
                btn1[4, 4].BackColor = Color.Red;
                player_totalline += 1;
                label3.Text = player_totalline.ToString();
            }
            if ((btn2[0, 0].Text == "click") && (btn2[1, 1].Text == "click") && (btn2[2, 2].Text == "click") && (btn2[3, 3].Text == "click") && (btn2[4, 4].Text == "click"))
            {
                btn2[0, 0].BackColor = Color.Red;
                btn2[1, 1].BackColor = Color.Red;
                btn2[2, 2].BackColor = Color.Red;
                btn2[3, 3].BackColor = Color.Red;
                btn2[4, 4].BackColor = Color.Red;
                computer_totalline += 1;
                label3.Text = computer_totalline.ToString();
            }
            if ((btn1[0, 4].Text == "click") && (btn1[1, 3].Text == "click") && (btn1[2, 2].Text == "click") && (btn1[3, 1].Text == "click") && (btn1[4, 0].Text == "click"))
            {
                btn1[0, 4].BackColor = Color.Red;
                btn1[1, 3].BackColor = Color.Red;
                btn1[2, 2].BackColor = Color.Red;
                btn1[3, 1].BackColor = Color.Red;
                btn1[4, 0].BackColor = Color.Red;
                player_totalline += 1;
                label3.Text = player_totalline.ToString();
            }
            if ((btn2[0, 4].Text == "click") && (btn2[1, 3].Text == "click") && (btn2[2, 2].Text == "click") && (btn2[3, 1].Text == "click") && (btn2[4, 0].Text == "click"))
            {
                btn2[0, 4].BackColor = Color.Red;
                btn2[1, 3].BackColor = Color.Red;
                btn2[2, 2].BackColor = Color.Red;
                btn2[3, 1].BackColor = Color.Red;
                btn2[4, 0].BackColor = Color.Red;
                computer_totalline += 1;
                label3.Text = computer_totalline.ToString();
            }
            if (computer_totalline >= 3 && player_totalline >= 3 && whowin == true)
            {
                MessageBox.Show("平手");
                label4.Text = "平手";
                //label4.BackColor = Color.Blue;
                button1.Enabled = true;
                whowin = false;
                for (int i = 0; i < 25; i++)
                {
                    btn1[i / 5, i % 5].Enabled = false;
                }
            }
            if (player_totalline >= 3 && whowin == true)
            {
                MessageBox.Show("你贏了");
                label4.Text = "你贏了";
                //label4.BackColor = Color.Blue;
                button1.Enabled = true;
                whowin = false;
                for (int i = 0; i < 25; i++)
                {
                    btn1[i / 5, i % 5].Enabled = false;
                }
            }
            if (computer_totalline >= 3 && whowin == true)
            {
                MessageBox.Show("你輸了");
                label4.Text = "你輸了";
                //label4.BackColor = Color.Blue;
                button1.Enabled = true;
                whowin = false;
                for (int i = 0; i < 25; i++)
                {
                    btn1[i / 5, i % 5].Enabled = false;
                }
            }
        }
        void setplace(Button[,] btn, int loc)//設置動態陣列+亂數
        {
            for (int x = 0; x < btn.GetLongLength(0); x++)
            {
                for (int y = 0; y < btn.GetLongLength(0); y++)
                {
                    this.Controls.Remove(btn[x, y]);
                    btn[x, y] = new Button();//新增一個按鈕
                    btn[x, y].SetBounds(40 * x, 40 * y, 35, 34);
                    btn[x, y].Location = new Point((loc + 40 * x), (50 + 40 * y));
                    btn[x, y].Click += new EventHandler(Buttons_Click);
                    this.Controls.Add(btn[x, y]);
                    btn[x, y].Enabled = true;
                }
            }
            for (int i = 0; i < 25; i++)
            {
                tmp[i] = rd.Next(1, 26);
                for (int j = 0; j < i; j++)
                {
                    while (tmp[i] == tmp[j])
                    {
                        tmp[i] = rd.Next(1, 26);
                        j = 0;
                    }
                }
                btn[i / 5, i % 5].Text = tmp[i].ToString();
            }
            clean_trash = true;
        }
        void removeplace(Button[,] btn)//歸還記憶體
        {
            if (clean_trash == true)
            {
                for (int x = 0; x < btn.GetLongLength(0); x++)
                {
                    for (int y = 0; y < btn.GetLongLength(0); y++)
                    {
                        this.Controls.Remove(btn[x, y]);
                    }
                }
            }
            else
                return;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            check_line();
            if (whowin == true)
            {
                a();
                check_line();
            }
        }
    }
}
