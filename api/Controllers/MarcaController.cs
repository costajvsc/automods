using System;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers   
{
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MarcaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/marcas")]
        public async Task<IActionResult> Index()
        {
            return StatusCode(200, await _context.Marcas.ToListAsync());
        } 

        [HttpGet]
        [Route("/marcas/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var marca = await _context.Marcas.FirstOrDefaultAsync(c => c.IdMarca == id);
            if(id == null || marca == null)
                return NotFound();

            return StatusCode(200, marca);
        }

        [HttpPost]
        [Route("/marcas")]
        public async Task<IActionResult> Create([Bind("Nome, Origem, AnoFuncacao")] Marca marca)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, marca);

            _context.Add(marca);
            await _context.SaveChangesAsync();
            return StatusCode(201, marca);
        }

        [HttpPut]
        [Route("/marcas/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Nome, Origem, AnoFuncacao")] Marca marca)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, marca);

            try
            {
                marca.IdMarca = id;
                _context.Marcas.Update(marca);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!MarcaExist(id))
                    return NotFound();
            }

            return StatusCode(200, marca);
        }
    
        [HttpDelete]
        [Route("/marcas/destroy/{id}")]
        public async Task<IActionResult> Destroy(int? id)
        {
            var marca = await _context.Marcas.FindAsync(id);   
            _context.Remove(marca);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

        private bool MarcaExist(int id)
        {
            return _context.Marcas.Any(c => c.IdMarca == id);
        }
    }
}