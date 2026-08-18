using System.Collections;
using UnityEngine;

public enum WeatherState
{
    ¸¼À½,
    Èå¸²,
    ºñ,
    ÃµµÕ
}

public class WeatherManager : MonoBehaviour
{
    public WeatherState WeatherType;
    public GameObject RainObj;

    public GameObject[] ThunderObj;
    public GameObject ThunderEnter;

    private void Start()
    {
        RandomWeather();
    }

    public void Weathering()
    {
        RenderSettings.fog = false;
        RainObj.SetActive(false);

        switch (WeatherType)
        {
            case WeatherState.¸¼À½:
                break;

            case WeatherState.Èå¸²:
                RenderSettings.fogColor = new Color(0.8f, 0.8f, 0.8f);
                RenderSettings.fog = true;
                break;

            case WeatherState.ºñ:
                RainObj.SetActive(true);
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color(0.5f, 0.5f, 0.5f);
                break;

            case WeatherState.ÃµµÕ:
                RainObj.SetActive(true);
                StartCoroutine(RandomThnuder());
                break;
        }
    }

    public void RandomWeather()
    {
        WeatherType = (WeatherState)Random.Range(0, 4);
        Weathering();   
    }

    public IEnumerator RandomThnuder()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int random = Random.Range(0, ThunderObj.Length);
            ThunderObj[random].SetActive(true);

            yield return new WaitForSeconds(3f);

            Instantiate(ThunderEnter, ThunderObj[random].transform.position, transform.rotation);
            ThunderObj[random].SetActive(false);
        }
    }

    public void WeatherChatKey()
    {
        WeatherType = WeatherType + 1;
        if (WeatherType >= (WeatherState)4)
        {
            WeatherType = 0;
        }

        Weathering();
    }
}
