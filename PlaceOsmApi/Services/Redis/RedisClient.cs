using StackExchange.Redis;
using Newtonsoft.Json;

namespace PlaceOsmApi.Services.Redis
{
    public class RedisClient
    {
        private ConnectionMultiplexer redis;
        private IDatabase db;

        public RedisClient(ConnectionMultiplexer connection = null)
        {
            redis = connection ?? ConnectionMultiplexer.Connect("localhost");
            db = redis.GetDatabase();
        }

        public TItem Get<TItem>(string key)
        {
            var item = db.StringGet(key);
            return item.IsNull 
                ? default(TItem) 
                : JsonConvert.DeserializeObject<TItem>(item);
        }

        public void Set<TItem>(string key, TItem item)
        {
            db.StringSet(key, JsonConvert.SerializeObject(item));
        }
        public void Set(string key, string item)
        {
            db.StringSet(key, item);
        }
    }
}
