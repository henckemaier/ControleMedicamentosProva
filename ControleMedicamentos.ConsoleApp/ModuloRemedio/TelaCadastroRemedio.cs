using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloRemedio
{
    public class TelaCadastroRemedio : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioRemedio _repositorioRemedio;
        private readonly Notificador _notificador;

        public TelaCadastroRemedio(RepositorioRemedio repositorioRemedio, Notificador notificador)
            : base("Cadastro de Remedios")
        {
            _repositorioRemedio = repositorioRemedio;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Remedio");
            Remedio novoRemedio = ObterRemedio();

            _repositorioRemedio.Inserir(novoRemedio);

            _notificador.ApresentarMensagem("Remedio cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Remedio");

            bool temRemediosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRemediosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum remedio cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRemedio = ObterNumeroRegistro();

            Remedio remedioAtualizado = ObterRemedio();

            bool conseguiuEditar = _repositorioRemedio.Editar(numeroRemedio, remedioAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Remedio editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Remedio");

            bool temRemediosRegistrados = VisualizarRegistros("Pesquisando");

            if (temRemediosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum remedio cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroRemedio = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioRemedio.Excluir(numeroRemedio);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Remedio excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Remedios");

            List<Remedio> remedios = _repositorioRemedio.SelecionarTodos();

            if (remedios.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum fornecedor disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Remedio remedio in remedios)
                Console.WriteLine(remedio.ToString());

            Console.ReadLine();

            return true;
        }

        private Remedio ObterRemedio()
        {
            Console.WriteLine("Digite o nome do remedio: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a descrição do remedio: ");
            string descricao = Console.ReadLine();

            Console.WriteLine("Digite a quantidade do remedio: ");
            double quantidade = Convert.ToInt32(Console.ReadLine());


            return new Remedio(nome, descricao, quantidade);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Remédio que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioRemedio.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Remédio não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}
