using System;
using System.Windows.Forms;

namespace MT
{
    public partial class Form1 : Form
    {
        public PictureBox[] imagenes = new PictureBox[121];
        public int count = 0;
        public int modulo = 0;
        public int tamaño = 3;
        public string w;

        public Form1()
        {

            InitializeComponent();
            CrearMatriz(11);
        }

        private void CrearMatriz(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(pictureBox)).BeginInit();
                    pictureBox.Location = new System.Drawing.Point(300 + (j * 47), 50 + (i * 47));
                    pictureBox.Size = new System.Drawing.Size(47, 47);
                    pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pictureBox.TabStop = false;
                    Controls.Add(pictureBox);
                    imagenes[(i * 11) + j] = pictureBox;

                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (count > textBox2.Text.Length && count > 0)
            {
                if (count % tamaño == 0) modulo--;
                count--;
                imagenes[(count % tamaño) + (modulo * 11)].Image = null;
            }
            else
            {
                if (textBox2.Text[count].Equals('x') || textBox2.Text[count].Equals('X'))
                {
                    imagenes[(count % tamaño) + (modulo * 11)].Image = global::MT.Properties.Resources.Equis;
                    count++;
                }
                else if (textBox2.Text[count].Equals('o') || textBox2.Text[count].Equals('O'))
                {
                    imagenes[(count % tamaño) + (modulo * 11)].Image = global::MT.Properties.Resources.Circulo;
                    count++;
                }
                if (count % tamaño == 0) modulo++;
            }
        }

        private void descartarletra(KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (count < tamaño * tamaño)
            {
                MessageBox.Show("Debe ingresar todos los valores, faltan : " + ((tamaño * tamaño) - count));
            }
            else
            {
                w = textBox2.Text;
                ValidarHorizontal(w, tamaño);
            }
        }
        public void ValidarHorizontal(string w, int tamaño)
        {
            int ganadorO = tamaño;
            int ganadorX = tamaño;
            for (int i = 0; i < tamaño; i++)
            {
                if (w[i * tamaño].Equals('x'))
                {
                    ganadorO -= 1;
                }
                else
                {
                    ganadorX -= 1;
                }
                for (int j = 1; j < tamaño; j++)
                {
                    if (!w[((i * tamaño) + j) - 1].Equals(w[((i * tamaño) + j)]))
                    {
                        ganadorO -= 1;
                        ganadorX -= 1;
                        break;
                    }

                }
            }
            MessageBox.Show("x: " + ganadorX + "     O: " + ganadorO);
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            descartarletra(e);
        }

        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && Convert.ToInt32(textBox1.Text) >= 3)
            {
                tamaño = Convert.ToInt32(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Debe ingresar un número mayor a 3");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar.Equals('x') || e.KeyChar.Equals('o')) && count < tamaño * tamaño)
            {
                e.Handled = false;
            }
            else
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
            if (count >= tamaño * tamaño) MessageBox.Show("Se han ingresado todos los valores ");
        }
    }
}