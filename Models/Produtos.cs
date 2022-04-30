using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V3.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key] //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [MaxLength(50)]
        [Column("Nome",TypeName = "varchar")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Campo Obrigatorio")]
        [Column("Preco",TypeName = "double")]
        public decimal Preco { get; set; }
    }
}