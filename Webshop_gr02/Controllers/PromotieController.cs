using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webshop_gr02.Controllers
{
    public class PromotieController : Controller
    {

        //
        // GET: /Promotie/
   
           public ActionResult PromotieToevoegen()
        {
            return View();
        }

        //Post Methode uploaden bestanden
        //
        [HttpPost]
           public ActionResult PromotieToevoegen(HttpPostedFileBase file)

        {
             try
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                file.SaveAs(path);
            }
            ViewBag.Message = "Bestand is geupload";
        }
             catch
             {
                 ViewBag.Message = "Het is niet gelukt op het bestand te uploaden";
             }
             return View();
        }

    }
}