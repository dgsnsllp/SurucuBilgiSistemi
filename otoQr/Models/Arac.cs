using System;
using System.Collections.Generic;

namespace otoQr.Models
{
    public class IletisimMesaji
    {
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string AdSoyad { get; set; }
        public string Mesaj { get; set; }
        public string SikayetTuru { get; set; }
        public string FotoUrl { get; set; }
    }

    public class Arac
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Telefon { get; set; }
        public string Sifre { get; set; }
        public DateTime? BakimZamani { get; set; }
        public List<ServisKaydi> ServisKayitlari { get; set; } = new List<ServisKaydi>();
        public List<IletisimMesaji> IletisimMesajlari { get; set; } = new List<IletisimMesaji>();
    }

    public class ServisKaydi
    {
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string Not { get; set; }
    }
}
