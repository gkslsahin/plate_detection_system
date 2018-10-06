using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bitirme_v1
{
    public partial class Form2 : Form
    {


        Image[] araba_resmi = new Image[2];
        Image[] plaka_resmi = new Image[2];
        int sayac = 1;
        public Form2()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("plaka", 300);
            listView1.Columns.Add("tarih", 300);
        /*
            string[] bilgiler = { "34KOD56", "30.03.2018 14:57" };

            listView1.Items.Add(new ListViewItem(bilgiler));

            string[] bilgiler1 = { "26KV1961", "30.03.2018 15:57" };

            listView1.Items.Add(new ListViewItem(bilgiler1));

            string[] bilgiler2 = { "61TS1661", "30.03.2018 16:59" };

            listView1.Items.Add(new ListViewItem(bilgiler2));

            string[] bilgiler3 = { "26GA841", "03.13.2018 17:02" };

            listView1.Items.Add(new ListViewItem(bilgiler3));
            */
            comboBox1.Items.Add("kamera 1");
            comboBox1.Items.Add("kamera 2");
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            veritabanıclass veribag = new veritabanıclass();
            veribag.f2 = this;

            SqlDataReader dr = veribag.sorgucalıs();

            byte[] img = null;
           
            while (dr.Read())
            {
               // listView1.Items.Add(dr["Araba.plaka"].ToString(),System.DateTime.Now.ToString());

                string[] bilgiler = { dr["plaka"].ToString(), System.DateTime.Now.ToString() };

                listView1.Items.Add(new ListViewItem(bilgiler));


                // BinaryReader br = new BinaryReader((Image) dr["araba_resmi"]);

               img=(byte[]) dr["araba_resmi"];
                using (MemoryStream rstream = new MemoryStream(img))

                    araba_resmi[sayac] = (Image)Image.FromStream(rstream);
                Array.Resize(ref araba_resmi, araba_resmi.Length + 1);
              //  plaka_resmi[0] = (Image)dr["plaka_resmi"];
              //  Array.Resize(ref plaka_resmi, plaka_resmi.Length + 1);
                sayac++;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

                pictureBox1.Image=araba_resmi[listView1.SelectedItems.Count];
                

        }
    }

}