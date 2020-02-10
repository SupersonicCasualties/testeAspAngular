using AspNetWebApi.Classes;
using AspNetWebApi.Context;
using AspNetWebApi.Models;
using AspNetWebApi.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class ProdutoController : ApiController
    {
        private Contexto db = new Contexto();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var produtos = db.Produtos.Include(b => b.ProdutoCategoria).OrderBy(p => p.Descricao).ToList();
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
        [Route("api/produto/{id}")]
        public IHttpActionResult Get(long id)
        {
            var produto = db.Produtos.Include(b => b.ProdutoCategoria).First(b => b.Id == id);

            if (produto == null)
            {
                return Util.ResponseError(Request, "Produto não encontrado!");
            }

            BaseClass produtoClass = new ProdutoClass().mapFromModel(produto);

            return Util.ResponseSuccess(Request, produtoClass, "Sucesso");
        }

        [HttpPost]
        [Route("api/produto/novo")]
        public IHttpActionResult Novo(ProdutoClass produtoClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ProdutoCategoria categoria = db.ProdutoCategorias.First(x => x.Id == produtoClass.ProdutoCategoriaId);
                produtoClass.ProdutoCategoria = categoria;
                produtoClass.CodigoBarras = GenerateCodigoBarras();

                var produto = produtoClass.mapToModel(true);

                db.Produtos.Add(produto);
                db.SaveChanges();

                produtoClass.mapFromModel(produto);
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, produtoClass, "Produto inserido com sucesso!");
        }

        [HttpPut]
        [Route("api/produto/{id}/update")]
        public IHttpActionResult Update(long id, ProdutoClass produtoClass)
        {
            Produto produto = db.Produtos.Include(b => b.ProdutoCategoria).First(b => b.Id == id);

            try
            {
                if (produto != null)
                {
                    if (produtoClass.ProdutoCategoriaId != produto.ProdutoCategoria.Id)
                    {
                        ProdutoCategoria categoria = db.ProdutoCategorias.First(x => x.Id == produtoClass.ProdutoCategoriaId);
                        produto.ProdutoCategoria = categoria;
                    }

                    produtoClass.mapToModel(produto);

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            produtoClass.mapFromModel(produto);

            return Util.ResponseSuccess(Request, produtoClass, "Produto atualizado com sucesso!");
        }

        [HttpDelete]
        [Route("api/produto/{id}/remove")]
        public IHttpActionResult DeleteProduto(long id)
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

        private int GenerateCodigoBarras()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);

            if (CodigoBarrasExiste(myRandomNo))
            {
                GenerateCodigoBarras();
            }

            return myRandomNo;
        }

        private bool CodigoBarrasExiste(int codigo)
        {
            return db.Produtos.Any(x => x.CodigoBarras == codigo);
        }
    }
}
