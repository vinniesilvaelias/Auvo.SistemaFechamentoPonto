using Auvo.ProcessoSeletivo.Infraestrutura.Utilitarios;
using AuvoProcessoSeletivo.Negocio.Objetos;
using AuvoProcessoSeletivo.Negocio.Utilitarios;
using System.Text.Json.Serialization;
using System.Text.Json;
using Auvo.ProcessoSeletivo.Infraestrutura.Inteface;

namespace Auvo.ProcessoSeletivo.Servico.Servicos
{
    public class ServicoDeArquivo
    {
        public static IList<string> ObtenhaContextoRegistros(string nomeArquivo) =>  Path.GetFileNameWithoutExtension(nomeArquivo).Split(UtilitarioConstantes.DELIMITADOR_NOME_ARQUIVO);
        public static string ObtenhaNomeAquivo(string arquivo) => Path.GetFileNameWithoutExtension(arquivo);
        public static void SalveFormatoJson<TObjeto>(string path, IList<TObjeto> registros, string nomeDoArquivo = UtilitarioConstantes.NOME_PADRAO_ARQUIVO)
            where TObjeto : IObjeto
        {
            File.WriteAllText(Path.Combine(path, nomeDoArquivo + ".json"), JsonSerializer.Serialize(registros, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                NumberHandling = JsonNumberHandling.WriteAsString
            }));
        }
        public static async Task<IDictionary<int, IList<RegistroFuncionario>>> ObtenhaRegistrosPorAquivo(string registrosFuncionarios)
        {
            var dicionarioDeRegistrosDosFuncionarios = new Dictionary<int, IList<RegistroFuncionario>>();
            var registros = registrosFuncionarios.Split(UtilitarioConstantes.DELIMITADOR_FIM_DA_LINHA).Skip( UtilitarioConstantes.CABECALHO_ARQUIVO);

            var tasks = new List<Task>();
            foreach (var registro in registros)
            {
                if (!string.IsNullOrEmpty(registro))
                {
                    var funcionarioTask = registro.FuncionarioAssincrono();
                    tasks.Add(funcionarioTask.ContinueWith(task =>
                    {
                        var funcionario = task.Result;
                        lock (dicionarioDeRegistrosDosFuncionarios)
                        {
                            if (!dicionarioDeRegistrosDosFuncionarios.ContainsKey(funcionario.Codigo))
                            {
                                dicionarioDeRegistrosDosFuncionarios[funcionario.Codigo] = new List<RegistroFuncionario>();
                            }

                            dicionarioDeRegistrosDosFuncionarios[funcionario.Codigo].Add(funcionario);
                        }
                    }));
                }
            }

            await Task.WhenAll(tasks);

            return dicionarioDeRegistrosDosFuncionarios;
        }
        public static async Task<IList<Tuple<string, string>>> LeiaArquivosAssincrono(string path)
        {
            var tarefasParaLeituraDeArquivo = new List<Tuple<string, string>>();

            foreach (var arquivoDaPasta in Directory.GetFiles(path))
            {
                tarefasParaLeituraDeArquivo.Add(new Tuple<string, string>(ObtenhaNomeAquivo(arquivoDaPasta), await LeiaArquivoAssincrono(arquivoDaPasta)));
            }

            return tarefasParaLeituraDeArquivo;
        }
        private static async Task<string> LeiaArquivoAssincrono(string arquivo)
        {
            using var streamReader = new StreamReader(arquivo);
            var conteudoDoArquivo = await streamReader.ReadToEndAsync();
            return conteudoDoArquivo;
        }
    }
}
