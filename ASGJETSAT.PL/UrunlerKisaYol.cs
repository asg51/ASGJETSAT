using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASGJETSAT.Entities;

namespace ASGJETSAT.PL
{
    public partial class UrunlerKisaYol : Form
    {
        public UrunlerKisaYol()
        {
            //var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            //connectionStringsSection.ConnectionStrings["Q"].ConnectionString = "-1";
            //config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("connectionStrings");
            //MessageBox.Show(ConfigurationManager.ConnectionStrings["Q"].ConnectionString);
        }

        private void cmbxKısaYolKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxKısaYolKategori.SelectedIndex != -1)
            {
                cmbxKısaYolUrun.Items.Clear();
                cmbxKısaYolUrun.Text = "";
                try
                {
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER.Count; i++)
                    {
                        cmbxKısaYolUrun.Items.Add(BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[i].URUN);
                        cmbxKısaYolUrun.SelectedIndex = 0;
                    }
                }
                catch
                {
                    cmbxKısaYolUrun.SelectedIndex = -1;
                }
            }
        }

        private void cmbxKısaYolUrun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxKısaYolUrun.SelectedIndex != -1)
            {
                cmbxKısaYolOzellik.Items.Clear();
                cmbxKısaYolOzellik.Text = "";
                try
                {
                    for (int i = 0; i < BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK.Count; i++)
                    {
                        cmbxKısaYolOzellik.Items.Add(BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK[i].OZELLIK);
                        cmbxKısaYolOzellik.SelectedIndex = 0;
                    }
                }
                catch
                {
                    cmbxKısaYolOzellik.SelectedIndex = -1;
                }
            }
        }

        private void txtKısaYolBarkod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string deger = BL.BarkodAyar.hizliArama(int.Parse(txtKısaYolBarkod.Text));
                if (deger != "-1")
                {
                    string[] indisler = deger.Split('_');

                    cmbxKısaYolKategori.SelectedIndex = int.Parse(indisler[0]);

                    cmbxKısaYolUrun.SelectedIndex = int.Parse(indisler[1]);

                    cmbxKısaYolOzellik.SelectedIndex = int.Parse(indisler[2]);

                }
            }
            catch
            {

            }
            txtKısaYolBarkod.Select(txtKısaYolBarkod.Text.Length, 0);
        }

        private void txtKısaYolBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Keys.Back.GetHashCode())
                txtKısaYolBarkod.Text = "";
        }

        public void UrunlerKisaYol_Load(object sender, EventArgs e)
        {
            
        }

        private void klavyeAyar()
        {
            if (ConfigurationManager.ConnectionStrings["Q"].ConnectionString != "-1")
                btn_Q.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_Q.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["W"].ConnectionString != "-1")
                btn_W.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_W.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["E"].ConnectionString != "-1")
                btn_E.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_E.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["R"].ConnectionString != "-1")
                btn_R.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_R.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["T"].ConnectionString != "-1")
                btn_T.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_T.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["Y"].ConnectionString != "-1")
                btn_Y.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_Y.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["U"].ConnectionString != "-1")
                btn_U.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_U.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["I"].ConnectionString != "-1")
                btn_I.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_I.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["O"].ConnectionString != "-1")
                btn_O.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_O.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["P"].ConnectionString != "-1")
                btn_P.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_P.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["Ğ"].ConnectionString != "-1")
                btn_GG.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_GG.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["Ü"].ConnectionString != "-1")
                btn_UU.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_UU.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["A"].ConnectionString != "-1")
                btn_A.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_A.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["S"].ConnectionString != "-1")
                btn_S.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_S.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["D"].ConnectionString != "-1")
                btn_D.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_D.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["F"].ConnectionString != "-1")
                btn_F.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_F.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["G"].ConnectionString != "-1")
                btn_G.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_G.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["H"].ConnectionString != "-1")
                btn_H.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_H.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["J"].ConnectionString != "-1")
                btn_J.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_J.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["K"].ConnectionString != "-1")
                btn_K.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_K.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["L"].ConnectionString != "-1")
                btn_L.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_L.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["Ş"].ConnectionString != "-1")
                btn_SS.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_SS.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["İ"].ConnectionString != "-1")
                btn_II.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_II.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["Z"].ConnectionString != "-1")
                btn_Z.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_Z.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["X"].ConnectionString != "-1")
                btn_X.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_X.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["C"].ConnectionString != "-1")
                btn_C.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_C.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["V"].ConnectionString != "-1")
                btn_V.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_V.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["B"].ConnectionString != "-1")
                btn_B.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_B.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["N"].ConnectionString != "-1")
                btn_N.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_N.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["M"].ConnectionString != "-1")
                btn_M.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_M.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["Ö"].ConnectionString != "-1")
                btn_OO.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_OO.BackColor = Color.Black;

            if (ConfigurationManager.ConnectionStrings["Ç"].ConnectionString != "-1")
                btn_CC.BackColor = Color.FromArgb(20, 20, 20);
            else
                btn_CC.BackColor = Color.Black;
        }

        private void cmbxDoldur()/*comboboxları structdaki bilgilerie dolduracak kısım.*/
        {
            cmbxKısaYolKategori.Items.Clear();

            for (int i = 0; i < BL.UrunBilgisi.Urunler.Count; i++)
            {
                cmbxKısaYolKategori.Items.Add(BL.UrunBilgisi.Urunler[i].KATEGORI);
            }

        }

        private void cmbxIndexAyar()/*comboboxları selected index ayarı yapılacak kısım.*/
        {
            try
            {
                cmbxKısaYolKategori.SelectedIndex = 0;
            }
            catch
            {
                cmbxKısaYolKategori.SelectedIndex = -1;
            }

            try
            {
                cmbxKısaYolUrun.SelectedIndex = 0;
            }
            catch
            {
                cmbxKısaYolUrun.SelectedIndex = -1;
            }

            try
            {
                cmbxKısaYolOzellik.SelectedIndex = 0;
            }
            catch
            {
                cmbxKısaYolOzellik.SelectedIndex = -1;
            }
        }


        private void cmbxKısaYolOzellik_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxKısaYolOzellik.SelectedIndex != -1)
            {
                txtKısaYolBarkod.Text = BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK[cmbxKısaYolOzellik.SelectedIndex].BARKOD.ToString();
                try
                {
                    lbl_KısaYol.Text = "";
                    foreach (ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
                    {
                        if (ConfigurationManager.ConnectionStrings[connection.Name].ConnectionString == BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK[cmbxKısaYolOzellik.SelectedIndex].ID.ToString())
                        {
                            lbl_KısaYol.Text = connection.Name;
                            break;
                        }

                    }
                    if (lbl_KısaYol.Text == "")
                        lbl_KısaYol.Text = "Yok";
                }
                catch
                {

                }
            }
        }

        private void Klavye_Click(object sender, EventArgs e)
        {
            string karakter = (sender as Button).Name;

            for (int i = 0; i < karakter.Length; i++)
            {
                if (karakter[i] == '_')
                {
                    karakter = karakter.Substring(i + 1);
                    break;
                }
            }

            switch (karakter)
            {
                case "GG":
                    {
                        lbl_KısaYol.Text = "Ğ";
                        break;
                    }
                case "UU":
                    {
                        lbl_KısaYol.Text = "Ü";
                        break;
                    }
                case "SS":
                    {
                        lbl_KısaYol.Text = "Ş";
                        break;
                    }
                case "II":
                    {
                        lbl_KısaYol.Text = "İ";
                        break;
                    }
                case "OO":
                    {
                        lbl_KısaYol.Text = "Ö";
                        break;
                    }
                case "CC":
                    {
                        lbl_KısaYol.Text = "Ç";
                        break;
                    }
                default:
                    {
                        lbl_KısaYol.Text = karakter;
                        break;
                    }
            }

            string[] indexler = BL.KisaYolAyar.hizliArama(int.Parse(ConfigurationManager.ConnectionStrings[lbl_KısaYol.Text].ConnectionString)).Split('_');
            if (indexler[0] != "-1")
            {
                cmbxKısaYolKategori.SelectedIndex = int.Parse(indexler[0]);
                cmbxKısaYolUrun.SelectedIndex = int.Parse(indexler[1]);
                cmbxKısaYolOzellik.SelectedIndex = int.Parse(indexler[2]);
            }
        }

        private void btnKisaYolSil_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
                {
                    if (ConfigurationManager.ConnectionStrings[connection.Name].ConnectionString == BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK[cmbxKısaYolOzellik.SelectedIndex].ID.ToString() && connection.Name == lbl_KısaYol.Text)
                    {
                        DialogResult dialogResult = MessageBox.Show("Tuş Atamasını Kaldırmak İstiyorumusunuz?", "Atama Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (dialogResult == DialogResult.Yes)
                        {
                            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                            connectionStringsSection.ConnectionStrings[connection.Name].ConnectionString = "-1";
                            config.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("connectionStrings");


                            klavyeAyar();

                            lbl_KısaYol.Text = "Yok";
                            MessageBox.Show("Tuş Ataması Kaldırılmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }

                }
            }
            catch
            {

            }
        }

        private void KisaYolEkle()
        {
            foreach (ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
            {/////////////////
                if (ConfigurationManager.ConnectionStrings[connection.Name].ConnectionString == BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK[cmbxKısaYolOzellik.SelectedIndex].ID.ToString().ToString())
                {
                    var config1 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var connectionStringsSection1 = (ConnectionStringsSection)config1.GetSection("connectionStrings");
                    connectionStringsSection1.ConnectionStrings[connection.Name].ConnectionString = "-1";
                    config1.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("connectionStrings");
                    break;
                }

            }

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[lbl_KısaYol.Text].ConnectionString = BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK[cmbxKısaYolOzellik.SelectedIndex].ID.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
            klavyeAyar();

            MessageBox.Show("Kısa Yol Atanmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnKisaYolEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[lbl_KısaYol.Text].ConnectionString == "-1")
                {
                    KisaYolEkle();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Bu Kısa Yol Kullanılmaktadır, Bu ürüne atamak istiyormusunuz?", "Hata!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        KisaYolEkle();
                    }
                    else
                    {
                        foreach (ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
                        {
                            if (ConfigurationManager.ConnectionStrings[connection.Name].ConnectionString == BL.UrunBilgisi.Urunler[cmbxKısaYolKategori.SelectedIndex].URUNLER[cmbxKısaYolUrun.SelectedIndex].URUNOZELLIK[cmbxKısaYolOzellik.SelectedIndex].ID.ToString())
                            {
                                lbl_KısaYol.Text = ConfigurationManager.ConnectionStrings[connection.Name].ConnectionString;
                                break;
                            }
                        }
                        if (lbl_KısaYol.Text != "Yok")
                            lbl_KısaYol.Text = "Yok";
                    }
                }
            }
            catch
            {

            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            /*AnaSayfa geçis kodu*/
            Sayfalar.anaSayfa.Show();
            this.Hide();
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {
            /*UrunlerKategori geçis kodu*/
            Sayfalar.urunlerKategori.Show();
            this.Hide();
        }

        private void btnUrun_Click(object sender, EventArgs e)
        {
            /*UrunlerUrun geçis kodu*/
            Sayfalar.urunlerUrun.Show();
            this.Hide();
        }

        private void btnOzellik_Click(object sender, EventArgs e)
        {
            /*UrunlerOzellik geçis kodu*/
            Sayfalar.urunlerOzellik.Show();
            this.Hide();
        }

        private void btnStok_Click(object sender, EventArgs e)
        {
            Sayfalar.urunlerStok.Show();
            this.Hide();
        }

        private void UrunlerKisaYol_Shown(object sender, EventArgs e)
        {
            cmbxDoldur();
            cmbxIndexAyar();
            klavyeAyar();
        }
    }
}
