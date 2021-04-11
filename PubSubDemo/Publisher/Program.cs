using System;
using EasyNetQ;
using Messages;

namespace Publisher {
	class Program {
        const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";
		static void Main(string[] args) {
			using var bus = RabbitHutch.CreateBus(AMQP);
            bus.PubSub.Publish(new Greeting("Hello fwdays"));
		}
	}
}
