using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;

namespace AspNetWebApi.Classes
{
    public class ProdutoClass : BaseClass
    {
        public string Descricao { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Imagem { get; set; }
        public int CodigoBarras { get; set; }
        public long ProdutoCategoriaId { get; set; }
        public ProdutoCategoria ProdutoCategoria { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ProdutoClass() { }

        public ProdutoClass mapFromModel(Produto produto)
        {
            Id = produto.Id;
            Descricao = produto.Descricao;
            PrecoVenda = produto.PrecoVenda;
            Imagem = produto.Imagem;
            CodigoBarras = produto.CodigoBarras;
            ProdutoCategoria = produto.ProdutoCategoria;
            CreatedAt = produto.CreatedAt;
            UpdatedAt = produto.UpdatedAt;

            return this;
        }

        public Produto mapToModel(bool create)
        {
            var produto = new Produto();
            produto.Descricao = Descricao;
            produto.PrecoVenda = PrecoVenda;
            produto.Imagem = Imagem;
            produto.ProdutoCategoria = ProdutoCategoria;
            produto.CodigoBarras = produto.CodigoBarras > 0 ? produto.CodigoBarras : CodigoBarras;
            produto.CreatedAt = create ? DateTime.Now : CreatedAt;
            produto.UpdatedAt = DateTime.Now;

            return produto;
        }

        public Produto mapToModel(Produto produto)
        {
            produto.Descricao = Descricao ?? produto.Descricao;
            produto.PrecoVenda = PrecoVenda > 0 ? PrecoVenda : produto.PrecoVenda;
            produto.Imagem = Imagem ?? produto.Imagem;
            produto.CodigoBarras = produto.CodigoBarras > 0 ? produto.CodigoBarras : CodigoBarras;
            produto.CreatedAt = produto.CreatedAt;
            produto.UpdatedAt = DateTime.Now;

            return produto;
        }

    }
}