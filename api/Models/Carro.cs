using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models 
{
    [Table("carros")]
    public partial class Carro
    {
        [Key]
        public int IdCarro { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; }
        
        public int Potencia { get; set; }
        public double Autonomia { get; set; }
        public double Peso { get; set; }
        public int Ano { get; set; }
        
        public int MarcaId { get; set; }
        
        public Marca Marca { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
        
    }
}