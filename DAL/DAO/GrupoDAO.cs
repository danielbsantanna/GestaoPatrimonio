using DAL.Base;
using DAL.Data;
using Model.Metadados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class GrupoDAO : CrudDAO<Grupo>
    {
        public GrupoDAO(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Grupo>> Listar()
        {
            return await Task.FromResult(_context.Grupo.ToList());
        }
    }
}
