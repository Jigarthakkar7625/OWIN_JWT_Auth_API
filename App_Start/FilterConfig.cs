﻿using System.Web;
using System.Web.Mvc;

namespace OWIN_JWT_Auth_API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
