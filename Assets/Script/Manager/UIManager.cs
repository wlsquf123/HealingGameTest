using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text DayText;
    public Text PointText;

    private void Update()
    {
        //state 관리
        GameManager gm = GameManager.instance;
        DayText.text = "Day" + gm.Day + "\n" + gm.H.ToString("00") + ":" + gm.M.ToString("00"); // 시간
        PointText.text = "포인트: " + gm.Point;
    }
}
