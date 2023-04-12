namespace Auvo.ProcessoSeletivo.Infraestrutura.Utilitarios
{
    public class Mensagem
    {
        public const string CAMINHO_NAO_ENCONTRADO = "O caminho informado não foi encontrado.";
        public const string SOLICITE_PATH = "Informe o caminho da pasta: ";
        public static void Escreva(string mensage, bool quebraLinha = true)
        {
            if (quebraLinha)
            {
                Console.WriteLine(mensage);
            }
            else
            {
                Console.Write(mensage);
            }
        }
        public static void CaminhoNaoEncontrado() => Escreva(CAMINHO_NAO_ENCONTRADO);
        public static string ObtenhaPath()
        {
            Escreva(SOLICITE_PATH, false);

            return Console.ReadLine() ?? string.Empty;
        }
        public static void Escreva(string mensage) => Console.WriteLine(mensage);
        public static void Aguarde(int segundos) => Thread.Sleep(segundos);
    }
}
