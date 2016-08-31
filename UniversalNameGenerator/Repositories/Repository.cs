﻿using System;
using System.Collections.Generic;

using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.Repositories
{
    public class Repository<T> where T : EntityBase
    {
        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        protected List<T> Entities { get; set; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size
        {
            get { return Entities.Count; }
        }

        public Repository()
        {
            Entities = new List<T>();
        }

        /// <summary>
        /// Add the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public virtual void Add(T entity)
        {
            if (Entities.Contains(entity))
                throw new RepositoryException("The specified entity already exists");

            Entities.Add(entity);
        }

        public virtual T Get(string id)
        {
            T entity = Entities.Find(E => E.Id == id);

            if (entity == null)
                throw new RepositoryException("An entity with the specified identifier does not exist");

            return entity;
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The all.</returns>
        public List<T> GetAll()
        {
            return Entities;
        }

        /// <summary>
        /// Remove the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public virtual void Remove(T entity)
        {
            if (!Contains(entity))
                throw new RepositoryException("The specified entity does not exist");

            Entities.Remove(entity);
        }

        public virtual void Remove(string id)
        {
            if (!Contains(id))
                throw new RepositoryException("An entity with the specified identifier does not exist");

            Entities.RemoveAll(T => T.Id == id);
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            Entities.Clear();
        }

        /// <summary>
        /// Contains the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public bool Contains(T entity)
        {
            return Entities.Find(E => E.Equals(entity)) != null;
        }

        /// <summary>
        /// Contains the specified entity.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public bool Contains(string id)
        {
            return Entities.Find(E => E.Id == id) != null;
        }
    }
}
