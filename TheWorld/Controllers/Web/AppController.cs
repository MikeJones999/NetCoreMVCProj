using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private IConfigurationRoot _config;

        //Uses Dependency injection to supply a mailservice object (via startup.cs) - which can then be used to call methods within mailservice
        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            this._mailService = mailService;
            this._config = config;
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

            //in exceptional cases server can combat certain validation - forexample fake email addresses from a provider
            //the validation message below will be displayed next to the email
            //only one error message for the addmodelerror
            if (model.Email.ToLower().Contains("aol.com"))
            {
               // ModelState.AddModelError("Email", "Dont support AOL.com - infact why would you be using that");
                ModelState.AddModelError("Summary", "This message will show up in the summary");
            }


            //use view model to tell client  that the validation is incorrect - however the server is never informed of this therefore use modelsate to check
            //tells you if any model errors have been added to ModelState. 
            if (ModelState.IsValid)
            {
                //using config.json - make a call to that file to get the ToAddress stipulated
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From TheWorld Server", model.Message);
                //Debug.WriteLine(model.Message);


                //clear the model to clear all data before the view is reloaded below
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent Successfully";
            }
            return View();
        }



        public IActionResult About()
        {
            return View();
        }
    }
}
