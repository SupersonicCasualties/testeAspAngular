using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;

namespace AspNetWebApi.Classes
{
    public class CondicaoPagamentoClass : BaseClass
    {
        public string Descricao { get; set; }

        public int Tipo { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public CondicaoPagamentoClass() { }

        public CondicaoPagamentoClass mapFromModel(CondicaoPagamento condicao)
        {
            Id = condicao.Id;
            Descricao = condicao.Descricao;
            Tipo = condicao.Tipo;
            CreatedAt = condicao.CreatedAt;
            UpdatedAt = condicao.UpdatedAt;

            return this;
        }

        public CondicaoPagamento mapToModel(bool create)
        {
            CondicaoPagamento condicao = new CondicaoPagamento();
            condicao.Descricao = Descricao;
            condicao.Tipo = Tipo;
            condicao.CreatedAt = create ? DateTime.Now : condicao.CreatedAt;
            condicao.UpdatedAt = DateTime.Now;

            return condicao;
        }

        public CondicaoPagamento mapToModel(CondicaoPagamento condicao)
        {
            condicao.Descricao = Descricao ?? condicao.Descricao;
            condicao.Tipo = Tipo >= 1 || Tipo <= 3 ? condicao.Tipo : Tipo;
            condicao.CreatedAt = condicao.CreatedAt;
            condicao.UpdatedAt = DateTime.Now;

            return condicao;
        }
    }
}