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
    public class NotaDAO : CrudDAO<Nota>
    {
        public NotaDAO(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Nota>> Listar(int grupo)
        {
            return await Task.FromResult(_context.Nota.Where(x => x.GrupoId == grupo).ToList());
        }
    }
}
