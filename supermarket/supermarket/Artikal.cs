using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket
{
    public class Artikal
    {
        //Class representing the items available in the store
        public Artikal()
        {
        }

        public Artikal(int sifra, string naziv, int barkod, float cena)
        {
            Sifra = sifra;
            Naziv = naziv;
            Barkod = barkod;
            Cena = cena;
        }

        public int Sifra {  get; set; }
        public string Naziv { get; set; }
        public int Barkod { get; set; }
        public float Cena { get; set; }
        public int BrStavki { get; set; }
        //The Display property is used to display the item in the listbox
        public string Display { get { return Naziv + " " + Cena + " RSD " + BrStavki + " primerak"; } } 
        
    }
}
