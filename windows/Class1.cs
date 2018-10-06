using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bitirme_v1
{
    public class veritabanıclass
    {
        public Form2 f2;

        SqlConnection baglanti;
        public string connectionstring = "Data Source=192.168.65.51\\DESKTOP-6A4L41G\\GKSL\\SQLEXPRESS;Initial Catalog=platedb;User Id=plate;Password=asdasd12";
            // "Data Source=tcp:192.168.137.221,1433;Network Library = DBMSSOCN; Initial Catalog = platedb; User ID =sa; Password=Password;";

        //"Data Source=192.168.65.51,1433;Network Library=DBMSSOCN;Initial Catalog=platedb;User ID=plaka;Password=asdasd12;";
        //Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;\SQLEXPRESS
        //  Initial Catalog = myDataBase; User ID = myUsername; Password=myPassword;\DESKTOP-6A4L41G\\GKSL\
        SqlCommand komut;
        public veritabanıclass() {
            /*   try
                {
                    baglanti = new SqlConnection(this.connectionstring);
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                }
                catch (Exception)
                {
                  //  MessageBox.Show("BAGLANTI OLUSTURULAMADI ! ");
                }

                komut = new SqlCommand("SqlCommand komut", baglanti);
                komut.Parameters.AddWithValue("@tarih", SqlDbType.SmallDateTime).Value = System.DateTime.Now;
                komut.CommandType = CommandType.StoredProcedure;



                komut.ExecuteNonQuery();
                SqlDataReader dr = komut.ExecuteReader();


               // while (dr.Read())
                {
                  /* çalışanseçe.Items.Add(dr["Adi"].ToString() + "  " + (dr["Soyadi"].ToString()));
                    falcıseç.Items.Add(dr["Adi"].ToString() + "  " + (dr["Soyadi"].ToString()));
                    çalışanseçç.Items.Add(dr["Adi"].ToString().Replace(" ", "") + "  " + (dr["Soyadi"].ToString()).Replace(" ", ""));
                    
                    

            }
   */


        }

        public SqlDataReader sorgucalıs()
        {

            try
            {
                baglanti = new SqlConnection("Data Source=192.168.137.177,1433;Initial Catalog=platedb;User Id=plaka;Password=asdasd12;");
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
            }
            catch (Exception e)
            {
                string bos = e.Message;
                MessageBox.Show( bos+"fgfdg");

            }
            
                        komut = new SqlCommand("arabagetir", baglanti);
                        komut.Parameters.AddWithValue("@tarih", SqlDbType.SmallDateTime).Value = "2018-05-10 16:34:00";
                        komut.CommandType = CommandType.StoredProcedure;



                        komut.ExecuteNonQuery();
                        SqlDataReader dr = komut.ExecuteReader();
                        return dr;
            

            //   String resimPath= @"C:\Users\Toshiba\Pictures\plate1.png";
            /*
                  FileStream fs = new FileStream(resimPath2, FileMode.Open, FileAccess.Read);

                  //BinaryReader ile byte dizisi ile FileStream arasında veri akışı sağlanıyor.

                  BinaryReader br = new BinaryReader(fs);



                  byte[] resim = br.ReadBytes((int)fs.Length);//www.gorselprogramlama.com


                  br.Close();

                  fs.Close();
                */
/*
            string resimPath = @"C:\Users\Toshiba\Pictures\plate2.png";
            FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);

        

            BinaryReader br = new BinaryReader(fs);

     
            byte[] resim = br.ReadBytes((int)fs.Length);//www.gorselprogramlama.com

            br.Close();

            fs.Close();


            komut = new SqlCommand("kayıtgirr", baglanti);
            komut.Parameters.AddWithValue("@plakaa", SqlDbType.NChar).Value = "26 GA 841";
            komut.Parameters.AddWithValue("@arabaresmi", SqlDbType.Image).Value =resim;
            komut.Parameters.AddWithValue("@plakaresmi", SqlDbType.Image).Value =resim;
            komut.Parameters.AddWithValue("@kaynak", SqlDbType.NChar).Value = "kaynak1";
            komut.CommandType = CommandType.StoredProcedure;



            komut.ExecuteNonQuery();
            SqlDataReader dr = komut.ExecuteReader();
            return dr;

    */
    


        }


    }
}
