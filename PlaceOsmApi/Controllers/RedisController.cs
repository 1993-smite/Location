using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Models.Redis;
using PlaceOsmApi.Services.RouteService.ItineroRouteService.Resolvers;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace PlaceOsmApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RedisController : ControllerBase
    {
        private IConnectionMultiplexer redis;
        private IDatabase db;

        public RedisController(IConnectionMultiplexer connection)
        {
            redis = connection;
            db = redis.GetDatabase();
        }

        [HttpGet]
        public IActionResult Index()
        {
            EndPoint endPoint = redis.GetEndPoints().First();
            IServer server = redis.GetServer(endPoint);

            var response = new List<Record<string>>();
            foreach (var key in server.Keys(pattern: $"*{CachedResolver.redisResolverPrefixKey}*"))
            {
                response.Add(new Record<string>(key, db.StringGet(key)));
            }
            //server.FlushDatabase();

            return Ok(response);
        }

        [HttpDelete]
        public IActionResult DeleteAllRedisDatas() {
            EndPoint endPoint = redis.GetEndPoints().First();
            IServer server = redis.GetServer(endPoint);

            StringBuilder response = new StringBuilder();
            foreach (var key in server.Keys(pattern: $"*{CachedResolver.redisResolverPrefixKey}*"))
            {
                db.KeyDelete(key);
            }
            server.FlushDatabase();

            return Ok("Db is empty");
        }
    }
}
