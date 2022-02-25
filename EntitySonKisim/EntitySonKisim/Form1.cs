using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitySonKisim
{
    public partial class Form1 : Form
    {
        DbOgrencilerEntities db = new DbOgrencilerEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // var degerler = db.TBL_OGRENCI.OrderBy(x => x.SEHIR).GroupBy(y => y.SEHIR).Select(z => new { Şehir = z.Key, Toplam = z.Count() });
            var degerler = db.TBL_OGRENCI.OrderBy(x => x.SEHIR).GroupBy(y => y.SEHIR).Select(z => new { Şehir = z.Key, Toplam = z.Count() }).OrderBy(k => k.Toplam);  // Burada da Toplam a göre azdan çoka doğru sıralama yapılıyor, OrderByDescending kumonutu ilede çoktanaza doğru sıralanabilir
            dataGridView1.DataSource = degerler.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // en yüksek ortalama
            //label1.Text = db.TBL_NOTLAR.Max(x => x.ORTALAMA).ToString();
            label1.Text = db.TBL_NOTLAR.Min(y => y.SINAV1).ToString();


            // kalan öğrenciler arasında en yüksek ortalamayı bulan sorgu
            var eleman = db.TBL_NOTLAR.Where(x => x.DURUM == false).OrderByDescending(y => y.ORTALAMA).Take(1).Select(z => new
            {
                Öğrenci = z.OGR,
                Ortalama = z.ORTALAMA,
                Durum = z.DURUM
            });
            dataGridView1.DataSource = eleman.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // toplam ürün çrşidi sayısı
            if(radioButton1.Checked)
            {
                label2.Text = db.TBL_URUNLER.Count().ToString()+ " Çeşit Ürün Vardır.";
            }
            // toplam stok sayısı
            if(radioButton2.Checked)
            {
                label3.Text=db.TBL_URUNLER.Sum(x=>x.STOK).ToString() + " Adet ürün stokdadır.";
            }
            //Buzdolabı çeşidi adeti
            if(radioButton3.Checked)
            {
                label4.Text = db.TBL_URUNLER.Count(x => x.AD == "BUZDOLABI").ToString() + " çeşit buzdolabı vardır.";
            }
            // Bütün ürünlerin ortalama fiyatı
            if(radioButton4.Checked)
            {
                label5.Text = db.TBL_URUNLER.Average(x => x.FIYAT).ToString();
            }
            // Ortalama Buzdolabı Fiyatı
            if(radioButton5.Checked)
            {
                label6.Text = db.TBL_URUNLER.Where(x => x.AD == "BUZDOLABI").Average(y => y.FIYAT).ToString();
            }
            // En Fazla Stoğu Olan Ürünü Bul
            if(radioButton6.Checked)
            {
                label7.Text=(from x in db.TBL_URUNLER
                             orderby x.STOK descending
                             select x.AD).First();
            }
            // en az stoku olan ürün
            if (radioButton7.Checked)
            {
                label8.Text = (from x in db.TBL_URUNLER
                               orderby x.STOK ascending
                               select x.AD).First();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // veri tabanı üzerinde tanımladığımız prosedürü metodumuza ekledikten sonra tek satırda verileri çekiyoruz....
            /*
             *      CREATE PROCEDURE Kulupler
                    AS 
                    select AD,SOYAD,SEHIR,KULUPAD FROM TBL_OGRENCI
                    INNER JOIN TBL_KULUPLER ON TBL_OGRENCI.OGRKULUP=TBL_KULUPLER.KULUPID
             * */
            dataGridView1.DataSource = db.Kulupler().ToList();
        }
    }
}
