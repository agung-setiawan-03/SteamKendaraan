using SteamKendaraan.Models;

namespace SteamKendaraan.Repositories
{
    public interface IMasterOrder
    {
        Task<IEnumerable<MasterOrder>> Get();
        Task<MasterOrder> Find(int id);
        Task<MasterOrder> Add(MasterOrder model);

    }
}
