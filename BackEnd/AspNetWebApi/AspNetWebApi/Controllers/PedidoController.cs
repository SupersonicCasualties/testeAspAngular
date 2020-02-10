using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AspNetWebApi.Classes;
using AspNetWebApi.Context;
using AspNetWebApi.Utils;
using System.Data.Entity;
using AspNetWebApi.Models;
using AspNetWebApi.Services;

namespace AspNetWebApi.Controllers
{
    public class PedidoController : ApiController
    {
        Contexto db = new Contexto();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var pedidos = db.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.CondicaoPagamento)
                .Include(p => p.PedidoItems)
                .Include("PedidoItems.Produto") 
                .OrderByDescending(p => p.DataHora)
                .Take(25)
                .ToList();

            var pedidosList = new List<BaseClass>();

            foreach (var ped in pedidos)
            {
                var pedidoClass = new PedidoClass();
                pedidoClass.mapFromModel(ped);
                pedidosList.Add(pedidoClass);
            }

            return Util.ResponseSuccess(Request, pedidosList, "Sucesso");
        }

        [HttpGet]
        [Route("api/pedido/{id}")]
        public IHttpActionResult Get(long id)
        {
            var pedido = db.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.CondicaoPagamento)
                .Include(p => p.PedidoItems)
                .Include("PedidoItems.Produto")
                .First(p => p.Id == id);

            PedidoClass pedidoClass = new PedidoClass();

            pedidoClass.mapFromModel(pedido);

            return Util.ResponseSuccess(Request, pedidoClass, "Sucesso");
        }

        [HttpPost]
        [Route("api/pedido/novo")]
        public IHttpActionResult Novo(PedidoClass pedidoClass)
        {
            try
            {
                PedidoService service = new PedidoService(pedidoClass);

                Pedido pedido = service.ProcessaNovoPedido();

                pedidoClass.mapFromModel(pedido);
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, pedidoClass, "Sucesso");
        }

        [HttpPut]
        [Route("api/pedido/{id}/update")]
        public IHttpActionResult Update(long id, PedidoClass pedidoClass)
        {
            try
            {
                PedidoService service = new PedidoService(pedidoClass);

                Pedido pedido = service.ProcessaEdicaoPedido(id);

                pedidoClass.mapFromModel(pedido);
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, pedidoClass, "Sucesso");
        }

        [HttpDelete]
        [Route("api/pedido/{id}/remove")]
        public IHttpActionResult Remove(long id)
        {
            try
            {
                PedidoService service = new PedidoService();

                service.RemovePedido(id);
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, "Pedido removido com sucesso");
        }
    }
}
