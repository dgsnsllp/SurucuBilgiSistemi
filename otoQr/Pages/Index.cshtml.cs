using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using otoQr.Models;
using otoQr.Data;
using QRCoder;
using System.Drawing;
using System.IO;

namespace otoQr.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Plaka { get; set; }
        [BindProperty]
        public string Marka { get; set; }
        [BindProperty]
        public string Model { get; set; }
        [BindProperty]
        public string Telefon { get; set; }
        [BindProperty]
        public string Sifre { get; set; }

        public bool BasariliKayit { get; set; } = false;
        public Guid SonEklenenId { get; set; }
        public string QrCodeBase64 { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Plaka) && !string.IsNullOrWhiteSpace(Marka) && !string.IsNullOrWhiteSpace(Model) && !string.IsNullOrWhiteSpace(Telefon) && !string.IsNullOrWhiteSpace(Sifre))
            {
                var yeniArac = new Arac
                {
                    Plaka = Plaka,
                    Marka = Marka,
                    Model = Model,
                    Telefon = Telefon,
                    Sifre = Sifre
                };
                AracVeritabani.Araclar.Add(yeniArac);
                AracVeritabani.Yedekle();
                BasariliKayit = true;
                SonEklenenId = yeniArac.Id;

                // QR kod üretimi
                string url = $"https://localhost:5001/arac/{yeniArac.Id}";
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                using (MemoryStream ms = new MemoryStream())
                {
                    qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    QrCodeBase64 = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
