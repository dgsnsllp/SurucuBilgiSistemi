using System.Collections.Generic;
using otoQr.Models;

namespace otoQr.Data
{
    public static class AracVeritabani
    {
        private static readonly string DosyaYolu = "araclar.json";
        public static List<Arac> Araclar { get; set; } = new List<Arac>();

        static AracVeritabani()
        {
            YedektenYukle();
        }

        public static void YedektenYukle()
        {
            if (System.IO.File.Exists(DosyaYolu))
            {
                var json = System.IO.File.ReadAllText(DosyaYolu);
                var list = System.Text.Json.JsonSerializer.Deserialize<List<Arac>>(json);
                if (list != null)
                    Araclar = list;
            }
        }

        public static void Yedekle()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(Araclar, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(DosyaYolu, json);
        }
    }
}
