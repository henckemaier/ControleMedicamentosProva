using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class Funcionario : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _login;
        private readonly string _senha;

        public string Nome { get => _nome; }

        public Funcionario(string nome, string login, string senha)
        {
            _nome = nome;
            _login = login;
            _senha = senha;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                erros.Add("Um funcionario precisa ter um nome válido!");

            if (string.IsNullOrEmpty(_login))
                erros.Add("Um funcionario precisa ter um login!");

            if (string.IsNullOrEmpty(_senha))
                erros.Add("Um funcionario precisa ter uma senha!");

            return new ResultadoValidacao(erros);
        }
    }
}
