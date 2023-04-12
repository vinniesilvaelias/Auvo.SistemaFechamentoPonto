using Auvo.ProcessoSeletivo.Infraestrutura.Objetos;
using AuvoProcessoSeletivo.Negocio.Model;
using System.Text.Json.Serialization;

namespace AuvoProcessoSeletivo.Negocio.Objetos
{
    public class Departamento : ObjetoBase
    {
        public Departamento()
        {
            Funcionarios = new List<FuncionarioModel>();
            RegistrosFuncionarios = new Dictionary<int, IList<RegistroFuncionario>>();
        }

        [JsonIgnore]
        public new int Codigo { get; set; }
        public string Nome { get; set; }
        public string MesVigencia { get; set; }
        public string AnoVigencia { get; set; }
        public double TotalPagar { get; set; }
        public double TotalDesconto { get; set; }
        public double TotalExtras { get; set; }
        public List<FuncionarioModel> Funcionarios { get; set; }

        [JsonIgnore]
        public IDictionary<int, IList<RegistroFuncionario>> RegistrosFuncionarios { get; set; }
    }
}
