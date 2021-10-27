using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASGJETSAT.PL
{
    public partial class Yuklenme : Form
    {
        public Yuklenme()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        int sayac = 0;
        private void Yuklenme_Shown(object sender, EventArgs e)
        {
            
            Thread thread = new Thread(new ThreadStart(bilgial));
            thread.Start();
            thread.Join();
            //if (thread.ThreadState != ThreadState.Running)
            //{

            //}
            //timerZaman.Stop();
            //lblDurum.Text = "Veritabanından Bilgiler Alınıdı.";
            //Thread.Sleep(5000);
            //lblDurum.Text = "Veriler Sisteme Ekleniyor";
            //Thread.Sleep(1000);
            //thread = new Thread(new ThreadStart(bilgisistemegiris));
            //sayac = 0;
            //timerZaman.Start();
            //thread.Start();
            //thread.Join();
            //if (thread.ThreadState == ThreadState.Stopped)
            //{

            //}
            //timerZaman.Stop();
            //lblDurum.Text = "Veriler Sisteme Eklendi.";
            //Thread.Sleep(1000);
            this.Hide();
            Sayfalar.giris.Show();

        }

        void bilgisistemegiris()
        { EventArgs eventArgs= new EventArgs();
            PL.Sayfalar.urunlerKategori.UrunlerKategori_Load("",eventArgs);
            PL.Sayfalar.urunlerUrun.UrunlerUrun_Load("", eventArgs);
            PL.Sayfalar.urunlerOzellik.UrunlerOzellik_Load("", eventArgs);
            PL.Sayfalar.urunlerStok.UrunlerStok_Load("", eventArgs);
            PL.Sayfalar.muhasebe.Muhasebe_Load("", eventArgs);
            PL.Sayfalar.urunlerKisaYol.UrunlerKisaYol_Load("", eventArgs);
            PL.Sayfalar.jETSAT.JETSAT_Load("", eventArgs);
        }

        void bilgial()
        {lblDurum.Text = "Veritabanından Bilgiler Alınıyor";
            Thread.Sleep(5000);
            timerZaman.Start();
            BL.KullanıcıAyar.KullanıcıBilgileriDoldur();
            BL.UrunKategori.kategorilerdoldur();
            BL.BarkodAyar.BarkodSirala();
            BL.KisaYolAyar.KisaYolYenile();
            BL.UrunIptal.iptaldoldur();
            lblDurum.Text = "Veritabanından Bilgiler Alınıdı.";
            Thread.Sleep(2000);
            lblDurum.Text = "Ekranlar oluşturuluyor";
            sayac = 0;
            EkranAyarları();
            lblDurum.Text = "Ekranlar oluşturuldu.";
            Thread.Sleep(2000);
            lblDurum.Text = "Veriler Sisteme Ekleniyor";
            sayac = 0;
            bilgisistemegiris();
            lblDurum.Text = "Veriler Sisteme Eklendi.";
            Thread.Sleep(2000);
            lblDurum.Text = "Menüler Oluşturuluyor";
            sayac = 0;
            Sayfalar.jETSAT.MenuDuzenle();
            lblDurum.Text = "Menüler Oluşturuldu.";
            Thread.Sleep(2000);
            timerZaman.Stop();


            lblDurum.Text = "Sisteme Giriş Yapılıyor.";
            sayac = 0;

           
            Thread.Sleep(3000);
        }
        private void EkranAyarları()
        {
            Sayfalar.anaSayfa.InitializeComponent();
            Sayfalar.ayarlar.InitializeComponent();
            Sayfalar.giris.InitializeComponent();
            Sayfalar.jETSAT.InitializeComponent();
            Sayfalar.muhasebe.InitializeComponent();
            Sayfalar.urunlerKategori.InitializeComponent();
            Sayfalar.urunlerOzellik.InitializeComponent();
            Sayfalar.urunlerStok.InitializeComponent();
            Sayfalar.urunlerUrun.InitializeComponent();
            Sayfalar.urunOzellikMDICmbx.InitializeComponent();
            Sayfalar.urunOzellikMDITxt.InitializeComponent();
            Sayfalar.urunlerKisaYol.InitializeComponent();
        }
        private void timerZaman_Tick(object sender, EventArgs e)
        {if (sayac == 3)
            {
                sayac = 0;
                lblDurum.Text = lblDurum.Text.Substring(0, lblDurum.Text.Length - 3);
            }
            lblDurum.Text += ".";
            sayac++;
        }
    }
}
