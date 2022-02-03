using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Banca_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox3.UseSystemPasswordChar = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }
        static string Nome;
        static string Cognome;
        public static string getName()
        {
            return Nome;
        }
        public static string getCognome()
        {
            return Cognome;
        }
        //Read something from text
        private void button1_Click(object sender, EventArgs e)
        {
            //path
            string path = @"IdentitàConti.txt";
            //output


            //verification
            if (!File.Exists(path))
            {
                //file non esiste
                MessageBox.Show("File doesn't exist");

                using (StreamWriter write = File.CreateText(path))
                {
                    MessageBox.Show("New File Created");
                }
            }
            else
            {
                StreamReader verifica = new StreamReader(path);
                string riga;
                while ((riga = verifica.ReadLine()) != null)
                {
                    string[] array = System.Text.RegularExpressions.Regex.Split(riga, @",");
                    if (array[0] == textBox1.Text && array[1] == textBox2.Text && array[2] == textBox3.Text)
                    {
                        MessageBox.Show("Login successful");
                        verifica.Close();
                        Nome = array[0];
                        Cognome = array[1];
                        this.Hide();
                        Form3 form3 = new Form3();
                        form3.Show();
                        break;
                    }
                    else if (array[0] != textBox1.Text && array[1] == textBox2.Text && array[2] == textBox3.Text)
                    {
                        MessageBox.Show("Wrong Name,Retry please");
                        verifica.Close();
                        break;
                    }
                    else if (array[0] == textBox1.Text && array[1] != textBox2.Text && array[2] == textBox3.Text)
                    {
                        MessageBox.Show("Wrong Username,Retry please");
                        verifica.Close();
                        break;
                    }
                    else if (array[0] == textBox1.Text && array[1] == textBox2.Text && array[2] != textBox3.Text)
                    {
                        MessageBox.Show("Wrong Password Retry please");
                        verifica.Close();
                        break;
                    }
                }
                verifica.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"IdentitàConti.txt";
            listBox1.Items.Clear();
            //stampa tutto il contenuto della txt
            using (StreamReader fs = new StreamReader(path))
            {
                string riga;
                while ((riga = fs.ReadLine()) != null)
                {
                    listBox1.Items.Add(riga.Replace(",", " "));
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsPunctuation(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
        }
    }
}
