using Auvo.ProcessoSeletivo.Infraestrutura.Utilitarios;
using Auvo.ProcessoSeletivo.Servico.Servicos;
using AuvoProcessoSeletivo.Negocio.Model;
using AuvoProcessoSeletivo.Negocio.Objetos;
internal class Program
{
    private static string? Path { get; set; }
    private static IList<Departamento> ListaDepartamentos { get; set; }
    private static string NovaIteracao { get; set; }
    private static bool OcorreuAlgumErro { get; set; }
    private static void Main(string[] args)
    {
        do
        {
            ObtenhaPathValido();
            ProcesseEntradaUsuario().Wait();
            EfetueFechamentosPonto().Wait();
            EfetueCalculoFolhaDePagamento();
            SalveResultado();
            SoliciteNovaIteracao();

        } while (NovaIteracao == UtilitarioConstantes.VERDADEIRO);

        Console.Clear();

        if (!OcorreuAlgumErro)
        {
            Mensagem.Escreva("Programa finalizado com sucesso.");
        }

        Mensagem.Aguarde(UtilitarioConstantes.TEMPO_PADRAO);
    }
    private static void SoliciteNovaIteracao()
    {
        Mensagem.Escreva(UtilitarioConstantes.SUCESSO);
        Mensagem.Escreva(UtilitarioConstantes.OPCOES_MENU);
        NovaIteracao = Console.ReadLine()??string.Empty;
        Console.Clear();
    }
    private static void SalveResultado()
    {
        Mensagem.Escreva($"Salvando o resultado na pasta {Path}");
        ServicoDeArquivo.SalveFormatoJson(Path, ListaDepartamentos);
    }
    private static async Task<bool> ProcesseEntradaUsuario()
    {
        Mensagem.Escreva("Processando arquivos...");
        Mensagem.Escreva("Arquivos descobertos:");
        var conteudoDaPasta = ServicoDeArquivo.LeiaArquivosAssincrono(Path).GetAwaiter().GetResult();
        ListaDepartamentos = new List<Departamento>();
        var departamentosFuncionarios = new List<IDictionary<int, IList<RegistroFuncionario>>>();

        var tarefas = new List<Task>();
        try
        {
            foreach (var arquivo in conteudoDaPasta)
            {
                tarefas.Add(Task.Run(async () =>
                {
                    ListaDepartamentos.Add(await ObtenhaDeparamento(arquivo));
                }));
            }

            Task.WaitAll(tarefas.ToArray());
        }
        catch (Exception e)
        {
            Mensagem.Escreva(UtilitarioConstantes.ERRO_PROCESSAMENTO_ENTRADA + e.Message);
            OcorreuAlgumErro = true; 
            return false;
        }

        return OcorreuAlgumErro;
    }
    private static async Task<bool> EfetueFechamentosPonto()
    {
        try
        {
            ListaDepartamentos.ToList().ForEach(departamento =>
            {
                var listaFuncionarioModel = new List<FuncionarioModel>();
                foreach (var registrosFunionario in departamento.RegistrosFuncionarios)
                {
                    var model = Task.Run(async () => await ServicoDeFolhaPagamento
                                                     .FechamentoPonto(registrosFunionario.Value.ToList()))
                                                     .GetAwaiter()
                                                     .GetResult();

                    listaFuncionarioModel.Add(model);
                }

                departamento.Funcionarios = listaFuncionarioModel;
            });
        }
        catch (Exception e)
        {
            Mensagem.Escreva(UtilitarioConstantes.ERRO_CALCULO_FOLHA_PAGAMENTO + e.Message);
            Mensagem.Aguarde(UtilitarioConstantes.TEMPO_PADRAO);
            OcorreuAlgumErro = true;
            return false;
        }

        return OcorreuAlgumErro;
    }
    private static async Task<Departamento> ObtenhaDeparamento(Tuple<string, string> arquivo)
    {
        
        var contextoArquivo = ServicoDeArquivo.ObtenhaContextoRegistros(arquivo.Item1);

        Mensagem.Escreva(arquivo.Item1);

        return new Departamento
        {
            Nome = contextoArquivo[UtilitarioConstantes.NOME_DEPARTAMENTO],
            MesVigencia = contextoArquivo[UtilitarioConstantes.MES_VIGENCIA],
            AnoVigencia = contextoArquivo[UtilitarioConstantes.ANO_VIGENCIA],
            RegistrosFuncionarios = await ServicoDeArquivo.ObtenhaRegistrosPorAquivo(arquivo.Item2)
        };
    }
    private static bool EfetueCalculoFolhaDePagamento()
    {
        Mensagem.Escreva("Calculando folha da pagamento...");
        try
        {
            ListaDepartamentos.ToList().ForEach(departamento => ServicoDeFolhaPagamento.CalculeFolha(departamento));
        }
        catch (Exception e)
        {
            Mensagem.Escreva(UtilitarioConstantes.ERRO_CALCULO_FOLHA_PAGAMENTO + e.Message);
            OcorreuAlgumErro = true;
            return false;
        }

        return OcorreuAlgumErro;
    }
    private static bool PathExite()
    {
        Mensagem.Escreva($"Verificando caminho {Path}");

        var pathExite = Directory.Exists(Path);

        if (!pathExite)
        {
            Console.Clear();
            Mensagem.CaminhoNaoEncontrado();
            Thread.Sleep(2000);
        }

        return pathExite;
    }
    private static void ObtenhaPathValido()
    {
        do
        {
            Console.Clear();
            Path = Mensagem.ObtenhaPath();
        } while (!PathExite());
    }
}