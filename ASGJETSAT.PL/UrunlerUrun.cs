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
    public partial class UrunlerUrun : Form
    {

        public UrunlerUrun()
        {

        }

        public void UrunlerUrun_Load(object sender, EventArgs e)
        {/*comboboxları doldurup index ayarları yapılıyor.*/
            
        }

        private void cmbxIndexAyarı()
        {/*comboboxların index ayarları.*/
            try
            {
                cmbxUrunEkleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxUrunEkleKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxDuzenleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxDuzenleKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxDuzenleUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxDuzenleUrun.SelectedIndex = -1;
            }

            try
            {
                cmbxUrunSilKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxUrunSilKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxDuzenleKategoriYeni.SelectedIndex = 0;
            }
            catch
            {
                cmbxDuzenleKategoriYeni.SelectedIndex = -1;
            }

            try
            {
                cmbxUrunSilUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxUrunSilUrun.SelectedIndex = -1;
            }
        }
        public void cmbxDoldur()
        {/*kategori comboboxlarını içeriklerini temizliyor.*/
            cmbxUrunEkleKategori.Items.Clear();
            cmbxDuzenleKategori.Items.Clear();
            cmbxUrunSilKategori.Items.Clear();
            cmbxDuzenleKategoriYeni.Items.Clear();

            /*kategori comboboxlarını doluduruyoruz.*/
            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                cmbxUrunEkleKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
                cmbxDuzenleKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
                cmbxUrunSilKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
                cmbxDuzenleKategoriYeni.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
            }
            cmbxIndexAyarı();
        }

        private void btn_UrunEkleSil_Click(object sender, EventArgs e)
        {/*btn_UrunEkleSil click eventi calıştıdıgı an txt_UrunEkleUrun ve txt_UrunEkleFiyat textleri sıfırlanıyor ve cmbxUrunEkleKategori index ayarı yapılıyor.*/
            txt_UrunEkleUrun.Text = "";
            txt_UrunEkleFiyat.Text = "";

            try
            {
                cmbxUrunEkleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxUrunEkleKategori.SelectedIndex = -1;
            }
        }

        private void btn_UrunEkleEkle_Click(object sender, EventArgs e)
        {/*btn_UrunEkleEkle click eventi calıstıgı an urunu database ekler.*/
            string urun = "";
            double fiyat = 0.0;

            /*eger txt_UrunEkleUrun texti boş degilse*/
            if (txt_UrunEkleUrun.Text != "" && txt_UrunEkleFiyat.Text != "")
            {
                urun = txt_UrunEkleUrun.Text;
                bool Bdurum = true;/*fiyat float durumuna dondermede hata olması durumu.*/

                try
                {
                    fiyat = float.Parse(txt_UrunEkleFiyat.Text);
                }
                catch
                {
                    Bdurum = false;
                }

                if (Bdurum)/*fiyat dondurmede hata yoksa yapılacak durumlar.*/
                {
                    /*urunu ekliyor.*/
                    int durum = BL.UrunUrun.UrunEkle(urun, BL.UrunBilgisi.Urunler[cmbxUrunEkleKategori.SelectedIndex].ID,cmbxUrunEkleKategori.SelectedIndex);
                    if (durum != -1)/*urunu eklendıyse yapılacak işlemler*/
                    {
                        string barkod = "";
                        if (txt_UrunEkleBarkod.Text == "")
                        {
                            barkod = "-1";
                        }
                        else
                        {
                            barkod = txt_UrunEkleBarkod.Text;
                        }
                        try
                        {
                            int.Parse(txt_UrunEkleBarkod.Text);
                       
                            int ID = BL.UrunUrun.EnSonEklenenUrunIDBul();/*en son eklenen urunun ıd getir*/
                            if (ID != -1)
                            {/*en son eklenen ıd gore yeni urun ozellik ekle.*/
                                durum = BL.UrunUrun.UrunOzellikEkle(BL.UrunBilgisi.Urunler[cmbxUrunEkleKategori.SelectedIndex].ID,ID, urun, barkod, decimal.Parse(txt_UrunEkleFiyat.Text), cmbxUrunEkleKategori.SelectedIndex, BL.UrunBilgisi.Urunler[cmbxUrunEkleKategori.SelectedIndex].URUNLER.Count-1);

                                if (durum != -1)
                                {
                                    BL.BarkodAyar.BarkodSirala();
                                    MessageBox.Show("Ürün Eklenmiştir");
                                }
                                else
                                {
                                    MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }

                cmbxDoldur();
            }
            else if (txt_UrunEkleUrun.Text != "" && txt_UrunEkleFiyat.Text == "" && txt_UrunEkleBarkod.Text == "")
            {
                urun = txt_UrunEkleUrun.Text;
                int durum = BL.UrunUrun.UrunEkle(urun, BL.UrunBilgisi.Urunler[cmbxUrunEkleKategori.SelectedIndex].ID, cmbxUrunEkleKategori.SelectedIndex);

                if (durum != -1)
                {
                    Sayfalar.jETSAT.UrunEkle(cmbxUrunEkleKategori.SelectedIndex,BL.UrunBilgisi.Urunler[cmbxUrunEkleKategori.SelectedIndex].URUNLER.Count-1);
                    MessageBox.Show("Ürün Eklenmiştir");
                }
                else
                {
                    MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                cmbxDoldur();
            }

            else
                MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            txt_UrunEkleUrun.Text = "";
            txt_UrunEkleFiyat.Text = "";
            txt_UrunEkleBarkod.Text = "";

            cmbxIndexAyarı();

        }

        private void cmbxDuzenleKategori_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxDuzenleKategori selected index secildigi an cmbxDuzenleUrun içerigini temizleyip yeni urunleri içine ekliyoruz ve index ayarını yapıyoruz.*/
            if (cmbxDuzenleKategori.SelectedIndex != -1)
            {
                cmbxDuzenleUrun.Items.Clear();
                cmbxDuzenleUrun.Text = "";

                for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER.Count; i++)
                {
                    cmbxDuzenleUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[i].URUN);
                }
                try
                {
                    cmbxDuzenleUrun.SelectedIndex = 0;
                }
                catch
                {
                    cmbxDuzenleUrun.SelectedIndex = -1;
                }

                cmbxDuzenleKategoriYeni.SelectedIndex = cmbxDuzenleKategori.SelectedIndex;
                txtDuzenleUrunYeni.Text = cmbxDuzenleUrun.Text;
                try
                {/*eklenen urunun ozellik sayısı 1 veya daha az ise txtDuzenleUrunFiyat, txtDuzenleUrunFiyatYeni ve lblUrunDuzenleFiyat ekranda goster ve textboxlara fiyatları yaz.*/
                    if (BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[cmbxDuzenleUrun.SelectedIndex].URUNOZELLIK.Count <= 1)
                    {
                        txtDuzenleUrunFiyat.Visible = true;
                        txtDuzenleUrunFiyatYeni.Visible = true;
                        lblUrunDuzenleFiyat.Visible = true;
                        txtDuzenleUrunFiyat.Text = BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[cmbxDuzenleUrun.SelectedIndex].URUNOZELLIK[0].FIYAT.ToString();
                        txtDuzenleUrunFiyatYeni.Text = BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[cmbxDuzenleUrun.SelectedIndex].URUNOZELLIK[0].FIYAT.ToString();
                    }
                    else/*degilse textboxları gizle ve icerikleri sıfırla.*/
                    {
                        txtDuzenleUrunFiyat.Visible = false;
                        txtDuzenleUrunFiyatYeni.Visible = false;
                        lblUrunDuzenleFiyat.Visible = false;
                        txtDuzenleUrunFiyat.Text = "";
                        txtDuzenleUrunFiyatYeni.Text = "";
                    }
                }
                catch
                {
                    txtDuzenleUrunFiyat.Visible = false;
                    txtDuzenleUrunFiyatYeni.Visible = false;
                    lblUrunDuzenleFiyat.Visible = false;
                }
            }
        }

        private void btn_UrunDuzenleSil_Click(object sender, EventArgs e)/*btn_UrunDuzenleSil click eventi calıştıgı an comobox ayarı yap.*/
        {
            cmbxIndexAyarı();
        }

        private void btn_UrunDuzenleEkle_Click(object sender, EventArgs e)
        {/*btn_UrunDuzenleEkle click eventi calıstıgı an urunun databaseye guncellemış oluruz.*/
            if (txtDuzenleUrunFiyat.Visible == true
                && txtDuzenleUrunFiyatYeni.Visible == true)// eger urun ozellik fiyatları ekran gozukuyorsa yanı urun ozellik 1 tane ise.
            {
                bool durum = true;
                double fiyat = 0;
                try/*fiyat alırken hata alma durumu*/
                {
                    fiyat = double.Parse(txtDuzenleUrunFiyatYeni.Text);
                }
                catch
                {
                    durum = false;
                    MessageBox.Show("Hata Sayı Giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (durum)
                {/*duzenlenmıs urunu database guncelleme.*/
                    if (BL.UrunUrun.UrunDuzenle(BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[cmbxDuzenleUrun.SelectedIndex].ID,
                          BL.UrunBilgisi.Urunler[cmbxDuzenleKategoriYeni.SelectedIndex].ID, txtDuzenleUrunYeni.Text,
                          BL.UrunUrun.EnSonEklenenUrunOzellikIDBul(), decimal.Parse(txtDuzenleUrunFiyatYeni.Text),
                          cmbxDuzenleKategori.SelectedIndex, cmbxDuzenleUrun.SelectedIndex,cmbxDuzenleKategoriYeni.SelectedIndex) == -1)
                        MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        Sayfalar.jETSAT.UrunGuncelle(cmbxDuzenleKategori.SelectedIndex, cmbxDuzenleUrun.SelectedIndex, cmbxDuzenleKategoriYeni.SelectedIndex, decimal.Parse(txtDuzenleUrunFiyatYeni.Text));
                        MessageBox.Show("Başarlı Bir Güncellenmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbxDoldur();/* urun guncellendıgı an struct yeniden doldurup comboboxları yenıden doldurup ayar yap.*/
                        cmbxIndexAyarı();
                    }
                }
            }
            else
            {/*eger ozellik cok ise sadece urun guncelleme kodu.*/
                if (BL.UrunUrun.UrunDuzenle(BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[cmbxDuzenleUrun.SelectedIndex].ID,
                        BL.UrunBilgisi.Urunler[cmbxDuzenleKategoriYeni.SelectedIndex].ID, txtDuzenleUrunYeni.Text, 
                        cmbxDuzenleKategori.SelectedIndex, cmbxDuzenleUrun.SelectedIndex, cmbxDuzenleKategoriYeni.SelectedIndex) == -1)
                {
                    MessageBox.Show("Hata Eklenemedi Yeniden Deneyiniz", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Sayfalar.jETSAT.UrunGuncelle(cmbxDuzenleKategori.SelectedIndex, cmbxDuzenleUrun.SelectedIndex, cmbxDuzenleKategoriYeni.SelectedIndex);
                    MessageBox.Show("Başarlı Bir Şeklide Güncellenmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbxDoldur();
                }
            }
        }

        private void cmbxUrunSilKategori_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxUrunSilKategori selected index calıstıgı an cmbxUrunSilUrun icerisini temizle ve yeni urunleri icine ekle ve index ayarı yap.*/
            if (cmbxUrunSilKategori.SelectedIndex != -1)
            {
                cmbxUrunSilUrun.Items.Clear();
                cmbxUrunSilUrun.Text = "";
                for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxUrunSilKategori.SelectedIndex].URUNLER.Count; i++)
                {
                    cmbxUrunSilUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxUrunSilKategori.SelectedIndex].URUNLER[i].URUN);
                }
                try
                {
                    cmbxUrunSilUrun.SelectedIndex = 0;
                }
                catch
                {
                    cmbxUrunSilUrun.SelectedIndex = -1;
                }
            }
        }

        private void btn_UrunSilSil_Click(object sender, EventArgs e)
        {/*btn_UrunSilSil Click eventi calıstıgı an urunsil paneline ait comboboxların seleceted indexini ayarla.*/
            try
            {
                cmbxUrunSilKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxUrunSilKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxUrunSilUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxUrunSilUrun.SelectedIndex = -1;
            }
        }

        private void btn_UrunSilEkle_Click(object sender, EventArgs e)
        {/*btn_UrunSilEkle Click eventi calıstıgı an secilen urun databaseden silinip databaseden yeniden veriler cekilip comboboxlar doldurulur.*/
            if (BL.UrunUrun.UrunSil(BL.UrunBilgisi.Urunler[cmbxUrunSilKategori.SelectedIndex].ID,
                BL.UrunBilgisi.Urunler[cmbxUrunSilKategori.SelectedIndex].URUNLER[cmbxUrunSilUrun.SelectedIndex].ID,
                cmbxUrunSilKategori.SelectedIndex, cmbxUrunSilUrun.SelectedIndex) != -1)
            {
                Sayfalar.jETSAT.UrunSil(cmbxUrunSilKategori.SelectedIndex, cmbxUrunSilUrun.SelectedIndex);
                MessageBox.Show("Başarlı Bir Şeklide Silinmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbxDoldur();
                cmbxIndexAyarı();
            }
            else
            {
                MessageBox.Show("Hata Silinemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxDuzenleUrun_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxDuzenleUrun selected index calıstıgı an txtDuzenleUrunYeni cmbxDuzenleUrun textleri esitle ve txtDuzenleUrunFiyat ve txtDuzenleUrunFiyatYeni textleri fiyatları ekle.*/
            if (cmbxDuzenleUrun.SelectedIndex != -1)
            {
                txtDuzenleUrunYeni.Text = cmbxDuzenleUrun.Text;
                try
                {
                    txtDuzenleUrunFiyat.Text = BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[cmbxDuzenleUrun.SelectedIndex].URUNOZELLIK[0].FIYAT.ToString();
                    txtDuzenleUrunFiyatYeni.Text = BL.UrunBilgisi.Urunler[cmbxDuzenleKategori.SelectedIndex].URUNLER[cmbxDuzenleUrun.SelectedIndex].URUNOZELLIK[0].FIYAT.ToString();
                }
                catch
                {
                    txtDuzenleUrunFiyat.Text = "";
                    txtDuzenleUrunFiyatYeni.Text = "";
                }
            }
        }

        private void btn_UrunOzellıkMDIEkle_Click(object sender, EventArgs e)
        {/* btn_UrunOzellıkMDIEkle Click eventi calıstıgı an UrunOzellikMDITxt nesnesi olustur ve MDI olarak ac ve içindeki TextBoxUrun textini
            txt_UrunEkleUrun texti olarak aktar ComboBoxKategori verileri ile doldur.*/

            Sayfalar.urunOzellikMDITxt.MdiParent = this.MdiParent;
            Sayfalar.urunOzellikMDITxt.cmbxKategori.Items.Clear();
            foreach (string data in cmbxUrunEkleKategori.Items)
                Sayfalar.urunOzellikMDITxt.cmbxKategori.Items.Add(data);
            Sayfalar.urunOzellikMDITxt.txtUrun.Text = txt_UrunEkleUrun.Text;
            Sayfalar.urunOzellikMDITxt.cmbxKategori.SelectedIndex = cmbxUrunEkleKategori.SelectedIndex;

            txt_UrunEkleUrun.Text = "";
            cmbxIndexAyarı();

            Sayfalar.urunOzellikMDITxt.ShowDialog();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {/*AnaSayfa gitme kodu.*/
            Sayfalar.anaSayfa.Show();
            this.Hide();
        }

        private void btnStok_Click(object sender, EventArgs e)
        {/*UrunlerStok gitme kodu.*/
            Sayfalar.urunlerStok.Show();
            this.Hide();
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {/*UrunlerKategori gitme kodu.*/
            Sayfalar.urunlerKategori.Show();
            this.Hide();
        }

        private void btnOzellik_Click(object sender, EventArgs e)
        {/*UrunlerOzellik gitme kodu.*/
            Sayfalar.urunlerOzellik.Show();
            this.Hide();
        }

        private void btn_KisaYol_Click(object sender, EventArgs e)
        {
            Sayfalar.urunlerKisaYol.Show();
            this.Hide();
        }

        private void UrunlerUrun_Shown(object sender, EventArgs e)
        {
            cmbxDoldur();
        }
    }
}
