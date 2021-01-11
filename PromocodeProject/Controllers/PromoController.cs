using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromoCodeProject.Common;
using PromoCodeProject.Models;

namespace PromoCodeProject.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trusted")]
    public class PromoController : Controller
    {
        private readonly PromoDbContext _context;

        public PromoController(PromoDbContext context)
        {
            _context = context;
        }
        [HttpGet("[action]")]
        public IActionResult GetAll(string title)
        {

            var promotes = _context.PromoCodes.AsQueryable();
            if (!string.IsNullOrEmpty(title))
                promotes = promotes.Where(p => p.Title.Contains(title));

            var response = new GenericResponse()
            {
                Data = promotes.ToList(),
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Message = MessagesConstants.PromoCodeLoad
            };
            return Ok(response);
        }
        [HttpPost("[action]")]
        public IActionResult ActivateDeactivate([FromBody] ActivationViewModel model)
        {
            var promoCodeEntity = _context.PromoCodes.FirstOrDefault(p => p.Id == model.PromoCodeId);
            if (promoCodeEntity != null)
            {
                promoCodeEntity.Activated = model.ToggleActivation;
                _context.SaveChanges();

                return Ok(promoCodeEntity);
            }
            var response = new GenericResponse()
            {
                Data = promoCodeEntity,
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Message = MessagesConstants.PromoCodeToggle
            };
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromoCodes([FromRoute] int id, [FromBody] PromoCodes promoCodes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promoCodes.Id)
            {
                return BadRequest();
            }

            _context.Entry(promoCodes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromoCodesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var response = new GenericResponse()
            {
                Data = promoCodes,
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Message = MessagesConstants.PromoCodeUpdate
            };
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromoCodes([FromRoute] int id)
        {
            var promoCode = await _context.PromoCodes.FindAsync(id);

            if (promoCode == null)
            {
                return NotFound();
            }
            var response = new GenericResponse()
            {
                Data = promoCode,
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Message = MessagesConstants.PromoCodeLoad
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostPromoCodes([FromBody] PromoCodes promoCodes)
        {
            _context.PromoCodes.Add(promoCodes);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPromoCodes", new { id = promoCodes.Id }, promoCodes);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromoCodes([FromRoute] int id)
        {
            var promoCodes = await _context.PromoCodes.FindAsync(id);
            if (promoCodes == null)
            {
                return NotFound();
            }

            _context.PromoCodes.Remove(promoCodes);
            await _context.SaveChangesAsync();

            var response = new GenericResponse()
            {
                Data = promoCodes,
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Message = MessagesConstants.PromoCodeDelete
            };
            return Ok(response);
        }

        private bool PromoCodesExists(int id)
        {
            return _context.PromoCodes.Any(e => e.Id == id);
        }
    }
}
