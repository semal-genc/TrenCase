namespace WebApi
{
    public class Tren
    {
        public required string Ad { get; set; }
        public required List<Vagon> Vagonlar { get; set; }

    }
    public class Vagon
    {
        public required string Ad { get; set; }
        public int Kapasite { get; set; }
        public int DoluKoltukAdet { get; set; }
    }
    public class RezervasyonRequest
    {
        public required Tren Tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }

    public class RezervasyonResponse
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<VagonYerlesim> YerlesimAyrinti { get; set; } = new();
    }

    public class VagonYerlesim
    {
        public required string VagonAdi { get; set; }
        public int KisiSayisi { get; set; }
    }

}