﻿using JobShopAPI.Repository.Interfaces;
using JobShopWeb.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JobShopWeb.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
            private readonly IHttpClientFactory _clientFactory;

            public AccountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
            {
                _clientFactory = clientFactory;

            }

        /// <summary>
        /// Função para fazer o login frontend
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objToCreate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<User> LoginAsync(string url, User objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return new User();
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<User>(jsonString);
            }
            else
            {
                return new User();
            }
        }


        /// <summary>
        /// Registar utilizador
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objToCreate"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(string url, CreateUserDto objToCreate, string token ="")
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objToUpdate"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string url, UpdateUserDto objToUpdate, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (objToUpdate != null)
            {
                if(objToUpdate.OldPassword == null && objToUpdate.NewPassword == null)
                {
                    objToUpdate.NewPassword = "";
                    objToUpdate.OldPassword = "";
                    objToUpdate.Role = "x";
                }
                request.Content = new StringContent(JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var client = _clientFactory.CreateClient();
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objToUpdate"></param>
        /// <param name="token"></param>
        /// <returns></returns>
         public async Task<bool> DeleteAsync(string url, DeleteUserDto objToUpdate, string token = "")
         {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (objToUpdate != null)
            {
                if (objToUpdate.Role != "x")
                {
                    objToUpdate.Role = "x";
                }
                request.Content = new StringContent(JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var client = _clientFactory.CreateClient();
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}



