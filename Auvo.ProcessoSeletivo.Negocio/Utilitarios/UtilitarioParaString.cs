using Auvo.ProcessoSeletivo.Infraestrutura.Utilitarios;
using AuvoProcessoSeletivo.Negocio.Dto;
using AuvoProcessoSeletivo.Negocio.Objetos;
using System.Globalization;
namespace AuvoProcessoSeletivo.Negocio.Utilitarios
{
    public static class UtilitarioParaString
    {
        public static TimeOnly EntradaAlmoco(this string entrada) => entrada.Trim().Split(UtilitarioConstantes.DELIMITADOR_ALMOCO)[UtilitarioConstantes.ENTRADA_ALMOCO].Hora("HH:mm");
        public static TimeOnly SaidaAlmoco(this string entrada) => entrada.Split(UtilitarioConstantes.DELIMITADOR_ALMOCO)[UtilitarioConstantes.SAIDA_ALMOCO].Hora("HH:mm");
        public static double ValorHora(this string entrada) => double.Parse(entrada.Trim().Replace(UtilitarioConstantes.MOEDA, UtilitarioConstantes.VAZIO).Replace(UtilitarioConstantes.DELIMITADOR_VIRGULA, UtilitarioConstantes.DELIMITADOR_DECIMAL));
        public static int Codigo(this string entrada) => string.IsNullOrEmpty(entrada) ? 0 : int.Parse(entrada);
        public static DateOnly Data(this string entrada) => DateOnly.ParseExact(entrada, UtilitarioConstantes.FORMATO_DATA, CultureInfo.InvariantCulture);
        public static TimeOnly Entra(this string entrada) => TimeOnly.ParseExact(entrada, UtilitarioConstantes.FORMATO_DATA, CultureInfo.InvariantCulture);
        public static IList<DtoFuncionario> FuncionariosSincrono(this string entrada)
        {
            var linhas = entrada.Split(UtilitarioConstantes.DELIMITADOR_FIM_DA_LINHA).Skip(1).ToList();
            var listaFuncionarios = new List<DtoFuncionario>();
            foreach (var linha in linhas)
            {
                var campos = linha.Split(UtilitarioConstantes.DELIMITADOR_CAMPOS);
                listaFuncionarios.Add(new DtoFuncionario()
                {
                    Codigo = campos[UtilitarioConstantes.CODIGO].Codigo(),
                    Nome = campos[UtilitarioConstantes.NOME],
                    ValorHora = campos[UtilitarioConstantes.VALOR_HORA].ValorHora(),
                    Data = campos[UtilitarioConstantes.DATA].Data(),
                    Entrada = campos[UtilitarioConstantes.ENTRADA].Hora("Hh:mm"),
                    Saida = campos[UtilitarioConstantes.SAIDA].Hora("Hh:mm"),
                    Almoco = campos[UtilitarioConstantes.ESCALA]
                });
            };

            return listaFuncionarios;
        }
        public static async Task<RegistroFuncionario> FuncionarioAssincrono(this string dadosFuncionario)
        {
            var campos = await Task.Run(() => dadosFuncionario.Split(UtilitarioConstantes.DELIMITADOR_CAMPOS));

            return new RegistroFuncionario()
            {
                Codigo = campos[UtilitarioConstantes.CODIGO].Codigo(),
                Nome = campos[UtilitarioConstantes.NOME],
                ValorHora = campos[UtilitarioConstantes.VALOR_HORA].ValorHora(),
                Data = campos[UtilitarioConstantes.DATA].Data(),
                Entrada = campos[UtilitarioConstantes.ENTRADA].Hora("HH:mm"),
                Saida = campos[UtilitarioConstantes.SAIDA].Hora("HH:mm"),
                HorarioAlmoco = new Escala(campos[UtilitarioConstantes.ESCALA])
            };
        }
    }
}

