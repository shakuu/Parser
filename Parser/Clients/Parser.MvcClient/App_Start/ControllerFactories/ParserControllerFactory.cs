using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Parser.MvcClient.App_Start.ControllerFactories
{
    public class ParserControllerFactory : IControllerFactory
    {
        private readonly INinjectControllerFactory ninjectControllerFactory;

        public ParserControllerFactory(INinjectControllerFactory ninjectControllerFactory)
        {
            this.ninjectControllerFactory = ninjectControllerFactory;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            return this.ninjectControllerFactory.GetController(controllerName);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            (controller as IDisposable)?.Dispose();
        }
    }
}