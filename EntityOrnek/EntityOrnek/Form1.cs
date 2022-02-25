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

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        DbOgrencilerEntities db = new DbOgrencilerEntities();


        public Form1()
        {
            InitializeComponent();
        }

        private void BtnDersListesi_Click(object sender, EventArgs e)
        {
            DersleriListele();
        }
        void DersleriListele()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DbOgrenciler;Integrated Security=True");
            SqlCommand komut = new SqlCommand("Select * From tbl_dersler", baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnOgrenciListele_Click(object sender, EventArgs e)
        {
            OgrenciListle();
        }
        void OgrenciListle()
        {
            dataGridView1.DataSource = db.TBL_OGRENCI.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            // NOT : Ctrl + K + D yaparsak aşağıdak igibi komutlar alt alta gelecek şekilde listelenecektir.************************
            var query = from item in db.TBL_NOTLAR
                        select new
                        {
                            item.NOTID,
                            item.TBL_OGRENCI.AD,
                            item.TBL_OGRENCI.SOYAD,
                            item.TBL_DERSLER.DERSAD,
                            item.SINAV1,
                            item.SINAV2,
                            item.SINAV3,
                            item.ORTALAMA,
                            item.DURUM
                        };

            dataGridView1.DataSource = query.ToList();
            //dataGridView1.DataSource = db.TBL_NOTLAR.ToList();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            // Yeni Öğrenci Ekleme

            TBL_OGRENCI t = new TBL_OGRENCI();
            t.AD = TxtAd.Text;
            t.SOYAD = TxtSoyad.Text;
            db.TBL_OGRENCI.Add(t);
            db.SaveChanges();
            OgrenciListle();
            MessageBox.Show("Öğrenci Listeye Eklenmiştir");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrendiId.Text);
            var x = db.TBL_OGRENCI.Find(id);
            db.TBL_OGRENCI.Remove(x);
            db.SaveChanges();
            OgrenciListle();
            MessageBox.Show("Öğrenci Sistemden Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrendiId.Text);
            var x = db.TBL_OGRENCI.Find(id);
            x.AD = TxtAd.Text.ToUpper();
            x.SOYAD = TxtSoyad.Text.ToUpper();
            x.FOTOGRAF = TxtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Bilgileri Başarıyla Güncellendi");
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TBL_OGRENCI.Where(x => x.AD == TxtAd.Text && x.SOYAD==TxtSoyad.Text).ToList();
            dataGridView1.DataSource = db.TBL_OGRENCI.Where(x => x.AD == TxtAd.Text | x.SOYAD == TxtSoyad.Text).ToList();
        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtAd.Text.ToUpper();
            var degerler = from item in db.TBL_OGRENCI
                           where item.AD.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                // Asc - Ascending
                List<TBL_OGRENCI> liste1 = db.TBL_OGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked == true)
            {
                // Desc - Descending 
                List<TBL_OGRENCI> liste2 = db.TBL_OGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
            // ilk üç kayıt sorgusu
            if (radioButton3.Checked == true)
            {
                List<TBL_OGRENCI> liste3 = db.TBL_OGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            // ID ye göre verileri getirme
            if (radioButton4.Checked)
            {
                List<TBL_OGRENCI> liste4 = db.TBL_OGRENCI.Where(p => p.ID == 5).ToList();
                dataGridView1.DataSource = liste4;
            }
            // Adı A ile başlayanlar
            if (radioButton5.Checked)
            {
                List<TBL_OGRENCI> liste5 = db.TBL_OGRENCI.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            // Adı A ile bitenler
            if (radioButton6.Checked)
            {
                List<TBL_OGRENCI> liste6 = db.TBL_OGRENCI.Where(p => p.AD.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }
            // Değer var mı_?
            if (radioButton7.Checked)
            {
                //bool deger = db.TBL_OGRENCI.Any();
                bool deger = db.TBL_KULUPLER.Any();

                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Toplam Öğrenci Sayısını Getirme
            if (radioButton8.Checked)
            {
                int toplam = db.TBL_OGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Sınav 1 Toplam puan
            if (radioButton9.Checked)
            {
                var toplam = db.TBL_NOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show("Toplam Öğrenci 1.Sınav Not Toplamları : " + toplam.ToString(), "1.Snav Notlar Toplamı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // sınav  ortalama puan 
            if (radioButton10.Checked)
            {
                var ortalama = db.TBL_NOTLAR.Average(p => p.SINAV1);
                MessageBox.Show("Toplam Öğrenci 1.Sınav Not Ortalaması : " + ortalama.ToString(), "1.Snav Notlar Ortalaması", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // 2.sınavıtToplam 1.sınav ortalamasından yüksek olanlarıgetiren <<<ÖDEV>>>
            if (radioButton11.Checked)
            {
                var ortalama = db.TBL_NOTLAR.Average(p => p.SINAV1);
                List<TBL_NOTLAR> liste = db.TBL_NOTLAR.Where(p => p.SINAV2 > ortalama).ToList();
                dataGridView1.DataSource = liste;
            }
            //En yüksek Sınav 1 Puanı
            if (radioButton12.Checked)
            {
                var enyuksek = db.TBL_NOTLAR.Max(p => p.SINAV1);
                MessageBox.Show("Birinci Sınavın En Yüksek Notu : " + enyuksek.ToString(), "1.Snav En Yüksek Not", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //En düşük Sınav 1 Puanı
            if (radioButton13.Checked)
            {
                var enyuksek = db.TBL_NOTLAR.Min(p => p.SINAV1);
                MessageBox.Show("Birinci Sınavın En Düşük Notu : " + enyuksek.ToString(), "1.Snav En Düşük Not", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //En yüksek sınav notu kimin_?
            if (radioButton14.Checked)
            {
                var enyuksek = db.TBL_NOTLAR.Max(p => p.SINAV1);

                var result = db.TBL_NOTLAR.Where(p => p.SINAV1 == enyuksek).ToList();

                var query = from item in result
                            select new
                            {
                                item.NOTID,
                                item.TBL_OGRENCI.AD,
                                item.TBL_OGRENCI.SOYAD,
                                item.TBL_DERSLER.DERSAD,
                                item.SINAV1,
                                item.SINAV2,
                                item.SINAV3,
                                item.ORTALAMA,
                                item.DURUM
                            };

                dataGridView1.DataSource = query.ToList();

            }
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from item_1 in db.TBL_NOTLAR
                        join item_2 in db.TBL_OGRENCI
                        on item_1.OGR equals item_2.ID
                        join item_3 in db.TBL_DERSLER
                        on item_1.DERS equals item_3.DERSID

                        select new
                        {
                            Öğrenci = item_2.AD + " " + item_2.SOYAD,
                            Ders = item_3.DERSAD,
                            Sınav1 = item_1.SINAV1,
                            Sınav2 = item_1.SINAV2,
                            Sınav3 = item_1.SINAV3,
                            Ortalama=item_1.ORTALAMA
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }

        private void BtnForm2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();            
            frm.Show();
            
        }
    }
}
