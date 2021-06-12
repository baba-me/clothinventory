using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.Barcode;
using System.Data.SqlClient;
using System.IO;

namespace barcoder
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        BarcodeDecoder scanner;
        //BarcodeEncoder generator;
        //SaveFileDialog sd;
        //OpenFileDialog od;
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=momin;Integrated Security=True");
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            con.Open();
            string qur = " select  brc from brcode where brc ='" + t1.Text + "'";
            //SqlCommand cmd = new SqlCommand(qu, con);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    t2.Text = reader.GetString(0);
            //}
            //reader.Close();
            SqlDataAdapter sda = new SqlDataAdapter(qur, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "brcode");
            scanner = new BarcodeDecoder();
           
        }
    }
}
