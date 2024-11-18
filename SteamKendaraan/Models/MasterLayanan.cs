namespace SteamKendaraan.Models
{
    public class MasterLayanan
    {
        public int ID { get; set; }
        public string Nama_Layanan { get; set; }
        public decimal Harga { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
