using Auvo.ProcessoSeletivo.Infraestrutura.Objetos;
using Auvo.ProcessoSeletivo.Servico.Interfaces;
using AuvoProcessoSeletivo.Negocio.Model;
using AuvoProcessoSeletivo.Negocio.Objetos;

namespace Auvo.ProcessoSeletivo.Servico.Servicos
{
    public class ServicoDeFolhaPagamento : IServico<ObjetoBase>
    {
        public static Task<FuncionarioModel> FechamentoPonto(IList<RegistroFuncionario> registroFuncionario)
        {
            var horasTrabalhadas = TimeSpan.Zero;
            var horasExtras = TimeSpan.Zero;

            var valorHora = registroFuncionario.First().ValorHora;
            var dataReferencia = registroFuncionario.First().Data;
            var codigoFuncionario = registroFuncionario.First().Codigo;
            var nomeFuncionario = registroFuncionario.First().Nome;

            foreach (var registro in registroFuncionario)
            {
                var jornada = registro.Saida - registro.Entrada;
                var intervaloAlmoco = registro.HorarioAlmoco.Saida - registro.HorarioAlmoco.Entrada;

                horasTrabalhadas += jornada - intervaloAlmoco;
            }

            var diasTrabalhados = registroFuncionario.Count;
            return Task.Run(() => InsiraDadosFolhaPagamento(horasTrabalhadas, valorHora, diasTrabalhados, codigoFuncionario, nomeFuncionario));
        }
        private static double CalculeTotalDescontos(IList<FuncionarioModel> funcionarios)
        {
            var totalDescontos = 0.0;
            foreach (var registroFuncionario in funcionarios)
            {
                totalDescontos += registroFuncionario.HorasDebito * registroFuncionario.ValorHora;
            }
            return totalDescontos < 0 ? 0.0 : Math.Round(totalDescontos);
        }
        public static void CalculeFolha(Departamento departamento)
        {
            departamento.TotalDesconto = CalculeTotalDescontos(departamento.Funcionarios.ToList()) ;
            departamento.TotalExtras = CalculeTotalExtra(departamento.Funcionarios.ToList());
            departamento.TotalPagar = CalculeTotalPagar(departamento.Funcionarios.ToList()) + departamento.TotalExtras - departamento.TotalDesconto;
        }
        private static double CalculeTotalExtra(IList<FuncionarioModel> funcionarios)
        {
            var totalExtra = 0.0;
            foreach (var funcionario in funcionarios)
            {
                totalExtra += funcionario.HorasExtras * funcionario.ValorHora;
            }

            return totalExtra < 0 ? 0 : Math.Round(totalExtra);
        }
        private static double CalculeTotalPagar(IList<FuncionarioModel> funcionarios)
        {
            var totalPagar = 0.0;
            foreach (var funcionario in funcionarios)
            {
                totalPagar += funcionario.TotalReceber;
            }

            return totalPagar < 0 ? 0 : Math.Round(totalPagar);
        }
        private static FuncionarioModel InsiraDadosFolhaPagamento(TimeSpan horasTrabalhadas,
            double valorHora, int diasTrabalhados, int codigoFuncionario, string nome)
        {
            var horasExtras = horasTrabalhadas - TimeSpan.FromHours(160);
            var horasDebito = TimeSpan.FromHours(160) - horasTrabalhadas;
            var valorHorasDebito = horasDebito.TotalHours * valorHora;
            var valorPagoHorasTrabalhadas = horasTrabalhadas.TotalHours * valorHora;
            var totalASerPago = valorPagoHorasTrabalhadas - valorHorasDebito;
            var diasExtra = 30 - diasTrabalhados;
            var diasFalta = 30 - diasTrabalhados;

            return new FuncionarioModel
            {
                Codigo = codigoFuncionario,
                Nome = nome,
                TotalReceber = Math.Round(totalASerPago, 2),
                HorasTrabalhadas = Math.Round(horasTrabalhadas.TotalHours, 2),
                ValorHora = Math.Round(valorHora, 2),
                HorasExtras = horasExtras.Hours,
                HorasDebito = Math.Round(horasDebito.TotalHours, 2),
                DiasFalta = diasFalta,
                DiasExtras = diasExtra,
                DiasTrabalhados = diasTrabalhados,
            };
        }
    }
}
