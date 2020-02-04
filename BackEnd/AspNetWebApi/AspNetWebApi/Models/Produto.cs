using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetWebApi.Models
{
    public class Produto : BaseModelo
    {
        [Required(ErrorMessage = "Por favor, informe a Descrição."), MaxLength(250)]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Por favor, informe o Preço de Venda do produto.")]
        public decimal PrecoVenda { get; set; }

        [MaxLength]
        public string Imagem { get; set; }

        public int CodigoBarras { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Categoria do produto.")]
        public ProdutoCategoria ProdutoCategoria { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}