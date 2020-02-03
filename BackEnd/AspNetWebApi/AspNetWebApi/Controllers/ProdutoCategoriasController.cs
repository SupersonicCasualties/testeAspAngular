using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using AspNetWebApi.Classes;
using AspNetWebApi.Context;
using AspNetWebApi.Models;
using AspNetWebApi.Utils;

namespace AspNetWebApi.Controllers
{
    public class ProdutoCategoriasController : ApiController
    {
        private Contexto db = new Contexto();

        [HttpGet]
        public IHttpActionResult Get()
        {
            //return db.ProdutoCategorias;

            var _dbCon = db.ProdutoCategorias;

            var categorias = _dbCon.ToList();
            var categoriasList = new List<BaseClass>();

            foreach (var cate in categorias)
            {
                var categoria = new produtoCategoriaClass();
                categoria.mapFromModel(cate);
                categoriasList.Add(categoria);
            }

            return Util.ResponseSuccess(Request, categoriasList, "Sucesso");
        }

        [HttpGet]
        [Route("api/produtocategorias/{id}")]
        public IHttpActionResult Get(long id)
        {
            var categoria = db.ProdutoCategorias.Find(id);

            if (categoria == null)
            {
                return Util.ResponseError(Request, "Categoria de Produto não encontrado!");
            }
            BaseClass produtoCategoria = new produtoCategoriaClass().mapFromModel(categoria);

            return Util.ResponseSuccess(Request, produtoCategoria, "Sucesso");
        }

        [HttpPut]
        [Route("api/produtocategorias/{id}/update")]
        public IHttpActionResult Update(long id, produtoCategoriaClass produtoCategoriaClass)
        {
            var _Cate = db.ProdutoCategorias;
            ProdutoCategoria categoria = _Cate.Find(id);

            try
            {
                if (categoria != null)
                {
                    produtoCategoriaClass.mapToModel(categoria);

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            produtoCategoriaClass.mapFromModel(categoria);

            return Util.ResponseSuccess(Request, produtoCategoriaClass, "Categoria de Produto atualizado com sucesso!");
        }

        [HttpPost]
        public IHttpActionResult Novo(produtoCategoriaClass produtoCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categoria = produtoCategoria.mapToModel(true);

                db.ProdutoCategorias.Add(categoria);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, produtoCategoria, "Categoria de Produto inserido com sucesso!");
        }

        [HttpDelete]
        [Route("api/produtocategorias/{id}/remove")]
        public IHttpActionResult DeleteProdutoCategoria(long id)
        {
            try
            {
                ProdutoCategoria produtoCategoria = db.ProdutoCategorias.Find(id);
                if (produtoCategoria == null)
                {
                    return NotFound();
                }

                db.ProdutoCategorias.Remove(produtoCategoria);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, "Categoria de Produto removido com sucesso");
        }

    }
}