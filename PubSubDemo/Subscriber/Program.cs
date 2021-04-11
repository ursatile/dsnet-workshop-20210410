using System;
using EasyNetQ;
using Messages;

namespace Subscriber {
	class Program {
		const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";
		static void Main(string[] args) {
			using var bus = RabbitHutch.CreateBus(AMQP);
			bus.PubSub.Subscribe<Greeting>("dylan-in-london", HandleGreeting);
			Console.ReadKey();
		}

		static void HandleGreeting(Greeting greeting) {
			Console.WriteLine(greeting.Text);
            Console.WriteLine($"(received at {greeting.Date}");
		}
	}
}
