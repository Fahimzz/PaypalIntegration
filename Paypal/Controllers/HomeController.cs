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
            //using (var httpClient = new HttpClient())
            //{
            //    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api-m.sandbox.paypal.com/v1/oauth2/token"))
            //    {
            //        PayPalSettings payPalSettings = new PayPalSettings();
            //        payPalSettings.client_id = "AfRsYpE7GqYdI1tPsVlsxxZgqYQ1IjsbtUMqVXW04zVtvEoi3TRNPoQSYhxrfk7i5_xWu-mCqzB7TEKO";
            //        payPalSettings.client_secret = "EJkEf071qRvJbLOah1-I2DycaVgREgPiB1t73PKpYp_poL4KTI2pLEVVJGo0yA1TuEC_tUxMf1ZbTFsw";
            //        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(payPalSettings.client_id+":"+payPalSettings.client_secret);
            //        string val = System.Convert.ToBase64String(plainTextBytes);
            //        request.Headers.TryAddWithoutValidation("Authorization", "Basic "+val);


            //        var contentList = new List<string>();
            //        contentList.Add($"grant_type={Uri.EscapeDataString("client_credentials")}");
            //        contentList.Add($"ignoreCache={Uri.EscapeDataString("true")}");
            //        contentList.Add($"return_authn_schemes={Uri.EscapeDataString("true")}");
            //        contentList.Add($"return_client_metadata={Uri.EscapeDataString("true")}");
            //        contentList.Add($"return_unconsented_scopes={Uri.EscapeDataString("true")}");
            //        request.Content = new StringContent(string.Join("&", contentList));
            //        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            //        var response = httpClient.SendAsync(request);
            //        string responseStream = await response.Content.ReadAsStringAsync();

            //    }
            //}
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
            Root deptObj = JsonSerializer.Deserialize<Root>(responseBody);
            Console.WriteLine(responseBody);
            return new PayPalResBBody();
        }


        
    }
}
