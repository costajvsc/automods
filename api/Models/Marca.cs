using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("marcas")]
    public partial class Marca
    {
        [Key]
        public int IdMarca { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(3)]
        public string Origem { get; set; }

        public DateTime Fundacao { get; set; }

        [JsonIgnore] 
        public List<Carro> Carros { get; set; }
    }
}