namespace Auvo.ProcessoSeletivo.Infraestrutura.Utilitarios
{
    public static class UtilitarioHora
    {
        public static TimeOnly Hora(this string input, string formato = "HH:mm:ss")
        {
            return TimeOnly.ParseExact(input.Trim(), formato, null);
        }
        public static TimeSpan Diferenca(this TimeOnly tempoFinal, TimeOnly tempoInicial)
        {
            return tempoFinal - tempoInicial;
        }
        public static double TotalHoras(this TimeSpan tempoFinal, TimeSpan tempoInicial)
        {
            return (tempoFinal - tempoInicial).TotalHours;
        }
        public static bool EhFinalDeSemana(this DateOnly data)
        {
            return data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday;
        }
        public static bool HorasTrabalhadasMenorQueEscala(this TimeSpan horas)
        {
            return horas.Hours < TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_TRABALHO).Hours;
        }
        public static bool HorasTrabalhadasMaiorQueEscala(this TimeSpan horas)
        {
            return horas.Hours > TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_TRABALHO).Hours;
        }
        public static bool EhHoraExtra(this TimeSpan horas)
        {
            return horas.Hours > TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_TRABALHO).Hours;
        }
        public static bool IntervalorAlmocoMenorQueEscala(this TimeSpan horas)
        {
            return horas.Hours < TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_ALMOCO).Hours;
        }
        public static bool IntervalorAlmocoMaiorQueEscala(this TimeSpan horas)
        {
            return horas.Hours > TimeSpan.FromHours(UtilitarioConstantes.HORAS_DE_ALMOCO).Hours;
        }
    }
}
