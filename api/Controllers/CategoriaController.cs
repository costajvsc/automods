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
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/categorias")]
        public async Task<IActionResult> Index()
        {
            return StatusCode(200, await _context.Categorias.ToListAsync());
        } 

        [HttpGet]
        [Route("/categorias/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if(id == null || categoria == null)
                return NotFound();

            return StatusCode(200, categoria);
        }

        [HttpPost]
        [Route("/categorias")]
        public async Task<IActionResult> Create([Bind("Id, Nome")] Categoria categoria)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, categoria);

            _context.Add(categoria);
            await _context.SaveChangesAsync();
            return StatusCode(201, categoria);
        }

        [HttpPut]
        [Route("/categorias/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Categoria")] Categoria categoria)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, categoria);

            try
            {
                categoria.Id = id;
                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CategoriaExist(id))
                    return NotFound();
            }

            return StatusCode(200, categoria);
        }

        [HttpDelete]
        [Route("/categorias/destroy/{id}")]
        public async Task<IActionResult> Destroy(int? id)
        {
            var categoria = await _context.Categorias.FindAsync(id);   
            _context.Remove(categoria);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

        private bool CategoriaExist(int id)
        {
            return _context.Categorias.Any(c => c.Id == id);
        }
    }
}