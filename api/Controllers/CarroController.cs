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
    public class CarroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarroController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/carros")]
        public async Task<IActionResult> Index()
        {
            return StatusCode(200, await _context.Carros.ToListAsync());
        }

        [HttpGet]
        [Route("/carros/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var carro = await _context.Carros.FirstOrDefaultAsync(c => c.Id == id);
            
            if(id == null || carro == null)
                return NotFound();

            return StatusCode(200, carro);
        }

        [HttpPost]
        [Route("/carros")]
        public async Task<IActionResult> Create([Bind("Id, Modelo, Potencia, Autonomia, Peso, Ano")] Carro carro)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, carro);

            _context.Add(carro);
            await _context.SaveChangesAsync();
            return StatusCode(201, carro);
        }

        [HttpPut]
        [Route("/carros/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Modelo, Pitencia, Autonimia, Peso, Ano")] Carro carro)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, carro);
            
            try
            {
                carro.Id = id;
                _context.Carros.Update(carro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CarroExists(id))
                    return NotFound();
            }
            return StatusCode(200, carro);
        }

        [HttpDelete]
        [Route("/carros/destroy/{id}")]
        public async Task<IActionResult> destroy(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

        private bool CarroExists(int id)
        {
            return _context.Carros.Any(c => c.Id == id);
        }
    }
}