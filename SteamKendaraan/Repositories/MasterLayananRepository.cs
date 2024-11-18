using Dapper;
using SteamKendaraan.Data;
using SteamKendaraan.Models;
using System.Data;

namespace SteamKendaraan.Repositories
{
    public class MasterLayananRepository : IMasterLayanan
    {
        private readonly SteamKendaraanDBContext context;

        public MasterLayananRepository(SteamKendaraanDBContext context)
        {
            this.context = context;
        }

        public async Task<MasterLayanan> Add(MasterLayanan model)
        {
            model.CreatedOn = DateTime.Now;
            model.UpdatedOn = DateTime.Now;

            var sql = @"INSERT INTO [dbo].[MasterLayanan]
                            ([Nama_Layanan],
                             [Harga],
                             [CreatedOn])
                        VALUES
                            (@Nama_Layanan,
                             @Harga,
                             @CreatedOn);
                        SELECT CAST(SCOPE_IDENTITY() as int)"; // Mendapatkan ID yang baru ditambahkan

            using var connection = context.CreateConnection();
            model.ID = await connection.QuerySingleAsync<int>(sql, model); // Mendapatkan ID dan menyimpannya ke model
            return model;
        }

        // Find Layanan by ID
        public async Task<MasterLayanan> Find(int id)
        {
            var sql = @"SELECT [ID],
                               [Nama_Layanan],
                               [Harga],
                               [CreatedOn],
                               [UpdatedOn]
                        FROM [MasterLayanan]
                        WHERE [ID] = @id";

            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterLayanan>(sql, new { id });
        }

        // Get all Layanan
        public async Task<IEnumerable<MasterLayanan>> Get()
        {
            var sql = @"SELECT [ID],
                               [Nama_Layanan],
                               [Harga],
                               [CreatedOn],
                               [UpdatedOn]
                        FROM [MasterLayanan]";

            using var connection = context.CreateConnection();
            return await connection.QueryAsync<MasterLayanan>(sql);
        }


        // Remove Layanan
        public async Task<MasterLayanan> Remove(MasterLayanan model)
        {
            var sql = @"DELETE FROM [dbo].[MasterLayanan]
                        WHERE [ID] = @ID";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, new { model.ID });
            return model;
        }

        // Update existing Layanan
        public async Task<MasterLayanan> Update(MasterLayanan model)
        {
            model.UpdatedOn = DateTime.Now;

            var sql = @"UPDATE [dbo].[MasterLayanan]
                           SET [Nama_Layanan] = @Nama_Layanan,
                               [Harga] = @Harga,
                               [UpdatedOn] = @UpdatedOn
                        WHERE [ID] = @ID";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
