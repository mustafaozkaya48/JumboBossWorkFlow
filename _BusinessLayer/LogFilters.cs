using _DbEntities;
using _DbEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _BusinessLayer
{
    public class LogFilter : FilterAttribute, IActionFilter
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                db.Logs.Add(new Logs()
                {
                    UserName = "system",
                    ActionName = filterContext.ActionDescriptor.ActionName,
                    ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    DateTime = DateTime.Now,
                    About = "OnActionExecuted"
                });
                db.SaveChanges();
            }
            catch (Exception)
            {

                return;
            }

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
