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
            var carros = await (
                from carro in _context.Carros
                join c in _context.Categorias on carro.CategoriaId equals c.IdCategoria
                join m in _context.Marcas on carro.MarcaId equals m.IdMarca
                select new {
                    idCarro = carro.IdCarro,
                    modelo = carro.Modelo,
                    ano = carro.Ano,
                    categoria = c.Nome,
                    marca = m.Nome,
                    origem = m.Origem,
                    autonomia = carro.Autonomia,
                    potencia = carro.Potencia,
                    peso = carro.Peso
                }
            ).ToListAsync(); 

            return StatusCode(200, carros);
        }

        [HttpGet]
        [Route("/carros/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var carro = await (
                from car in _context.Carros
                join c in _context.Categorias on car.CategoriaId equals c.IdCategoria
                join m in _context.Marcas on car.MarcaId equals m.IdMarca
                where car.IdCarro == id
                select new {
                    idCarro = car.IdCarro,
                    modelo = car.Modelo,
                    ano = car.Ano,  
                    categoria = c.Nome,
                    marca = m.Nome,
                    origem = m.Origem,
                    autonomia = car.Autonomia,
                    potencia = car.Potencia,
                    peso = car.Peso
                }
            ).FirstAsync(); 
            
            if(id == null || carro == null)
                return NotFound();

            return StatusCode(200, carro);
        }

        [HttpPost]
        [Route("/carros")]
        public async Task<IActionResult> Create([Bind("IdCarro, Modelo, Potencia, Autonomia, Peso, Ano, IdMarca, IdCategoria")] Carro carro)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, carro);

            _context.Add(carro);
            await _context.SaveChangesAsync();
            return StatusCode(201, carro);
        }

        [HttpPut]
        [Route("/carros/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarro, Modelo, Pitencia, Autonimia, Peso, Ano, IdMarca, IdCategoria")] Carro carro)
        {
            if(!ModelState.IsValid)
                return StatusCode(404, carro);
            
            try
            {
                carro.IdCarro = id;
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
        public async Task<IActionResult> Destroy(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

        private bool CarroExists(int id)
        {
            return _context.Carros.Any(c => c.IdCarro == id);
        }
    }
}