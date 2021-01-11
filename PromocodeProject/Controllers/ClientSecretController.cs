using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromoCodeProject.Common;
using PromoCodeProject.Models;

namespace PromoCodeProject.Controllers
{
    public class ClientSecretController : Controller
    {
        private readonly PromoDbContext _context;
        private readonly IJwtManager _jwtManager;
        public ClientSecretController(PromoDbContext context, IJwtManager jwtManager)
        {
            _context = context;
            _jwtManager = jwtManager;
        }

        // GET: ClientSecret
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientSecrets.ToListAsync());
        }

        // GET: ClientSecret/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientSecrets = await _context.ClientSecrets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientSecrets == null)
            {
                return NotFound();
            }

            return View(clientSecrets);
        }

        // GET: ClientSecret/Create
        public IActionResult Create()
        {
            return View(new ClientSecrets()
            {
                AppKey = Guid.NewGuid().ToString(),
                AppValue = Guid.NewGuid().ToString(),
                IsActive = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AppKey,AppValue,IsActive")] ClientSecrets clientSecrets)
        {
            if (ModelState.IsValid)
            {
                clientSecrets.AppKey = _jwtManager.EncryptText(clientSecrets.Name + clientSecrets.AppKey, clientSecrets.AppKey);
                clientSecrets.AppValue = _jwtManager.GenerateAppToken(clientSecrets.Name, clientSecrets.AppKey);
                clientSecrets.CreatedDate = DateTime.UtcNow;
                clientSecrets.UpdateDate = DateTime.UtcNow;
                _context.Add(clientSecrets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientSecrets);
        }

        // GET: ClientSecret/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientSecrets = await _context.ClientSecrets.FindAsync(id);
            if (clientSecrets == null)
            {
                return NotFound();
            }
            return View(clientSecrets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,AppKey,AppValue,IsActive")] ClientSecrets clientSecrets)
        {
            if (id != clientSecrets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _context.ClientSecrets.Find(clientSecrets.Id);
                    entity.Name = clientSecrets.Name;
                    entity.AppKey = clientSecrets.AppKey;
                    entity.AppValue = clientSecrets.AppValue;
                    entity.IsActive = clientSecrets.IsActive;
                    entity.UpdateDate = DateTime.UtcNow;
                    _context.Update(clientSecrets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientSecretsExists(clientSecrets.Id))
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
            return View(clientSecrets);
        }

        // GET: ClientSecret/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientSecrets = await _context.ClientSecrets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientSecrets == null)
            {
                return NotFound();
            }

            return View(clientSecrets);
        }

        // POST: ClientSecret/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var clientSecrets = await _context.ClientSecrets.FindAsync(id);
            _context.ClientSecrets.Remove(clientSecrets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientSecretsExists(long id)
        {
            return _context.ClientSecrets.Any(e => e.Id == id);
        }
    }
}
