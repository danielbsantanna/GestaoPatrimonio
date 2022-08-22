using BAL.Base;
using DAL.DAO;
using Model.Metadados;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RegraNegocio
{
    public class NotaRegraNegocio : CrudRegraNegocio<Nota>
    {
        private NotaDAO notaDAO;
        private GrupoRegraNegocio grupoRegraNegocio;
        public NotaRegraNegocio(NotaDAO notaDAO, GrupoRegraNegocio grupoRegraNegocio) : base(notaDAO)
        {
            this.notaDAO = notaDAO;
            this.grupoRegraNegocio = grupoRegraNegocio;
        }

        public async Task<List<Nota>> Listar(int grupo)
        {
            return await notaDAO.Listar(grupo);
        }

        public async Task<List<DemonstrativoPatrimonioViewModel>> DemostrativoPatrimonio()
        {
            var retorno = new List<DemonstrativoPatrimonioViewModel>();
            var grupos = await grupoRegraNegocio.Listar();
            foreach (var grupo in grupos)
            {
                var notas = await notaDAO.Listar(grupo.Id);
                var notasValidas = notas.Where(x => (DateTime.Now - (x.DataEntrada ?? DateTime.Now)).Days / (365.25 / 12) <= grupo.VidaUtil).ToList();

                decimal total = 0;
                decimal totalResidual = 0;
                decimal totalDepreciado = 0;

                foreach (var item in notasValidas)
                {
                    var totalItem = item.Valor * item.Quantidade;
                    var mesesDepreciados = (int)((DateTime.Now - (item.DataEntrada ?? DateTime.Now)).Days / (365.25 / 12));
                    var valorMensalDepreciado = totalItem * (((100 + (grupo.DepreciacaoMensal / grupo.VidaUtil)) / 100) - 1);
                    var valorDepreciado = valorMensalDepreciado * mesesDepreciados;

                    total += totalItem;
                    totalResidual += totalItem - valorDepreciado;
                    totalDepreciado += valorDepreciado;
                }

                retorno.Add(new DemonstrativoPatrimonioViewModel()
                {
                    Grupo = grupo.Nome,
                    Valor = total,
                    ValorDepreciado = totalDepreciado,
                    ValorResidual = totalResidual
                });
            }

            return retorno;
        }
    }
}
