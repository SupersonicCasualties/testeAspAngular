using AspNetWebApi.Classes;
using AspNetWebApi.Context;
using AspNetWebApi.Models;
using AspNetWebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class ProdutoController : ApiController
    {
        private Contexto db = new Contexto();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var _dbCon = db.Produtos;

            var produtos = _dbCon.ToList();
            var produtosList = new List<BaseClass>();

            foreach (var prod in produtos)
            {
                var produto = new ProdutoClass();
                produto.mapFromModel(prod);
                produtosList.Add(produto);
            }

            return Util.ResponseSuccess(Request, produtosList, "Sucesso");
        }

        [HttpGet]
        [Route("api/produtos/{id}")]
        public IHttpActionResult Get(long id)
        {
            var produto = db.Produtos.Find(id);

            if (produto == null)
            {
                return Util.ResponseError(Request, "Produto não encontrado!");
            }

            BaseClass produtoClass = new ProdutoClass().mapFromModel(produto);

            return Util.ResponseSuccess(Request, produtoClass, "Sucesso");
        }

        [HttpDelete]
        [Route("api/produtos/{id}/remove")]
        public IHttpActionResult DeleteProdutoCategoria(long id)
        {
            try
            {
                Produto produto = db.Produtos.Find(id);
                if (produto == null)
                {
                    return NotFound();
                }

                db.Produtos.Remove(produto);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, "Produto removido com sucesso");
        }
    }
}
