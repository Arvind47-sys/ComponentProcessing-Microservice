using Api.Entities;
using Api.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Api.BLOs
{
    /// <summary>
    /// Contains the logic for calling the PackagingAndDelivery Microservice
    /// </summary>
    public class PackagingAndDeliveryBLO : IPackagingAndDeliveryBLO
    {
        private readonly IConfiguration _config;

        public PackagingAndDeliveryBLO(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets the computed packaging and delivery cost based on the component type and count
        /// </summary>
        /// <param name="processRequest"></param>
        /// <returns></returns>
        public async Task<double> ComputePackagingAndDeliveryCost(ProcessRequest processRequest)
        {
            //string BaseUrl = "https://localhost:44335/";

            string BaseUrl = _config["PackagingAndDeliverySvcURL"];

            double packagingAndDeliveryCharge = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync(string.Format("PackagingAndDelivery/GetPackagingDeliveryCharge?" +
                    "componentType={0}&count={1}", processRequest.DefectiveComponentType, processRequest.Quantity));

                if (Res.IsSuccessStatusCode)
                {

                    var ObjResponse = Res.Content.ReadAsStringAsync().Result;
                    packagingAndDeliveryCharge = JsonConvert.DeserializeObject<double>(ObjResponse);

                }
                else
                {
                    throw new Exception("Error in getting the packaging and delivery cost from Packaging and Delivery Microservice");
                }
            }

            return packagingAndDeliveryCharge;
        }
    }
}
