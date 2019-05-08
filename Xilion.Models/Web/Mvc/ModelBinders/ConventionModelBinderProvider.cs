using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Xilion.Framework;

namespace Xilion.Models.Web.Mvc.ModelBinders
{
    /// <summary>
    /// A convention based model binder provider using StructureMap.
    /// </summary>
    public class ConventionModelBinderProvider : IModelBinderProvider
    {
        private static readonly IDictionary<Type, Type> _binders = new Dictionary<Type, Type>();
        private static readonly object _syncLock = new Object();

        #region IModelBinderProvider Members

        /// <summary>
        /// Interface for IModelBinderProvider.GetBinder.
        /// Checks Binders for the passed type else loop through the assembly and look for a binder
        /// </summary>
        /// <param name="modelType">The type of object we're going to bind to</param>
        /// <returns>An instance of IModelBinder if it can be found.</returns>
        public IModelBinder GetBinder(Type modelType)
        {
            Type binderType;

            lock (_syncLock)
            {
                // Check to see if a type was already bound.
                if (_binders.ContainsKey(modelType))
                    binderType = _binders[modelType];
                else
                {
                    // Check the assembly and look for a <name>ModelBinder
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Type[] types = assembly.GetTypes();

                    // Convention over configuration
                    binderType = types.FirstOrDefault(t => t.Name == modelType.Name + "ModelBinder");

                    // Enumeration type Model Binder
                    if (binderType == null && typeof (Enumeration).IsAssignableFrom(modelType))
                        binderType = typeof (EnumerationModelBinder);

                    if (binderType != null)
                        _binders[modelType] = binderType;
                }
            }

            if (binderType == null)
                return null;

            var binder = (IModelBinder) DependencyResolver.Current.GetService(binderType);
            return binder;
        }

        #endregion

        /// <summary>
        /// Manually register ModelBinders.
        /// </summary>
        /// <param name="modelType">The type of object we're binding to</param>
        /// <param name="binderType">The type of the IModelBinder that can bind the model</param>
        public static void RegisterType(Type modelType, Type binderType)
        {
            lock (_syncLock)
            {
                _binders[modelType] = binderType;
            }
        }
    }
}