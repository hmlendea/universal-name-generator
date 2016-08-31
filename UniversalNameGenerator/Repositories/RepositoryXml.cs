﻿using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.Repositories
{
    /// <summary>
    /// XML Repository.
    /// </summary>
    public class RepositoryXml<T> : Repository<T> where T : EntityBase
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Repositories.XmlRepository`1"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public RepositoryXml(string fileName)
        {
            FileName = fileName;
            Load();
        }

        /// <summary>
        /// Add the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public override void Add(T entity)
        {
            base.Add(entity);
            Save();
        }

        /// <summary>
        /// Remove the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public override void Remove(T entity)
        {
            base.Remove(entity);
            Save();
        }

        /// <summary>
        /// Save this instance.
        /// </summary>
        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<T>));
                xs.Serialize(sw, Entities);
            }
        }

        /// <summary>
        /// Load this instance.
        /// </summary>
        public void Load()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<T>));

            if (!File.Exists(FileName))
                return;
            
            using (StreamReader sr = new StreamReader(FileName))
            {
                Entities = (List<T>)xs.Deserialize(sr);
            }
        }
    }
}