using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Containers
{
    public sealed class SingletonIOCcontainer
    {
        private static readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();
        private static readonly IDictionary<Type, object> typeInstances = new Dictionary<Type, object>();


        public void Register<TContract, TImplementation>()
        {
            if (types.ContainsKey(typeof(TContract)))
                throw new ArgumentException("Types already registerd");
            types[typeof(TContract)] = typeof(TImplementation);
            typeInstances[typeof(TContract)] = Activator.CreateInstance<TImplementation>();

        }
    }
}