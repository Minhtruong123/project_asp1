using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Areas.Songs.Controllers
{
    [Area("Songs")]
    [Route("Songs/Song/[action]/{id?}")]
    public class SongController : Controller
    {
        private readonly AppDbContext _context;

        public SongController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Song
        public async Task<IActionResult> Index()
        {
            return View(await _context.SongSet.ToListAsync());
        }

        // GET: Song/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.SongSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Song/Create
        public IActionResult Create()
        {
            // Truy vấn danh sách các loại sách từ bảng "BookType"
            var typeSongs = _context.TypeSongSet.ToList();

            // Tạo SelectList
            var selectList = new SelectList(typeSongs, "Id", "TypeName");

            // Truyền SelectList vào ViewBag để sử dụng trong View
            ViewBag.TypeList = selectList ?? new SelectList(new List<TypeSong>(), "Id", "TypeName");
            return View();
        }

        // POST: Song/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameSong,PublicationDate,TypeSongId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Song/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.SongSet.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            // Truy vấn danh sách các loại sách từ bảng "BookType"
            var typeSongs = _context.TypeSongSet.ToList();

            // Tạo SelectList
            var selectList = new SelectList(typeSongs, "Id", "TypeName");

            // Truyền SelectList vào ViewBag để sử dụng trong View
            ViewBag.TypeList = selectList ?? new SelectList(new List<TypeSong>(), "Id", "TypeName");
            
            return View(song);
        }

        // POST: Song/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameSong,PublicationDate,TypeSongId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Song/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.SongSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Song/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.SongSet.FindAsync(id);
            if (song != null)
            {
                _context.SongSet.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.SongSet.Any(e => e.Id == id);
        }
    }
}
