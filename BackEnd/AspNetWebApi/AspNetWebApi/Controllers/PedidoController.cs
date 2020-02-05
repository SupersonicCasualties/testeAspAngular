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
                .ToList();

            var pedidosList = new List<BaseClass>();

            foreach (var ped in pedidos)
            {
                //var condicao = new CondicaoPagamentoClass();
                //condicao.mapFromModel(cond);
                //condicaoList.Add(condicao);
            }

            return Util.ResponseSuccess(Request, pedidosList, "Sucesso");
        }
    }
}
