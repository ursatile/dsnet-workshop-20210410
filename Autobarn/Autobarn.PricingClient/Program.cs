using System;
using Grpc.Net.Client;
using Autobarn.PricingServer;
using Autobarn.Messages;
using EasyNetQ;
using System.Threading.Tasks;

namespace Autobarn.PricingClient {
	class Program {
		const string AMQP = "amqps://uzvpuvak:CmgAweRCxXXm5L0wRC9aAWVRuMklAamN@rattlesnake.rmq.cloudamqp.com/uzvpuvak";
		static IBus bus;
		static Pricer.PricerClient grpcClient;

		static void Main(string[] args) {
			using var channel = GrpcChannel.ForAddress("https://workshop.ursatile.com:5003/");
			grpcClient = new Pricer.PricerClient(channel);
			bus = RabbitHutch.CreateBus(AMQP);
			bus.PubSub.Subscribe<NewCarMessage>($"dashboard-{Environment.MachineName}", HandleNewCarMessage);
			Console.ReadKey();
		}

		static void HandleNewCarMessage(NewCarMessage m) {
            Console.WriteLine(m.Registration);
			CalculatePrice(m);
		}

		private static void CalculatePrice(NewCarMessage message) {
			var request = new PriceRequest {
				Make = message.Make,
				Model = message.Model,
				Color = message.Color,
				Year = message.Year
			};
			var reply = grpcClient.GetPrice(request);
			Console.WriteLine(reply.Price);
		}
	}
}

