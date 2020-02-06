using AspNetWebApi.Classes;
using AspNetWebApi.Context;
using AspNetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public PedidoService()
        {
        }

        public Pedido ProcessaNovoPedido()
        {
            preencherCampos(this.pedidoClass);

            Pedido pedido = pedidoClass.mapToModel(true);

            Pedido ped = db.Pedidos.Add(pedido);
            adicionarPedidoItems(pedido, pedidoClass.PedidoItems);
            db.SaveChanges();

            return ped;
        }

        public Pedido ProcessaEdicaoPedido()
        {
            preencherCampos(this.pedidoClass);

            Pedido pedido = this.pedidoClass.mapToModel(true);

            adicionarPedidoItems(pedido, this.pedidoClass.PedidoItems);
            db.SaveChanges();

            return pedido;
        }

        private void adicionarPedidoItems(Pedido pedido, ICollection<PedidoItemClass> pedidoItems)
        {
            List<PedidoItem> itemModel = new List<PedidoItem>();

            foreach (var item in pedidoItems)
            {
                item.Pedido = pedido;
                PedidoItem pedidoItem = item.mapToModel(true);
                itemModel.Add(pedidoItem);
                db.PedidoItems.Add(pedidoItem);
            }

            pedido.PedidoItems = itemModel;
        }

        private void preencherCampos(PedidoClass pedidoClass)
        {

            if (pedidoClass.Id > 0)
            {
                Pedido ped = db.Pedidos
                    .Where(p => p.Id == pedidoClass.Id)
                    .Include(p => p.PedidoItems)
                    .SingleOrDefault();

                if (ped == null)
                {
                    return;
                }

                pedidoClass.mapToModel(ped);
                db.PedidoItems.RemoveRange(ped.PedidoItems);
            }

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

        internal void RemovePedido(long id)
        {
            Pedido pedido = db.Pedidos.First(p => p.Id == id);

            if (pedido == null) return;

            db.Pedidos.Remove(pedido);
            db.SaveChanges();
        }
    }
}