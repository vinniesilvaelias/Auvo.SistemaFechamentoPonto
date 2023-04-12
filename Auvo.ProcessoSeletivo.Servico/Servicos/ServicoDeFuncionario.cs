using Auvo.ProcessoSeletivo.Infraestrutura.Inteface;
using Auvo.ProcessoSeletivo.Infraestrutura.Utilitarios;
using Auvo.ProcessoSeletivo.Servico.Interfaces;
using AuvoProcessoSeletivo.Negocio.Model;
using AuvoProcessoSeletivo.Negocio.Objetos;

namespace Auvo.ProcessoSeletivo.Servico.Servicos
{
    public class ServicoDeFuncionario : IServico<RegistroFuncionario>
    {
        public static int ObtenhaDiasTrabalhados(IList<RegistroFuncionario> registroFuncionario)
        {
            var diasTrabalhados =  new TimeSpan();

            foreach (var registro in registroFuncionario.ToList())
            {
                var horasAlmoco = registro.HorarioAlmoco.Saida - registro.HorarioAlmoco.Entrada;
                var horasTrabalho = registro.Saida - registro.Entrada;
                diasTrabalhados += horasTrabalho - horasAlmoco;
            }

            return diasTrabalhados.Days;
        }
        public static int ObtenhaDiasExtras(IList<RegistroFuncionario> registroFuncionario)
        {
            var diasTrabalhados = new TimeSpan();

            foreach (var registro in registroFuncionario.ToList())
            {
                var horasAlmoco = registro.HorarioAlmoco.Saida - registro.HorarioAlmoco.Entrada;
                var horasTrabalho = registro.Saida - registro.Entrada;
                diasTrabalhados += horasTrabalho - horasAlmoco;
            }

            return diasTrabalhados.Days - UtilitarioConstantes.DIAS_DE_TRABALHO_EFETIVO;
        }
        public static FuncionarioModel Obtenha(IList<RegistroFuncionario> registroFuncionario)
        {
            var horasTrabalhadas = new TimeSpan();
            var horasExtras = new TimeSpan();
            var horasFalta = new TimeSpan();
            var valorPagoHorasExtras = 0.0;
            var valorDescontoHorasFalta = 0.0;
            var valorPagoJornadaTrabalho = 0.0;
            var totalAReceber = 0.0;

            foreach (var registro in registroFuncionario.ToList())
            {
                var intervaloAlmoco = registro.HorarioAlmoco.Saida - registro.HorarioAlmoco.Entrada;
                var jornadaDeTrabalho = registro.Saida - registro.Entrada;
                
                horasTrabalhadas += jornadaDeTrabalho - intervaloAlmoco;

                if (registro.Data.EhFinalDeSemana())
                {
                    horasExtras += horasTrabalhadas;
                }
                else if (jornadaDeTrabalho.EhHoraExtra())
                {
                    horasExtras += horasTrabalhadas - TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_TRABALHO);
                }
                else if (intervaloAlmoco.IntervalorAlmocoMenorQueEscala())
                {
                    horasExtras += TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_ALMOCO) - intervaloAlmoco;
                }

                if (jornadaDeTrabalho.HorasTrabalhadasMenorQueEscala())
                {
                    horasFalta += TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_TRABALHO) - jornadaDeTrabalho;
                }

                if (intervaloAlmoco.IntervalorAlmocoMaiorQueEscala())
                {
                    horasFalta += TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_ALMOCO) - intervaloAlmoco;
                }

                valorPagoHorasExtras = horasExtras.Days * UtilitarioConstantes.HORAS_DE_TRABALHO * registro.ValorHora + horasExtras.Hours * registro.ValorHora;
                valorDescontoHorasFalta = horasFalta.Days * UtilitarioConstantes.HORAS_DE_TRABALHO * registro.ValorHora + horasFalta.Hours * registro.ValorHora;
                valorPagoJornadaTrabalho = horasTrabalhadas.Days * UtilitarioConstantes.HORAS_DE_TRABALHO * registro.ValorHora + horasTrabalhadas.Hours * registro.ValorHora;
                totalAReceber += valorPagoJornadaTrabalho + valorPagoHorasExtras - valorDescontoHorasFalta;
            }

            var diasTrabalhados = horasTrabalhadas.Days + horasExtras.Days;
            Mensagem.Escreva($"Horas de trabalho: {horasTrabalhadas.Hours}\nHoras extra: {horasExtras.Hours}\nHoras Falta: {horasFalta.Hours}\n");
            Mensagem.Escreva($"Dias de trabalho: {diasTrabalhados}\nDias extra: {horasExtras.Days}\nDias Falta: {horasFalta.Days}\n");
            Mensagem.Escreva($"Total a receber:R${totalAReceber}\n");
            Mensagem.Escreva($"-----------------------------------------\n");
            //return diasTrabalhados.Days;
            return new FuncionarioModel
            {
                TotalReceber = totalAReceber,
                HorasExtras = horasExtras.Hours,
                HorasDebito = horasFalta.Hours,
                DiasTrabalhados = horasTrabalhadas.Days,
                DiasExtras = horasExtras.Days,
                DiasFalta = horasFalta.Days
            };
        }
    }
}
