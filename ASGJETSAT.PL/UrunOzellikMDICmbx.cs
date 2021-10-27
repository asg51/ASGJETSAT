using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASGJETSAT.PL
{
    public partial class UrunOzellikMDICmbx : Form
    {
        private class MDICmbx

        {
            public TableLayoutPanel MenuTable;
            public Panel FlowLayoutPanel;
            public Label Label1;
            public Label Label2;
            public Label Label3;
            public TextBox TextBox1;
            public TextBox TextBox2;
            public TextBox TextBox3;

            public MDICmbx(TableLayoutPanel menuTable, Panel flowLayoutPanel, Label label1, Label label2,
                Label label3, TextBox textBox1, TextBox textBox2, TextBox textBox3)
            {
                this.MenuTable = menuTable;
                this.FlowLayoutPanel = flowLayoutPanel;
                this.Label1 = label1;
                this.Label2 = label2;
                this.Label3 = label3;
                this.TextBox1 = textBox1;
                this.TextBox2 = textBox2;
                this.TextBox3 = textBox3;
            }
        }

        private List<MDICmbx> ViewList = new List<MDICmbx>();
        private int _panelSayac = 0;
        public UrunOzellikMDICmbx()
        {

        }

        void panelEkle()
        {/*yeni eklencek ozellik girişlerini artırıyoruz.*/

            if (ViewList.Count < _panelSayac)
            {
                ViewList[_panelSayac].TextBox1.Text = "";
                ViewList[_panelSayac].TextBox2.Text = "";
                ViewList[_panelSayac].TextBox3.Text = "";
                flowLayoutPanel1.Controls.Add(ViewList[_panelSayac].FlowLayoutPanel);
            }
            else
            {
                Panel pnl = new Panel();/*her giriş nesnesini tek tutacak panel.*/
                pnl.Height = 60;
                pnl.Width = flowLayoutPanel1.Width - 40;
                pnl.Dock = DockStyle.Top;
                pnl.BackgroundImage = global::ASGJETSAT.PL.Properties.Resources.ArkaPlan;
                pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                pnl.BackColor = Color.Black;

                TableLayoutPanel table = new TableLayoutPanel();/*her ozelliklere giriş nesneleri duzenli tutan nesne.*/
                table.Location = new System.Drawing.Point(0, 0);
                table.Name = "MenuTableLayoutPanel1";
                table.ColumnCount = 9;
                table.RowCount = 1;
                table.Height = 60;
                table.Dock = DockStyle.Fill;
                table.Width = pnl.Width;
                // Add rows and columns  


                table.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100.0 / table.RowCount)));

                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));


                Label label1 = new Label();/*ozellik sırasını gosteren label nesnesi*/
                label1.Anchor = AnchorStyles.Right;
                label1.Text = (_panelSayac + 1).ToString() + ". Özellik";
                label1.Name = "lbl-" + (_panelSayac + 1).ToString();
                label1.ForeColor = Color.White;
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));

                Label label2 = new Label();/*fiyat sırasını gosteren label nesnesi*/
                label2.Anchor = AnchorStyles.Right;
                label2.Text = (_panelSayac + 1).ToString() + ". Fiyat";
                label2.Name = "lbl-" + (_panelSayac + 1).ToString();
                label2.ForeColor = Color.White;
                label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));

                Label label3 = new Label();/*fiyat sırasını gosteren label nesnesi*/
                label3.Anchor = AnchorStyles.Right;
                label3.Text = (_panelSayac + 1).ToString() + ". Barkod";
                label3.Name = "lbl-" + (_panelSayac + 1).ToString();
                label3.ForeColor = Color.White;
                label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));

                TextBox textBox1 = new TextBox();/*ozellik girişi alınacak textbox*/
                textBox1.Anchor = AnchorStyles.Left;

                TextBox textBox2 = new TextBox();/*fiyat girişi alınacak textbox*/
                textBox2.Anchor = AnchorStyles.Left;

                TextBox textBox3 = new TextBox();/*fiyat girişi alınacak textbox*/
                textBox2.Anchor = AnchorStyles.Left;

                table.Controls.Add(label1, 1, 0);/*eklenen nesnelerin menutablelayoutpanele duzenlı sekilde eklenmesi.*/
                table.Controls.Add(textBox1, 2, 0);
                table.Controls.Add(label2, 4, 0);
                table.Controls.Add(textBox2, 5, 0);
                table.Controls.Add(label3, 7, 0);
                table.Controls.Add(textBox3, 8, 0);

                pnl.Controls.Add(table);/*menutablelayoutpanele flowlayoutoanele ekliyoruz*/
                flowLayoutPanel1.Controls.Add(pnl);/*flowlayoutoaneli flowLayoutPanel1 olan bolume ekleyerek yeni ozelligimizi ekliyoruz.*/

                ViewList.Add(new MDICmbx(table, pnl, label1, label2, label3, textBox1, textBox2, textBox3));
            }
            _panelSayac++;/*sıradaki eklenecek panelin indexini tutuyor.*/
        }

        private void panelSil()
        {/*ozellik nesnelerini son olanını siliyor.*/
            _panelSayac--;/*silincek nesne indexi olması için bir azaltıyoruz.*/
            flowLayoutPanel1.Controls.Remove(ViewList[_panelSayac].FlowLayoutPanel);
        }

        private void btn_OzellikEkle_Click(object sender, EventArgs e)
        {/*btn_OzellikEkle Click eventine tıklandıgı an panel 50 den az ise panel eklenecek.*/
            panelEkle();
        }

        private void btn_OzellikSil_Click(object sender, EventArgs e)
        {/*btn_OzellikSil Click eventine tıklandıgı an paneller 2 dan az degilse panel silinecek.*/
            if (_panelSayac <= 2)
            {
                MessageBox.Show("2'den Daha Az Özellik Girilemez!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                panelSil();
            }
        }

        private void btn_KategoriEkleSil_Click(object sender, EventArgs e)
        {/*btn_KategoriEkleSil Click eventine tıklandıgı an form kapatılacak.*/
            this.Close();
            GC.SuppressFinalize(this);
        }

        private void btn_KategoriEkleEkle_Click(object sender, EventArgs e)
        {/*btn_KategoriEkleEkle Click eventine tıklandıgı an ekrandıki panellerin hepsinin fiyatları kontrol edilir hata vermez ve ozellik girişleri boş degilse hepsi eklenir.*/
            int durum = -1;
            bool IcerikDurumu = true;
            for (int i = 0; i < _panelSayac; i++)
            {
                try
                {
                    double.Parse(ViewList[i].TextBox2.Text);
                }
                catch
                {
                    MessageBox.Show((i + 1).ToString() + " Sayı Giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (ViewList[i].TextBox1.Text == "" || ViewList[i].TextBox2.Text == "")
                    IcerikDurumu = false;
            }
            if (IcerikDurumu)
            {
                for (int i = 0; i < _panelSayac; i++)
                {
                    string barkod = "";
                    if (ViewList[i].TextBox3.Text == "")
                    {
                        barkod = "-1";
                    }
                    else
                    {
                        barkod = ViewList[i].TextBox3.Text;
                    }
                    durum = BL.UrunUrunOzellik.OzellikEkle(BL.UrunBilgisi.Urunler[cmbxKategori.SelectedIndex].URUNLER[cmbxUrun.SelectedIndex].ID, ViewList[i].TextBox1.Text, int.Parse(barkod),
                        decimal.Parse(ViewList[i].TextBox2.Text), cmbxKategori.SelectedIndex, cmbxUrun.SelectedIndex);
                }


                if (durum != -1)
                {
                    Sayfalar.jETSAT.UrunOzellikEkle(cmbxKategori.SelectedIndex, cmbxUrun.SelectedIndex,
                           BL.UrunBilgisi.Urunler[cmbxKategori.SelectedIndex].URUNLER[cmbxUrun.SelectedIndex].URUNOZELLIK.Count - 1);
                    BL.BarkodAyar.BarkodSirala();
                    MessageBox.Show("Başarılı Bir Şekilde Kayıt Edilmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void UrunOzellikMDICmbx_Load(object sender, EventArgs e)
        {/*ekrana iki tane ozellik giriş eklenir.*/

        }

        private void UrunOzellikMDICmbx_Shown(object sender, EventArgs e)
        {
            while (_panelSayac < 2)
                panelEkle();
            while (_panelSayac > 2)
                panelSil();
        }
    }
}
