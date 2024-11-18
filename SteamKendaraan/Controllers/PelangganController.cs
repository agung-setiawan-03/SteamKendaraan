using Microsoft.AspNetCore.Mvc;
using SteamKendaraan.Models;
using SteamKendaraan.Repositories;
using System.Threading.Tasks;

namespace SteamKendaraan.Controllers
{
    public class PelangganController : Controller
    {
        private readonly IPelanggan pelangganRepository;

        public PelangganController(IPelanggan pelangganRepository)
        {
            this.pelangganRepository = pelangganRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pelanggan = await pelangganRepository.Get();
            return View(pelanggan);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var pelanggan = await pelangganRepository.Find(id);
            if (pelanggan == null)
            {
                return NotFound();
            }

            return View(pelanggan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelanggan model)
        {
            if (ModelState.IsValid)
            {
                await pelangganRepository.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var pelanggan = await pelangganRepository.Find(id);
            if (pelanggan == null)
            {
                return NotFound();
            }

            return View(pelanggan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pelanggan model)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await pelangganRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var pelanggan = await pelangganRepository.Find(id);
            if (pelanggan == null)
            {
                return BadRequest();
            }

            return View(pelanggan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var pelanggan = await pelangganRepository.Find(id);
            if (pelanggan != null)
            {
                await pelangganRepository.Remove(pelanggan);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
