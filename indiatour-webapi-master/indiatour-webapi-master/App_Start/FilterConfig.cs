﻿using System.Web;
using System.Web.Mvc;

namespace indiatour_webapi_master
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
