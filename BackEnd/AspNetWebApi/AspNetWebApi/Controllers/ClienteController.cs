using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AspNetWebApi.Context;
using AspNetWebApi.Classes;
using AspNetWebApi.Models;
using AspNetWebApi.Utils;

namespace AspNetWebApi.Controllers
{
    public class ClienteController : ApiController
    {
        Contexto db = new Contexto();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var clientes = db.Clientes.ToList();
            
            var clientesList = new List<BaseClass>();

            foreach (var cliente in clientes)
            {
                var cli = new ClienteClass();
                cli.MapFromClienteModel(cliente);
                clientesList.Add(cli);
            }

            return Util.ResponseSuccess(Request, clientesList, "Sucesso");
        }

        [HttpGet]
        [Route("api/cliente/{id}")]
        public IHttpActionResult Get(long id)
        {
            Cliente cliente = db.Clientes.Find(id);

            if (cliente == null)
            {
                return Util.ResponseError(Request, "Cliente não encontrado!");
            }

            BaseClass clienteClass = new ClienteClass().MapFromClienteModel(cliente);

            return Util.ResponseSuccess(Request, clienteClass, "Sucesso");
        }

        [HttpPost]
        [Route("api/cliente/novo")]
        public IHttpActionResult Novo(ClienteClass clienteClass)
        {
            try
            {
                var _Cli = db.Clientes;

                var dbCliente = clienteClass.MapToClienteModel(true);

                _Cli.Add(dbCliente);
                db.SaveChanges();

                clienteClass.MapFromClienteModel(dbCliente);
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, clienteClass, "Cliente inserido com sucesso!");
        }

        [HttpPut]
        [Route("api/cliente/{id}/update")]
        public IHttpActionResult Update(long Id, ClienteClass clienteClass)
        {
            Cliente cliente = db.Clientes.Find(Id);

            try
            {
                if (cliente != null)
                {
                    clienteClass.MapToClienteModel(cliente);

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            clienteClass.MapFromClienteModel(cliente);

            return Util.ResponseSuccess(Request, clienteClass, "Cliente atualizado com sucesso!");
        }

        [HttpDelete]
        [Route("api/cliente/{id}/remove")]
        public IHttpActionResult Remove(long id)
        {
            try
            {
                Cliente cliente = db.Clientes.Find(id);

                db.Clientes.Remove(cliente);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, "Cliente removido com sucesso");
        }
    }
}
