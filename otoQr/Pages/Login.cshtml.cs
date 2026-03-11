using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using otoQr.Data;
using otoQr.Models;
using System.Linq;

namespace otoQr.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Telefon { get; set; }
        [BindProperty]
        public string Sifre { get; set; }
        public bool GirisBasarisiz { get; set; } = false;

        public IActionResult OnPost()
        {
            var arac = AracVeritabani.Araclar.FirstOrDefault(x => x.Telefon == Telefon);
            // Şifre kontrolü aracın kaydedilen şifresi ile yapılır.
            if (arac != null && Sifre == arac.Sifre)
            {
                // Kullanıcı oturumu için TempData veya Cookie kullanılabilir
                TempData["KullaniciId"] = arac.Id.ToString();
                return RedirectToPage("/KullaniciPaneli");
            }
            GirisBasarisiz = true;
            return Page();
        }
    }
}
