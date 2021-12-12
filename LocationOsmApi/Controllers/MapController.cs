using Itinero;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Controllers
{
    public class MapController : Controller
    {
        protected Lazy<IMapManager> lazyMapManager;
        protected IMapManager MapManager => lazyMapManager.Value;

        public MapController(IMapManager mapManager)
        {
            lazyMapManager = new Lazy<IMapManager>(() => mapManager);
        }
    }
}
