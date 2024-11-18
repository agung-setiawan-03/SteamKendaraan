using Microsoft.AspNetCore.Mvc;
using SteamKendaraan.Models;
using SteamKendaraan.Repositories;
using System.Threading.Tasks;
using System;

namespace SteamKendaraan.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMasterKaryawan _karyawanRepository;
        private readonly IMasterLayanan _layananRepository;
        private readonly IMasterOrder _orderRepository;
        private readonly IPelanggan _pelangganRepository;

        public OrderController(
            IMasterKaryawan karyawanRepository,
            IMasterLayanan layananRepository,
            IMasterOrder orderRepository,
            IPelanggan pelangganRepository)
        {
            _karyawanRepository = karyawanRepository;
            _layananRepository = layananRepository;
            _orderRepository = orderRepository;
            _pelangganRepository = pelangganRepository;
        }

        // Menampilkan daftar order
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.Get();
            var orderViewModels = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var pelanggan = await _pelangganRepository.Find(order.ID_Pelanggan);
                var layanan = await _layananRepository.Find(order.ID_Layanan);
                var karyawan = await _karyawanRepository.Find(order.ID_Karyawan);

                orderViewModels.Add(new OrderViewModel
                {
                    ID = order.ID,
                    NamaPelanggan = pelanggan?.Nama ?? "Tidak Ditemukan",
                    NamaLayanan = layanan?.Nama_Layanan ?? "Tidak Ditemukan",
                    NamaKaryawan = karyawan?.Nama ?? "Tidak Ditemukan",
                    Payment_Method = order.Payment_Method,
                    Uang_Bayar = (decimal)order.Uang_Bayar,
                    Kembalian = (decimal)order.Kembalian,
                    Barcode = order.Barcode,
                    Total_Amount = order.Total_Amount,
                    CreatedOn = (DateTime)order.CreatedOn
                });
            }

            return View(orderViewModels);
        }



        // Menampilkan form Create Order
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var layananList = await _layananRepository.Get();
            var pelangganList = await _pelangganRepository.Get();
            var karyawanList = await _karyawanRepository.Get();

            ViewBag.LayananList = layananList;
            ViewBag.PelangganList = pelangganList;
            ViewBag.KaryawanList = karyawanList;
            return View();
        }



        // Meng-handle form submission
        [HttpPost]
        public async Task<IActionResult> CreateOrder(MasterOrder model)
        {
            var pelanggan = await _pelangganRepository.Find(model.ID_Pelanggan);
            var layanan = await _layananRepository.Find(model.ID_Layanan);

            if (pelanggan == null || layanan == null)
            {
                return BadRequest("ID Tidak Ditemukan.");
            }

            // Menghitung total bayar dan kembalian
            var totalAmount = layanan.Harga;
            if (model.Payment_Method == "Cash" && model.Uang_Bayar < totalAmount)
            {
                ModelState.AddModelError("Uang_Bayar", "Jumlah bayar tidak mencukupi.");
                return View(model);
            }

            var kembalian = model.Payment_Method == "Cash" ? model.Uang_Bayar - totalAmount : 0;

            var newOrder = new MasterOrder
            {
                ID_Pelanggan = pelanggan.ID,
                ID_Layanan = layanan.ID,
                ID_Karyawan = model.ID_Karyawan,
                Payment_Method = model.Payment_Method,
                Uang_Bayar = model.Payment_Method == "Cash" ? model.Uang_Bayar : 0,
                Kembalian = kembalian,
                Total_Amount = totalAmount,
                CreatedOn = DateTime.Now
            };

            await _orderRepository.Add(newOrder);

            // Jika metode pembayaran QRIS, tampilkan halaman QRIS
            if (model.Payment_Method == "QRIS")
            {
                return RedirectToAction("ShowQRIS", new { orderId = newOrder.ID });
            }

            // Cetak struk pembayaran
            return RedirectToAction("PrintReceipt", new { orderId = newOrder.ID });
        }


        // Menampilkan halaman QRIS
        [HttpGet]
        public IActionResult ShowQRIS(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        // Mencetak struk pembayaran
        [HttpGet]
        public async Task<IActionResult> PrintReceipt(int orderId)
        {
            var order = await _orderRepository.Find(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetHargaLayanan(int id)
        {
            var layanan = await _layananRepository.Find(id);
            if (layanan == null)
            {
                return NotFound(new { message = "Layanan tidak ditemukan." });
            }

            // Mengembalikan harga dalam format JSON
            return Json(new { harga = layanan.Harga });
        }

        // Menampilkan halaman detail order
        [HttpGet]
        public async Task<IActionResult> Details(int ID)
        {
            // Mengambil satu order berdasarkan ID
            var order = await _orderRepository.Find(ID);
            if (order == null)
            {
                return NotFound("Order tidak ditemukan.");
            }

            // Mengambil detail pelanggan, layanan, dan karyawan
            var pelanggan = await _pelangganRepository.Find(order.ID_Pelanggan);
            var layanan = await _layananRepository.Find(order.ID_Layanan);
            var karyawan = await _karyawanRepository.Find(order.ID_Karyawan);

            // Membuat model untuk view
            var orderViewModel = new OrderViewModel
            {
                ID = order.ID,
                NamaPelanggan = pelanggan?.Nama ?? "Tidak Ditemukan",
                NamaLayanan = layanan?.Nama_Layanan ?? "Tidak Ditemukan",
                NamaKaryawan = karyawan?.Nama ?? "Tidak Ditemukan",
                Payment_Method = order.Payment_Method,
                Uang_Bayar = order.Uang_Bayar ?? 0, // Menghindari null reference
                Kembalian = order.Kembalian ?? 0,   // Menghindari null reference
                Barcode = order.Barcode,
                Total_Amount = order.Total_Amount,
                CreatedOn = order.CreatedOn ?? DateTime.MinValue // Menghindari null reference
            };

            return View(orderViewModel);
        }




    }
}
