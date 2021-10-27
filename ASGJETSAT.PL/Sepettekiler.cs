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
    public partial class Sepettekiler : Form
    {
        FlowLayoutPanel[] _flowLayoutPanel;
        TableLayoutPanel[] _MenuTableLayoutPanel;
        Label[] _lblSepetAdi;
        Label[] _lblUcret;
        Button[] _btnOnayla;


        public Sepettekiler()
        {
            InitializeComponent();
        }

        public void sepettekileriEkle()
        {
            flowLayoutPanelSepetekiler.Controls.Clear();
            nesneleriOlustur();
            nesneleriDoldur();
        }

        private void nesneleriOlustur()
        {
            _flowLayoutPanel = new FlowLayoutPanel[BL.SepetAyar.ListeFiyat.Count];
            _MenuTableLayoutPanel = new TableLayoutPanel[BL.SepetAyar.ListeFiyat.Count];
            _lblSepetAdi = new Label[BL.SepetAyar.ListeFiyat.Count];
            _lblUcret = new Label[BL.SepetAyar.ListeFiyat.Count];
            _btnOnayla = new Button[BL.SepetAyar.ListeFiyat.Count];
        }

        private void nesneleriDoldur()
        {
            for (int i = 0; i < BL.SepetAyar.ListeFiyat.Count; i++)
            {

                _flowLayoutPanel[i] = new FlowLayoutPanel(); //yeni bir oluşturyoruz.
                _flowLayoutPanel[i].Height = 40;
                _flowLayoutPanel[i].Width = flowLayoutPanelSepetekiler.Width -40;
                _flowLayoutPanel[i].Margin = new Padding(0);
                _flowLayoutPanel[i].BackColor = Color.Yellow;
                _flowLayoutPanel[i].Dock = DockStyle.Top;// uste sıralı şekilde ayarlıyoruz.

                _MenuTableLayoutPanel[i] = new TableLayoutPanel(); //her nesnemiz duzenli olsun diye hepsine TableLayoutPanel oluştuuryoruz.
                _MenuTableLayoutPanel[i].Location = new System.Drawing.Point(0, 0);
                _MenuTableLayoutPanel[i].Name = "MenuTableLayoutPanel-" + i.ToString();
                _MenuTableLayoutPanel[i].ColumnCount = 3;
                _MenuTableLayoutPanel[i].RowCount = 1;
                _MenuTableLayoutPanel[i].Size = new System.Drawing.Size(_flowLayoutPanel[i].Width, _flowLayoutPanel[i].Height);
                if(i%2==0)
                _MenuTableLayoutPanel[i].BackColor = Color.Black;
                else
                _MenuTableLayoutPanel[i].BackColor = Color.FromArgb(20,20,20);
                _MenuTableLayoutPanel[i].Margin = new Padding(0);


                _MenuTableLayoutPanel[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                _MenuTableLayoutPanel[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
                _MenuTableLayoutPanel[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
                _MenuTableLayoutPanel[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

                _lblSepetAdi[i] = new Label();
                _lblSepetAdi[i].Text = "Sepet " + (i+1).ToString();
                _lblSepetAdi[i].Name = "LabelsSepet-" + i.ToString();
                _lblSepetAdi[i].ForeColor = Color.White;
                _lblSepetAdi[i].Anchor = AnchorStyles.None;
                _lblSepetAdi[i].Margin = _MenuTableLayoutPanel[i].Margin;
                _lblSepetAdi[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                _MenuTableLayoutPanel[i].Controls.Add(_lblSepetAdi[i], 0, 0);//menuye 0 satırın 0 sutuna eklenmesini istiyoruz.


                _lblUcret[i] = new Label();
                _lblUcret[i].Text = BL.SepetAyar.ListeFiyat[i].ToString() + " TL";
                _lblUcret[i].Name = "LabelsUrunFiyat-" + i.ToString();
                _lblUcret[i].ForeColor = Color.White;
                _lblUcret[i].Anchor = AnchorStyles.None;
                _lblUcret[i].Margin = _MenuTableLayoutPanel[i].Margin;
                _lblUcret[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                _MenuTableLayoutPanel[i].Controls.Add(_lblUcret[i], 1, 0);//menuye 0 satırın 1 sutuna eklenmesini istiyoruz.

                _btnOnayla[i] = new Button();/*urun ozelliklerin adet birer birer artırmak için kullanılacak button.*/
                if (i % 2 == 0)
                    _btnOnayla[i].BackColor = Color.FromArgb(20, 20, 20);
                else
                {
                    _btnOnayla[i].BackColor = Color.Black;
                    _btnOnayla[i].FlatAppearance.MouseOverBackColor = Color.FromArgb(10, 10, 10);
                }
                _btnOnayla[i].ForeColor = Color.White;
                _btnOnayla[i].Name = "ButtonsOnayla-" + i.ToString();
                _btnOnayla[i].Text = "Onayla";
                _btnOnayla[i].FlatAppearance.BorderSize = 0;
                _btnOnayla[i].Margin = _MenuTableLayoutPanel[i].Margin;
                _btnOnayla[i].Dock = DockStyle.Fill;
                _btnOnayla[i].FlatStyle = FlatStyle.Flat;
                _btnOnayla[i].Click += _btnOnayla_Click;/*ortak click event kullanımı veriyoruz*/

                _MenuTableLayoutPanel[i].Controls.Add(_btnOnayla[i], 2, 0);//menuye 0 satırın 3 sutuna eklenmesini istiyoruz.

                _flowLayoutPanel[i].Controls.Add(_MenuTableLayoutPanel[i]);
                flowLayoutPanelSepetekiler.Controls.Add(_flowLayoutPanel[i]);
            }
        }

        int indexbul(string data)
        {
            for(int i=0;i<data.Length;i++)
            {
                if (data[i] == '-')
                    return int.Parse(data.Substring(i + 1));
            }
            return -1;
        }

        private void _btnOnayla_Click(object sender, EventArgs e)
        {
            Sayfalar.jETSAT.SepetGetir(indexbul((sender as Button).Name));
            this.Hide();
        }

        public void sepettekileriGoster()
        {
            nesneleriOlustur();
        }

        private void btn_Cikis_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_Temizle_Click(object sender, EventArgs e)
        {
            BL.SepetAyar.ListeFiyat.Clear();
            BL.SepetAyar.ListeSepet.Clear();
            flowLayoutPanelSepetekiler.Controls.Clear();
            this.Hide();
        }
    }
}
