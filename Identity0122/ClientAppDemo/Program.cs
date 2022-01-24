﻿using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientAppDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001/");

                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "client_console",
                    ClientSecret = "secret_console",
                    Scope = "api_demo_scope"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                }

                Console.WriteLine(tokenResponse.Json);
                Console.WriteLine();

                using (var apiClient = new HttpClient())
                {
                    apiClient.SetBearerToken(tokenResponse.AccessToken);

                    var response = await apiClient.GetAsync("https://localhost:5101/weatherforecast");
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);
                    }

                }
            }
            Console.ReadLine();
        }
    }
}
