using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace otoQr.Pages
{
    public class AdminLoginModel : PageModel
    {
        [BindProperty]
        public string KullaniciAdi { get; set; }
        [BindProperty]
        public string Sifre { get; set; }
        public bool GirisBasarisiz { get; set; } = false;

        public IActionResult OnPost()
        {
            if (KullaniciAdi == "admin" && Sifre == "123456")
            {
                TempData["AdminGiris"] = true;
                return RedirectToPage("/Admin");
            }
            GirisBasarisiz = true;
            return Page();
        }
    }
}
