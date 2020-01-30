using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Caching;
using System.Web.Http;
using AspNetWebApi.Context;
using AspNetWebApi.Classes;
using AspNetWebApi.Models;

namespace AspNetWebApi.Controllers
{
    public class ClienteController : ApiController
    {

        [HttpGet]
        public List<ClienteClass> Get()
        {
            var _dbCon = new Contexto().Clientes;

            var clientes = _dbCon.ToList();
            var clientesList = new List<ClienteClass>();

            foreach (var cliente in clientes)
            {
                var cli = new ClienteClass();
                cli.MapFromClienteModel(cliente);
                clientesList.Add(cli);
            }

            return clientesList;
        }

        [HttpPost]
        public HttpResponseMessage Novo(ClienteClass clienteClass)
        {
            var _dbCon = new Contexto();
            var _Cli = _dbCon.Clientes;

            var dbCliente = clienteClass.MapToClienteModel(true);

            var a = _Cli.Add(dbCliente);
            var b = _dbCon.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut]
        [Route("api/cliente/{id}/update")]
        public bool Update(long Id, ClienteClass clienteClass)
        {
            var _dbCon = new Contexto();
            var _Cli = _dbCon.Clientes;
            bool status = true;

            try
            {
                Cliente cliente = _Cli.Find(Id);
                if (cliente != null)
                {
                    clienteClass.MapToClienteModel(cliente);

                    _dbCon.SaveChanges();
                }
            }
            catch (DbEntityValidationException val)
            {
                status = false;
                string errors = "";
                foreach (var error in val.EntityValidationErrors)
                {
                    foreach (var err in error.ValidationErrors)
                    {
                        errors += $"Propriedade: {err.PropertyName}, Erro: {err.ErrorMessage}";
                    }
                }

            }
            catch (Exception e)
            {
                status = false;
            }

            return status;
        }

        [HttpDelete]
        [Route("api/cliente/{id}/remove")]
        public bool Remove(long id)
        {
            var _dbCon = new Contexto();
            var _Cli = _dbCon.Clientes;

            Cliente cliente = _Cli.Find(id);

            _Cli.Remove(cliente);
            _dbCon.SaveChanges();

            return true;
        }
    }
}
