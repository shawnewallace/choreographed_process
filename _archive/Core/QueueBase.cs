﻿using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace Core
{
    public static class Constants
    {
        public const string EXCHANGE_NAME = "retail_system";

        public const string LOGGING_QUEUE_NAME = "logger_q";
        public const string INVOICING_QUEUE_NAME = "invoicer_q";
        public const string SHIPPING_QUEUE_NAME = "shipping_q";
        public const string WAREHOUSE_QUEUE_NAME = "warehouse_q";
        public const string MASTER_CUSTOMER_QUEUE_NAME = "customer_q";

        public const string ORDER_ACCEPTED_EVENT = "order_accepted";
        public const string CUSTOMER_BILLED_EVENT = "customer_billed";
        public const string READY_FOR_SHIPMENT_EVENT = "order_ready_to_ship";
        public const string SHIPPED_EVENT = "order_shipped";
        public const string NEW_CUSTOMER_EVENT = "new_customer";

        public static IEnumerable<string> EventCollection => new List<string>()
        {
            ORDER_ACCEPTED_EVENT,
            CUSTOMER_BILLED_EVENT,
            READY_FOR_SHIPMENT_EVENT,
            SHIPPED_EVENT,
      NEW_CUSTOMER_EVENT
        };
    }

    public class QueueBase
    {
        protected static ConnectionFactory GetConnectionFactory()
        {
            //var factory = new ConnectionFactory()
            //{
            //  HostName = "equinox.local",
            //  Port = 5672,
            //  VirtualHost = "/",
            //  UserName = "dev",
            //  Password = "password"
            //};

            var factory = new ConnectionFactory()
            {
                Uri = "amqp://icxhlaxw:_2rjFrd8QEr5abxfi0PLjMXSUPasC67N@fox.rmq.cloudamqp.com/icxhlaxw",
                UserName = "icxhlaxw",
                Password = "_2rjFrd8QEr5abxfi0PLjMXSUPasC67N"
            };

            //var factory = new ConnectionFactory()
            //{
            //    HostName = "localhost",
            //    Port = 5672,
            //    //VirtualHost = "/",
            //    //UserName = "dev",
            //    //Password = "password"
            //};

            return factory;
        }

        protected static void DeclareExchange(IModel channel)
        {
            channel.ExchangeDeclare(Constants.EXCHANGE_NAME, "direct", true);
        }

        protected static void DeclareQ(IModel channel, string name)
        {
            channel.QueueDeclare(name, true, false, false, null);
        }

        protected static void SetUpQoS(IModel channel)
        {
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        protected static void BindToQ(IModel channel, string queuename, string evt)
        {
            Console.Write("Binding to event {0}...", evt);

            channel.QueueBind(
                        queue: queuename,
                        exchange: Constants.EXCHANGE_NAME,
                        routingKey: evt);
            Console.WriteLine("Done");
        }
    }
}