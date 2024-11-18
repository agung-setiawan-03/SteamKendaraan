using System.ComponentModel.DataAnnotations;

namespace SteamKendaraan.Models
{
    public class Pelanggan
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public string Plat_nomor { get; set; }

        public int ID_Layanan { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
