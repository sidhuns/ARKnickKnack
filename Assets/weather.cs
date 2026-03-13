using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Required for TextMeshPro
using System.Collections;
using System;

public class weather : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI weatherText;

    [Header("API Settings")]
    public string city = "Cincinnati";
    public string apiKey = "10a7ca6c0e6176da822692bbe6372763"; 

    private string apiUrl = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

    void Start()
    {
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        weatherText.text = "Loading weather...";

        string url = string.Format(apiUrl, city, apiKey);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                weatherText.text = "Sunshine and Daisies ";
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonResponse);

                weatherText.text = $"<b>{weatherData.name}</b>\n" +
                                   $"Temp: {weatherData.main.temp}°C\n" +
                                   $"Condition: {weatherData.weather[0].description}";
            }
        }
    }
}


[Serializable]
public class WeatherData
{
    public string name; 
    public MainData main;
    public WeatherDesc[] weather;
}

[Serializable]
public class MainData
{
    public float temp; 
}

[Serializable]
public class WeatherDesc
{
    public string description; 
}