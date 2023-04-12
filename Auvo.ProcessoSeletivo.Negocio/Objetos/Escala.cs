using Auvo.ProcessoSeletivo.Infraestrutura.Objetos;
using AuvoProcessoSeletivo.Negocio.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoProcessoSeletivo.Negocio.Objetos
{
    public class Escala : ObjetoBase
    {
        public Escala(string horarios)
        {
            Entrada = horarios.EntradaAlmoco();
            Saida = horarios.SaidaAlmoco();
        }

        public Escala() { }

        public TimeOnly Entrada { get; set; }
        public TimeOnly Saida { get; set; }
    }
}
