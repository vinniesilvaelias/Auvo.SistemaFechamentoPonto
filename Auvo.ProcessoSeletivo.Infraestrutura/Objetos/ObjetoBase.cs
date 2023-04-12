using Auvo.ProcessoSeletivo.Infraestrutura.Inteface;

namespace Auvo.ProcessoSeletivo.Infraestrutura.Objetos
{
    public abstract class ObjetoBase : IObjeto
    {
        public int Codigo { get; set; }
    }
}
