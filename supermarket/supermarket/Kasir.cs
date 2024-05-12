using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket
{
    public class Kasir
    {
        public Kasir()
        {
        }

        public Kasir(int sifra, string ime, string prezime)
        {
            Sifra = sifra;           
            Ime = ime;
            Prezime = prezime;
        }

        public int Sifra { get; set; }
        public string Korisnickoime { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
       
    }
}
