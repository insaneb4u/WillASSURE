﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WillAssure.Controllers
{
    public class AddAssetsController : Controller
    {
        // GET: AddAssets
        public ActionResult AddAssetsIndex()
        {
            return View("~/Views/AddAssets/AddAssetsPageContent.cshtml");
        }
    }
}