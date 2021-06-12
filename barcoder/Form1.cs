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
using System.Drawing.Imaging;
using System.Data.SqlClient;
using IronBarCode;
using Microsoft.PointOfService;
using Microsoft.PointOfService.WMI;
//using MessagingToolkit.Barcode;




namespace barcoder
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        //BarcodeGenerator barcode = null;
       //BarcodeDecoder scanner;
       // BarcodeEncoder barcode =null;
        //SaveFileDialog sd;
        //OpenFileDialog od;
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=momin;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            string barcode = textBox1.Text;
            Bitmap bitmap = new Bitmap(barcode.Length*40 ,90);
            using (Graphics grap = Graphics.FromImage(bitmap))
            {
                Font ofont = new System.Drawing.Font("CODE3X", 60);
                PointF point = new PointF(2f, 2f);
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush white = new SolidBrush(Color.White);
                grap.FillRectangle(white, 0, 0, bitmap.Width, bitmap.Height);
                grap.DrawString("" + barcode + "", ofont, black, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                pictureBox1.Image = bitmap;
                pictureBox1.Height = bitmap.Height;
                pictureBox1.Width = bitmap.Width;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
          //  byte stream = 0;
            string qu ="insert into brcode values('"+textBox1.Text+"',@pic,'"+textBox2.Text+"','"+textBox3.Text+"')";
            SqlCommand cmd = new SqlCommand(qu, con);
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save( ms , System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic = ms.ToArray();
            cmd.Parameters.AddWithValue("@pic", pic);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //rpt_br rb = new rpt_br();
            //crystalReportViewer1.ReportSource = rb;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string qur = "select * from brcode where brc='"+t2.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(qur,con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "brcode");
            rpt_br rr = new rpt_br();
            rr.SetDataSource(ds);
            crystalReportViewer1.ReportSource = rr;
            crystalReportViewer1.Refresh();
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            b1.Focus();
	

             
            //Scanner_Activation_Form.KeyPreview = True;
            //BarcodeResult QRResult = BarcodeReader.QuicklyReadOneBarcode("QR.png");
            //// Work with the results
            //if (QRResult != null)
            //{
            //    string Value = QRResult.Value;
            //    Bitmap Img = QRResult.BarcodeImage;
            //    BarcodeEncoding BarcodeType = QRResult.BarcodeType;
            //    byte[] Binary = QRResult.BinaryValue;
            //    // Console.WriteLine(QRResult.Value);
            //    comboBox1.Text = QRResult.Value;
            //}
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

                con.Open();
                string qu = " select  brc,iname,price from brcode where brc ='"+textBox1.Text+"'";
                SqlCommand cmd = new SqlCommand(qu, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    b1.Text = reader.GetString(0);
                    b2.Text = reader.GetString(1);
                    b3.Text = reader.GetString(2);
                }
                reader.Close();
                con.Close();
           // }
           // MessageBox.Show("jjjj");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //con.Open();
            //string qu = " select  brc from brcode where brc ='" + textBox1.Text + "'";
            //SqlCommand cmd = new SqlCommand(qu, con);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    b1.Text = reader.GetString(0);
            //    b2.Text = reader.GetString(1);
            //    b3.Text = reader.GetString(2);
            //}
            //reader.Close();
            //con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
