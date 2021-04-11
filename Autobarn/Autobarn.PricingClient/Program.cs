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

		private static void CalculatePrice(NewCarMessage newCarMessage) {
			var request = new PriceRequest {
				Make = newCarMessage.Make,
				Model = newCarMessage.Model,
				Color = newCarMessage.Color,
				Year = newCarMessage.Year
			};
			var reply = grpcClient.GetPrice(request);
			var priceMessage = new NewCarPriceMessage {
                Make = newCarMessage.Make,
                Model = newCarMessage.Model,
				Color = newCarMessage.Color,
				Year = newCarMessage.Year,
                Price = reply.Price,
                Currency = reply.Currency
            };
            bus.PubSub.Publish(priceMessage);
		}
	}
}

