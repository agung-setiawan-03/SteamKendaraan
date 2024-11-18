using Dapper;
using SteamKendaraan.Data;
using SteamKendaraan.Models;
using System.Data;

namespace SteamKendaraan.Repositories
{
    public class MasterOrderRepository : IMasterOrder
    {
        private readonly SteamKendaraanDBContext context;

        public MasterOrderRepository(SteamKendaraanDBContext context)
        {
            this.context = context;
        }

        public async Task<MasterOrder> Add(MasterOrder model)
        {
            model.CreatedOn = DateTime.Now;

            var sql = @"INSERT INTO [dbo].[MasterOrder]
                            ([ID_Pelanggan],
                             [ID_Layanan],
                             [ID_Karyawan],
                             [Payment_Method],
                             [Uang_Bayar],
                             [Kembalian],
                             [Barcode],
                             [Total_Amount],
                             [CreatedOn])
                        VALUES
                            (@ID_Pelanggan,
                             @ID_Layanan,
                             @ID_Karyawan,
                             @Payment_Method,
                             @Uang_Bayar,
                             @Kembalian,
                             @Barcode,
                             @Total_Amount,
                             @CreatedOn);
                        SELECT CAST(SCOPE_IDENTITY() as int)"; // Mendapatkan ID yang baru ditambahkan

            using var connection = context.CreateConnection();
            model.ID = await connection.QuerySingleAsync<int>(sql, model); // Mendapatkan ID dan menyimpannya ke model
            return model;
        }

        public async Task<MasterOrder> Find(int id)
        {
            var sql = @"SELECT [ID],
                               [ID_Pelanggan],
                               [ID_Layanan],
                               [ID_Karyawan],
                               [Payment_Method],
                               [Uang_Bayar],
                               [Kembalian],
                               [Barcode],
                               [Total_Amount],
                               [CreatedOn]
                        FROM [MasterOrder]
                        WHERE [ID] = @id";

            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterOrder>(sql, new { id });
        }

        public async Task<IEnumerable<MasterOrder>> Get()
        {
            var sql = @"SELECT [ID],
                               [ID_Pelanggan],
                               [ID_Layanan],
                               [ID_Karyawan],
                               [Payment_Method],
                               [Uang_Bayar],
                               [Kembalian],
                               [Barcode],
                               [Total_Amount],
                               [CreatedOn]
                        FROM [MasterOrder]";

            using var connection = context.CreateConnection();
            return await connection.QueryAsync<MasterOrder>(sql);
        }
    }
}
