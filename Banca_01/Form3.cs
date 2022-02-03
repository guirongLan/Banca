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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            string nome = Form1.getName();
            string cognome = Form1.getCognome();
            string path = @"I conti.txt";
            string path2 = "tempo.txt";
            string pathTipologie = "tipologieDiOperazioni.txt";
            int i = 0, j = 0;
            if (!File.Exists(path))
            {
                using (StreamWriter write = File.CreateText(path))
                {
                    MessageBox.Show("New File Created");
                }
            }
            else
            {
                StreamReader streamReader = new StreamReader(path);
                string riga;
                while ((riga = streamReader.ReadLine()) != null)
                {
                    i++;
                }
                Conti_Correnti[] conti = new Conti_Correnti[i];
                streamReader.Close();

                using (streamReader = new StreamReader(path))
                {
                    string riga2;
                    while ((riga2 = streamReader.ReadLine()) != null)
                    {
                        string[] array = System.Text.RegularExpressions.Regex.Split(riga2, @",");
                        conti[j] = new Conti_Correnti(array[0], array[1], array[2], array[3], Convert.ToDouble(array[4]), Convert.ToInt32(array[5]));
                        j++;
                    }
                }

                using (streamReader = new StreamReader(path))
                {
                    for (int k = 0; k < i; k++)
                    {
                        if (conti[k].getName() == nome && conti[k].getCognome() == cognome)
                        {
                            label1.Text = Convert.ToString(conti[k].getSoldi());
                            label7.Text = conti[k].getType();
                        }
                    }
                }

                if (!File.Exists(pathTipologie))
                {
                    using (StreamWriter write = File.CreateText(pathTipologie))
                    {
                        MessageBox.Show("New File Created");
                        write.WriteLine("Deposito");
                        write.WriteLine("Pagamento");
                        write.WriteLine("Bonifici");
                    }
                    Stampa(pathTipologie);
                }
                else
                {
                    Stampa(pathTipologie);
                }
            }


        }

        static int i = 0;
        public Conti_Correnti[] conti = new Conti_Correnti[i];

        public Conti_Correnti[] Crea(string path)//crea conti
        {
            i = 0;
            int j = 0;
            StreamReader streamReader = new StreamReader(path);
            string riga;

            while ((riga = streamReader.ReadLine()) != null)
            {
                i++;
            }

            Conti_Correnti[] conti = new Conti_Correnti[i];
            streamReader.Close();

            using (streamReader = new StreamReader(path))
            {
                string riga2;
                while ((riga2 = streamReader.ReadLine()) != null)
                {
                    string[] array = System.Text.RegularExpressions.Regex.Split(riga2, @",");
                    conti[j] = new Conti_Correnti(array[0], array[1], array[2], array[3], Convert.ToDouble(array[4]), Convert.ToInt32(array[5]));
                    j++;
                }
            }
            return conti;
        }
        public void Stampa(string pathTipologie)
        {
            StreamReader tipologia = new StreamReader(pathTipologie);
            string riga2 = tipologia.ReadLine();
            while (riga2 != null)
            {
                comboBox1.Items.Add(riga2);
                riga2 = tipologia.ReadLine();
            }
            comboBox1.Text = "Deposito";
            tipologia.Close();
        }

        public void soprascrive(string path, int lenght, Conti_Correnti[] conti)
        {
            StreamWriter write = new StreamWriter(path);
            for (int i = 0; i < lenght; i++)
            {   //string Name,string Cognome, string Codice,string Type, double Soldi
                write.WriteLine(conti[i].getName() + "," + conti[i].getCognome() + "," + conti[i].getCodice() + "," + conti[i].getType() + "," + conti[i].getSoldi() + "," + conti[i].getDobbio() + ",");
            }
            write.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//utente puo inserire solo numeri e punto
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')
              )
            {
                e.Handled = true;
            }
        }
        static int contatore;
        int contatore1 = 0;
        public void StampaDatagridview(string a, double b, DateTimePicker date, string c, double d)
        {
            dataGridView1.RowCount = 50;
            dataGridView1.ColumnCount = 6;
            dataGridView1.Rows[0].Cells[0].Value = "Numero"; dataGridView1.Rows[0].Cells[1].Value = "Motivo";
            dataGridView1.Rows[0].Cells[2].Value = "Soldi"; dataGridView1.Rows[0].Cells[3].Value = "Data";
            dataGridView1.Rows[0].Cells[4].Value = "Tipo di account"; dataGridView1.Rows[0].Cells[5].Value = "Gestione operazione";
            dataGridView1.Rows[contatore1].Cells[0].Value = contatore1;
            dataGridView1.Rows[contatore1].Cells[1].Value = a;
            dataGridView1.Rows[contatore1].Cells[2].Value = b;
            dataGridView1.Rows[contatore1].Cells[3].Value = date.Value;
            dataGridView1.Rows[contatore1].Cells[4].Value = c;
            dataGridView1.Rows[contatore1].Cells[5].Value = d;
        }
        public void Negativo()
        {
            MessageBox.Show("Il tuo saldo risulta negativo ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = comboBox1.Text;
            string b = label7.Text;
            double gestione = 0;
            if (b == "Standard")
            {
                gestione = 1.50;
            }
            else if (b == "Giovani")
            {
                gestione = 1.0;
            }
            else if (b == "Over 60")
            {
                gestione = 0.5;
            }
            else
            {
                gestione = 5;
            }
            double soldi;
            bool verifica = false;
            if (textBox1.Text == "")
            {
                soldi = 0;
                verifica = true;
                MessageBox.Show("inserisci il soldi perfavore perfavore");
            }
            else
            {
                soldi = Convert.ToDouble(textBox1.Text);
            }
            if (verifica == false)
            {
                string nome = Form1.getName();
                string cognome = Form1.getCognome();
                string path = @"I conti.txt";
                Conti_Correnti[] conti = Crea(path);
                if (a == "Deposito")
                {
                    for (int k = 0; k < conti.Length; k++)
                    {
                        if (conti[k].getName() == nome && conti[k].getCognome() == cognome)
                        {
                            conti[k].Entrata(soldi, b);
                            if (conti[k].getDobbio() != 0)
                            {
                                int c = conti[k].getDobbio();
                                for (int i = k + 1; i < conti.Length; i++)
                                {
                                    if (c == conti[i].getDobbio())
                                    {
                                        conti[i].Entrata(soldi, b);
                                    }
                                }
                            }
                            label1.Text = Convert.ToString(conti[k].getSoldi());
                            MessageBox.Show("Operazione successo");
                            soprascrive(path, conti.Length, conti);
                            contatore1++;
                            if (contatore1 <= 50)
                            {
                                StampaDatagridview("Deposito", soldi, dateTimePicker1, b, gestione);
                            }
                            else
                            {
                                dataGridView1.Rows.Clear();
                                contatore1 = 1;
                                StampaDatagridview("Deposito", soldi, dateTimePicker1, b, gestione);
                            }
                            if (conti[k].getSoldi() < 0)
                            {
                                Negativo();
                            }
                            break;
                        }
                    }
                }
                else if (a == "Pagamento")
                {
                    for (int k = 0; k < conti.Length; k++)
                    {
                        if (conti[k].getName() == nome && conti[k].getCognome() == cognome)
                        {
                            conti[k].Uscita(soldi, b);

                            if (conti[k].getDobbio() != 0)
                            {
                                int c = conti[k].getDobbio();
                                for (int i = k + 1; i < conti.Length; i++)
                                {
                                    if (c == conti[i].getDobbio())
                                    {
                                        conti[i].Uscita(soldi, b);
                                    }
                                }
                            }
                            label1.Text = Convert.ToString(conti[k].getSoldi());
                            MessageBox.Show("Operazione successo");
                            soprascrive(path, conti.Length, conti);
                            contatore1++;
                            if (contatore1 <= 50)
                            {
                                StampaDatagridview("pagamento", -soldi, dateTimePicker1, b, gestione);
                            }
                            else
                            {
                                dataGridView1.Rows.Clear();
                                contatore1 = 1;
                                StampaDatagridview("pagamento", -soldi, dateTimePicker1, b, gestione);
                            }
                            if (conti[k].getSoldi() < 0)
                            {
                                Negativo();
                            }
                            break;
                        }
                    }
                }
                else if (a == "Bonifici")
                {
                    if (textBox2.Text == "" || textBox3.Text == "")
                    {
                        MessageBox.Show("inserisci il Nome o il Cognome di utente in cui vuole fare il bonifico");
                    }
                    else
                    {
                        for (int k = 0; k < conti.Length; k++)//verificare se esiste utente
                        {
                            if (conti[k].getName() == textBox2.Text && conti[k].getCognome() == textBox3.Text)
                            {
                                verifica = false;
                                conti[k].Entrata(soldi, b);

                                if (conti[k].getDobbio() != 0)
                                {
                                    int c = conti[k].getDobbio();
                                    for (int i = k + 1; i < conti.Length; i++)
                                    {
                                        if (c == conti[i].getDobbio())
                                        {
                                            conti[i].Entrata(soldi, b);
                                        }
                                    }
                                }
                                break;
                            }
                            else
                            {
                                verifica = true;
                            }
                        }
                        for (int k = 0; k < conti.Length; k++)
                        {
                            if (conti[k].getName() == nome && conti[k].getCognome() == cognome && verifica == false)
                            {
                                conti[k].Bonifici(soldi, b);

                                if (conti[k].getDobbio() != 0)
                                {
                                    int c = conti[k].getDobbio();
                                    for (int i = k + 1; i < conti.Length; i++)
                                    {
                                        if (c == conti[i].getDobbio())
                                        {
                                            conti[i].Bonifici(soldi, b);
                                        }
                                    }
                                }

                                label1.Text = Convert.ToString(conti[k].getSoldi());
                                soprascrive(path, conti.Length, conti);
                                contatore1++;
                                if (contatore1 <= 50)
                                {
                                    StampaDatagridview("Bonifico", -soldi, dateTimePicker1, b, gestione);
                                }
                                else
                                {
                                    dataGridView1.Rows.Clear();
                                    contatore1 = 1;
                                    StampaDatagridview("Bonifico", -soldi, dateTimePicker1, b, gestione);
                                }
                                MessageBox.Show("Operazione successo");
                                if (conti[k].getSoldi() < 0)
                                {
                                    Negativo();
                                }
                                break;
                            }
                            else if (verifica == true)
                            {
                                MessageBox.Show("Utente non esiste");
                                break;
                            }
                        }
                    }
                }
            }

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Bonifici")
            {
                label3.Visible = true;
                label4.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = Form1.getName();
            string cognome = Form1.getCognome();
            string path = @"I conti.txt";
            double a = 0, b = 0;
            bool dobbio = false;
            using (StreamReader conta = new StreamReader(path))
            {
                string riga;
                while ((riga = conta.ReadLine()) != null)
                {
                    string[] array = System.Text.RegularExpressions.Regex.Split(riga, @",");

                    if (Convert.ToInt32(array[5]) > contatore)
                    {
                        contatore = Convert.ToInt32(array[5]);
                    }
                }
            }
            Conti_Correnti[] conti = Crea(path);
            bool verifica = false;
            if (textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("inserisci il Nome o il Cognome di utente in cui vuole fare collegamento");
            }
            else
            {
                for (int k = 0; k < conti.Length; k++)//verificare se esiste utente
                {
                    if (conti[k].getName() == textBox4.Text && conti[k].getCognome() == textBox5.Text && conti[k].getDobbio() == 0)
                    {
                        b = conti[k].getSoldi();
                        contatore++;
                        verifica = false;
                        break;
                    }
                    else if (conti[k].getName() == textBox4.Text && conti[k].getCognome() == textBox5.Text && conti[k].getDobbio() != 0)
                    {
                        dobbio = true;
                        break;
                    }
                    else
                    {
                        verifica = true;
                    }
                }
                if (dobbio == true)
                {
                    MessageBox.Show("Utente gia collegato");
                }
                if (verifica == false && dobbio == false)
                {
                    for (int k = 0; k < conti.Length; k++)
                    {
                        if (conti[k].getName() == nome && conti[k].getCognome() == cognome && conti[k].getDobbio() == 0)
                        {
                            a = conti[k].getSoldi();
                            a = a + b;
                            conti[k].setSoldi(a);
                            conti[k].setDobbio(contatore);
                            MessageBox.Show("Collegamento col successo");
                            break;
                        }
                    }
                    for (int k = 0; k < conti.Length; k++)
                    {
                        if (conti[k].getName() == textBox4.Text && conti[k].getCognome() == textBox5.Text)
                        {
                            conti[k].setSoldi(a);
                            conti[k].setDobbio(contatore);
                            break;
                        }
                    }
                    if (verifica == false)
                    {
                        soprascrive(path, conti.Length, conti);
                    }
                }
                if (verifica == true && dobbio == false)
                {
                    MessageBox.Show("Utente non esiste");
                }
            }
        }
        static DateTimePicker date = new DateTimePicker();

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string path = "tempo.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter write = File.CreateText(path))
                {
                    MessageBox.Show("New File Created");
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader(path))
                {
                   date.Value = Convert.ToDateTime(reader.ReadLine());
                }
            }
            if (date.Value > dateTimePicker1.Value.Date)
            {
                MessageBox.Show("non puoi andare passato");
                dateTimePicker1.Value = date.Value;
            }
            else if (date.Value < dateTimePicker1.Value.Date)
            {
                date.Value = dateTimePicker1.Value.Date;
                if (dateTimePicker1.Value.Day == 1)
                {
                    string nome = Form1.getName();
                    string cognome = Form1.getCognome();
                    string path2 = @"I conti.txt";
                    Conti_Correnti[] conti = Crea(path2);
                    for (int k = 0; k < conti.Length; k++)
                    {
                        if (conti[k].getName() == nome && conti[k].getCognome() == cognome)
                        {
                            if (contatore1 <= 50)
                            {
                                conti[k].Entrata(1000, "none");////stipendio
                                contatore1++;
                                StampaDatagridview("stipendio", 1000, date, conti[k].getType(), 0);
                                conti[k].Uscita(10, "none");//tenuta conto
                                contatore1++;
                                StampaDatagridview("tenuta conto", 0, date, conti[k].getType(), 10);
                                soprascrive(path2, conti.Length, conti);
                                if (conti[k].getDobbio() != 0)
                                {
                                    for (int j = k + 1; j < conti.Length; j++)
                                    {
                                        if (conti[k].getDobbio() == conti[j].getDobbio())
                                        {
                                            conti[j].Entrata(1000, "none");//stipendi
                                            conti[j].Uscita(10, "none");//tenuta conto
                                            soprascrive(path2, conti.Length, conti);
                                            break;
                                        }
                                    }
                                }
                                label1.Text = Convert.ToString(conti[k].getSoldi());
                                break;
                            }
                            else
                            {
                                dataGridView1.Rows.Clear();
                                contatore1 = 1;
                                conti[k].Entrata(1000, "none");////stipendio
                                contatore1++;
                                StampaDatagridview("stipendio", 1000, date, conti[k].getType(), 0);
                                conti[k].Uscita(10, "none");//tenuta conto
                                contatore1++;
                                StampaDatagridview("tenuta conto", 0, date, conti[k].getType(), 10);
                                soprascrive(path2, conti.Length, conti);
                                if (conti[k].getDobbio() != 0)
                                {
                                    for (int j = k + 1; j < conti.Length; j++)
                                    {
                                        if (conti[k].getDobbio() == conti[j].getDobbio())
                                        {
                                            conti[j].Entrata(1000, "none");//stipendi
                                            conti[j].Uscita(10, "none");//tenuta conto
                                            soprascrive(path2, conti.Length, conti);
                                            break;
                                        }
                                    }
                                }
                                label1.Text = Convert.ToString(conti[k].getSoldi());
                                break;
                            }
                        }
                    }
                }
            }
            using(StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(date.Value);
            }

        }
    }
}
