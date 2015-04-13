using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;

namespace Webshop_gr02.ViewModels
{
    public class RegisterAdminViewModel
    {
        public Registratie Registratie { get; set; }
        public SelectList Rollen { get; set; }

        public int SelectedRolID { get; set; }
    }
}