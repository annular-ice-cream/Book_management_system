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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Books_management_system
{
    public partial class books : Form
    {
        public books()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ShuDuoDuoShopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        private void populate()
        {
            if (Con.State != ConnectionState.Open)
            {
                Con.Open();
            }
            //Con.Open();
            string query = "select * from BookTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void Filter()//用于查询时指定类别（书目类型）的筛选
        {
            Con.Open();
            string query = "select * from BookTb1 where BCat = '"+CatCbSearchCb.SelectedItem.ToString()+"' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = null;
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void label5_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            dashboard obj = new dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            users obj = new users();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();//当用户点击刷新按钮时，全部提取数据库中全部记录
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
        
        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//书籍信息表
        {//让选定行的书籍信息填入到上方的文本输入框和下拉列表框中
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BautTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            BCatCb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            BAddtB.Text = BookDGV.SelectedRows[0].Cells[6].Value.ToString();
            //如果没有选中，则key = 0；如果选中，则捕获数据记录第一列的书籍ID号 
            if (BTitleTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Reset()//将输入框和下拉列表中已有内容设置为空
        {
            BTitleTb.Text = "";
            BautTb.Text = "";
            BCatCb.SelectedIndex = -1;
            QtyTb.Text = "";
            PriceTb.Text = "";
            BAddtB.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)//重置按钮代码区
        {
            Reset();
            CatCbSearchCb.SelectedIndex = -1;//重置后将过滤列表框中的选定类型也清除
        }

        private void button3_Click(object sender, EventArgs e)
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
                    string query = "delete from BookTb1 where BId = "+ key +"";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息删除成功！");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BautTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BCatCb.SelectedIndex == -1|| BAddtB.Text == "")
            {
                MessageBox.Show("信息缺失！！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update BookTb1 set BTitle='"+BTitleTb.Text+"',BAuthor='"+BautTb.Text+"',BCat='"+BCatCb.SelectedItem.ToString()+"',BQty="+QtyTb.Text+",BPrice="+PriceTb.Text+",BAdd='"+BAddtB.Text+"' where BId = "+key+" ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息更新成功！");
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(BTitleTb.Text == ""||BautTb.Text == ""||QtyTb.Text == ""||PriceTb.Text == ""||BCatCb.SelectedIndex ==-1||BAddtB.Text=="")
            {
                MessageBox.Show("信息缺失！！！");
            }
            else
            {
                try 
                {
                    Con.Open();
                    string query = "insert into BookTb1 values('"+BTitleTb.Text+"','"+BautTb.Text+"','"+BCatCb.SelectedItem.ToString()+"',"+QtyTb.Text+","+PriceTb.Text+",'"+BAddtB.Text+"')";
                    SqlCommand cmd = new SqlCommand(query,Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息保存成功！");
                    Con.Close();
                    populate();//书籍保存成功后刷新下方的书籍列表
                    Reset();//从而显示全部的书籍记录
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void CatCbSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            Con.Open();
            string query = "select * from BookTb1 where BTitle like  '" + BTitleTb.Text.Trim() + "' or BAuthor like '" + BautTb.Text.Trim() + "' or BAdd like '"+ BAddtB.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void BAddtB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
