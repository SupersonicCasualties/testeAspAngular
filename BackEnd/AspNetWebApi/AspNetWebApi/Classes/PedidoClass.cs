using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;

namespace AspNetWebApi.Classes
{
    public class PedidoClass : BaseClass
    {
        public long ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public decimal ValorLiquido { get; set; }

        public decimal ValorBruto { get; set; }

        public decimal Desconto { get; set; }

        public DateTime DataHora { get; set; }

        public long CondicaoPagamentoId { get; set; }

        public CondicaoPagamento CondicaoPagamento { get; set; }

        public ICollection<PedidoItemClass> PedidoItems { get; set; }

        public ICollection<PedidoItem> PedidoItemsModel { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public  PedidoClass() { }

        public PedidoClass mapFromModel(Pedido pedido)
        {
            Id = pedido.Id;
            ClienteId = pedido.Cliente.Id;
            Cliente = pedido.Cliente;
            ValorLiquido = pedido.ValorLiquido;
            ValorBruto = pedido.ValorBruto;
            Desconto = pedido.Desconto;
            DataHora = pedido.DataHora;
            CondicaoPagamentoId = pedido.CondicaoPagamento.Id;
            CondicaoPagamento = pedido.CondicaoPagamento;
            CreatedAt = pedido.CreatedAt;
            UpdatedAt = pedido.UpdatedAt;

            if (pedido.PedidoItems.Count <= 0) return this;
            List<PedidoItemClass> items = new List<PedidoItemClass>();
            foreach (var item in pedido.PedidoItems)
            {
                PedidoItemClass itemClass = new PedidoItemClass();
                itemClass.mapFromModel(item);
                items.Add(itemClass);
            }

            PedidoItems = items;

            return this;
        }

        public Pedido mapToModel(bool create)
        {
            Pedido pedido = new Pedido();

            pedido.Cliente = Cliente;
            pedido.ValorLiquido = ValorLiquido;
            pedido.ValorBruto = ValorBruto;
            pedido.Desconto = Desconto;
            pedido.DataHora = DataHora;
            pedido.CondicaoPagamento = CondicaoPagamento;
            pedido.CreatedAt = create ? DateTime.Now : CreatedAt;
            pedido.UpdatedAt = DateTime.Now;

            return pedido;
        }

        public Pedido mapToModel(Pedido pedido)
        {
            pedido.ValorLiquido = ValorLiquido > 0 ? ValorLiquido : pedido.ValorLiquido;
            pedido.ValorBruto = ValorBruto > 0 ? ValorBruto : pedido.ValorBruto;
            pedido.Desconto = Desconto > 0 ? Desconto : pedido.Desconto;
            pedido.DataHora = DataHora;
            pedido.CreatedAt = pedido.CreatedAt;
            pedido.UpdatedAt = DateTime.Now;

            return pedido;
        }

    }
}