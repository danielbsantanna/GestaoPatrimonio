using DAL.Base;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Base
{
    public abstract class CrudRegraNegocio<T> : IRegraNegocio<T> where T : Entity
    {
        protected CrudDAO<T> _repositorio;

        public CrudRegraNegocio(CrudDAO<T> repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual Task<bool> InserirAsync(T entidade)
        {
            return _repositorio.InserirAsync(entidade);
        }

        public virtual Task<bool> InserirAsync(List<T> entidades)
        {
            return _repositorio.InserirAsync(entidades);
        }

        public virtual Task<bool> AlterarAsync(T entidade)
        {
            return _repositorio.AlterarAsync(entidade);
        }

        public virtual Task<bool> AlterarAsync(List<T> entidades)
        {
            return _repositorio.AlterarAsync(entidades);
        }

        public virtual Task<bool> ExcluirAsync(T entidade)
        {
            return _repositorio.ExcluirAsync(entidade);
        }

        public virtual Task<bool> ExcluirAsync(List<T> entidades)
        {
            return _repositorio.ExcluirAsync(entidades);
        }

        public virtual Task<T> ConsultarPorIdAsync(params object[] chaves)
        {
            return _repositorio.ConsultarPorIdAsync(chaves);
        }

        public virtual Task<T> ConsultarPorIdAsync(bool consultaResumida, params object[] chaves)
        {
            return _repositorio.ConsultarPorIdAsync(consultaResumida, chaves);
        }
    }

}
