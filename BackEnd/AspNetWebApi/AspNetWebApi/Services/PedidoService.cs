using AspNetWebApi.Classes;
using AspNetWebApi.Context;
using AspNetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Services
{
    public class PedidoService
    {

        Contexto db = new Contexto();

        PedidoClass pedidoClass;

        public PedidoService(PedidoClass pedidoClass)
        {
            this.pedidoClass = pedidoClass;
        }

        public Pedido ProcessaNovoPedido()
        {
            preencherCampos(pedidoClass);

            Pedido pedido = pedidoClass.mapToModel(true);

            Pedido ped = db.Pedidos.Add(pedido);
            adicionarPedidoItems(pedido, pedidoClass.PedidoItems);
            db.SaveChanges();

            return ped;
        }

        private void adicionarPedidoItems(Pedido pedido, ICollection<PedidoItemClass> pedidoItems)
        {
            foreach (var item in pedidoItems)
            {
                item.Pedido = pedido;
                PedidoItem pedidoItem = item.mapToModel(true);
                db.PedidoItems.Add(pedidoItem);
            }
        }

        private void preencherCampos(PedidoClass pedidoClass)
        {

            pedidoClass.Cliente = db.Clientes.Find(pedidoClass.ClienteId);
            pedidoClass.CondicaoPagamento = db.CondicaoPagamentos.Find(pedidoClass.CondicaoPagamentoId);

            decimal valorBruto = 0;
            decimal valorLiquido = 0;
            decimal desconto = 0;
            foreach (var item in pedidoClass.PedidoItems)
            {
                item.Produto = db.Produtos.Find(item.ProdutoId);
                valorLiquido += item.ValorLiquido;
                valorBruto += item.ValorBruto;
                desconto += item.Desconto;
            }

            pedidoClass.ValorBruto = valorBruto;
            pedidoClass.ValorLiquido = valorLiquido;
            pedidoClass.Desconto = desconto;

        }
    }
}