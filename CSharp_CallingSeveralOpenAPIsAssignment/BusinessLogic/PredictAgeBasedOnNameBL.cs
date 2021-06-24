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
    public class PredictAgeBasedOnNameBL
    {
        public async Task<string> PredictAgeBasedOnNameAsync(string name)
        {

            //API doc: https://agify.io/

            string url = $"https://api.agify.io/?name={name}";
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
                    PredictAgeBasedOnNameModel predictAgeBasedOnNameModel = JsonConvert.DeserializeObject<PredictAgeBasedOnNameModel>(jsonString, settings);

                    return predictAgeBasedOnNameModel.age.ToString();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
