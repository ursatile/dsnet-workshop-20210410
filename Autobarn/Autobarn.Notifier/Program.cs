using System;
using Autobarn.Messages;
using EasyNetQ;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Autobarn.Notifier {
	class Program {
        static HubConnection hub;
        private const string SIGNALR_URL = "https://workshop.ursatile.com:5001/newcarhub";
		const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";
		static async Task Main(string[] args) {
            hub = new HubConnectionBuilder().WithUrl(SIGNALR_URL).Build();
            await hub.StartAsync();
            await hub.SendAsync("DoMessage", "Autobarn.PricingClient", "PricingClient has connected! Yay!");

			using var bus = RabbitHutch.CreateBus(AMQP);
			bus.PubSub.Subscribe<NewCarPriceMessage>($"dashboard-{Environment.MachineName}", HandleNewCarPriceMessage);
			Console.ReadKey();
		}

		static async Task HandleNewCarPriceMessage(NewCarPriceMessage m) {
			SendEmailAboutCar(m);
            await TellWebsiteAboutCar(m);
		}

        static async Task TellWebsiteAboutCar(NewCarPriceMessage m) {
            await hub.SendAsync(
                "SendMessage", "Autobarn.Notifier", 
                JsonConvert.SerializeObject(m)
            );
        }

		static void SendEmailAboutCar(NewCarPriceMessage m) {
			var client = new SmtpClient("smtp.mailtrap.io", 2525) {
				Credentials = new NetworkCredential("36192fbee550e6a2f", "ea20ee3fe3a9e5"),
				EnableSsl = true
			};
			var subject = $"NEW CAR! {m.Make} {m.Model} {m.Year}";
			var body = $@"A new car is for sale!
Make: {m.Make}
Model: {m.Model}
Year: {m.Year}
Price: {m.Price} {m.Currency}";
			client.Send("cars@autobarn.com", "customer@example.com", subject, body);
		}

	}
}



