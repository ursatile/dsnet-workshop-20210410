using System;
using EasyNetQ;
using Messages;

namespace Publisher {
	class Program {
		const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";
		static void Main(string[] args) {
			using var bus = RabbitHutch.CreateBus(AMQP);
			for(var i = 1; i <= 100; i++) {
				Console.WriteLine("Enter your message:");
				var message = $"This is message no. {i}";
				bus.PubSub.Publish(new Greeting(message));
			}
		}
	}
}
