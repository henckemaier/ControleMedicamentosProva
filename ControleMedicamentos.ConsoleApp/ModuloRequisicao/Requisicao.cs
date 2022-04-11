using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloRemedio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class Requisicao : EntidadeBase
    {
        public Paciente paciente;
        public Remedio remedio;
        private DateTime dataRequisicao;
        private bool estaAberto;
        public bool EstaAberto { get => estaAberto; }
        public DateTime DataRequisicao { get => dataRequisicao; }
        public string Status { get => EstaAberto ? "Aberto" : "Fechado"; }

        public Requisicao(Paciente paciente, Remedio remedio)
        {
            this.paciente = paciente;
            this.remedio = remedio;

            Abrir();
        }

        public override string ToString()
        {
            return "ID: " + id + Environment.NewLine +
                "Remédio requisitado: " + remedio.Nome + Environment.NewLine +
                "Nome do Paciente: " + paciente.Nome + Environment.NewLine +
                "Data da requisição: " + DataRequisicao.ToShortDateString() + Environment.NewLine +
                "Status da requisição: " + Status + Environment.NewLine;
        }

        public void Abrir()
        {
            if (!estaAberto)
            {
                estaAberto = true;
                dataRequisicao = DateTime.Today;

                paciente.RegistrarRequisicao(this);
                remedio.RegistrarRequisicao(this);
            }
        }

        public void Fechar()
        {
            if (estaAberto)
            {
                estaAberto = false;

                DateTime dataRealDevolucao = DateTime.Today;
            }
        }

        public override ResultadoValidacao Validar()
        {
            return new ResultadoValidacao(new List<string>());
        }
    }
}
