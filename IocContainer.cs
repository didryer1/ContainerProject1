using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication1.Containers
{
    public class IocContainer
    {

        // [ThreadStatic]- to prevent multiple threads accessing a common set of statics, 
        //create multiple static instances of that member – one for each isolated thread
        //A static variable marked with the ThreadStatic attribute is not shared between threads, 
        //therefore each thread gets it’s own instance of the static variable
        private static readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();
       // [ThreadStatic]
        private static readonly IDictionary<Type, object> typeInstances = new Dictionary<Type, object>();
        public void Register<TContract, TImplementation>()
        {
            if (types.ContainsKey(typeof(TContract)))
                throw new ArgumentException("Types already registerd");
            types[typeof(TContract)] = typeof(TImplementation);
            
        }
        public void Register<TContract, TImplementation>(TImplementation instance)
        {
            if (typeInstances.ContainsKey(typeof(TContract)))
                throw new ArgumentException("Instance Already created");
            typeInstances[typeof(TContract)] = instance;
        }

        public void Resolve<TContract, TImplementation>(LifeCycle lifecycle)
        {
            if (lifecycle.Singleton == true)
            {
                if (typeInstances.ContainsKey(typeof(TContract)))
                    throw new ArgumentException("Instance Already created");
                typeInstances[typeof(TContract)] = Activator.CreateInstance<TImplementation>();
            }
            typeInstances[typeof(TContract)] = Activator.CreateInstance<TImplementation>();

        }

        public  T Resolve<T>()
        {
            //singleton,,if instance already exist. 
            if (CreatedIbsatnce(typeof(T)))
            {
                return (T)typeInstances[typeof(T)];
            }

            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type contract)
        {
            //singleton,,if instance already exist. 
            //if (typeInstances.ContainsKey(contract))
            //{
            //    return typeInstances[contract];
            //}
            //else
            //{
                Type implementation = types[contract];
                ConstructorInfo constructor = implementation.GetConstructors()[0];
                ParameterInfo[] constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                {
                    return Activator.CreateInstance(implementation);
                }
                //this resolves rigestered types in constructors...
                List<object> parameters = new List<object>(constructorParameters.Length);
                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(Resolve(parameterInfo.ParameterType));
                }
                return constructor.Invoke(parameters.ToArray());
           // }
        }

        public bool CreatedIbsatnce(Type contract)
        {
            return typeInstances.ContainsKey(contract);
        }

        public bool InterfaceRegistered<T>()
        {
            return types.ContainsKey(typeof(T));
        }

        public bool InterfaceRegistered(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return types.ContainsKey(type);


        }

    }

    } 