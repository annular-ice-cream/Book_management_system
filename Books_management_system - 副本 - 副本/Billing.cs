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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ShuDuoDuoShopDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        String data = System.DateTime.Now.ToString("d");
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
        private void BautTb_TextChanged(object sender, EventArgs e)
        {

        }
        int key = 0,stock = 0;//stock存储数的库存数
        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLbl.Text = login.UserName;
        }
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //让选定行的书籍信息填入到最上方的文本输入框和下拉列表框中
            BTitleTb2.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BautTb2.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            BCatCb2.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            QtyTb2.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb2.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            BAddtextBox.Text = BookDGV.SelectedRows[0].Cells[6].Value.ToString();
            //如果没有选中，则key = 0；如果选中，则捕获数据记录第一列的书籍ID号 
            if (BTitleTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
            //让选定行的书籍信息填入到上方的文本输入框和下拉列表框中
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            //BautTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();//注释掉不需要显示的信息
            //BCatCb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            QtyTb.Text = ""; //数量默认显示为空//BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            //如果没有选中，则key = 0；如果选中，则捕获数据记录第一列的书籍ID号 
            if (BTitleTb.Text == "")
            {
                key = 0;
                stock = 0 ;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Reset1()//清空显示当前点击栏的 信息
        {
            BTitleTb.Text = ""; 
            QtyTb.Text = "";
            PriceTb.Text = "";
           
        }
        private void Reset2()//清空显示当前点击栏的 信息
        {
            BTitleTb2.Text = "";
            BautTb2.Text = "";
            BCatCb2.SelectedIndex = -1;
            QtyTb2.Text = "";
            PriceTb2.Text = "";
            BAddtextBox.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset1();
        }
        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);//新库存量=原库存量-订购量
            try
            {
                Con.Open();
                string query = "update BookTb1 set BQty=" + newQty + "  where BId = " + key + " ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("成功加入购物车！");
                Con.Close();
                populate();//书籍删除成功后刷新下方的书籍列表
                Reset1();//清空 上方显示当前点击栏的 信息
                Reset2();
                key = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int n = 0, GrdTotal = 0;//n存储订单编号，GrdTotal存储订单总金额

        private void label10_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BTitleTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset1();
            Reset2();
        }

        private void Filter()//用于查询时指定类别（书目类型）的筛选
        {
            Con.Open();
            string query = "select * from BookTb1 where BCat = '" + BCatCb2.SelectedItem.ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = null;
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Con.Open();
            string query ="select * from BookTb1 where BTitle like  '"+BTitleTb2.Text.Trim()+"' or BAuthor like '"+ BautTb2.Text.Trim() + "'or BAdd like '"+ BAddtextBox.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();//当用户点击刷新按钮时，全部提取数据库中全部记录
        }

        private void BCatCb2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void BCatCb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void BAddtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Feedback fb = new Feedback();
            fb.Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     
        //int nid = 0;//订单编号（忘记给订单编号设置标志种子自增了，所以需要手动写变量进行自增）
    
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            
            //结算不光是要打印，而且要保存在数据库的Billtb1表中
            //如果购物车中没有商品，点击结算提示“您还没有挑选图书”，如果有就结算
            if (BillDGV.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("您还没有挑选图书");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Billtb1 values('"+UserNameLbl.Text+"',"+GrdTotal+",'"+data+"')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("订单信息保存成功！");
                    Con.Close();
                    //populate();//书籍保存成功后刷新下方的书籍列表
                    //Reset();//从而显示全部的书籍记录
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 350, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                //nid++;
            }
        }
   

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("书多多书店",new Font("幼圆",12,FontStyle.Bold),Brushes.Red,new Point(80));
            e.Graphics.DrawString("编号  产品        价格      数量     总计", new Font("幼圆", 10, FontStyle.Bold), Brushes.Red, new Point(26,40));
            foreach(DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column7"].Value);
                prodname = "" + row.Cells["Column8"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column9"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column10"].Value);
                tottal = Convert.ToInt32(row.Cells["Column11"].Value);
                e.Graphics.DrawString(""+prodid, new Font("幼圆", 8, FontStyle.Bold), Brushes.Red, new Point(26,pos));
                e.Graphics.DrawString("" + prodname, new Font("幼圆", 8, FontStyle.Bold), Brushes.Red, new Point(50, pos));
                e.Graphics.DrawString("" + prodprice, new Font("幼圆", 8, FontStyle.Bold), Brushes.Red, new Point(165, pos));
                e.Graphics.DrawString("" + prodqty, new Font("幼圆", 8, FontStyle.Bold), Brushes.Red, new Point(238, pos));
                e.Graphics.DrawString("" + tottal, new Font("幼圆", 8, FontStyle.Bold), Brushes.Red, new Point(298, pos));
                pos = pos + 20;

            }
            e.Graphics.DrawString("订单总金额：￥" + GrdTotal, new Font("幼圆", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos+50));
            e.Graphics.DrawString("订单时间        " + data, new Font("幼圆", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 80));
            e.Graphics.DrawString("**********书多多书店**********", new Font("幼圆", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 97));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
        }

        


        private void AddtoBillBtn_Click(object sender, EventArgs e)
        {
            
            if(QtyTb.Text == ""||Convert.ToInt32(QtyTb.Text)>stock)
            {
                MessageBox.Show("库存不足！！");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text)*Convert.ToInt32(PriceTb.Text);//计算金额
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;//订单编号+1
                UpdateBook();//更新库存量函数
                GrdTotal = GrdTotal + total;//计算总金额
                TotalLbl.Text = "￥"+ GrdTotal;
            }
        }
    }
}
