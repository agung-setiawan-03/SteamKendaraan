using SteamKendaraan.Data;
using SteamKendaraan.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<SteamKendaraanDBContext, SteamKendaraanDBContext>();
builder.Services.AddTransient<IMasterKaryawan, MasterKaryawanRepository>();
builder.Services.AddTransient<IMasterLayanan, MasterLayananRepository>();
builder.Services.AddTransient<IMasterOrder, MasterOrderRepository>();
builder.Services.AddTransient<IPelanggan, PelangganRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
