using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket
{
    public class Kasir
    {
        //Class representing the cashier operating the cash register
        //allowing different users with different levels of privileges
        public Kasir()
        {
        }

        public Kasir(int sifra, string ime, string prezime, int isadmin)
        {
            Sifra = sifra;           
            Ime = ime;
            Prezime = prezime;
            if (isadmin == 1) { IsAdmin = true; }
            else IsAdmin = false;
        }

        public int Sifra { get; set; }
        public string Korisnickoime { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
       
    }
}
