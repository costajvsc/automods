using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models 
{
    [Table("carros")]
    public partial class Carro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; }
        
        public int Potencia { get; set; }
        public double Autonomia { get; set; }
        public double Peso { get; set; }
        public int Ano { get; set; }
        
    }
}