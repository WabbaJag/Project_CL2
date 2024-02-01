using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Extensions
{
    public enum NotificationType
    {
        Success,
        Error,
        Info
    }

    public class BaseController : Controller
    {


        public void BasicNotification(string msj, NotificationType type, string title = "")
        {
            TempData["notification"] = $"Swal.fire('{title}','{msj}', '{type.ToString().ToLower()}')";
        }

    }
}
