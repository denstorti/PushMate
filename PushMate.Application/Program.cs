﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PushMate.Domain;
using PushMate.Domain.Interfaces;
using PushMate.Infrastructure;
using System;

namespace PushMate.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(c => c.AddConsole())
                .AddSingleton<IHttpPushService, FcmPushService>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>().CreateLogger<Program>();
                

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation("Starting application");
            logger.LogDebug("LEVEL DEBUG application");

            //do the actual work here
            var senderRepository = serviceProvider.GetService<IHttpPushService>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            var notificationController = new NotificationController(senderRepository, loggerFactory);
            var result = notificationController.SendOnePushAndroid();
            result.Wait();

            logger.LogInformation("All done!");

            //var registrationId = "ID gerado quando o device é registrado no FCM";
            //var serverKey = "acesse https://console.firebase.google.com/project/MY_PROJECT/settings/cloudmessaging";

            //var registrationId = "dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym";
            //var serverKey = "AIzaSyC8dhbIHM0BEDextBkH1YRGwq2zWSPW2kk";


            //string title = "Teste .Net Core";
            //using (var sender = new Sender(serverKey))
            //{
            //    for (int i = 0; i < 3; i++)
            //    {
            //        var message = new Message
            //        {
            //            RegistrationIds = new List<string> { registrationId },
            //            Notification = new Notification
            //            {
            //                Title = title,
            //                Body = $"Hello World@!{DateTime.Now.ToString()}"
            //            }
            //        };
            //        var result = sender.SendAsync(message).Result;
            //        WriteResult(result);
            //        Console.WriteLine($"Success: {result.MessageResponse.Success}");

            //        var json = "{\"notification\":{\"title\":\"mensagem em json\",\"body\":\"funciona!\"},\"to\":\"" + registrationId + "\"}";
            //        result = sender.SendAsync(json).Result;
            //        WriteResult(result);

            //        Thread.Sleep(1000);
            //    }
            //}

        }

        //private static void WriteResult(IResponse result)
        //{
        //    Console.WriteLine($"StatusCode: {result.StatusCode}");
        //    Console.WriteLine($"ReasonPhrase: {result.ReasonPhrase}");
        //    if (result.MessageResponse == null) return;

        //    Console.WriteLine($"MessageResponse.Success: {result.MessageResponse.Success}");
        //    Console.WriteLine($"MessageResponse.Failure: {result.MessageResponse.Failure}");
        //    Console.WriteLine($"MessageResponse.MulticastId: {result.MessageResponse.MulticastId}");
        //    Console.WriteLine($"MessageResponse.CanonicalIds: {result.MessageResponse.CanonicalIds}");
        //    Console.WriteLine($"MessageResponse.InternalError: {result.MessageResponse.InternalError}");
        //    Console.WriteLine($"MessageResponse.ResponseContent: {result.MessageResponse.ResponseContent}");
        //    if (result.MessageResponse.Results == null) return;

        //    foreach (var item in result.MessageResponse.Results)
        //    {
        //        Console.WriteLine($"MessageResponse.Results.MessageId: {item.MessageId}");
        //        Console.WriteLine($"MessageResponse.Results.RegistrationId: {item.RegistrationId}");
        //        Console.WriteLine($"MessageResponse.Results.Error: {item.Error}");
        //    }
        //    Console.WriteLine(new string('-', 20));
        //}
    }
}
