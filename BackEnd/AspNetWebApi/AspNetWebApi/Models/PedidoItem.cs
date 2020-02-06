using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    public class PedidoItem : BaseModelo
    {
        [Required]
        public Pedido Pedido { get; set; }

        [Required]
        public Produto Produto { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal ValorBruto { get; set; }

        public decimal ValorLiquido { get; set; }

        public decimal Desconto { get; set; }

        public int Quantidade { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}