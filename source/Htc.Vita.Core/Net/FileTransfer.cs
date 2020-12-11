using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class FileTransfer.
    /// </summary>
    public abstract class FileTransfer
    {
        static FileTransfer()
        {
            TypeRegistry.RegisterDefault<FileTransfer, BitsFileTransfer>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : FileTransfer, new()
        {
            TypeRegistry.Register<FileTransfer, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>FileTransfer.</returns>
        public static FileTransfer GetInstance()
        {
            return TypeRegistry.GetInstance<FileTransfer>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>FileTransfer.</returns>
        public static FileTransfer GetInstance<T>()
                where T : FileTransfer, new()
        {
            return TypeRegistry.GetInstance<FileTransfer, T>();
        }

        /// <summary>
        /// Gets the job identifier list.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetJobIdList()
        {
            List<string> result = null;
            try
            {
                result = OnGetJobIdList();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileTransfer)).Error(e.ToString());
            }
            return result ?? new List<string>();
        }

        /// <summary>
        /// Called when getting job identifier list.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        protected abstract List<string> OnGetJobIdList();
    }
}
