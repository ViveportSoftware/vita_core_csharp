using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class TypeRegistry.
    /// </summary>
    public static class TypeRegistry
    {
        private static readonly Dictionary<Type, Type> AbstractClassTypeWithConcreteClassType = new Dictionary<Type, Type>();
        private static readonly Dictionary<string, object> Instances = new Dictionary<string, object>();

        private static TBaseClass DoGetInstance<TBaseClass>(
                Type abstractClassType,
                Type subClassType)
                        where TBaseClass : class
        {
            var concreteClassType = AbstractClassTypeWithConcreteClassType[abstractClassType];
            if (subClassType != null)
            {
                concreteClassType = subClassType;
            }

            var key = $"{concreteClassType.FullName}_";
            var instance = default(TBaseClass);
            lock (Instances)
            {
                if (Instances.ContainsKey(key))
                {
                    instance = Instances[key] as TBaseClass;
                }
                if (instance == null)
                {
                    Logger.GetInstance(typeof(TypeRegistry)).Info($"Initializing {key}...");
                    var constructor = concreteClassType.GetConstructor(Array.Empty<Type>());
                    if (constructor != null)
                    {
                        instance = (TBaseClass)constructor.Invoke(Array.Empty<object>());
                    }
                }
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TBaseClass">The base class type.</typeparam>
        /// <returns>TBaseClass.</returns>
        public static TBaseClass GetInstance<TBaseClass>()
                where TBaseClass : class
        {
            return DoGetInstance<TBaseClass>(
                    typeof(TBaseClass),
                    null
            );
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TBaseClass">The base class type.</typeparam>
        /// <typeparam name="TSubClass">The sub class type.</typeparam>
        /// <returns>TBaseClass.</returns>
        public static TBaseClass GetInstance<TBaseClass, TSubClass>()
                where TSubClass : TBaseClass, new()
                where TBaseClass : class
        {
            return DoGetInstance<TBaseClass>(
                    typeof(TBaseClass),
                    typeof(TSubClass)
            );
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="TBaseClass">The base class type.</typeparam>
        /// <typeparam name="TSubClass">The sub class type.</typeparam>
        public static void Register<TBaseClass, TSubClass>()
                where TBaseClass : class
                where TSubClass : TBaseClass, new()
        {
            var baseClass = typeof(TBaseClass);
            var subClass = typeof(TSubClass);

            if (!AbstractClassTypeWithConcreteClassType.ContainsKey(baseClass))
            {
                AbstractClassTypeWithConcreteClassType.Add(baseClass, subClass);
                Logger.GetInstance(typeof(TypeRegistry)).Info($"Registered {baseClass.Name} type to {subClass.FullName}");
                return;
            }

            if (subClass != AbstractClassTypeWithConcreteClassType[baseClass])
            {
                AbstractClassTypeWithConcreteClassType[baseClass] = subClass;
                Logger.GetInstance(typeof(TypeRegistry)).Info($"Registered {baseClass.Name} type to {subClass.FullName}");
            }
        }

        /// <summary>
        /// Registers the default instance type.
        /// </summary>
        /// <typeparam name="TBaseClass">The base class type.</typeparam>
        /// <typeparam name="TSubClass">The sub class type.</typeparam>
        public static void RegisterDefault<TBaseClass, TSubClass>()
                where TBaseClass : class
                where TSubClass : TBaseClass, new()
        {
            var baseClass = typeof(TBaseClass);
            var subClass = typeof(TSubClass);

            if (AbstractClassTypeWithConcreteClassType.ContainsKey(baseClass))
            {
                var oldSubClass = AbstractClassTypeWithConcreteClassType[baseClass];
                if (oldSubClass != subClass)
                {
                    Logger.GetInstance(typeof(TypeRegistry)).Error($"{baseClass.FullName} had been registered to {oldSubClass.FullName}. Registering to {subClass.FullName} will be ignored.");
                }
                return;
            }

            AbstractClassTypeWithConcreteClassType.Add(
                    baseClass,
                    subClass
            );
        }
    }
}
