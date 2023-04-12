namespace Auvo.ProcessoSeletivo.Infraestrutura.Utilitarios
{
    public static class UtilitarioConstantes
    {
        public const string DELIMITADOR_ALMOCO = "-";
        public const string DELIMITADOR_NOME_ARQUIVO = "-";
        public const string DELIMITADOR_FIM_DA_LINHA = "\n";
        public const string DELIMITADOR_CAMPOS = ";";
        public const string NOME_PADRAO_ARQUIVO = "resultado";
        public const string EXTENSAO_JSON = ".json";
        public const string MOEDA = "R$";
        public const string VERDADEIRO = "1";
        public const string DELIMITADOR_DECIMAL = ".";
        public const string DELIMITADOR_VIRGULA = ",";
        public const string VAZIO = "";
        public const string FORMATO_DATA = "dd/MM/yyyy";
        public const int ENTRADA_ALMOCO = 0;
        public const int SAIDA_ALMOCO = 1;
        public const int CODIGO = 0;
        public const int NOME = 1;
        public const int VALOR_HORA = 2;
        public const int DATA = 3;
        public const int ENTRADA = 4;
        public const int SAIDA = 5;
        public const int ESCALA = 6;
        public const int NOME_DEPARTAMENTO = 0;
        public const int MES_VIGENCIA = 1;
        public const int ANO_VIGENCIA = 2;
        public const int DIAS_DE_TRABALHO_EFETIVO = 22;
        public const int HORAS_DE_TRABALHO = 8;
        public const int HORAS_DE_ALMOCO = 1;
        public const int CABECALHO_ARQUIVO = 1;
        public const int TEMPO_PADRAO = 9000;
        public const string OPCOES_MENU = "[1] - Continuar [2] - Finalizar";
        public const string ERRO_PROCESSAMENTO_ENTRADA = "Ocorreu um erro na leitura dos arquivos.\n";
        public const string ERRO_FECHAMENTO_PONTO = "Ocorreu um erro no fechamento do ponto.\n";
        public const string ERRO_CALCULO_FOLHA_PAGAMENTO = "Ocorreu um erro no calculo da folha de pagamento.\n";
        public const string SUCESSO = "Arquivos processados com sucesso. O resultado está salvo na pasta indicada.\n";
    }
}
