using Auvo.ProcessoSeletivo.Infraestrutura.Objetos;

namespace AuvoProcessoSeletivo.Negocio.Objetos
{
    public class RegistroFuncionario : ObjetoBase
    {
        public RegistroFuncionario()
        {
            Data = new DateOnly();
            Saida = new TimeOnly();
            HorarioAlmoco = new Escala();
        }
        public string? Nome { get; set; }
        public double ValorHora { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Entrada { get; set; }
        public TimeOnly Saida { get; set; }
        public Escala? HorarioAlmoco { get; set; }
    }
}
