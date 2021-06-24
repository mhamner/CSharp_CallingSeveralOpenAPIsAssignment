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
    public class CountryInfoBL
    {
        public async Task<List<CountryInfoModel>> GetCountryInfoAsync(string countryName)
        {
            //API doc:  https://restcountries.eu/

            string url = $"https://restcountries.eu/rest/v2/name/{countryName}";
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

                    List<CountryInfoModel> countryInfoModels = JsonConvert.DeserializeObject<List<CountryInfoModel>>(jsonString, settings);

                    return countryInfoModels;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
