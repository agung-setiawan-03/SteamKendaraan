using SteamKendaraan.Models;

namespace SteamKendaraan.Repositories
{
    public interface IPelanggan
    {
        Task<IEnumerable<Pelanggan>> Get();  
        Task<Pelanggan> Find(int id);        
        Task<Pelanggan> Add(Pelanggan model);

        Task<Pelanggan> Update(Pelanggan model); 
        Task<Pelanggan> Remove(Pelanggan model);
    }
}
