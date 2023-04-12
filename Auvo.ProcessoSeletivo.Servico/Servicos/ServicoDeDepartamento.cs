using Auvo.ProcessoSeletivo.Servico.Interfaces;
using AuvoProcessoSeletivo.Negocio.Dto;
using AuvoProcessoSeletivo.Negocio.Model;
using AuvoProcessoSeletivo.Negocio.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auvo.ProcessoSeletivo.Servico.Servicos
{
    public class ServicoDeDepartamento : IServico<Departamento>
    {
        public static async Task<List<FuncionarioModel>> CalculeFechamentoDepartamentos(IList<Departamento> listaDepartamentos)
        {
            var listaFuncionarioModel = new List<FuncionarioModel>();

            foreach (var departamento in listaDepartamentos)
            {
                foreach (var registrosFuncionario in departamento.RegistrosFuncionarios)
                {
                    var model = await ServicoDeFolhaPagamento.FechamentoPonto(registrosFuncionario.Value.ToList());
                    listaFuncionarioModel.Add(model);
                }
                departamento.Funcionarios = listaFuncionarioModel;
            }

            return listaFuncionarioModel;
        }

    }
}
