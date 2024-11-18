namespace SteamKendaraan.Models
{
    public class MasterKaryawan
    {
        public int ID { get; set; }
        public string Nama { get; set; }

        public string Alamat { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
