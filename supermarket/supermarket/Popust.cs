using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket
{
    public class Popust
    {
        //Class represents discounts that may be applied to specific items
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
