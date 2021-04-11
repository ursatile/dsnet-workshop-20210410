using System;
using Autobarn.Messages;
using EasyNetQ;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace Autobarn.Notifier {
	class Program {
		const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";
		static void Main(string[] args) {
			using var bus = RabbitHutch.CreateBus(AMQP);
			bus.PubSub.Subscribe<NewCarPriceMessage>($"dashboard-{Environment.MachineName}", HandleNewCarPriceMessage);
			Console.ReadKey();
		}

		static void HandleNewCarPriceMessage(NewCarPriceMessage m) {
			SendEmailAboutCar(m);
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



