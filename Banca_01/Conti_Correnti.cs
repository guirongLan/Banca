using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banca_01
{
    public class Conti_Correnti
    {
        string Name;
        string Cognome;
        string Codice;
        string Type;
        double Soldi;
        int Dobbio;

        public Conti_Correnti()
        {
            Name = "NONE";
            Cognome = "NONE";
            Codice = "NONE";
            this.Type = "NONE";
            Soldi = 0;
            Dobbio = 0; ;

        }
        public Conti_Correnti(string Name,string Cognome, string Codice,string Type, double Soldi,int Dobbio)
        {
            this.Name = Name;
            this.Cognome = Cognome;
            this.Codice = Codice;
            this.Type = Type;
            this.Soldi = Soldi;
            this.Dobbio = Dobbio;

        }
        public string getName()
        {
            return Name;
        }
        public string getCognome()
        {
            return Cognome;
        }
        public string getCodice()
        {
            return Codice;
        }
        public string getType()
        {
            return Type;
        }
        public double getSoldi()
        {
            return Soldi;
        }
        public int getDobbio()
        {
            return Dobbio;
        }
        public double setSoldi(double Soldi)
        {
            return this.Soldi = Soldi;
        }
        public int setDobbio(int Dobbio)
        {
            this.Dobbio = Dobbio;
            return Dobbio;
        }
        
        //Metodi della classe di conto correnti standard

        public virtual double Entrata(double a,string b)
        {
            if(b == "Standard")
            {
                a = a - 1.50;
            }
            else if (b == "Giovani")
            {
                a = a - 1.0;
            }
            else if(b == "Over 60")
            {
                a = a - 0.5;
            }
            else if (b == "Azienda")
            {
                a = a - 5;
            }
            Soldi = a + Soldi; 
            return Soldi;
        }
        public virtual double Uscita(double a,string b)
        {
            if (b == "Standard")
            {
                a = a + 1.50;
            }
            else if (b == "Giovani")
            {
                a = a + 1.0;
            }
            else if (b == "Over 60")
            {
                a = a + 0.5;
            }
            else if (b == "Azienda")
            {
                a = a + 5;
            }
            Soldi = Soldi - a;
            return Soldi;
        }
        public virtual double Bonifici(double a, string b)
        {
            if (b == "Standard")
            {
                a = a + 1.50;
            }
            else if (b == "Giovani")
            {
                a = a + 1.0;
            }
            else if (b == "Over 60")
            {
                a = a + 0.5;
            }
            else if(b== "Azienda")
            {
                a = a + 5;
            }
            Soldi = Soldi - a;
            return Soldi;
        }
    }
}