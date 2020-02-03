using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Caching;
using System.Web.Http;
using AspNetWebApi.Context;
using AspNetWebApi.Classes;
using AspNetWebApi.Classes.Response;
using AspNetWebApi.Models;
using AspNetWebApi.Utils;

namespace AspNetWebApi.Controllers
{
    public class ClienteController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Get()
        {
            var _dbCon = new Contexto().Clientes;

            var clientes = _dbCon.ToList();
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
        [Route("api/clientes/{id}")]
        public IHttpActionResult Get(long id)
        {
            var _dbCon = new Contexto().Clientes;
            Cliente cliente = _dbCon.Find(id);

            if (cliente == null)
            {
                return Util.ResponseError(Request, "Cliente não encontrado!");
            }

            BaseClass clienteClass = new ClienteClass().MapFromClienteModel(cliente);

            return Util.ResponseSuccess(Request, clienteClass, "Sucesso");
        }

        [HttpPost]
        public IHttpActionResult Novo(ClienteClass clienteClass)
        {
            try
            {
                var _dbCon = new Contexto();
                var _Cli = _dbCon.Clientes;

                var dbCliente = clienteClass.MapToClienteModel(true);

                var a = _Cli.Add(dbCliente);
                var b = _dbCon.SaveChanges();
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
            var _dbCon = new Contexto();
            var _Cli = _dbCon.Clientes;
            Cliente cliente = _Cli.Find(Id);

            try
            {
                if (cliente != null)
                {
                    clienteClass.MapToClienteModel(cliente);

                    _dbCon.SaveChanges();
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
                var _dbcon = new Contexto();
                var _cli = _dbcon.Clientes;

                Cliente cliente = _cli.Find(id);

                _cli.Remove(cliente);
                _dbcon.SaveChanges();
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, "Cliente removido com sucesso");
        }
    }
}
