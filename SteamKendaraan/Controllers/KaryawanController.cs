using Microsoft.AspNetCore.Mvc;
using SteamKendaraan.Models;
using SteamKendaraan.Repositories;

namespace SteamKendaraan.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly IMasterKaryawan karyawanRepository;

        public KaryawanController(IMasterKaryawan karyawanRepository)
        {
            this.karyawanRepository = karyawanRepository;
        }

        public async Task<IActionResult> Index()
        {
            var karyawan = await karyawanRepository.Get();
            return View(karyawan);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var karyawan = await karyawanRepository.Find(id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MasterKaryawan model)
        {
            if (ModelState.IsValid)
            {
                await karyawanRepository.Add(model);
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

            var karyawan = await karyawanRepository.Find(id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MasterKaryawan model)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await karyawanRepository.Update(model);
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

            var karyawan = await karyawanRepository.Find(id);
            if (karyawan == null)
            {
                return BadRequest();
            }

            return View(karyawan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var karyawan = await karyawanRepository.Find(id);
            if (karyawan != null)
            {
                await karyawanRepository.Remove(karyawan);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
