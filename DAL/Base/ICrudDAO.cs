using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base
{
    public interface ICrudDAO<T>
    {
        Task<bool> AlterarAsync(T entidade);

        Task<bool> AlterarAsync(List<T> entidades);

        Task<bool> ExcluirAsync(T entidade);

        Task<bool> ExcluirAsync(List<T> entidades);

        Task<bool> InserirAsync(T entidade);

        Task<bool> InserirAsync(List<T> entidades);

        Task<T> ConsultarPorIdAsync(params object[] chaves);
    }

}
