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
    public class ZipCodeInfoBL
    {
        public async Task<ZipCodeInfoModel> GetZipCodeInfoAsync(string zipCode)
        {
            //API doc:  http://www.zippopotam.us/

            string url = $"https://api.zippopotam.us/us/{zipCode}";
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
                    ZipCodeInfoModel zipCodeInfoModel = JsonConvert.DeserializeObject<ZipCodeInfoModel>(jsonString, settings);

                    return zipCodeInfoModel;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
