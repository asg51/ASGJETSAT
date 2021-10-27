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
    public partial class UrunlerOzellik : Form
    {

        public UrunlerOzellik()
        {

        }


        private void cmbxDoldur()/*comboboxları structdaki bilgilerie dolduracak kısım.*/
        {
            cmbxOzellikDuzenleKategori.Items.Clear();
            cmbxOzellikDuzenleKategoriYeni.Items.Clear();
            cmbxOzellikEkleKategori.Items.Clear();
            cmbxOzellikSilKategori.Items.Clear();

            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                cmbxOzellikDuzenleKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
                cmbxOzellikDuzenleKategoriYeni.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
                cmbxOzellikEkleKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
                cmbxOzellikSilKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
            }

        }

        private void cmbxIndexAyar()/*comboboxları selected index ayarı yapılacak kısım.*/
        {
            try
            {
                cmbxOzellikDuzenleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikDuzenleKategoriYeni.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleKategoriYeni.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikDuzenleOzellik.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleOzellik.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikDuzenleUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleUrun.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikDuzenleUrunYeni.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleUrunYeni.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikEkleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikEkleKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikEkleUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikEkleUrun.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikSilKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikSilKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikSilOzellik.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikSilOzellik.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikSilUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikSilUrun.SelectedIndex = -1;
            }
        }

        public void UrunlerOzellik_Load(object sender, EventArgs e)
        {/*ilk once struct verileri doldurulur daha sonra comboboxa veriler eklenir daha sonra comboboxlardaki verilerin seleceted index ayarı yapılır.*/
            
        }

        private void cmbxOzellikEkleKategori_SelectedIndexChanged(object sender, EventArgs e)/*cmbxOzellikEkleKategori_SelectedIndexChanged secilen
            indexe gore cmbxOzellikEkleUrun yeniden duzenlenir yani her kategoriye gore urun bilgileri gelir.*/
        {
            if (cmbxOzellikEkleKategori.SelectedIndex != -1)
            {
                cmbxOzellikEkleUrun.Items.Clear();
                cmbxOzellikEkleUrun.Text = "";
                txtOzellikEkleFiyat.Text = "";
                txtOzellikEkleOzellik.Text = "";

                for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxOzellikEkleKategori.SelectedIndex].URUNLER.Count; i++)
                {
                    cmbxOzellikEkleUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxOzellikEkleKategori.SelectedIndex].URUNLER[i].URUN);
                }

                try
                {
                    cmbxOzellikEkleUrun.SelectedIndex = 0;
                }
                catch
                {
                    cmbxOzellikEkleUrun.SelectedIndex = -1;
                }
            }
        }

        private void btnOzellikEkleSil_Click(object sender, EventArgs e)/*btnOzellikEkleSil buttonu basınca cmbxOzellikEkleKategori secilen index ayarı duzeltılır. */
        {
            try
            {
                cmbxOzellikEkleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikEkleKategori.SelectedIndex = -1;
            }
        }

        private void btnOzellikEkleEkle_Click(object sender, EventArgs e)/*btnOzellikEkleEkle buttonu basılınca yeni bir ozellik eklenir.*/
        {
            bool durum = false;
            foreach (Control control in pnlOzellikEkle.Controls)//pnlOzellikEkle ait controleri donguyle kontrol edıyoruz.
            {
                if (control.GetType() == typeof(ComboBox) || control.GetType() == typeof(TextBox))//textbox ve combobox mı diye kontrol ediyoruz.
                    if (control.Text == "" && control != txtOzellikEkleBarkod)
                        durum = true;
            }

            if (durum)//text durumu bos cıkma durumu
                MessageBox.Show("Hata Bütün Bölümleri Doldurun!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                double fiyat = -1;
                try// fiyat double donusebilyormu diye kontrol ediliyor.
                {
                    fiyat = double.Parse(txtOzellikEkleFiyat.Text);
                    try
                    {
                        int.Parse(txtOzellikEkleBarkod.Text);
                    }
                    catch
                    {
                        txtOzellikEkleBarkod.Text = "";
                    }
                }
                catch
                {
                    MessageBox.Show("Fiyata Sayı Giriniz!.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (fiyat != -1.0)//donuştuyse fiyat ekleniyor.
                {
                    string barkod = "";
                    if (txtOzellikEkleBarkod.Text == "")
                    {
                        barkod = "-1";
                    }
                    else
                    {
                        barkod = txtOzellikEkleBarkod.Text;
                    }
                    if (BL.UrunUrunOzellik.OzellikEkle(BL.UrunBilgisi.Urunler[cmbxOzellikEkleKategori.SelectedIndex].URUNLER[cmbxOzellikEkleUrun.SelectedIndex].ID, txtOzellikEkleOzellik.Text, int.Parse(barkod), decimal.Parse(txtOzellikEkleFiyat.Text), cmbxOzellikEkleKategori.SelectedIndex, cmbxOzellikEkleUrun.SelectedIndex) != -1)
                    {
                        Sayfalar.jETSAT.UrunOzellikEkle(cmbxOzellikEkleKategori.SelectedIndex, cmbxOzellikEkleUrun.SelectedIndex,
                            BL.UrunBilgisi.Urunler[cmbxOzellikEkleKategori.SelectedIndex].URUNLER[cmbxOzellikEkleUrun.SelectedIndex].URUNOZELLIK.Count - 1);
                        MessageBox.Show("Başarılı Bir Şeklide Eklenmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbxDoldur();
                        cmbxIndexAyar();
                        BL.BarkodAyar.BarkodSirala();
                    }
                    else
                    {
                        MessageBox.Show("Hata Eklenemedi.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                /*paneldeki nesnelerin ayarı yapılıyor.*/
                txtOzellikEkleFiyat.Text = "";
                txtOzellikEkleOzellik.Text = "";

                try
                {
                    cmbxOzellikEkleKategori.SelectedIndex = 0;
                }
                catch
                {
                    cmbxOzellikEkleKategori.SelectedIndex = -1;
                    cmbxOzellikEkleKategori.Text = "";
                }

                try
                {
                    cmbxOzellikEkleUrun.SelectedIndex = 0;
                }
                catch
                {
                    cmbxOzellikEkleUrun.SelectedIndex = -1;
                    cmbxOzellikEkleUrun.Text = "";
                }

            }
        }

        private void cmbxOzellikDuzenleKategori_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxOzellikDuzenleKategori index secildigi an cmbxOzellikDuzenleKategoriYeni degeride aynı olur ve cmbxOzellikDuzenleUrun ve cmbxOzellikDuzenleUrunYeni yeni urun bilgilerini alır.*/
            if (cmbxOzellikDuzenleKategori.SelectedIndex != -1)
            {
                cmbxOzellikDuzenleKategoriYeni.SelectedIndex = cmbxOzellikDuzenleKategori.SelectedIndex;

                cmbxOzellikDuzenleUrun.Items.Clear();
                cmbxOzellikDuzenleUrun.Text = "";
                cmbxOzellikDuzenleUrunYeni.Items.Clear();
                cmbxOzellikDuzenleUrunYeni.Text = "";
                cmbxOzellikDuzenleOzellik.Items.Clear();
                cmbxOzellikDuzenleOzellik.Text = "";
                txtOzellikDuzenleOzellikYeni.Text = "";
                txtOzellikDuzenleFiyat.Text = "";
                txtOzellikDuzenleFiyatYeni.Text = "";

                try
                {
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER.Count; i++)
                    {
                        cmbxOzellikDuzenleUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[i].URUN);
                        cmbxOzellikDuzenleUrunYeni.Items.Add(BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[i].URUN);
                    }
                    cmbxOzellikDuzenleUrun.SelectedIndex = 0;
                    cmbxOzellikDuzenleUrunYeni.SelectedIndex = 0;
                }
                catch
                {
                    cmbxOzellikDuzenleUrun.SelectedIndex = -1;
                    cmbxOzellikDuzenleUrunYeni.SelectedIndex = -1;
                }
            }
        }

        private void cmbxOzellikDuzenleUrun_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxOzellikDuzenleUrun seclidigi an cmbxOzellikDuzenleUrunYeni aynı degeri alırken cmbxOzellikDuzenleOzellik yeni urunozelliklerini alır.*/
            if (cmbxOzellikDuzenleUrun.SelectedIndex != -1)
            {
                cmbxOzellikDuzenleUrunYeni.SelectedIndex = cmbxOzellikDuzenleUrun.SelectedIndex;

                cmbxOzellikDuzenleOzellik.Text = "";
                cmbxOzellikDuzenleOzellik.Items.Clear();


                for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrun.SelectedIndex].URUNOZELLIK.Count; i++)
                {
                    cmbxOzellikDuzenleOzellik.Items.Add(BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrun.SelectedIndex].URUNOZELLIK[i].OZELLIK);
                }
                cmbxOzellikDuzenleOzellik.SelectedIndex = 0;

                txtOzellikDuzenleOzellikYeni.Text = cmbxOzellikDuzenleOzellik.Text;
            }
        }
        private void cmbxOzellikDuzenleOzellik_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxOzellikDuzenleOzellik selected index secildigi an txtOzellikDuzenleOzellikYeni aynı degeri alır ve txtOzellikDuzenleFiyat ve txtOzellikDuzenleFiyatYeni urun ozelliginin fıyatlarını alır.*/
            if (cmbxOzellikDuzenleOzellik.SelectedIndex != -1)
            {
                txtOzellikDuzenleOzellikYeni.Text = cmbxOzellikDuzenleOzellik.Text;

                txtOzellikDuzenleFiyat.Text = BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrun.SelectedIndex].URUNOZELLIK[cmbxOzellikDuzenleOzellik.SelectedIndex].FIYAT.ToString();
                txtOzellikDuzenleFiyatYeni.Text = BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrun.SelectedIndex].URUNOZELLIK[cmbxOzellikDuzenleOzellik.SelectedIndex].FIYAT.ToString();
                txtOzellikDuzenleBarkod.Text = BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrun.SelectedIndex].URUNOZELLIK[cmbxOzellikDuzenleOzellik.SelectedIndex].BARKOD.ToString();
                txtOzellikDuzenleBarkodYeni.Text = BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrun.SelectedIndex].URUNOZELLIK[cmbxOzellikDuzenleOzellik.SelectedIndex].BARKOD.ToString();
            }
        }

        private void btnOzellikDuzenleSil_Click(object sender, EventArgs e)
        {/*urun ozellik duzunle panelinin combobox selected index ayarları yaplır.*/
            try
            {
                cmbxOzellikDuzenleKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxOzellikDuzenleUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleUrun.SelectedIndex = -1;
            }


            try
            {
                cmbxOzellikDuzenleOzellik.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikDuzenleOzellik.SelectedIndex = -1;
            }
        }

        private void btnOzellikDuzenleEkle_Click(object sender, EventArgs e)
        {/*ozellik duzenleme kısmı.*/
            bool durum = false;
            foreach (Control control in pnlOzellikDuzenle.Controls)//pnlOzellikDuzenle içindeki textbox ve comboboxları arıyıp text durumu bos olup olmam durumununa bakıyoruz.
            {
                if (control.GetType() == typeof(TextBox) || control.GetType() == typeof(ComboBox))
                {
                    if (control.Text == "" && control != txtOzellikDuzenleBarkod && control != txtOzellikDuzenleBarkodYeni)
                        durum = true;
                }
            }

            if (durum)
                MessageBox.Show("Hata Bütün Bölümleri Doldurun!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else//bos veri yok ise yapılacak işlemler.
            {
                
                    try
                    {
                        double.Parse(txtOzellikDuzenleFiyatYeni.Text);
                        string barkod = "";
                        if (txtOzellikDuzenleBarkodYeni.Text == "")
                        {
                            barkod = "-1";
                        }
                        else
                        {
                            barkod = txtOzellikDuzenleBarkodYeni.Text;
                        }
                        if (BL.UrunUrunOzellik.OzellikDuzenle(BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategori.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrun.SelectedIndex].URUNOZELLIK[cmbxOzellikDuzenleOzellik.SelectedIndex].ID,
                            BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategoriYeni.SelectedIndex].URUNLER[cmbxOzellikDuzenleUrunYeni.SelectedIndex].ID, txtOzellikDuzenleOzellikYeni.Text, int.Parse(barkod), 
                            decimal.Parse(txtOzellikDuzenleFiyatYeni.Text), cmbxOzellikDuzenleKategori.SelectedIndex,
                            cmbxOzellikDuzenleUrun.SelectedIndex, cmbxOzellikDuzenleOzellik.SelectedIndex, cmbxOzellikDuzenleKategoriYeni.SelectedIndex, cmbxOzellikDuzenleUrunYeni.SelectedIndex)
                            != -1)
                        {
                            Sayfalar.jETSAT.UrunOzellikGuncelle(cmbxOzellikDuzenleKategori.SelectedIndex, cmbxOzellikDuzenleUrun.SelectedIndex,
                                cmbxOzellikDuzenleOzellik.SelectedIndex, cmbxOzellikDuzenleKategoriYeni.SelectedIndex, cmbxOzellikDuzenleUrunYeni.SelectedIndex);
                            BL.BarkodAyar.BarkodSirala();
                            MessageBox.Show("Başarılı Bir Şekilde Güncellenmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbxDoldur();
                            cmbxIndexAyar();
                        }
                        else
                            MessageBox.Show("Hata Güncellenemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        MessageBox.Show("Hata Sayı Giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
            }
        }

        private void cmbxOzellikSilKategori_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxOzellikSilKategori secilen indexin cmbxOzellikSilUrun yeni urunlerin eklenmesi kısmı.*/
            if (cmbxOzellikSilKategori.SelectedIndex != -1)
            {
                cmbxOzellikSilUrun.Items.Clear();
                cmbxOzellikSilUrun.Text = "";
                try
                {
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxOzellikSilKategori.SelectedIndex].URUNLER.Count; i++)
                    {
                        cmbxOzellikSilUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxOzellikSilKategori.SelectedIndex].URUNLER[i].URUN);
                        cmbxOzellikSilUrun.SelectedIndex = 0;
                    }
                }
                catch
                {
                    cmbxOzellikSilUrun.SelectedIndex = -1;
                }
            }
        }

        private void cmbxOzellikSilUrun_SelectedIndexChanged(object sender, EventArgs e)
        {/*cmbxOzellikSilUrun secilen indexin cmbxOzellikSilOzellik yeni ozellikler ekleme durumu.*/
            if (cmbxOzellikSilUrun.SelectedIndex != -1)
            {
                cmbxOzellikSilOzellik.Items.Clear();
                cmbxOzellikSilOzellik.Text = "";
                try
                {
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxOzellikSilKategori.SelectedIndex].URUNLER[cmbxOzellikSilUrun.SelectedIndex].URUNOZELLIK.Count; i++)
                    {
                        cmbxOzellikSilOzellik.Items.Add(BL.UrunBilgisi.Urunler[cmbxOzellikSilKategori.SelectedIndex].URUNLER[cmbxOzellikSilUrun.SelectedIndex].URUNOZELLIK[i].OZELLIK);
                        cmbxOzellikSilOzellik.SelectedIndex = 0;
                    }
                }
                catch
                {
                    cmbxOzellikSilOzellik.SelectedIndex = -1;
                }
            }
        }

        private void btnOzellikSilSil_Click(object sender, EventArgs e)
        {/*btnOzellikSilSil buttonu ozellik sil panelinin içindeki comboboxların selected index ayarını yapmaktadır.*/
            try
            {
                cmbxOzellikSilKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxOzellikSilKategori.SelectedIndex = -1;
            }
        }

        private void btnOzellikSilEkle_Click(object sender, EventArgs e)
        {/*ozellik silme kısmı.*/
            bool durum = false;
            foreach (Control control in pnlOzellikSil.Controls)
            {/*ozellik sil panelin içindeli textbox ve comboboxların textlerinin bos olup olmadıgını kontrol edilıyor.*/
                if (control.GetType() == typeof(ComboBox))
                    if ((control as ComboBox).SelectedIndex == -1)
                        durum = true;
            }

            if (durum)
                MessageBox.Show("Hata Bütün Bölümleri Doldurun!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else/*boş degil ise urun databaseden silinip yeniden strcutlar doldurulup combobox ayarları yapılıyor.*/
            {
                if (BL.UrunUrunOzellik.OzellikSil(BL.UrunBilgisi.Urunler[cmbxOzellikSilKategori.SelectedIndex].URUNLER[cmbxOzellikSilUrun.SelectedIndex].URUNOZELLIK[cmbxOzellikSilOzellik.SelectedIndex].ID,
                    cmbxOzellikSilKategori.SelectedIndex, cmbxOzellikSilUrun.SelectedIndex, cmbxOzellikSilOzellik.SelectedIndex) != -1)
                {
                    Sayfalar.jETSAT.UrunOzellikSil(cmbxOzellikSilKategori.SelectedIndex, cmbxOzellikSilUrun.SelectedIndex, cmbxOzellikSilOzellik.SelectedIndex);
                    MessageBox.Show("Başarılı Bir Şekilde Silinmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbxDoldur();
                    cmbxIndexAyar();
                    BL.BarkodAyar.BarkodSirala();
                }
                else
                {
                    MessageBox.Show("Hata Silinemedi!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOzelliklerEkleEkle_Click(object sender, EventArgs e)
        {/*coklu ozellik eklemek için yeni bir MDI form acılıyor ve MDI form içindeki comboboxlar dolduruluyor. index ayarları yapılıyor.*/

            Sayfalar.urunOzellikMDICmbx.MdiParent = this.MdiParent;
            foreach (string data in cmbxOzellikEkleKategori.Items)
                Sayfalar.urunOzellikMDICmbx.cmbxKategori.Items.Add(data);
            foreach (string data in cmbxOzellikEkleUrun.Items)
                Sayfalar.urunOzellikMDICmbx.cmbxUrun.Items.Add(data);

            Sayfalar.urunOzellikMDICmbx.cmbxKategori.SelectedIndex = cmbxOzellikEkleKategori.SelectedIndex;
            Sayfalar.urunOzellikMDICmbx.cmbxUrun.SelectedIndex = cmbxOzellikEkleUrun.SelectedIndex;

            cmbxIndexAyar();

            Sayfalar.urunOzellikMDICmbx.ShowDialog();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {/*AnaSayfa geçmek için*/
            Sayfalar.anaSayfa.Show();
            this.Hide();
        }

        private void btnStok_Click(object sender, EventArgs e)
        {/*UrunlerStok geçmek için*/
            Sayfalar.urunlerStok.Show();
            this.Hide();
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {/*UrunlerKategori geçmek için*/
            Sayfalar.urunlerKategori.Show();
            this.Hide();
        }

        private void btnUrun_Click(object sender, EventArgs e)
        {/*UrunlerUrun geçmek için*/
            Sayfalar.urunlerUrun.Show();
            this.Hide();
        }

        private void txtOzellikDuzenleBarkod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string deger = BL.BarkodAyar.hizliArama(int.Parse(txtOzellikDuzenleBarkod.Text));
                if (deger != "-1")
                {
                    string[] indisler = deger.Split('_');

                    cmbxOzellikDuzenleKategori.SelectedIndex = int.Parse(indisler[0]);
                    cmbxOzellikDuzenleKategoriYeni.SelectedIndex = int.Parse(indisler[0]);

                    cmbxOzellikDuzenleUrun.SelectedIndex = int.Parse(indisler[1]);
                    cmbxOzellikDuzenleUrunYeni.SelectedIndex = int.Parse(indisler[1]);

                    cmbxOzellikDuzenleOzellik.SelectedIndex = int.Parse(indisler[2]);
                    txtOzellikDuzenleOzellikYeni.Text = cmbxOzellikDuzenleOzellik.Text;

                    txtOzellikDuzenleBarkodYeni.Text = BL.UrunBilgisi.Urunler[int.Parse(indisler[0])].URUNLER[int.Parse(indisler[1])].URUNOZELLIK[int.Parse(indisler[2])].OZELLIK;
                    txtOzellikDuzenleBarkodYeni.Text = txtOzellikDuzenleBarkod.Text;

                    txtOzellikDuzenleFiyat.Text = BL.UrunBilgisi.Urunler[int.Parse(indisler[0])].URUNLER[int.Parse(indisler[1])].URUNOZELLIK[int.Parse(indisler[2])].FIYAT.ToString();
                    txtOzellikDuzenleFiyatYeni.Text = BL.UrunBilgisi.Urunler[int.Parse(indisler[0])].URUNLER[int.Parse(indisler[1])].URUNOZELLIK[int.Parse(indisler[2])].FIYAT.ToString();
                }
            }
            catch
            {

            }
            txtOzellikDuzenleBarkod.Select(txtOzellikDuzenleBarkod.Text.Length, 0);
        }

        private void txtOzellikDuzenleBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Keys.Back.GetHashCode())
                txtOzellikDuzenleBarkod.Text = "";
        }

        private void txtOzellikSilBarkod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string deger = BL.BarkodAyar.hizliArama(int.Parse(txtOzellikSilBarkod.Text));
                if (deger != "-1")
                {
                    string[] indisler = deger.Split('_');

                    cmbxOzellikSilKategori.SelectedIndex = int.Parse(indisler[0]);

                    cmbxOzellikSilUrun.SelectedIndex = int.Parse(indisler[1]);

                    cmbxOzellikSilOzellik.SelectedIndex = int.Parse(indisler[2]);
                }
            }
            catch
            {

            }
            txtOzellikSilBarkod.Select(txtOzellikSilBarkod.Text.Length, 0);
        }

        private void txtOzellikSilBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Keys.Back.GetHashCode())
                txtOzellikSilBarkod.Text = "";
        }

        private void btn_KisaYol_Click(object sender, EventArgs e)
        {
            Sayfalar.urunlerKisaYol.Show();
            this.Hide();
        }

        private void UrunlerOzellik_Shown(object sender, EventArgs e)
        {
            cmbxDoldur();
            cmbxIndexAyar();
        }

        private void cmbxOzellikDuzenleKategoriYeni_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbxOzellikDuzenleUrunYeni.Controls.Clear();
            for(int i = 0; i < BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategoriYeni.SelectedIndex].URUNLER.Count; i++)
            {
                cmbxOzellikDuzenleUrunYeni.Items.Add(BL.UrunBilgisi.Urunler[cmbxOzellikDuzenleKategoriYeni.SelectedIndex].URUNLER[i].URUN);
            }
        }
    }
}
