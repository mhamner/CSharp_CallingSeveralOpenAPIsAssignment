using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_CallingSeveralOpenAPIsAssignment.Models;
using CSharp_CallingSeveralOpenAPIsAssignment.BusinessLogic;

namespace CSharp_CallingSeveralOpenAPIsAssignment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Calls several Open APIs based on user selection
            bool keepGoing = true;

            //Initialize our http client to get ready for our API call
            ApiHelper.InitializeClient();

            while (keepGoing)
            {
                Console.WriteLine("** MAIN MENU **");
                Console.WriteLine("Please choose an API to call.");
                Console.WriteLine("[1]  Predict Age Based on First Name");
                Console.WriteLine("[2]  Get a Random (clean) Joke");
                Console.WriteLine("[3]  Get FDA Recall Info. on 10 Items");
                Console.WriteLine("[4]  Get info. on a Particular Zipcode");
                Console.WriteLine("[5]  Get info. on a Particular Country");
                Console.WriteLine("");
                Console.Write("Your choice?  ");

                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        await RunPredictAgeBasedOnNameAPI();
                        break;
                    case "2":
                        await RunGetJokeAPI();
                        break;
                    case "3":
                        await RunGetFdaInfoAPI();
                        break;
                    case "4":
                        await RunGetZipcodeInfoAPI();
                        break;
                    case "5":
                        await RunGetCountryInfoAPI();
                        break;
                    default:
                        Console.WriteLine($"{choice} is not a valid option.");
                        break;
                }

                Console.Write("Do another [Y/N]?  ");
                keepGoing = (Console.ReadLine().ToLower() == "y") ? true : false;
            }
        }

        private static async Task RunPredictAgeBasedOnNameAPI()
        {
            Console.WriteLine("* Predict Age Based on Name API *");
            Console.Write("Please enter a first name:  ");
            string firstName = Console.ReadLine();

            PredictAgeBasedOnNameBL predictAgeBasedOnNameBL = new PredictAgeBasedOnNameBL();
            string predictedAge = await predictAgeBasedOnNameBL.PredictAgeBasedOnNameAsync(firstName);

            Console.WriteLine($"Predicted age for first name {firstName} is {predictedAge}.");
        }

        private static async Task RunGetJokeAPI()
        {
            Console.WriteLine("* Random Joke API *");

            JokeBL jokeBL = new JokeBL();
            string joke = await jokeBL.GetJokeAsync();

            Console.WriteLine($"Joke:  {joke}");
        }

        private static async Task RunGetFdaInfoAPI()
        {
            Console.WriteLine("* FDA Info. API *");

            FdaInfoBL fdaInfoBL = new FdaInfoBL();
            FdaInfoModel fdaInfoModel = await fdaInfoBL.GetFdaInfoAsync();

            foreach (Result res in fdaInfoModel.results)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Classification:  {res.classification}");
                Console.WriteLine($"Country:  {res.country}");
                Console.WriteLine($"Classification Date: {res.center_classification_date}");
                Console.WriteLine($"Product Description:  {res.product_description}");
                Console.WriteLine($"Reason for Recall:  {res.reason_for_recall}");
                Console.WriteLine("----------------------------------------------------------");
            }
        }

        private static async Task RunGetZipcodeInfoAPI()
        {
            Console.WriteLine("* Zipcode Info. API *");
            Console.Write("What zipcode would you like info. for?  ");
            string zipCode = Console.ReadLine();

            ZipCodeInfoBL zipCodeInfoBL = new ZipCodeInfoBL();
            ZipCodeInfoModel zipCodeInfoModel = await zipCodeInfoBL.GetZipCodeInfoAsync(zipCode);

            foreach (Place place in zipCodeInfoModel.places)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Name:  {place.placename}");
                Console.WriteLine($"State:  {place.state}");
                Console.WriteLine($"Latitude: {place.latitude}");
                Console.WriteLine($"Longitude:  {place.longitude}");
                Console.WriteLine("----------------------------------------------------------");
            }
        }

        private static async Task RunGetCountryInfoAPI()
        {
            Console.WriteLine("* Country Info. API *");
            Console.Write("What country would you like info. for (will get info. for all countries containing the text you enter)?  ");
            string country = Console.ReadLine();

            CountryInfoBL countryInfoBL = new CountryInfoBL();
            List<CountryInfoModel> countries = await countryInfoBL.GetCountryInfoAsync(country);

            foreach (CountryInfoModel countryInfoModel in countries)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Country Name:  {countryInfoModel.name}");
                Console.WriteLine($"Region:  {countryInfoModel.region}");
                Console.WriteLine($"Subregion: {countryInfoModel.subregion}");
                Console.WriteLine($"Capital:  {countryInfoModel.capital}");
                Console.WriteLine("Languages:");
                foreach(Language language in countryInfoModel.languages)
                {
                    Console.WriteLine(language.name);
                }
                Console.WriteLine("Bordering Countries:");
                foreach(string border in countryInfoModel.borders)
                {
                    Console.WriteLine(border);
                }
                Console.WriteLine("----------------------------------------------------------");
            }
        }
    }
}
