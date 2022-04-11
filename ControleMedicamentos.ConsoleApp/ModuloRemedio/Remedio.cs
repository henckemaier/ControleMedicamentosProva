using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloRemedio
{
    
    public class Remedio : EntidadeBase
    {
        private readonly List<Requisicao> historicoRequisicao = new List<Requisicao>();

        public Remedio( string nome, string descricao, double quantidade)
        {
            Nome = nome;
            Descricao = descricao;
            Quantidade = quantidade;

        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                " Nome " + Nome + Environment.NewLine +
                " Descricao " + Descricao + Environment.NewLine +
                " Quantidade " + Quantidade + Environment.NewLine;
        }

        public void RegistrarRequisicao(Requisicao requisicao)
        {
            historicoRequisicao.Add(requisicao);
        }

        internal bool EstaRequisitado()
        {
            bool temRequisicaoEmAberto = false;

            foreach (Requisicao requisicao in historicoRequisicao)
            {
                if (requisicao != null && requisicao.EstaAberto)
                {
                    temRequisicaoEmAberto = true;
                    break;
                }
            }
            return temRequisicaoEmAberto;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                erros.Add("Um remédio precisa ter um nome válido!");

            if (string.IsNullOrEmpty(Descricao))
                erros.Add("Um remédio precisa ter uma descrição!");

            if (Quantidade > -1)
                erros.Add("Um remédio precisa ter uma quantidade valida!");

            return new ResultadoValidacao(erros);
        }
    }
}
