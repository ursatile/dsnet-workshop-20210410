using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeter;

namespace GrpcGreeterClient {
	class Program {
		static async Task Main(string[] args) {
            using var channel = GrpcChannel.ForAddress("https://workshop.ursatile.com:5001/");
            var client =  new Greeter.GreeterClient(channel);
            var request = new HelloRequest { Name = "GreeterClient" };
            var reply = await client.SayHelloAsync(request);
            Console.WriteLine(reply.Message);
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
		}
	}
}
