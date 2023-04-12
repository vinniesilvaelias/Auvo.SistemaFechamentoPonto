using Auvo.ProcessoSeletivo.Infraestrutura.Objetos;
using System.Text.Json.Serialization;

namespace AuvoProcessoSeletivo.Negocio.Model
{
    public  class FuncionarioModel : ObjetoBase
    {
        public string? Nome { get; set; }
        public double TotalReceber { get; set; }
        public double HorasExtras { get; set; }

        [JsonIgnore]
        public double HorasTrabalhadas { get; set; }

        [JsonIgnore]
        public double ValorHora { get; set; }
        public double HorasDebito { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras { get; set; }
        public int DiasTrabalhados { get; set; }
    }
}
