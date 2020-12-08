using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EventBusRabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory connectionFactory;
        private IConnection connection;
        private bool disposed;

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            if (!IsConnected)
            {
                TryConnect();
            }
        }

        public bool IsConnected
        {
            get
            {
                return connection != null && connection.IsOpen && !disposed; 
            }
        }
        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Noo rabbit connection");
            }
            return connection.CreateModel();
        }

        public void Dispose()
        {
            if (disposed)
                return;

            try
            {
                connection.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        public bool TryConnect()
        {
            try
            {
                connection = connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException ex)
            {
                Thread.Sleep(2000);
                connection = connectionFactory.CreateConnection();
            }
            if (IsConnected)
                return true;

            return false;
        }
    }
}
