using Microsoft.AspNetCore.Mvc;
using MVCNetCore_Service_Scope_Transient_Singleton.Models;

namespace MVCNetCore_Service_Scope_Transient_Singleton.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IMyServiceSingleton _myServiceSingleton;
        private readonly IMyServiceSingleton _myServiceSingleton2;
        private readonly IMyServiceTransient _myServiceTransient;
        private readonly IMyServiceTransient _myServiceTransient2;
        private readonly IMyServiceScoped _myServiceScoped;
        private readonly IMyServiceScoped _myServiceScoped2;


        //public ServiceController(IMyServiceSingleton myServiceSingleton)
        //{   this._myServiceSingleton = myServiceSingleton;}

        public ServiceController(IMyServiceSingleton myServiceSingleton,  IMyServiceSingleton myServiceSingleton2
            , IMyServiceTransient myServiceTransient, IMyServiceTransient myServiceTransient2
            , IMyServiceScoped myServiceScoped, IMyServiceScoped myServiceScoped2)

        {
            this._myServiceSingleton = myServiceSingleton;
            this._myServiceSingleton2 = myServiceSingleton2;

            this._myServiceTransient = myServiceTransient;
            this._myServiceTransient2 = myServiceTransient2;

            this._myServiceScoped = myServiceScoped;
            this._myServiceScoped2 = myServiceScoped2;

        }
        public IActionResult Index()
        {
            ViewBag.SingletonMessage = _myServiceSingleton.GetMessage();
            ViewBag.SingletonCount = _myServiceSingleton.GetCount();
            ViewBag.SingletonMessage2 = _myServiceSingleton2.GetMessage();
            ViewBag.SingletonCount2 = _myServiceSingleton2.GetCount();

            ViewBag.TransientMessage = _myServiceTransient.GetMessage();
            ViewBag.TransientCount = _myServiceTransient.GetCount();
            ViewBag.TransientMessage2 = _myServiceTransient2.GetMessage();
            ViewBag.TransientCount2 = _myServiceTransient2.GetCount();

            ViewBag.ScopedMessage = _myServiceScoped.GetMessage();
            ViewBag.ScopedCount = _myServiceScoped.GetCount();
            ViewBag.ScopedMessage2 = _myServiceScoped2.GetMessage();
            ViewBag.ScopedCount2 = _myServiceScoped2.GetCount();

            return View();
        }
    }
}
