using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AspNetWebApi.Classes;
using AspNetWebApi.Context;
using AspNetWebApi.Models;
using AspNetWebApi.Utils;

namespace AspNetWebApi.Controllers
{
    public class ProdutoCategoriaController : ApiController
    {
        private Contexto db = new Contexto();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var categorias = db.ProdutoCategorias.OrderBy(c => c.descricao).ToList();
            var categoriasList = new List<BaseClass>();

            foreach (var cate in categorias)
            {
                var categoria = new ProdutoCategoriaClass();
                categoria.mapFromModel(cate);
                categoriasList.Add(categoria);
            }

            return Util.ResponseSuccess(Request, categoriasList, "Sucesso");
        }

        [HttpGet]
        [Route("api/produtocategoria/{id}")]
        public IHttpActionResult Get(long id)
        {
            var categoria = db.ProdutoCategorias.Find(id);

            if (categoria == null)
            {
                return Util.ResponseError(Request, "Categoria de Produto não encontrado!");
            }
            BaseClass produtoCategoria = new ProdutoCategoriaClass().mapFromModel(categoria);

            return Util.ResponseSuccess(Request, produtoCategoria, "Sucesso");
        }

        [HttpPost]
        [Route("api/produtocategoria/novo")]
        public IHttpActionResult Novo(ProdutoCategoriaClass produtoCategoria)
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

                produtoCategoria.mapFromModel(categoria);
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, produtoCategoria, "Categoria de Produto inserido com sucesso!");
        }

        [HttpPut]
        [Route("api/produtocategoria/{id}/update")]
        public IHttpActionResult Update(long id, ProdutoCategoriaClass produtoCategoriaClass)
        {
            ProdutoCategoria categoria = db.ProdutoCategorias.Find(id);

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

        [HttpDelete]
        [Route("api/produtocategoria/{id}/remove")]
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