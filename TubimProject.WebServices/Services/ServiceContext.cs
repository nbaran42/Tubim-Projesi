using Newtonsoft.Json;
using System.Text;
using TubimProject.Application.Constants;
using TubimProject.Application.DTOs.Services.DeserializeModels;
using TubimProject.Application.Enums;
using TubimProject.Application.Interfaces.WebServices;

namespace TubimProject.WebServices.Services
{
    public class ServiceContext : IServiceContext
    {
        public T GetServiceData<T>(long? lastId, JTabloEnums jTabloEnums)
        {
            using (HttpClient client = new HttpClient())
            {

                string? token = GetToken()?.token;
                client.BaseAddress =UTILS.JBaseUrl;
                client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

                var parameters = new
                {
                    TabloTanimId = (int)jTabloEnums,
                    AlinanSonId = lastId
                };
                HttpContent content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                var result = client.PostAsync("TopluVeri/TabloBaslangicBilgilerAktar", content).Result;

                if (result.IsSuccessStatusCode)
                {
                    var x = JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                    return x;
                }
                return default(T);
            }
        }

        public JGetToken? GetToken()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress=UTILS.JBaseUrl; 
                client.DefaultRequestHeaders.Add("Authorization", "Basic U1ZZREJVU0VSOj8qLlZZREIyMDIwMV8qLw==");

                var creds = new
                {
                    kullaniciadi = "EMNIYETKLN",
                    sifre = "?EGM_TUBIM_?2020.*"
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(creds), Encoding.UTF8, "application/json");

                var result = client.PostAsync("yetki/getirtoken", content).Result;

                if (result.IsSuccessStatusCode)
                {
                   return JsonConvert.DeserializeObject<JGetToken>(result.Content.ReadAsStringAsync().Result);
                
                }
                return null;
            }
        }

        public JGetYetkiOlunanTablolar? GetYetkiOlunanTablolar(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress =UTILS.JBaseUrl;
                client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");


                var result = client.GetAsync("TopluVeri/YetliliOlunanTablo").Result;

                if (result.IsSuccessStatusCode)
                {
                   return JsonConvert.DeserializeObject<JGetYetkiOlunanTablolar>(result.Content.ReadAsStringAsync().Result);
                   
                }
                return null;
            }
        }
    }
}
