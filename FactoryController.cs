using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Containers;

namespace WebApplication1.Controllers
{
    public class FactoryController : DefaultControllerFactory
    {
        private IocContainer _container { get; set; }

        public FactoryController()
        {
        }

        public FactoryController(IocContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                throw new HttpException(404, "Page not found: " + requestContext.HttpContext.Request.Path);

            if (!typeof(IController).IsAssignableFrom(controllerType))
                throw new ArgumentException("Type does not subclass IController", "controllerType");

            object[] parameters = null;

            ConstructorInfo constructor = controllerType.GetConstructors().FirstOrDefault(c => c.GetParameters().Length > 0);
            if (constructor != null)
            {
                ParameterInfo[] parametersInfo = constructor.GetParameters();
                parameters = new object[parametersInfo.Length];

                for (int i = 0; i < parametersInfo.Length; i++)
                {
                    ParameterInfo p = parametersInfo[i];

                    if (!_container.InterfaceRegistered(p.ParameterType))
                        throw new ApplicationException("Can't instanciate controller '" + controllerType.Name + "', one of its parameters is unknown to the IoC Container");

                    parameters[i] = _container.Resolve(p.ParameterType);
                }
            }

            try
            {
                return (IController)Activator.CreateInstance(controllerType, parameters);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentUICulture, "error creating controller", controllerType), ex);
            }
        }
    }
}