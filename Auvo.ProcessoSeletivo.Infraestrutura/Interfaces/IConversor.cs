using Auvo.ProcessoSeletivo.Infraestrutura.Objetos;

namespace Auvo.ProcessoSeletivo.Infraestrutura.Inteface
{
    public interface IConversor<TOrigem, TDestino>
        where TOrigem : ObjetoBase
        where TDestino : ObjetoBase
    {
        TOrigem Converta(TDestino origem);
        TDestino Converta(TOrigem origem);
    }
}
