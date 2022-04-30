using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V3.Models
{
    [Table("Compra")]
    public class Compra
    {
        [Key] //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [ForeignKey("CarrinhoId")]
        public int CarrinhoId { get; set; }


        [Required(ErrorMessage = "Campo Obrigatorio")]
        [Column("Valor",TypeName = "double")]
        public decimal Valor { get; set; }

        public Carrinho Carrinho { get; set; }

    }
}