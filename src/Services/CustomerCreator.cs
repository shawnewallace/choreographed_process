﻿using System;
using System.Text;
using eda.core;
using eda.core.events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace eda.services
{
  public class CustomerCreator
  {
    public void Submit(INewCustomer customer)
    {
      var factory = new ConnectionFactory { HostName = "localhost" };
      using (var connection = factory.CreateConnection())
      {
        using (var channel = connection.CreateModel())
        {
          channel.ExchangeDeclare(Constants.EXCHANGE_NAME, "direct", true);

          var message = JsonConvert.SerializeObject(customer);
          var body = Encoding.UTF8.GetBytes(message);

          channel.BasicPublish(Constants.EXCHANGE_NAME, Constants.NEW_CUSTOMER_EVENT, null, body);
          Console.WriteLine(" [x] Sent '{0}':'{1}'", Constants.NEW_CUSTOMER_EVENT, message);
        }
      }
    }
  }
}
