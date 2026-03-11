
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using otoQr.Models;
using otoQr.Data;
using System;
using System.Linq;

// ...sadece bir tane public class AracModel : PageModel tanımı olmalı. Fazla olanı kaldırıldı.

namespace otoQr.Pages
{
    public class AracModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid id { get; set; }
        public Arac Arac { get; set; }

        [BindProperty]
        public string YeniNot { get; set; }
        [BindProperty]
        public DateTime? BakimZamani { get; set; }
        public bool BasariliEkleme { get; set; } = false;

        [BindProperty]
        public string AdSoyad { get; set; }
        [BindProperty]
        public string Mesaj { get; set; }
        public bool MesajBasarili { get; set; } = false;

        public bool PublicView { get; set; }

        public void OnGet(Guid id, bool publicView = false)
        {
            Arac = AracVeritabani.Araclar.FirstOrDefault(a => a.Id == id);
            PublicView = publicView;
        }

        public IActionResult OnPost()
        {
            Arac = AracVeritabani.Araclar.FirstOrDefault(a => a.Id == id);
            if (Arac != null)
            {
                if (!string.IsNullOrWhiteSpace(YeniNot))
                {
                    Arac.ServisKayitlari.Add(new ServisKaydi { Not = YeniNot, Tarih = DateTime.Now });
                    BasariliEkleme = true;
                    otoQr.Data.AracVeritabani.Yedekle();
                }
                if (BakimZamani.HasValue)
                {
                    Arac.BakimZamani = BakimZamani;
                    otoQr.Data.AracVeritabani.Yedekle();
                }
            }
            return Page();
        }

        public IActionResult OnPostMesaj()
        {
            Arac = AracVeritabani.Araclar.FirstOrDefault(a => a.Id == id);
            if (Arac != null && !string.IsNullOrWhiteSpace(AdSoyad) && !string.IsNullOrWhiteSpace(Mesaj))
            {
                Arac.IletisimMesajlari.Add(new otoQr.Models.IletisimMesaji { AdSoyad = AdSoyad, Mesaj = Mesaj });
                MesajBasarili = true;
                otoQr.Data.AracVeritabani.Yedekle();
            }
            return Page();
        }
    }
}
