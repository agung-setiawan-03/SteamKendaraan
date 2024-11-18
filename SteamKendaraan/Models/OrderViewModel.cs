namespace SteamKendaraan.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public string NamaPelanggan { get; set; }
        public string NamaLayanan { get; set; }
        public string NamaKaryawan { get; set; }
        public string Payment_Method { get; set; }
        public decimal Uang_Bayar { get; set; }
        public decimal Kembalian { get; set; }
        public string Barcode { get; set; }
        public decimal Total_Amount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
