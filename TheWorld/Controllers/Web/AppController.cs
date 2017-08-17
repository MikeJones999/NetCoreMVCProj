using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;

        //Uses Dependency injection to supply a mailservice object (via startup.cs) - which can then be used to call methods within mailservice
        public AppController(IMailService mailService)
        {
            this._mailService = mailService;
        }


        public IActionResult Index()
        {
            //go find view index.cshtml in Views/App/- render it and return to user
            return View();
        }

        //get function 
        public IActionResult Contact()
        {
            return View();
        }

        //post function
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            _mailService.SendMail("mikiej@gmail.com", model.Email, "From TheWorld Server", model.Message);
            return View();
        }



        public IActionResult About()
        {
            return View();
        }
    }
}
