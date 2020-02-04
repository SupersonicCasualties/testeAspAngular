using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    public class CondicaoPagamento : BaseModelo
    {
        [Required(ErrorMessage = "Por favor, informe a descrição.")] public string Descricao { get; set; }

        public int Tipo { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}