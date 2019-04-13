using Eventos.IO.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventos.IO.Domain.MODELS
{
    public class Evento : Entity<Evento>
    {
        public Evento(
            string nome, 
            DateTime dataInicio, 
            DateTime dataFim, 
            bool gratuito,
            decimal valor,
            bool online,
            string nomeEmpresa)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
        }

        public string Nome { get; private set; }
        public string DescricaoCurta { get; private set; }
        public string DescricaoLonga { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Gratuito { get; private set; }
        public decimal Valor { get; private set; }
        public bool Online { get; private set; }
        public string NomeEmpresa { get; private set; }
        public Categoria Categoria { get; private set; }
        public ICollection<Tags> Tags{ get; private set; }
        public Endereco Endereco { get; private set; }
        public Organizador Organizador { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();

            ValidationResult = Validate(this);
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O Nome do evento precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do evento precisa ter entre 2 e 150 caractes");
        }

        private void ValidarValor()
        {
            if (!Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 50000)
                    .WithMessage("O valor precisa estar entre 1 e 50000");

            if (!Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(0, 0).When(e => e.Gratuito)
                    .WithMessage("O valor não deve ser diferente de 0 para evento gratuíto");
        }

        private void ValidarData()
        {
            RuleFor(c => c.DataInicio)
                .GreaterThan(c => c.DataFim)
                .WithMessage("A data inicio deve ser maior que a data final do evento");

            RuleFor(c => c.DataInicio)
                .LessThan(DateTime.Now)
                .WithMessage("A data de inicio não deve ser menor que a data atual");
        }

        private void ValidarLocal()
        {
            if (Online)
                RuleFor(c => c.Endereco)
                    .Null().When(c => c.Online == false)
                    .WithMessage("O evento não deve possuir um endereço se online");

            if (!Online)
                RuleFor(c => c.Endereco)
                    .NotNull().When(c => c.Online == false)
                    .WithMessage("O evento deve possuir um endereço");
        }

        private void ValidarNomeEmpresa()
        {
            RuleFor(c => c.NomeEmpresa)
                .NotEmpty().WithMessage("O nome do organizador precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do organizador precisa ter entre 2 e 150 caracteres");
        }
    }
}
