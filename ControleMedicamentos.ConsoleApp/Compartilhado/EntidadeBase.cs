namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase
    {
        public int id;

        public abstract ResultadoValidacao Validar();
    }
}
