//160202004-nursel çandıroğlu
//150202075-ayşin rodop
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;

namespace WindowsFormsApp2
{

    public partial class Form1 : Form
    {
        int x = 0;
        Button[] bt = new Button[2];
        Image temp;
        Bitmap orginal;
        List<Image> karismamis = new List<Image>();
        List<Image> karismis = new List<Image>();
        List<Image> karismis2 = new List<Image>();//karismis resimleri bu dizide tuttuk
        int skor = 100;

        public Form1()
        {
            InitializeComponent();
            button1.Click += new System.EventHandler(click);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private static void dosyayaYaz(int p)
        {
            string fileName = @"C:\Users\Nursel\Desktop\enyuksekskor.txt";
            //dosya yolu bulundu
            string writeText = Convert.ToString(p);

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

            fs.Close();
            File.AppendAllText(fileName, Environment.NewLine + writeText);
            //son satıra ekler

        }
        private void dosyadanOku()
        {

            string dosya_yolu = @"C:\Users\Nursel\Desktop\enyuksekskor.txt";
            //Okuma işlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            string yazi = sw.ReadLine();

            int skor, q = 0;
            if (yazi != null && yazi != "")
            {
                skor = Convert.ToInt32(yazi);
                q = skor;

            }

            string y;
            while (yazi != null)
            {
                yazi = sw.ReadLine();
                if (yazi != null && yazi != "")
                {
                    skor = Convert.ToInt32(yazi);

                    if (q < skor)
                    {
                        q = skor;
                    }
                }

            }
            y = Convert.ToString(q);
            textBox1.Text = y;

            //Satır satır okuma işlemini gerçekleştirdik ve ekrana yazdırdık
            //Son satır okunduktan sonra okuma işlemini bitirdik
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.

        }



        private void button33_Click(object sender, EventArgs e)
        {
            dosyadanOku();

            foreach (Button b in panel1.Controls)
                b.Enabled = true;



            //fotoğraf boyutunu 600*600 yaptık

            if (karismis.Count == 0)
                karismis = karismamis;

            karismis2.Clear();// tekrar karıştırma işlemi yapabilmek için
            butonlardaKarGoster();


            yanlisSayisi = 0;
            for (int i = 0; i < 16; i++)
            {
                esitlik(karismis2[i], karismamis[i]);
            }

            Console.WriteLine("yanlis sasyısı::::::::::" + yanlisSayisi);
            if (yanlisSayisi == 0)//tüm parçalar doğru yerdeyse tam puan verin
            {

                MessageBox.Show("tüm parçalar doğru yerde   SKOR:" + Convert.ToString(skor));

                dosyayaYaz(skor);
            }
            if (yanlisSayisi == 16)//tüm parçalar yanlış yerdeyse bi daha karıştırma işlemi yapıyor.
            {
                MessageBox.Show("tekrar karıştır");
                button19.Enabled = false;
            }
            else
                button19.Enabled = true;

        }

        private void butonlardaKarGoster()
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            arr = karistir(arr);

            foreach (Button b in panel1.Controls)
            {
                if (i < arr.Length)
                {
                    b.Image = karismis[arr[i]];  // şurada butonlarda image atandığını gösteriyoruz
                    karismis2.Add(karismis[arr[i]]); // karisik resimleri karisik2 dizisinde tutuyoruz.
                    i++;
                }


            }

        }

        private int[] karistir(int[] arr)
        {
            Random r = new Random();
            arr = arr.OrderBy(x => r.Next()).ToArray();
            return arr;
        }

        private void parcala(Image orginal, int w, int h)
        {
            karismamis.Clear();//yeni resim açmak için
            Bitmap bmp = new Bitmap(w, h);
            Graphics graphic = Graphics.FromImage(bmp);

            graphic.DrawImage(orginal, 0, 0, w, h);

            int movr = 0, movd = 0;
            for (int x = 0; x <= 15; x++)
            {
                Bitmap parca = new Bitmap(150, 150);

                for (int i = 0; i < 150; i++)
                    for (int j = 0; j < 150; j++)
                        parca.SetPixel(i, j, bmp.GetPixel(i + movr, j + movd));
                karismamis.Add(parca);
                movr += 150;

                if (movr == 600)
                {
                    movr = 0;
                    movd += 150;

                }

            }

        }


        int yanlisSayisi = 0;
        public void esitlik(Image img1, Image img2)
        {
            Bitmap rsm1 = new Bitmap(img1);
            Bitmap rsm2 = new Bitmap(img2);
            bool control = true;
            for (int x = 0; x < 150; x++)
            {
                for (int y = 0; y < 150; y++)
                {
                    if (rsm1.GetPixel(x, y).ToString() != rsm2.GetPixel(x, y).ToString())
                    {
                        control = false;//hatalı pixel
                    }
                }
            }

            if (control == false)
            {
                yanlisSayisi++;
            }

        }

        private void click(object sender, EventArgs e)
        {
            List<Image> asd = new List<Image>();
            Button s = ((Button)sender);
            s.Enabled = false;
            x = 0;

            foreach (Button b in panel1.Controls)
            {
                if (b.Enabled == false)
                {
                    bt[x] = b;


                    if (x == 1)
                    {
                        temp = bt[0].Image;
                        bt[0].Image = bt[1].Image;
                        bt[1].Image = temp;

                        foreach (Button ha in panel1.Controls)
                        {
                            asd.Add(ha.Image);
                        }

                        bt[0].Enabled = true;
                        bt[1].Enabled = true;
                    }
                    x++;
                }
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.InitialDirectory = "C:\\Users\\Nursel\\Desktop";
            dosya.ShowDialog();
            String resim = dosya.FileName;
            orginal = (Bitmap)Bitmap.FromFile(resim);
            parcala(orginal, 600, 600);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //// butonlar yer değiştikçe listeyi güncelliyor
            int i = 0;
            foreach (Button b in panel1.Controls)
            {
                if (i < 16)
                {
                    karismis2[i] = b.Image;
                    i++;
                }
            }
            /////
            yanlisSayisi = 0;
            for (i = 0; i < 16; i++)
            {
                esitlik(karismis2[i], karismamis[i]);
            }
            skor = (16 - yanlisSayisi) * 6;

            dosyayaYaz(skor);
            MessageBox.Show(yanlisSayisi + " tane parça yanlış yerde    SKOR:" + Convert.ToString(skor));
        }
    }
}