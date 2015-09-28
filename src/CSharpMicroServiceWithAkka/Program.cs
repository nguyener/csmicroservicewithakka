using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Akka;
using Akka.Actor;
namespace CSharpMicroServiceWithAkka
{

    public class Request
    {
        public Request(String request)
        {
            HTTP_Request = request;
        }

        public string HTTP_Request { get; set; }
    }


    public class Response
    {
        public Response(String response)
        {
            HTTP_Response = response;
        }

        public string HTTP_Response { get; set; }
    }

    public class RequestProcessor: ReceiveActor
    {
        public RequestProcessor()
        {
            Receive<Request>(request => Console.WriteLine("Request is: " + request.HTTP_Request));
            Receive<Response>(response => Console.WriteLine("Response is: " + response.HTTP_Response));
        }
    }

    public class Program
    {
        public void Main(string[] args)
        {
            var system = ActorSystem.Create("MySystem");
            var server = system.ActorOf<HTTPServer>("server");
            server.Tell(new Start());
            Console.Read();


        }
    }
}
