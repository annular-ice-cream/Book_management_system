using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books_management_system
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ShuDuoDuoShopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            books obj = new books();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            users obj = new users();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
          
        }

        private void dashboard_Load(object sender, EventArgs e)//显示总书数量、总用户数、总金额数
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(BQty) from BookTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookstockLbl.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(Amount) from Billtb1", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            AmountLbl.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from UserTb1", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            UsertotalLbl.Text = dt2.Rows[0][0].ToString();

            Con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AmountLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
