using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fiore.Data;
using Fiore.Models.Entities;
using Fiore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Fiore.Controllers
{
    public class ComplaintsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FioreDbContext _context;

        public ComplaintsController(UserManager<ApplicationUser> userManager, FioreDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Complaints
        public async Task<IActionResult> AllComplaints()
        {
            var fioreDbContext = _context.Complaints.Include(c => c.ApplicationUser);
            return View(await fioreDbContext.ToListAsync());
        }

        public IActionResult Index()
        {
            var cmpList = new List<Complaint>();
            var userId = _userManager.GetUserId(User);
            var complaints = _context.Complaints.Include(i => i.ApplicationUser);
            foreach (var cmp in complaints)
            {
                if (cmp.UserId == userId)
                    cmpList.Add(cmp);
            }
            return View(cmpList);
        }

        public IActionResult CheckComplaint(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                var cmp = _context.Complaints.Find(id);
                if (cmp != null)
                {
                    var userId = _userManager.GetUserId(User);
                    var user = _userManager.FindByIdAsync(userId);
                    cmp.Checked = true;
                    //cmp.ApplicationUser = user;
                    _context.Update(cmp);
                    _context.SaveChangesAsync();
                    return RedirectToAction("AllComplaints");
                }
                return NotFound();
            }
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Description,PhoneNumber,")] ComplaintViewModel complaint)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);
                var cmp = _context.Add(new Complaint
                {
                    Id = complaint.Id,
                    UserId = userId,
                    Subject = complaint.Subject,
                    Description = complaint.Description,
                    PhoneNumber = complaint.PhoneNumber,
                    CmpDate = DateTime.Now,
                    Checked = false,
                    ApplicationUser = user
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaint);
        }




        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Complaints == null)
            {
                return Problem("Entity set 'FioreDbContext.Complaints'  is null.");
            }
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return (_context.Complaints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
