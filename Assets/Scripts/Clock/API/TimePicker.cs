using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public partial class TimePicker
{
    private readonly string _refOnTimeApi = "https://timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow";

    public async Task<TimeDTO> GetTime()
    {
        TimeDTO timeResponse = new TimeDTO();

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_refOnTimeApi);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                timeResponse = JsonConvert.DeserializeObject<TimeDTO>(responseBody);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }

        return timeResponse;
    }
}
