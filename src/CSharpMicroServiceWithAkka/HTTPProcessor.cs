using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using Akka;
using Akka.Actor;
using Newtonsoft.Json;
namespace CSharpMicroServiceWithAkka
{
    public class HTTPProcessor : ReceiveActor 
    {

        HttpListenerContext ctx;
        
        public HTTPProcessor()
        {
            Receive<Process>(message =>
            {
                this.ctx = message.Context;
                ProcessRequest();
            });
        }

        public void ProcessRequest()
        {
            HttpListenerRequest request = ctx.Request;
            string raw_url = request.RawUrl;
            if (raw_url.IndexOf("/index") == 0 || raw_url == "/")
            {
                DisplayIndex();
                return;
            }

            string id = "";
            string[] data = raw_url.Split('/');
            string collection = data[1];
            if (data.Length > 2)
            {
                id = data[2];
            }
            switch (request.HttpMethod.ToLower())
            {
                case "get":
                    ProcessGET(collection, id);
                    break;
                case "post":
                    ProcessPOST(collection);
                    break;
                case "put":
                    ProcessPUT(collection, id);
                    break;
                case "delete":
                    ProcessDELETE(collection, id);
                    break;
                default:
                    ErrorResponse("Method Not Allowed", 405);
                    break;
            }
        }

        public void ProcessGET(string collection, string id)
        {
            Console.WriteLine("Method GET collection " + collection + " id " + id);

            // Obtain a response object.
            HttpListenerResponse response = ctx.Response;
            // Construct a response.

            //List<Product> fruits = loadData(collection);
            List<Product> fruits = new List<Product>();
            Product apple = new Product();
            apple.Name = "apple";
            apple.Price = 1;
            fruits.Add(apple);
            string responseString;
            if (id == "" || id == null)
            {
                responseString = JsonConvert.SerializeObject(fruits);
            }
            else
            {
                var fruit = fruits.Find(x => x.Name == id);
                if (fruit == null)
                {
                    responseString = "<HTML><BODY> NOT FOUND</BODY></HTML>";
                }
                else
                {
                    responseString = JsonConvert.SerializeObject(fruit);
                }

            }



            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            //response.ContentType = "application/json";


            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

        public void ProcessPOST(string collection)
        {
            Console.WriteLine("Method POST collection " + collection);
        }

        public void ProcessPUT(string collection, string id)
        {
            Console.WriteLine("Method PUT collection " + collection + " id " + id);
        }

        public void ProcessDELETE(string collection, string id)
        {
            Console.WriteLine("Method DELETE collection " + collection + " id " + id);
        }

        public void DisplayIndex()
        {

        }

        public void ErrorResponse(string message, int error)
        {

        }
    }
}
