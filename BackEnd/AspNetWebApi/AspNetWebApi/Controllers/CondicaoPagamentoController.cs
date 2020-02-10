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
    public class CondicaoPagamentoController : ApiController
    {
        private Contexto db = new Contexto();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var condicoes = db.CondicaoPagamentos.OrderBy(c => c.Descricao).ToList();
            var condicaoList = new List<BaseClass>();

            foreach (var cond in condicoes)
            {
                var condicao = new CondicaoPagamentoClass();
                condicao.mapFromModel(cond);
                condicaoList.Add(condicao);
            }

            return Util.ResponseSuccess(Request, condicaoList, "Sucesso");
        }

        [HttpGet]
        [Route("api/condicaopagamento/{id}")]
        public IHttpActionResult Get(long id)
        {
            var condicao = db.CondicaoPagamentos.Find(id);

            if (condicao == null)
            {
                return Util.ResponseError(Request, "Condição de Pagamento não encontrado!");
            }
            BaseClass condicaoPagamento = new CondicaoPagamentoClass().mapFromModel(condicao);

            return Util.ResponseSuccess(Request, condicaoPagamento, "Sucesso");
        }

        [HttpPut]
        [Route("api/condicaopagamento/{id}/update")]
        public IHttpActionResult Update(long id, CondicaoPagamentoClass condicaoPagamento)
        {
            CondicaoPagamento condicao = db.CondicaoPagamentos.Find(id);

            try
            {
                ValidateTipo(condicaoPagamento, false);

                if (condicao != null)
                {
                    condicaoPagamento.mapToModel(condicao);

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            condicaoPagamento.mapFromModel(condicao);

            return Util.ResponseSuccess(Request, condicaoPagamento, "Condição de Pagamento atualizada com sucesso!");
        }

        [HttpPost]
        [Route("api/condicaopagamento/novo")]
        public IHttpActionResult Novo(CondicaoPagamentoClass condicaoPagamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ValidateTipo(condicaoPagamento, true);

                var condicao = condicaoPagamento.mapToModel(true);

                db.CondicaoPagamentos.Add(condicao);
                db.SaveChanges();

                condicaoPagamento.mapFromModel(condicao);
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, condicaoPagamento, "Condição de Pagamento inserida com sucesso!");
        }

        [HttpDelete]
        [Route("api/condicaopagamento/{id}/remove")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                CondicaoPagamento condicaoPagamento = db.CondicaoPagamentos.Find(id);
                if (condicaoPagamento == null)
                {
                    return NotFound();
                }

                db.CondicaoPagamentos.Remove(condicaoPagamento);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Util.ResponseError(Request, e);
            }

            return Util.ResponseSuccess(Request, "Condição de Pagamento removida com sucesso");
        }

        private void ValidateTipo(CondicaoPagamentoClass condicaoPagamento, bool create)
        {
            if (condicaoPagamento.Tipo == 0 && create)
            {
                throw new Exception("O Tipo deve ser entre 1 e 3!");
            }
            else if ((condicaoPagamento.Tipo < 1 || condicaoPagamento.Tipo > 3) && create)
            {
                throw new Exception("O Tipo deve ser entre 1 e 3!");
            }
        }
    }
}
