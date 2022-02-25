using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        DbOgrencilerEntities db = new DbOgrencilerEntities();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                var degerler = db.TBL_NOTLAR.Where(x => x.SINAV1 < 50);

                var query = from item in degerler
                            select new
                            {
                                item.NOTID,
                                item.TBL_OGRENCI.AD,
                                item.TBL_DERSLER.DERSAD,
                                item.SINAV1,
                                item.SINAV2,
                                item.SINAV3,
                                item.ORTALAMA,
                                item.DURUM
                            };
                dataGridView1.DataSource = query.ToList();

            }
            // textbox daki adı veya soyadı değerine göre arama

            if (radioButton2.Checked)
            {
                var deger = db.TBL_OGRENCI.Where(x => x.AD == "Ali");
                dataGridView1.DataSource = deger.ToList();
            }
            // textbox a girilen adı veya soyadına göre sorgulama yapan kodlar
            if (radioButton3.Checked)
            {
                var deger = db.TBL_OGRENCI.Where(x => x.AD == textBox1.Text || x.SOYAD == textBox1.Text);
                dataGridView1.DataSource = deger.ToList();
            }
            // Sadece Öğrenci Soyadlarını Getiren Kodlar
            if (radioButton4.Checked)
            {
                var deger = db.TBL_OGRENCI.Select(x => new { Soyadı = x.SOYAD });
                dataGridView1.DataSource = deger.ToList();
            }
            // Adı bütük Soyadını küçük getiren kodlar
            if (radioButton5.Checked)
            {
                var deger = db.TBL_OGRENCI.Select(x => new { Adı = x.AD.ToUpper(), Soyadı = x.SOYAD.ToLower() });
                dataGridView1.DataSource = deger.ToList();
            }
            // şartlı seçim
            if (radioButton6.Checked)
            {
                var deger = db.TBL_OGRENCI.Select(x => new { Adı = x.AD.ToUpper(), Soyadı = x.SOYAD.ToLower() }).Where(x => x.Adı != "Ali");
                dataGridView1.DataSource = deger.ToList();
            }
            // geçti mi kaldı mı
            if (radioButton7.Checked)
            {
                var deger = db.TBL_NOTLAR.Select(x => new
                {
                    ÖğrenciAd = x.OGR,
                    Ortalaması = x.ORTALAMA,
                    Durumu = x.DURUM == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = deger.ToList();
            }
            // SelectMany Kullanımı 
            if (radioButton8.Checked)
            {
                var deger = db.TBL_NOTLAR.SelectMany(x => db.TBL_OGRENCI.Where(y => y.ID == x.OGR), (x, y) => new
                {
                    y.AD,
                    x.ORTALAMA,
                    Durumu=x.DURUM == true ? "Geçti" :"Kaldı"
                });
                dataGridView1.DataSource = deger.ToList();
            }
            // İlk 3 Değeri Getir
            if (radioButton9.Checked)
            {
                var deger = db.TBL_OGRENCI.OrderBy(x => x.ID).Take(3);
                dataGridView1.DataSource = deger.ToList();
            }
            // Son 3 Değeri Getir
            if (radioButton10.Checked)
            {
                var deger = db.TBL_OGRENCI.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = deger.ToList();
            }
            // Ada Göre Sırala
            if (radioButton11.Checked)
            {
                //var deger = db.TBL_OGRENCI.OrderBy(x => x.AD);
                var deger = db.TBL_OGRENCI.OrderByDescending(x => x.AD);
                dataGridView1.DataSource = deger.ToList();
            }
            // İlk 5 değeri atlamak
            if (radioButton12.Checked)
            {
                var deger = db.TBL_OGRENCI.OrderBy(x => x.ID).Skip(5);
                dataGridView1.DataSource = deger.ToList();
            }
        }

    }
}
