﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books_management_system
{
    public partial class Feedback : Form
    {
        public Feedback()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ShuDuoDuoShopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
        private void AddTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)//取消
        {
            Billing obj = new Billing();//进入订单结算页
            obj.Show();
            this.Close();
        }

        private void txtFeedBack_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)//保存
        {
            if (txtFeedBack.Text != "")
            {
                try
                {
                    Con.Open();
                    string query = "insert into FeedBackTb1 values("+txtFeedBack.Text+"','"+dateStr+"')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("反馈信息保存成功！");
                    Con.Close();
                    Billing obj = new Billing();//进入订单结算页
                    obj.Show();
                    this.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Billing obj = new Billing();//进入订单结算页
            obj.Show();
            this.Close();
        }
    }
}
