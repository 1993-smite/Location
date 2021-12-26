namespace PlaceOsmApi.Services.RouteService.ItineroRouteService.Resolvers
{
    interface IRedisCached
    {
        public TItem Get<TItem>(string key);
        public void Set<TItem>(string key, TItem item);
        public void Set(string key, string item);
    }
}
