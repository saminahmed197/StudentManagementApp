using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Authorize]
    public class StuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stu
        public async Task<IActionResult> Index()
        {
              return View(await _context.StudentManages.ToListAsync());
        }

        // GET: Stu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentManages == null)
            {
                return NotFound();
            }

            var studentEntity = await _context.StudentManages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentEntity == null)
            {
                return NotFound();
            }

            return View(studentEntity);
        }

        // GET: Stu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,UserName,Password,Address")] StudentEntity studentEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentEntity);
        }

        // GET: Stu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentManages == null)
            {
                return NotFound();
            }

            var studentEntity = await _context.StudentManages.FindAsync(id);
            if (studentEntity == null)
            {
                return NotFound();
            }
            return View(studentEntity);
        }

        // POST: Stu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,UserName,Password,Address")] StudentEntity studentEntity)
        {
            if (id != studentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentEntityExists(studentEntity.Id))
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
            return View(studentEntity);
        }

        // GET: Stu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentManages == null)
            {
                return NotFound();
            }

            var studentEntity = await _context.StudentManages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentEntity == null)
            {
                return NotFound();
            }

            return View(studentEntity);
        }

        // POST: Stu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentManages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentManages'  is null.");
            }
            var studentEntity = await _context.StudentManages.FindAsync(id);
            if (studentEntity != null)
            {
                _context.StudentManages.Remove(studentEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentEntityExists(int id)
        {
          return _context.StudentManages.Any(e => e.Id == id);
        }
    }
}
