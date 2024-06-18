using System;
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
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ShuDuoDuoShopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");

        private void populate()
        {
            Con.Open();
            string query = "select * from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void Reset()
        {
            UnameTb.Text = "";
            PhoneTb.Text = "";
            AddTb.Text = "";
            PassTb.Text = "";
        }


        private void users_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PhoneTb.Text == "" || AddTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("信息缺失！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into UserTb1 values('" + UnameTb.Text + "','" + PhoneTb.Text + "','" + AddTb.Text + "','" + PassTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息保存成功！");
                    Con.Close();
                    populate();//书籍保存成功后刷新下方的书籍列表
                    Reset();//从而显示全部的书籍记录
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        int key = 0;
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("信息缺失！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from UserTb1 where UId = " + key + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息删除成功！");
                    Con.Close();
                    populate();//书籍删除成功后刷新下方的书籍列表
                    Reset();//从而显示全部的书籍记录
                    key = 0;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //让选定行的书籍信息填入到上方的文本输入框和下拉列表框中
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            AddTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            PassTb.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();
            
            //如果没有选中，则key = 0；如果选中，则捕获数据记录第一列的书籍ID号 
            if (UnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PhoneTb.Text == "" || AddTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("信息缺失！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update UserTb1 set UName='" +UnameTb.Text + "',UPhone='" + PhoneTb.Text + "',UAdd='" + AddTb.Text + "',UPassword=" + PassTb.Text + "  where UId = " + key + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息更新成功！");
                    Con.Close();
                    populate();//书籍删除成功后刷新下方的书籍列表
                    Reset();//从而显示全部的书籍记录
                    key = 0;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
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
          
        }

        private void label9_Click(object sender, EventArgs e)
        {
            dashboard obj = new dashboard();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)//右上角关闭按钮
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            Con.Open();
            string query = "select * from UserTb1 where UName like  '" + UnameTb.Text.Trim() + "' or UPhone like '" + PhoneTb.Text.Trim() + "' or UAdd like '" + AddTb.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            populate();//当用户点击刷新按钮时，全部提取数据库中全部记录
        }
    }
}
