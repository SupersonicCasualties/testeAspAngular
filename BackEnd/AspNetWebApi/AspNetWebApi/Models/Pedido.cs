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

        public decimal ValorTotal { get; set; }

        public decimal Desconto { get; set; }

        //[Required]
        //public CondicaoPagamento condicaoPagamento { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}