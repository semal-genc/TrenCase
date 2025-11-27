using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]ler")]
    public class TrenController : ControllerBase
    {
        [HttpPost]
        public IActionResult RezervasyonYap([FromBody] RezervasyonRequest rezervasyon)
        {
            int kalanKisi = rezervasyon.RezervasyonYapilacakKisiSayisi;
            var yerlesim = new List<VagonYerlesim>();

            foreach (var Vagon in rezervasyon.Tren.Vagonlar)
            {
                int maxKoltuk = (int)(Vagon.Kapasite * .7) - Vagon.DoluKoltukAdet;
                if (maxKoltuk <= 0) continue;

                int yerlestirilecek = 0;

                if (rezervasyon.KisilerFarkliVagonlaraYerlestirilebilir)
                    yerlestirilecek = Math.Min(maxKoltuk, kalanKisi);
                else
                {
                    if (kalanKisi <= maxKoltuk)
                        yerlestirilecek = kalanKisi;
                    else
                        continue;
                }

                if (yerlestirilecek > 0)
                {
                    yerlesim.Add(new VagonYerlesim { VagonAdi = Vagon.Ad, KisiSayisi = yerlestirilecek });
                    kalanKisi -= yerlestirilecek;
                }

                if (!rezervasyon.KisilerFarkliVagonlaraYerlestirilebilir) break;
                if (kalanKisi == 0) break;
            }

            var response = new RezervasyonResponse
            {
                RezervasyonYapilabilir = kalanKisi == 0,
                YerlesimAyrinti = kalanKisi == 0 ? yerlesim : new List<VagonYerlesim>()
            };

            return Ok(response);
        }
    }
}