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
    public class TelaCadastroRequisicao : TelaBase
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Requisicao> repositorioRequisicao;
        private readonly IRepositorio<Remedio> repositorioRemedio;
        private readonly IRepositorio<Paciente> repositorioPaciente;
        private readonly TelaCadastroRemedio telaCadastroRemedio;
        private readonly TelaCadastroPaciente telaCadastroPaciente;
        public TelaCadastroRequisicao(
        Notificador notificador,
            IRepositorio<Requisicao> repositorioRequisicao,
            IRepositorio<Remedio> repositorioRemedio,
            IRepositorio<Paciente> repositorioPaciente,
            TelaCadastroRemedio telaCadastroRemedio,
            TelaCadastroPaciente telaCadastroPaciente) : base("Cadastro de Requisições")
        {
            this.notificador = notificador;
            this.repositorioRequisicao = repositorioRequisicao;
            this.repositorioRemedio = repositorioRemedio;
            this.repositorioPaciente = repositorioPaciente;
            this.telaCadastroRemedio = telaCadastroRemedio;
            this.telaCadastroPaciente = telaCadastroPaciente;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Registrar Requisição");
            Console.WriteLine("Digite 2 para Visualizar");
            Console.WriteLine("Digite 3 para Visualizar Requisição em Aberto");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void RegistrarRequisicao()
        {
            MostrarTitulo("Inserindo uma nova requisicao");

            Paciente pacienteSelecionado = ObtemPaciente();

            if (pacienteSelecionado == null)
            {
                notificador.ApresentarMensagem("Nenhum paciente selecionado", TipoMensagem.Erro);
                return;
            }

            if (pacienteSelecionado.TemRequisicaoEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo já tem um empréstimo em aberto.", TipoMensagem.Erro);
                return;
            }

            //Validação Remedio
            Remedio remedioSelecionado = ObtemRemedio();

            if (remedioSelecionado.EstaRequisitado())
            {
                notificador.ApresentarMensagem("A revista selecionada já foi emprestada.", TipoMensagem.Erro);
                return;
            }

            Requisicao requisicao = ObtemRequisicao(pacienteSelecionado, remedioSelecionado);

            string statusValidacao = repositorioRequisicao.Inserir(requisicao);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Requisição cadastrada com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

       

       

        public bool VisualizarRequisicaoEmAberto()
        {
                MostrarTitulo("Visualização de Empréstimos em Aberto");

            List<Requisicao> requisicaos = repositorioRequisicao.Filtrar(x => x.EstaAberto);

            if (requisicaos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum emprestimo em aberto disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao requisicao in requisicaos)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRequisicao()
        { 
                MostrarTitulo("Visualização de Requisições");

            List<Requisicao> requisicaos = repositorioRequisicao.SelecionarTodos();

            if (requisicaos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum empréstimo disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao requisicao in requisicaos)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }


        #region Métodos privados
        private Requisicao ObtemRequisicao(Paciente paciente, Remedio remedio)
        {
            Requisicao novaRequisicao = new Requisicao(paciente, remedio);

            return novaRequisicao;
        }

        private Paciente ObtemPaciente()
        {
            bool temPacientesDisponiveis = telaCadastroPaciente.VisualizarRegistros("Pesquisando");

            if (!temPacientesDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum paciente disponível para cadastrar requisição.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID do paciente que irá pegar a requisição: ");
            int numeroPacienteRequisicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistro(numeroPacienteRequisicao);

            return pacienteSelecionado;
        }

        private Remedio ObtemRemedio()
        {
            bool temRemedioDisponiveis = telaCadastroRemedio.VisualizarRegistros("Pesquisando");

            if (!temRemedioDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum remedio disponível para cadastrar requisição.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da revista que irá será emprestada: ");
            int numeroRemedioRequisicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Remedio remedioSelecionado = repositorioRemedio.SelecionarRegistro(numeroRemedioRequisicao);

            return remedioSelecionado;
        }

        private int ObterNumeroRequisicao()
        {
            int numeroRequisicao;
            bool numeroRequisicaoEncontrado;

            do
            {
                Console.Write("Digite o número do empréstimo que deseja selecionar: ");
                numeroRequisicao = Convert.ToInt32(Console.ReadLine());

                numeroRequisicaoEncontrado = repositorioRequisicao.ExisteRegistro(numeroRequisicao);

                if (!numeroRequisicaoEncontrado)
                    notificador.ApresentarMensagem("Número de empréstimo não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (!numeroRequisicaoEncontrado);

            return numeroRequisicao;
        }
        #endregion
    }
}
