using Basket.API.Data.Interfaces;
using StackExchange.Redis;
using System;

namespace Basket.API.Data
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer redisConnection;

        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            this.redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get ; }
    }
}
