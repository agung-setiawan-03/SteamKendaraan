using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SteamKendaraan.Data;
using SteamKendaraan.Models;

namespace SteamKendaraan.Repositories
{
    public class PelangganRepository : IPelanggan
    {
        private readonly SteamKendaraanDBContext context;

        public PelangganRepository(SteamKendaraanDBContext context)
        {
            this.context = context;
        }

        // Add a new Pelanggan
        public async Task<Pelanggan> Add(Pelanggan model)
        {
            model.CreatedOn = DateTime.Now;
            model.UpdatedOn = DateTime.Now;

            var sql = @"INSERT INTO [dbo].[Pelanggan]
                                ([Nama],
                                 [ID_Layanan],
                                 [Plat_nomor],
                                 [CreatedOn])
                                VALUES
                                (@Nama,
                                 @ID_Layanan,
                                 @Plat_nomor,
                                 @CreatedOn);
                        SELECT CAST(SCOPE_IDENTITY() as int)"; // Mendapatkan ID yang baru ditambahkan

            using var connection = context.CreateConnection();
            model.ID = await connection.QuerySingleAsync<int>(sql, model); // Mendapatkan ID dan menyimpannya ke model
            return model;
        }

        // Find Pelanggan by ID
        public async Task<Pelanggan> Find(int id)
        {
            var sql = @"SELECT [ID],
                               [Nama],
                               [ID_Layanan],
                               [Plat_nomor],
                               [CreatedOn],
                               [UpdatedOn]
                        FROM [Pelanggan]
                        WHERE [ID] = @id";

            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Pelanggan>(sql, new { id });
        }

        // Get all Pelanggan
        public async Task<IEnumerable<Pelanggan>> Get()
        {
            var sql = @"SELECT [ID],
                               [Nama],
                               [ID_Layanan],
                               [Plat_nomor],
                               [CreatedOn],
                               [UpdatedOn]
                        FROM [Pelanggan]";

            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Pelanggan>(sql);
        }

        // Remove Pelanggan
        public async Task<Pelanggan> Remove(Pelanggan model)
        {
            var sql = @"DELETE FROM [dbo].[Pelanggan]
                        WHERE [ID] = @ID";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, new { model.ID });
            return model;
        }

        // Update existing Pelanggan
        public async Task<Pelanggan> Update(Pelanggan model)
        {
            model.UpdatedOn = DateTime.Now;

            var sql = @"UPDATE [dbo].[Pelanggan]
                           SET [Nama] = @Nama,
                               [ID_Layanan] = @ID_Layanan,
                               [Plat_nomor] = @Plat_nomor,
                               [UpdatedOn] = @UpdatedOn
                        WHERE [ID] = @ID";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
