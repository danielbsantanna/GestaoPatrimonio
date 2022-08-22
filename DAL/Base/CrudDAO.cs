using DAL.Base;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base
{
    public abstract class CrudDAO<T> : ICrudDAO<T> where T : Entity
    {   

        public ApplicationDbContext _context;


        public CrudDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> AlterarAsync(T entidade)
        {
            var entidadeBanco = await _context.FindAsync<T>(GetKeysValues(entidade, _context));

            if (entidadeBanco != null)
            {
                _context.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public virtual async Task<bool> AlterarAsync(List<T> entidades)
        {
            _context.UpdateRange(entidades);
            await _context.SaveChangesAsync();
            return true;
        }


        public virtual async Task<bool> ExcluirAsync(T entidade)
        {
            _context.Remove(entidade);
            await _context.SaveChangesAsync();
            return true;
        }


        public virtual async Task<bool> ExcluirAsync(List<T> entidades)
        {
            _context.RemoveRange(entidades);
            await _context.SaveChangesAsync();
            return true;
        }


        public virtual async Task<bool> InserirAsync(T entidade)
        {
            await _context.AddAsync(entidade);
            await _context.SaveChangesAsync();
            return true;
        }


        public virtual async Task<bool> InserirAsync(List<T> entidades)
        {
            await _context.AddRangeAsync(entidades);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Consulta entidade genérica por IDs, trazendo todas as tabelas com FK e tabelas filhas
        /// </summary>
        /// <typeparam name="chaves">Lista de IDs da entidade</typeparam>
        public virtual async Task<T> ConsultarPorIdAsync(params object[] chaves)
        {
            try
            {
                var entidade = await _context.FindAsync<T>(chaves);

                if (entidade == null)
                {
                    return null;
                }
                return entidade;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        public virtual void Detach(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Detached;
        }


        public async Task CarregaColletionsRelacionadas(Entity entidade)
        {
            if (entidade == null) return;
            foreach (var entry in _context.Entry(entidade).References)
            {
                if (!entry.IsLoaded)
                {
                    await entry.LoadAsync();

                    try
                    {
                        var baseEntity = (Entity)entry.CurrentValue;

                        await CarregaColletionsRelacionadas(baseEntity);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            foreach (var entry in _context.Entry(entidade).Collections)
            {
                if (!entry.IsLoaded)
                {
                    await entry.LoadAsync();

                    foreach (var entidadeFilha in entry.CurrentValue)
                    {
                        try
                        {
                            var baseEntity = (Entity)entidadeFilha;

                            await CarregaColletionsRelacionadas(baseEntity);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }


        /// <summary>
        /// Consulta entidade genérica por IDs
        /// </summary>
        /// <typeparam name="entidadeResumida">Permite trazer a entidade resumida, sem referencias de FK ou tabelas filha</typeparam>
        /// <typeparam name="chaves">Lista de IDs da entidade</typeparam>
        public virtual async Task<T> ConsultarPorIdAsync(bool entidadeResumida, params object[] chaves)
        {
            T entidade = null;
            if (entidadeResumida)
            {
                entidade = await _context.FindAsync<T>(chaves);
            }
            else
            {
                entidade = await this.ConsultarPorIdAsync(chaves);
            }

            return entidade;
        }


        #region Funções para verificar se as FKs da entidade permitem a exclusão dela
        private object GetEntityFieldValue(T entityObj, string propertyName)
        {
            var pro = entityObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).First(x => x.Name == propertyName);
            return pro.GetValue(entityObj, null);

        }


        private IEnumerable<PropertyInfo> GetManyRelatedEntityNavigatorProperties(T entityObj)
        {
            var props = entityObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanWrite && x.GetGetMethod().IsVirtual && x.PropertyType.IsGenericType == true);
            return props;
        }


        public bool TemAlgumaRelacaoEstrangeira(T entityObj, bool val)
        {
            var collectionProps = GetManyRelatedEntityNavigatorProperties(entityObj);

            foreach (var item in collectionProps)
            {
                var collectionValue = GetEntityFieldValue(entityObj, item.Name);
                if (collectionValue != null && collectionValue is IEnumerable)
                {
                    var col = collectionValue as IEnumerable;
                    if (col.GetEnumerator().MoveNext())
                    {
                        return true;
                    }

                }
            }
            return false;
        }


        public static object[] GetKeysValues(T entity, ApplicationDbContext _context)
        {
            var keyNames = _context
                    .Model
                    .FindEntityType(typeof(T))
                    .FindPrimaryKey()
                    .Properties
                    .Select(x => x.Name).ToList();

            object[] keys = new object[keyNames.Count()];

            for (int i = 0; i < keyNames.Count(); i++)
            {
                keys[i] = entity
                    .GetType()
                    .GetProperties()
                    .Where(x => x.Name == keyNames[i])
                    .Select(x => x.GetValue(entity, null))
                    .FirstOrDefault();
            }

            return keys;
        }

        #endregion
    }

}
