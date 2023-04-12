using Auvo.ProcessoSeletivo.Infraestrutura.Objetos;

namespace AuvoProcessoSeletivo.Negocio.Objetos
{
    public abstract class HistoricoBase<TObjeto> : ObjetoBase
        where TObjeto : ObjetoBase
    {
        public HistoricoBase()
        {
            Historicos = new List<TObjeto>();
        }
        public IList<TObjeto>? Historicos { get; set; }
    }
}
