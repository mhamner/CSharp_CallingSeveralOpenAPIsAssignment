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
    public class FdaInfoBL
    {
        public async Task<FdaInfoModel> GetFdaInfoAsync()
        {
            //API doc:  https://open.fda.gov/

            string url = $"https://api.fda.gov/food/enforcement.json?limit=10";
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
                    FdaInfoModel fdaInfoModel = JsonConvert.DeserializeObject<FdaInfoModel>(jsonString, settings);

                    return fdaInfoModel;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
