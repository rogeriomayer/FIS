using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMC.Shortener.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using FMC.Shortener.Utils;
using FMC.Shortener.Utils.API.Shortener;
using System.IO;
using System.Text.RegularExpressions;

namespace FMC.Shortener.Controllers
{
    public class P1Controller : Controller
    {
        private readonly IMemoryCache _cache;
        public IHttpContextAccessor _contextAccessor;
        private readonly string _key;
        private CacheSession cache;
        public P1Controller(IMemoryCache _cache, IHttpContextAccessor _contextAccessor)
        {
            this._cache = _cache;
            this._contextAccessor = _contextAccessor;

            if (_contextAccessor.HttpContext.Session.GetString("_sID") != _contextAccessor.HttpContext.Session.Id)
            {
                _contextAccessor.HttpContext.Session.SetString("_sID", _contextAccessor.HttpContext.Session.Id);
                _key = _contextAccessor.HttpContext.Session.Id;
                cache = new CacheSession(_contextAccessor, _key);
            }
            else
            {
                _key = _contextAccessor.HttpContext.Session.GetString("_sID");
                cache = new CacheSession(_contextAccessor, _key);
            }
        }
        

    }
}
