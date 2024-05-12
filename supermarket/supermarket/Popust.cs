using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket
{
    public class Popust
    {
        public int Sifra {  get; set; }
        public string Opis { get; set; }
        public float Kolicina { get; set; }
        public int SifraArtikla { get; set; }
        

        public override string ToString()
        {
            return Opis + " u iznosu od: " + Kolicina + "%";
        }
    }
}
