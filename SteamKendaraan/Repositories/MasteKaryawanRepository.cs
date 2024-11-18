using Dapper;
using SteamKendaraan.Data;
using SteamKendaraan.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SteamKendaraan.Repositories
{
    public class MasterKaryawanRepository : IMasterKaryawan
    {
        private readonly SteamKendaraanDBContext _context;

        public MasterKaryawanRepository(SteamKendaraanDBContext context)
        {
            _context = context;
        }

        // Menambahkan Karyawan Baru
        public async Task<MasterKaryawan> Add(MasterKaryawan model)
        {
            model.CreatedOn = DateTime.Now;
            model.UpdatedOn = DateTime.Now;

            var sql = @"INSERT INTO [dbo].[MasterKaryawan]
                            ([Nama],
                             [Alamat],
                             [CreatedOn])
                        VALUES
                            (@Nama,
                             @Alamat,
                             @CreatedOn);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            model.ID = await connection.QuerySingleAsync<int>(sql, model);
            return model;
        }

        // Mengambil Karyawan Berdasarkan ID
        public async Task<MasterKaryawan> Find(int id)
        {
            var sql = @"SELECT [ID],
                               [Nama],
                               [Alamat],
                               [CreatedOn],
                               [UpdatedOn]
                        FROM [MasterKaryawan]
                        WHERE [ID] = @id";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterKaryawan>(sql, new { id });
        }

        // Mengambil Semua Data Karyawan
        public async Task<IEnumerable<MasterKaryawan>> Get()
        {
            var sql = @"SELECT [ID],
                               [Nama],
                               [Alamat],
                               [CreatedOn],
                               [UpdatedOn]
                        FROM [MasterKaryawan]";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<MasterKaryawan>(sql);
        }

        // Menghapus Data Karyawan
        public async Task<MasterKaryawan> Remove(MasterKaryawan model)
        {
            var sql = @"DELETE FROM [dbo].[MasterKaryawan]
                        WHERE [ID] = @ID";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { model.ID });
            return model;
        }

        // Memperbarui Data Karyawan
        public async Task<MasterKaryawan> Update(MasterKaryawan model)
        {
            model.UpdatedOn = DateTime.Now;

            var sql = @"UPDATE [dbo].[MasterKaryawan]
                           SET [Nama] = @Nama,
                               [Alamat] = @Alamat,
                               [UpdatedOn] = @UpdatedOn
                        WHERE [ID] = @ID";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
