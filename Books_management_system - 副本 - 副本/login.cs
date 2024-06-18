using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Books_management_system
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ShuDuoDuoShopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static string UserName = "";
        private void button1_Click(object sender, EventArgs e)//登录按钮
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTb1 where UName='"+UNameTb.Text+"' and UPassword = '"+UPassTb.Text+"'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")//该用户是系统内的用户
            {
                UserName = UNameTb.Text;
                Billing obj = new Billing();//进入订单结算页
                obj.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
            }
            Con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }

        private void logon_Click(object sender, EventArgs e)
        {
            register obj = new register();
            obj.Show();
            this.Hide();
        }

        private void UNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
