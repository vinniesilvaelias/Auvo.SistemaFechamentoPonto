namespace Auvo.ProcessoSeletivo.DadosCandidato.Models
{
    public class Candidato
    {
        public Candidato()
        {
            Nome = "Vinicius Elias da Silva";
            PretencaoSalarial = 5000.00M;
            DataTeste = new DateOnly(2022, 4, 11);
            Imagem = "profile.jpg";
        }
        public string Nome { get; set; }
        public decimal PretencaoSalarial { get; set; }
        public DateOnly? DataTeste { get; set; }
        public string  Imagem { get; set; }
    }
}
