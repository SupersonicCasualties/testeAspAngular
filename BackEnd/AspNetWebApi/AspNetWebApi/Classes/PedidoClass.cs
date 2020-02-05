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
        public Cliente Cliente { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal Desconto { get; set; }

        public DateTime DataHora { get; set; }

        public CondicaoPagamento condicaoPagamento { get; set; }

        public ICollection<PedidoItem> PedidoItems { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public  PedidoClass() { }

        public PedidoClass mapFromModel(Pedido pedido)
        {
            Id = pedido.Id;
            Cliente = pedido.Cliente;
            ValorTotal = pedido.ValorTotal;
            Desconto = pedido.Desconto;
            DataHora = pedido.DataHora;
            condicaoPagamento = pedido.CondicaoPagamento;
            PedidoItems = pedido.PedidoItems;
            CreatedAt = pedido.CreatedAt;
            UpdatedAt = pedido.UpdatedAt;

            return this;
        }

        public Pedido mapToModel(bool create)
        {
            Pedido pedido = new Pedido();

            pedido.Cliente = Cliente;
            pedido.ValorTotal = ValorTotal;
            pedido.Desconto = Desconto;
            pedido.DataHora = DataHora;
            pedido.CondicaoPagamento = condicaoPagamento;
            pedido.CreatedAt = create ? DateTime.Now : CreatedAt;
            pedido.UpdatedAt = DateTime.Now;

            return pedido;
        }

        public Pedido mapToModel(Pedido pedido)
        {
            pedido.ValorTotal = ValorTotal > 0 ? ValorTotal : pedido.ValorTotal;
            pedido.Desconto = Desconto > 0 ? Desconto : pedido.Desconto;
            pedido.DataHora = DataHora;
            pedido.CreatedAt = pedido.CreatedAt;
            pedido.UpdatedAt = DateTime.Now;

            return pedido;
        }

    }
}