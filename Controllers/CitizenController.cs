using EvironmentalMunicipality.Data;
using EvironmentalMunicipality.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EvironmentalMunicipality.Controllers
{
    public class CitizenController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CitizenController> _logger;

        public CitizenController(AppDbContext context, ILogger<CitizenController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Citizen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Citizen.ToListAsync());
        }

        // GET: Citizen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizen/Create
        [HttpPost]
        public async Task<IActionResult> Create(
            string FullName,
            string Address,
            string PhoneNumber,
            string Email,
            DateTime DateOfBirth)
        {
            var citizen = new Citizen
            {
                FullName = FullName,
                Address = Address,
                PhoneNumber = PhoneNumber,
                Email = Email,
                DateOfBirth = DateOfBirth,
                RegistrationDate = DateTime.Now
            };

            _context.Citizen.Add(citizen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Citizen/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen
                .FirstOrDefaultAsync(m => m.CitizenID == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // GET: Citizen/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }
            return View(citizen);
        }

        // POST: Citizen/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(
            int CitizenID,
            string FullName,
            string Address,
            string PhoneNumber,
            string Email,
            DateTime DateOfBirth)
        {
            var citizen = await _context.Citizen.FindAsync(CitizenID);
            if (citizen == null)
            {
                return NotFound();
            }

            citizen.FullName = FullName;
            citizen.Address = Address;
            citizen.PhoneNumber = PhoneNumber;
            citizen.Email = Email;
            citizen.DateOfBirth = DateOfBirth;

            try
            {
                _context.Update(citizen);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenExists(CitizenID))
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

        // GET: Citizen/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen
                .FirstOrDefaultAsync(m => m.CitizenID == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Citizen/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen != null)
            {
                _context.Citizen.Remove(citizen);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenExists(int id)
        {
            return _context.Citizen.Any(e => e.CitizenID == id);
        }

        [Route("/Citizen/Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}