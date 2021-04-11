using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeter;
using System.Diagnostics;
namespace GrpcGreeterClient {
	class Program {
		static async Task Main(string[] args) {
			var sw = new Stopwatch();
			using var channel = GrpcChannel.ForAddress("https://workshop.ursatile.com:5001/");
			var client = new Greeter.GreeterClient(channel);
            sw.Start();
			for (var i = 0; i < 1000; i++) {
				var request = new HelloRequest { Name = "GreeterClient" };
				var reply = await client.SayHelloAsync(request);
    			Console.WriteLine(reply.Message);
			}
            sw.Stop();
            Console.WriteLine($"Did 1000 messages in {sw.ElapsedMilliseconds}ms");
			Console.WriteLine("Press a key to exit");
			Console.ReadKey();
		}
	}
}
