using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                .ToList();

            var pedidosList = new List<BaseClass>();

            foreach (var ped in pedidos)
            {
                var condicao = new PedidoClass();
                //condicao.mapFromModel(cond);
                //condicaoList.Add(condicao);
            }

            return Util.ResponseSuccess(Request, pedidosList, "Sucesso");
        }

        [HttpPost]
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
    }
}
