using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using CSharp_CallingSeveralOpenAPIsAssignment.Models;

namespace CSharp_CallingSeveralOpenAPIsAssignment.BusinessLogic
{
    public class JokeBL
    {
        public async Task<string> GetJokeAsync()
        {
            //API doc:  https://sv443.net/jokeapi/v2/
            
            //Blacklist all bad categories so we only get good, clean, SFW jokes :)
            string url = $"https://v2.jokeapi.dev/joke/Any?blacklistFlags=nsfw,religious,political,racist,sexist,explicit";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    JokeModel jokeModel = JsonConvert.DeserializeObject<JokeModel>(jsonString, settings);

                    return jokeModel.joke;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
