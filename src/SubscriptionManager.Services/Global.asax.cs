using System.Web;
using System.Web.Http;
using SubscriptionManager.CompositionRoot;

namespace SubscriptionManager.Services
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            Composition.Load(container => Container.SetContainer(container));

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}