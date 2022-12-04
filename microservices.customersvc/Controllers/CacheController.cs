using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace microservices.customersvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        //private readonly IConnectionMultiplexer _redisCache;
        private readonly IDistributedCache _distributedCache;
        public CacheController(IDistributedCache distributedCache)
        {
            _distributedCache  = distributedCache;
        }
        // GET api/<CacheController>/5
        [HttpGet]
        public string? Get([FromQuery] string key)
        {
            return _distributedCache.GetString(key);
            //return "value";
        }

        // POST api/<CacheController>
        [HttpPost]
        public void Post([FromBody] KeyValuePair<string,string> item)
        {
            _distributedCache.SetString(item.Key,item.Value);
        }

        
    }
}
