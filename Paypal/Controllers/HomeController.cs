using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paypal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Paypal.Models.AccessTokenFormate;

namespace Paypal.Controllers
{
    [Route("api/[controller]")]

    public class HomeController : Controller
    {
        [HttpPost("Test")]

        public async Task<PayPalResBBody> Create(string value)
        {
           
            HttpClient client = new HttpClient();
            Uri baseUri = new Uri("https://api-m.sandbox.paypal.com/v1/oauth2/token");
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //Post body content
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(values);

            //payPalSettings.client_id = "AfRsYpE7GqYdI1tPsVlsxxZgqYQ1IjsbtUMqVXW04zVtvEoi3TRNPoQSYhxrfk7i5_xWu-mCqzB7TEKO";
            //payPalSettings.client_secret = "EJkEf071qRvJbLOah1-I2DycaVgREgPiB1t73PKpYp_poL4KTI2pLEVVJGo0yA1TuEC_tUxMf1ZbTFsw";

            var authenticationString = "AfRsYpE7GqYdI1tPsVlsxxZgqYQ1IjsbtUMqVXW04zVtvEoi3TRNPoQSYhxrfk7i5_xWu-mCqzB7TEKO:EJkEf071qRvJbLOah1-I2DycaVgREgPiB1t73PKpYp_poL4KTI2pLEVVJGo0yA1TuEC_tUxMf1ZbTFsw";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            //make the request
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            //response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Root authorization = JsonSerializer.Deserialize<Root>(responseBody);
            //create order
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api-m.sandbox.paypal.com/v2/checkout/orders"))
                {
                    request.Headers.TryAddWithoutValidation("Prefer", "return=representation");
                    request.Headers.TryAddWithoutValidation("PayPal-Request-Id", "1518cce6-09dc-44b2-b5cf-9ae1ca5f0e4e");
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer "+ authorization.access_token);
                    PayPalReqBody.Root root = new PayPalReqBody.Root();
                    root.intent = "CAPTURE";
                    
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var Orderresponse = await httpClient.SendAsync(request);
                }
            }
            return new PayPalResBBody();
        }


        
    }
}
