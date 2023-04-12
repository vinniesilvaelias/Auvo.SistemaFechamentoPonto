using Auvo.ProcessoSeletivo.Infraestrutura.Dto;

namespace AuvoProcessoSeletivo.Negocio.Dto
{
    public class DtoFuncionario : DtoBase
    {
        public string? Nome { get; set; }
        public double ValorHora { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Entrada { get; set; }
        public TimeOnly Saida { get; set; }
        public string? Almoco { get; set; }
    }
}
