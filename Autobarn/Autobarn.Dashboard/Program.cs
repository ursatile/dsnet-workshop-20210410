using System;
using Autobarn.Messages;
using EasyNetQ;

namespace Autobarn.Dashboard {
	class Program {
		const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";
		static void Main(string[] args) {
			using var bus = RabbitHutch.CreateBus(AMQP);
			bus.PubSub.Subscribe<NewCarMessage>($"dashboard-{Environment.MachineName}", HandleNewCarMessage);
			Console.ReadKey();
		}

		static void HandleNewCarMessage(NewCarMessage m) {
            Console.WriteLine($"NEW CAR! {m.Registration} ({m.Make} {m.Model} / {m.Year}");
			Console.WriteLine($"(received at {m.DateAdded}");
            Console.WriteLine(String.Empty.PadLeft(64,'-'));
		}
	}
}



