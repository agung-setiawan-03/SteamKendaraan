namespace SteamKendaraan.Models
{
    public class MasterOrder
    {
        public int ID { get; set; }
        public int ID_Pelanggan { get; set; }
        public int ID_Layanan { get; set; }
        public int ID_Karyawan { get; set; }
        public string Payment_Method { get; set; }
        public decimal? Uang_Bayar { get; set; }
        public decimal? Kembalian { get; set; }
        public string Barcode { get; set; }
        public decimal Total_Amount { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
