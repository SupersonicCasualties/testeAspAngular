using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;

namespace AspNetWebApi.Classes
{
    public class produtoCategoriaClass : BaseClass
    {
        public string Descricao { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public produtoCategoriaClass() { }

        public produtoCategoriaClass mapFromModel(ProdutoCategoria categoria)
        {
            Id = categoria.Id;
            Descricao = categoria.descricao;
            CreatedAt = categoria.CreatedAt;
            UpdatedAt = categoria.UpdatedAt;

            return this;
        }

        public ProdutoCategoria mapToModel(bool create)
        {
            var categoria = new ProdutoCategoria();
            categoria.descricao = Descricao;
            categoria.CreatedAt = create ? DateTime.Now : CreatedAt;
            categoria.UpdatedAt = DateTime.Now;

            return categoria;
        }

        public ProdutoCategoria mapToModel(ProdutoCategoria categoria)
        {
            categoria.descricao = Descricao ?? categoria.descricao;
            categoria.CreatedAt = categoria.CreatedAt;
            categoria.UpdatedAt = DateTime.Now;

            return categoria;
        }
    }
}