using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public Text DayText;
    public Text PointText;
    public Text WeatherText;

    public GameObject RatingImage;
    public GameObject[] RatingIndex;
    private Vector3 pos;

    private void Update()
    {
        //state 관리
        GameManager gm = GameManager.instance;
        DayText.text = "Day" + gm.Day + "\n" + gm.H.ToString("00") + ":" + gm.M.ToString("00"); // 시간
        PointText.text = "포인트: " + gm.Point;
        WeatherText.text = "현재 날씨: " + GameManager.instance.WeatherManager.WeatherType;


    }


    public void OpenAnimalRating(int rating, Vector3 transform)
    {
        RatingImage.SetActive(true);
        pos = transform;

        for (int i = 0; i < RatingIndex.Length; i++)
        {
            RatingIndex[i].SetActive(false);
        }

        RatingIndex[rating - 2].SetActive(true);
    }

    public void AnimalClick(GameObject prefab)
    {
        Instantiate(prefab, pos, Quaternion.identity);
        RatingImage.SetActive(false);
    }
}