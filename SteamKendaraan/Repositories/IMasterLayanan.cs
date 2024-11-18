using SteamKendaraan.Models;

namespace SteamKendaraan.Repositories
{
    public interface IMasterLayanan
    {
        Task<IEnumerable<MasterLayanan>> Get();
        Task<MasterLayanan> Find(int id);
        Task<MasterLayanan> Add(MasterLayanan model);
        Task<MasterLayanan> Update(MasterLayanan model);
        Task<MasterLayanan> Remove(MasterLayanan model);
    }
}
