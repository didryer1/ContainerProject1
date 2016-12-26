using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Containers;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            LifeCycle lifecycle = new LifeCycle();
            lifecycle.Singleton = true;
            IocContainer container = new IocContainer();
            

            container.Register<ITeacher, Teacher>();
            container.Register<IAdministrator, Administrator>();
            container.Resolve<ITeacher,Teacher>(lifecycle);

            ControllerBuilder.Current.SetControllerFactory(new FactoryController(container));
            //ControllerBuilder.Current.SetControllerFactory(typeof(FactoryController));

        }


    }
    
}
