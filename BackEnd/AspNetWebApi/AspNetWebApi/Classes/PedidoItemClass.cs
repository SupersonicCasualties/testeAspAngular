using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;

namespace AspNetWebApi.Classes
{
    public class PedidoItemClass : BaseClass
    {
        public Pedido Pedido { get; set; }

        public long ProdutoId { get; set; }

        public Produto Produto { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal ValorBruto { get; set; }

        public decimal ValorLiquido { get; set; }

        public int Quantidade { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public PedidoItemClass() { }

        public PedidoItemClass mapFromModel(PedidoItem item)
        {
            Id = item.Id;
            Pedido = item.Pedido;
            ProdutoId = item.Produto.Id;
            Produto = item.Produto;
            ValorUnitario = item.ValorUnitario;
            ValorBruto = item.ValorBruto;
            ValorLiquido = item.ValorLiquido;
            Quantidade = item.Quantidade;
            CreatedAt = item.CreatedAt;
            UpdatedAt = item.UpdatedAt;

            return this;
        }

        public PedidoItem mapToModel(bool create)
        {
            PedidoItem item = new PedidoItem();

            item.Pedido = Pedido;
            item.Produto = Produto;
            item.ValorUnitario = ValorUnitario;
            item.ValorBruto = ValorBruto;
            item.ValorLiquido = ValorLiquido;
            item.Quantidade = Quantidade;
            item.CreatedAt = create ? DateTime.Now : CreatedAt;
            item.UpdatedAt = DateTime.Now;

            return item;
        }

        public PedidoItem mapToModel(PedidoItem item)
        {
            item.ValorUnitario = ValorUnitario > 0 ? ValorUnitario : item.ValorUnitario;
            item.ValorBruto = ValorBruto > 0 ? ValorBruto : item.ValorBruto;
            item.ValorLiquido = ValorLiquido > 0 ? ValorLiquido : item.ValorLiquido;
            item.Quantidade = Quantidade > 0 ? Quantidade : item.Quantidade;
            item.CreatedAt = item.CreatedAt;
            item.UpdatedAt = DateTime.Now;

            return item;
        }
    }
}