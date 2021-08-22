using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("categorias")]
    public partial class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [JsonIgnore] 
        public List<Carro> Carros { get; set; }
    }
}