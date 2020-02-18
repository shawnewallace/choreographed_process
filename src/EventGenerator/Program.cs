﻿using System;
using System.Collections.Generic;
using eda.core;
using eda.core.events;
using eda.services;

namespace EventGenerator
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("hit enter to start");
      Console.ReadLine();

      var service = new OrderCreator();

      for (var i = 0; i < 100; i++)
      {
        var order = CreateNewSampleOrderAcceptedEvent();
        service.Submit(order);
      }
    }

    private static IOrderAccepted CreateNewSampleOrderAcceptedEvent()
    {
      IOrderAccepted order = new OrderAcceptedEvent();

      order.CustomerId = Guid.NewGuid();
      order.OrderId = Guid.NewGuid();
      order.OrderItems = new List<OrderItem>
      {
        new OrderItem {Description = "Item One", ItemId = Guid.NewGuid(), Price = 100.21, Quantity = 4},
        new OrderItem {Description = "Item Two", ItemId = Guid.NewGuid(), Price = 19.99, Quantity = 6},
        new OrderItem {Description = "Item Three", ItemId = Guid.NewGuid(), Price = 5, Quantity = 1}
      };
      return order;
    }
  }
}
