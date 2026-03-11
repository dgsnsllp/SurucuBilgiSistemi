using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using otoQr.Models;
using otoQr.Data;
using System.Linq;
using System.Collections.Generic;

namespace otoQr.Pages
{
    public class AdminModel : PageModel
    {
        public List<Arac> Araclar { get; set; } = new List<Arac>();
        public Arac SeciliArac { get; set; }


        public void OnGet()
        {
            var sorgu = Request.Query["Sorgu"].ToString();
            if (!string.IsNullOrWhiteSpace(sorgu))
            {
                Araclar = AracVeritabani.Araclar.Where(a =>
                    (a.Plaka != null && a.Plaka.Contains(sorgu)) ||
                    (a.Marka != null && a.Marka.Contains(sorgu)) ||
                    (a.Model != null && a.Model.Contains(sorgu))
                ).ToList();
                SeciliArac = Araclar.FirstOrDefault(a => a.Plaka == sorgu);
            }
            else
            {
                Araclar = AracVeritabani.Araclar.ToList();
            }
        }

        public IActionResult OnPostEkle(string Plaka, string Marka, string Model, string Telefon)
        {
            if (!string.IsNullOrWhiteSpace(Plaka) && !string.IsNullOrWhiteSpace(Marka) && !string.IsNullOrWhiteSpace(Model) && !string.IsNullOrWhiteSpace(Telefon))
            {
                var yeniArac = new Arac
                {
                    Plaka = Plaka,
                    Marka = Marka,
                    Model = Model,
                    Telefon = Telefon
                };
                AracVeritabani.Araclar.Add(yeniArac);
                AracVeritabani.Yedekle();
            }
            AracVeritabani.Yedekle();
            return RedirectToPage();
        }

        public IActionResult OnPostSil(Guid id)
        {
            var arac = AracVeritabani.Araclar.FirstOrDefault(a => a.Id == id);
            if (arac != null)
            {
                AracVeritabani.Araclar.Remove(arac);
                AracVeritabani.Yedekle();
            }
            return RedirectToPage();
        }
    }
}
