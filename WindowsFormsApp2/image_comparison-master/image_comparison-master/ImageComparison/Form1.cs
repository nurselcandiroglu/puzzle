using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace ImageComparison
{
    public partial class Form1 : Form
    {
        int g11, g22, r11, r22, b11, b22,x1,y1,z1;
        Color a,b;
        
        Bitmap img1, img2;
        int i, j;
        public Form1()
        {
            InitializeComponent();
        }

        static string fname1, fname2;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            pictureBox1.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Images"; 
            openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png"; 

            openFileDialog1.ShowDialog(); 
            if (openFileDialog1.FileName.ToString() != "")
            {
                fname1 = openFileDialog1.FileName.ToString();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog2.FileName = ""; 
            openFileDialog2.Title = "Images"; 
            openFileDialog2.Filter = "All Images|*.jpg; *.bmp; *.png"; 

            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileName.ToString() != "")
            {
                fname2 = openFileDialog2.FileName.ToString();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            
            img1 = new Bitmap(fname1);
            img2 = new Bitmap(fname2);
           



            progressBar1.Maximum = img1.Width;
            
            
                for ( i = 0; i < img1.Width; i++)
                {
                    for ( j = 0; j < img1.Height; j++)
                    {
                        
                       
                         a = img1.GetPixel(i, j);
                         b = img2.GetPixel(i, j);

                       
                       
                    }
                    
                    
                }


                String g1 = a.G.ToString();
               

                String g2 = b.G.ToString();
                String r1 = a.R.ToString();
                String r2 = b.R.ToString();
                String b1 = a.R.ToString();
                String b2 = b.R.ToString();
                 g11 = Convert.ToInt16(g1);
                 g22 = Convert.ToInt16(g2);
                 r11 = Convert.ToInt16(r1);
                 r22 = Convert.ToInt16(r2);
                 b11 = Convert.ToInt16(b1);
                 b22 = Convert.ToInt16(b2);
                
            if((g11>g22)&&(r11>r22)&&(b11>b22))
            {
                x1 = g11 - g22;
                y1 = r11 - r22;
                z1 = b11 - b22;
            }
            else
            {
                x1 = g22 - g11;
                y1 = r22 - r11;
                z1 = b22 - b11;
                
                    
             
            }

           



                 if ( (x1 > 20) || (y1 > 20) ||  (z1 > 20))
                 {


                  
                     
                     MessageBox.Show("Sorry, Images are not same ");
                     MessageBox.Show("The values of RGB of 1st image are R= "+r1+"G="+g1+"B="+b1+" And the values of Rgb of second Image are R="+r2+"G="+g2+"B="+b2);

                 }

                 else
                 {
                     
                     MessageBox.Show(" Images are same  ");
                     MessageBox.Show("The values of RGB of 1st image are R= " + r1 + "G=" + g1 + "B=" + b1 + " And the values of Rgb of second Image are R=" + r2 + "G=" + g2 + "B=" + b2);
                     
                     
                    
                    
                 }
            }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void add_Click(object sender, EventArgs e)
        {

        }
          
        }
    }
