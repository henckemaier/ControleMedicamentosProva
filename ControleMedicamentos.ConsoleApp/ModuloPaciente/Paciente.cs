using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {


        private readonly string nome;
        private readonly string idade;
        private readonly string telefone;


        private readonly List<Requisicao> historicoRequisicao = new List<Requisicao>();
        public string Nome => nome;
        public string Idade => idade;
        public string Telefone => telefone;
        public Paciente(string nome, string idade, string telefone)
        {
            this.nome = nome;
            this.idade = idade;
            this.telefone = telefone;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                " Nome " + Nome + Environment.NewLine +
                " Idade " + Idade + Environment.NewLine +
                " Telefone " + Telefone + Environment.NewLine;
        }

        public void RegistrarRequisicao(Requisicao requisicao)
        {
            historicoRequisicao.Add(requisicao);
        }

        public bool TemRequisicaoEmAberto()
        {
            bool temRequisicaoEmAberto = false;

            foreach (Requisicao requisicao in historicoRequisicao)
            {
                if (requisicao.EstaAberto)
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

            if (string.IsNullOrEmpty(nome))
                erros.Add("Um paciente precisa ter um nome válido!");

            if (string.IsNullOrEmpty(idade))
                erros.Add("Um paciente precisa ter uma idade!");

            if (telefone.Length < 9)
                erros.Add("Um paciente precisa ter um número de telefone com 9 digitos!");

            return new ResultadoValidacao(erros);
        }
    }
}
