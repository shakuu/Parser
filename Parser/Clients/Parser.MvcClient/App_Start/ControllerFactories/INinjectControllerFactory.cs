using System.Web.Mvc;

namespace Parser.MvcClient.App_Start.ControllerFactories
{
    public interface INinjectControllerFactory
    {
        IController GetController(string controllerName);
    }
}