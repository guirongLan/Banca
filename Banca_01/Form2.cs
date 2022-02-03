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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox4.UseSystemPasswordChar = true;
            StreamReader tipologia = new StreamReader(@"tipologia.txt");
            string riga = tipologia.ReadLine();
            while (riga != null)
            {
                comboBox1.Items.Add(riga);
                riga = tipologia.ReadLine();
            }
            comboBox1.Text = "Standard";
            tipologia.Close();
        }


        //Generation password
        public string codice(string randomchars)
        {
            string MyCodice = "";
            int numero;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                numero = random.Next(randomchars.Length);
                MyCodice += randomchars[numero]; // equal to : MyCodice = MyCodice + randomchars[numero];
            }
            return MyCodice;
        }

        //Write something in the text
        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"IdentitàConti.txt";
            string path2 = @"I conti.txt";
            string randomchars = "abcdefghijklmnopqrstuvwxyz~!@#$%^&*()_+|0123456789~!@#$%^&*()_+|ABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+|";
            string password;
            DateTimePicker date1 = new DateTimePicker();
            date1.Value = DateTime.Now.AddYears(-16);
            //generate password
            if (textBox4.Text == "")
            {
                password = codice(randomchars);
            }
            else
            {
                password = textBox4.Text;
            }


            if (!File.Exists(path))
            {
                using (StreamWriter write = File.CreateText(path))
                {
                    MessageBox.Show("New File Created,click again");
                }
            }
            else
            {
                using (StreamWriter write = File.AppendText(path))
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Please write about your Name");
                    }
                    else if (textBox3.Text == "")
                    {
                        MessageBox.Show("Please write about your Username");
                    }
                    else if (textBox2.Text == "")
                    {
                        MessageBox.Show("Please write about your Address");
                    }
                    else if (comboBox1.Text == "")
                    {
                        MessageBox.Show("Please select the type of your Account");
                    }
                    else
                    {
                        bool verificazione = false;
                        //verifico se esiste gia il cliente
                        if (verificazione == false)
                        {
                            write.Close();
                            StreamReader verifica = new StreamReader(path);
                            string riga;
                            //controllo tutta la txt
                            while ((riga = verifica.ReadLine()) != null)
                            {
                                string[] array = System.Text.RegularExpressions.Regex.Split(riga, @",");

                                if (array[0] == textBox1.Text && array[1] == textBox2.Text)
                                {
                                    verificazione = true;
                                    MessageBox.Show("Username and name already used");
                                    break;
                                }
                            }
                            verifica.Close();
                        }
                        if (verificazione == false)
                        {
                            if (date1.Value >= dateTimePicker1.Value)
                            {
                                //nome              //password      //indirizzo             //tipologia 
                                StreamWriter write2 = File.AppendText(path);
                                write2.WriteLine(textBox1.Text + "," + textBox2.Text + "," + password + "," + textBox3.Text + "," + comboBox1.Text + ",");
                                MessageBox.Show("Account registration successful , this is your Password : " + password);
                                //siccome ho fatto un nuovo devo chiudero
                                write2.Close();
                                //scrivo dentro un nuovo file in cui vado gestire i conti
                                StreamWriter streamWriter = new StreamWriter(path2, true);
                                streamWriter.WriteLine(textBox1.Text + "," + textBox2.Text + "," + password + "," + comboBox1.Text + "," + "0.00"+ ","+ 0);
                                streamWriter.Close();
                                //cambio form
                                Form1 form1 = new Form1();
                                form1.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("devi avere 16 anni");
                            }
                        }

                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.UseSystemPasswordChar = false;
            }
            else
            {
                textBox4.UseSystemPasswordChar = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsPunctuation(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
