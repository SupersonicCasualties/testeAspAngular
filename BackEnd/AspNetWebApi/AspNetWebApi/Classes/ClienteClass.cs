using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using AspNetWebApi.Models;

namespace AspNetWebApi.Classes
{
    public class ClienteClass : BaseClass
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public string Uf { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Rua { get; set; }

        public int? Numero { get; set; }

        public string PontoReferencia { get; set; }

        public string Complemento { get; set; }

        public bool Ativo { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Cep { get; set; }


        public ClienteClass() {}

        public ClienteClass MapFromClienteModel(Cliente cliente)
        {
            this.Id = cliente.Id;
            this.Nome = cliente.Nome;
            this.Email = cliente.Email;
            this.Cpf = cliente.Cpf;
            this.Uf = cliente.Uf;
            this.Cidade = cliente.Cidade;
            this.Bairro = cliente.Bairro;
            this.Rua = cliente.Rua;
            this.Numero = cliente.Numero;
            this.Cep = cliente.Cep;
            this.PontoReferencia = cliente.PontoReferencia;
            this.Complemento = cliente.Complemento;
            this.Ativo = cliente.Ativo;
            this.CreatedAt = cliente.CreatedAt;
            this.UpdatedAt = cliente.UpdatedAt;

            return this;
        }

        public Cliente MapToClienteModel(bool create)
        {
            var cliModel = new Cliente();
            cliModel.Nome = this.Nome;
            cliModel.Email = this.Email;
            cliModel.Cpf = this.Cpf;
            cliModel.Uf = this.Uf;
            cliModel.Cidade = this.Cidade;
            cliModel.Bairro = this.Bairro;
            cliModel.Rua = this.Rua;
            cliModel.Numero = this.Numero;
            cliModel.Cep = this.Cep;
            cliModel.PontoReferencia = this.PontoReferencia;
            cliModel.Complemento = this.Complemento;
            cliModel.Ativo = this.Ativo;
            cliModel.CreatedAt = create ? DateTime.Now : this.CreatedAt;
            cliModel.UpdatedAt = DateTime.Now;

            return cliModel;
        }

        public Cliente MapToClienteModel(Cliente cliente)
        {
            cliente.Nome = this.Nome;
            cliente.Email = this.Email;
            cliente.Cpf = this.Cpf;
            cliente.Uf = this.Uf;
            cliente.Cidade = this.Cidade;
            cliente.Bairro = this.Bairro;
            cliente.Rua = this.Rua;
            cliente.Numero = this.Numero;
            cliente.Cep = this.Cep;
            cliente.PontoReferencia = this.PontoReferencia;
            cliente.Complemento = this.Complemento;
            cliente.Ativo = this.Ativo;
            cliente.CreatedAt = this.CreatedAt;
            cliente.UpdatedAt = DateTime.Now;

            return cliente;
        }
    }
}