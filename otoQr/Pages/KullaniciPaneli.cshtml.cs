using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using otoQr.Data;
using otoQr.Models;
using System;
using System.Linq;

namespace otoQr.Pages
{
    public class KullaniciPaneliModel : PageModel
    {
        public Arac Arac { get; set; }
        [BindProperty]
        public DateTime? BakimZamani { get; set; }
        public bool BakimGuncellendi { get; set; } = false;

        public void OnGet()
        {
            if (TempData["KullaniciId"] != null)
            {
                Guid id = Guid.Parse(TempData["KullaniciId"].ToString());
                Arac = AracVeritabani.Araclar.FirstOrDefault(x => x.Id == id);
            }
        }

        public IActionResult OnPost()
        {
            if (TempData["KullaniciId"] != null)
            {
                Guid id = Guid.Parse(TempData["KullaniciId"].ToString());
                Arac = AracVeritabani.Araclar.FirstOrDefault(x => x.Id == id);
                if (Arac != null && BakimZamani.HasValue)
                {
                    Arac.BakimZamani = BakimZamani;
                    otoQr.Data.AracVeritabani.Yedekle();
                    BakimGuncellendi = true;
                }
            }
            return Page();
        }
    }
}
