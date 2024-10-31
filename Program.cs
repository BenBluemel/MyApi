using System;
using System.Net;
using System.Threading;

class Program
{
	static void Main()
	{
		HttpListener listener = new HttpListener();
		listener.Prefixes.Add("http://*:80/");
		listener.Start();
		Console.WriteLine("Listening...");

		while (true)
		{
			HttpListenerContext context = listener.GetContext();
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;

			switch (request.Url.AbsolutePath)
			{
				case "/health":
					HealthHandler(response);
					break;
				case "/testtimeout":
					TestTimeoutHandler(response);
					break;
				case "/testalmost":
					TestAlmostHandler(response);
					break;
				case "/test":
					TestHandler(response);
					break;
				default:
					response.StatusCode = (int)HttpStatusCode.NotFound;
					response.Close();
					break;
			}
		}
	}

	static void HealthHandler(HttpListenerResponse response)
	{
		response.StatusCode = (int)HttpStatusCode.OK;
		response.Close();
	}

	static void TestTimeoutHandler(HttpListenerResponse response)
	{
		Thread.Sleep(40000);
		response.StatusCode = (int)HttpStatusCode.OK;
		response.Close();
	}

	static void TestAlmostHandler(HttpListenerResponse response)
	{
		Thread.Sleep(25000);
		response.StatusCode = (int)HttpStatusCode.OK;
		response.Close();
	}

	static void TestHandler(HttpListenerResponse response)
	{
		response.StatusCode = (int)HttpStatusCode.OK;
		response.Close();
	}
}