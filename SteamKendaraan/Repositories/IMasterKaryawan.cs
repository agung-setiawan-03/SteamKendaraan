using SteamKendaraan.Models;

namespace SteamKendaraan.Repositories
{
    public interface IMasterKaryawan
    {
        Task<IEnumerable<MasterKaryawan>> Get();
        Task<MasterKaryawan> Find(int id);
        Task<MasterKaryawan> Add(MasterKaryawan model);

        Task<MasterKaryawan> Update(MasterKaryawan model);
        Task<MasterKaryawan> Remove(MasterKaryawan model);
    }
}
