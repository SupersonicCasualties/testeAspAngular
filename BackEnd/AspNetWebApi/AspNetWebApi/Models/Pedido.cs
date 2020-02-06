using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetWebApi.Models
{
    public class Pedido : BaseModelo
    {
        [Required]
        public Cliente Cliente { get; set; }

        public decimal ValorLiquido { get; set; }

        public decimal ValorBruto { get; set; }

        public decimal Desconto { get; set; }

        public DateTime DataHora { get; set; }

        [Required]
        public CondicaoPagamento CondicaoPagamento { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<PedidoItem> PedidoItems { get; set; }
    }
}