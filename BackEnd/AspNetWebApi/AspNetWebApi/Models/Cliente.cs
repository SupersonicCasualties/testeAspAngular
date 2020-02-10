using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    public class Cliente : BaseModelo
    {

        [Required(ErrorMessage = "Nome é obrigatório!"), MaxLength(100)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "E-Mail é obrigatório!"), MaxLength(200), EmailAddress(ErrorMessage = "Email invalido!"), Index(IsUnique = true)]
        public string Email { get; set; }

        [MaxLength(14)]
        public string Cpf { get; set; }

        [MaxLength(2)]
        public string Uf { get; set; }

        [MaxLength(50)]
        public string Cidade { get; set; }

        [MaxLength(50)]
        public string Bairro { get; set; }

        [MaxLength(50)]
        public string Rua { get; set; }

        public int? Numero { get; set; }

        [MaxLength(10)]
        public string Cep { get; set; }

        [MaxLength(30)]
        public string PontoReferencia { get; set; }

        [MaxLength(30)]
        public string Complemento { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}