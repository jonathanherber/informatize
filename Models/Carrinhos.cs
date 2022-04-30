using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace V3.Models
{
    [Table("Carrinho")]
    public class Carrinho
    {
        [Key] //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }


        [Required(ErrorMessage = "Campo Obrigatorio")]
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }


        [Required(ErrorMessage = "Campo Obrigatorio")]
        [ForeignKey("ProdutoId")]
        public int ProdutoId { get; set; }

        
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public int Quantidade { get; set; }
        
        [JsonIgnore]
        public Cliente? Cliente { get; private set; }
        [JsonIgnore]
        public Produto? Produto { get; private set; } 

    }
}