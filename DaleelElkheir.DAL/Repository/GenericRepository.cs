using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fields

        private readonly DaleelElkheirModel _context;
        private IDbSet<T> _entities;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public GenericRepository(DaleelElkheirModel context)
        {
            this._context = context;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(int id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// Get first or default By predicate
        /// </summary>
        /// <param name="predicate"> where clause</param>
        /// <returns>List of entities</returns>
        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate).FirstOrDefault<T>();
        }

        /// <summary>
        /// Get By predicate
        /// </summary>
        /// <param name="predicate"> where clause</param>
        /// <returns>List of entities</returns>
        public virtual List<T> Get(Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate).ToList<T>();
        }

        /// <summary>
        /// Get All Enitites
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll(bool _withTracking=true)
        {
            var data = this.Entities;
            
            if (!_withTracking)
            {
                var noTrackingData = data.AsNoTracking();
                return noTrackingData.ToList();
            }

            return data.ToList();
            //return this.Entities.AsNoTracking().ToList<T>();
        }


        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(typeof(T).Name);
                this.Entities.Add(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(typeof(T).Name);

                foreach (var entity in entities)
                    this.Entities.Add(entity);               
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(typeof(T).Name);
                this.Entities.Attach(entity);
                this._context.Entry(entity).State = EntityState.Modified;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(typeof(T).Name);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(typeof(T).Name);

                this.Entities.Remove(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }


        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Id of the Entity to delete</param>
        public virtual void Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                this.Delete(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Remove(entity);

               
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        #endregion

    }
}
