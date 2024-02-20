
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;


namespace ConsoleApplication2
{
    internal class Program
    {

        /*static string httpResponseBody(string domain, HttpWebResponse response)
        {
            Stream responseBody;
            responseBody = response.GetResponseStream();
            StreamReader readHtml = new StreamReader(responseBody);
            string htmlcontent = readHtml.ReadToEnd();

            return htmlcontent;
        }

        static WebHeaderCollection httpResponseHeader(string domain, HttpWebResponse response)
        {
            WebHeaderCollection header = response.Headers;
            return header;
        } */



        static void DownloadFile(string url, string savePath, HttpClient client)
        {
 
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    Stream contentStream = response.Content.ReadAsStream();

                    using (FileStream fileStream = File.Create(savePath))
                    {
                        contentStream.CopyTo(fileStream);
                        Console.WriteLine("File downloaded successfully");
                    }
                }
                else
                {
                    Console.WriteLine($"Error code: {response.StatusCode}, Error message: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void GetRequestHeaders(string domain, HttpClient client)
        {
            
            var request = new HttpRequestMessage(HttpMethod.Get, domain);
            var productValue = new ProductInfoHeaderValue("NetProbe", "1.0");
            request.Headers.UserAgent.Add(productValue);
            Console.WriteLine($"{request.Method} {request.RequestUri} HTTP/{request.Version}");
            foreach (var header in request.Headers.ToList())
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }


        }

        static string GetString(HttpClient client, string url)
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            string responseString = response.Content.ReadAsStringAsync().Result;
            return responseString;
        }

        static string GetHeader(HttpClient client, string url)
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}" );
            string header = response.Headers.ToString();
            return header;
        }
        
        static void addRequestHeader(string domain, HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, domain);
            var productValue = new ProductInfoHeaderValue("NetProbe", "1.0");
            request.Headers.UserAgent.Add(productValue);
            Console.WriteLine($"{request.Method} {request.RequestUri} HTTP/{request.Version}");
            foreach (var header in request.Headers.ToList())
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }


        public static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var productValue = new ProductInfoHeaderValue("NetProbe", "1.0");
            client.DefaultRequestHeaders.UserAgent.Add(productValue);


            Boolean isContuniue = true;


            while (isContuniue)
            {
                Console.WriteLine("Welcome to the Web Request Program");
                Console.WriteLine("What Do You Want To Do? (Please Enter Number)");
                Console.WriteLine("1. Get Response Header");
                Console.WriteLine("2. Get Response Body");
                Console.WriteLine("3. Get Request Header");
                Console.WriteLine("4. Download File");
                Console.WriteLine("5. Add Custom Header in Request");
                Console.WriteLine("6. Exit");
                string domain = string.Empty;
                string options = Console.ReadLine();
            
            
                
                

                switch (options)
                {
                    case "1":
                        Console.WriteLine("Enter Target Domain/URL");
                        domain = Console.ReadLine();
                        Console.WriteLine("Response Header: ");
                        Console.WriteLine(GetHeader(client,domain));
                        break;
                    case "2":
                        Console.WriteLine("Enter Target Domain/URL");
                        domain = Console.ReadLine();
                        Console.WriteLine("Response Body: ");
                        Console.WriteLine(GetString(client,domain));
                        break;
                    case "3":
                        Console.WriteLine("Enter Target Domain/URL");
                        domain = Console.ReadLine();
                        Console.WriteLine("Request Header: ");
                        GetRequestHeaders(domain,client);
                        break;
                    case "4":
                        Console.WriteLine("Enter Target Domain/URL");
                        domain = Console.ReadLine();
                        var parse_domain = new Uri(domain);
                        DownloadFile(domain, parse_domain.Segments.Last(), client);
                        break;
                    case "5":
                        Console.WriteLine("Enter Target Domain/URL");

                    case "6":
                        isContuniue = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }


                /*HttpWebRequest request = (HttpWebRequest)WebRequest.Create(domain);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Console.WriteLine("Response Header: ");
                Console.WriteLine(httpResponseHeader(domain, response));
                Console.WriteLine("Response Body: ");
                Console.WriteLine(httpResponseBody(domain, response));
                
                Console.WriteLine("if do you want install file write yes or no: ");
                string download_file = Console.ReadLine(); */
            
                
            }
        }
    }
}