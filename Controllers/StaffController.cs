using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvironmentalMunicipality.Models;
using EvironmentalMunicipality.Data;
using System.Diagnostics;

namespace EvironmentalMunicipality.Controllers
{
    public class StaffController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StaffController> _logger;

        public StaffController(AppDbContext context, ILogger<StaffController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staff.ToListAsync());
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        public async Task<IActionResult> Create(string FullName, string Position, string Department, string Email, string PhoneNumber)
        {
            var staff = new Staff
            {
                FullName = FullName,
                Position = Position,
                Department = Department,
                Email = Email,
                PhoneNumber = PhoneNumber,
                HireDate = DateTime.Now
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Staff/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staff/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staff/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Staff/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(
            int StaffID,
            string FullName,
            string Position,
            string Department,
            string PhoneNumber,
            string Email,
            DateTime DateOfBirth)
        {
            var staff = await _context.Staff.FindAsync(StaffID);
            if (staff == null)
            {
                return NotFound();
            }

            staff.FullName = FullName;
            staff.Position = Position;
            staff.Department = Department;
            staff.PhoneNumber = PhoneNumber;
            staff.Email = Email;

            try
            {
                _context.Update(staff);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(StaffID))
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

        private bool StaffExists(int staffID)
        {
            throw new NotImplementedException();
        }
    }
}

