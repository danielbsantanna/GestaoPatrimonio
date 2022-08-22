using BAL.Base;
using DAL.DAO;
using Model.Metadados;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RegraNegocio
{
    public class GrupoRegraNegocio : CrudRegraNegocio<Grupo>
    {
        private GrupoDAO grupoDAO;

        public GrupoRegraNegocio(GrupoDAO grupoDAO) : base(grupoDAO)
        {
            this.grupoDAO = grupoDAO;
        }

        public async Task<List<Grupo>> Listar()
        {
            return await grupoDAO.Listar();
        }
    }
}
