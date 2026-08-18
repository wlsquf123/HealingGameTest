using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UIManager;
    public FSMManager FSMManager;
    public InventoryManager InventoryManager;

    [Header("상태")]
    public float M; // 분
    public float H; // 시간
    public int Day = 1; // 일
    public int Point = 0;
    private float PointTimer;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        
        M += Time.deltaTime * 2.4f;
        if (M >= 60f)
        {
            M -= 60f;
            H++;
        }
        if (H >= 24f)
        {
            H -= 24;
            Day++;
        }

        PointTimer += Time.deltaTime * 2.4f;
        if (PointTimer >= 10f)
        {
            PointTimer -= 10f;
            Point++;
        }
    }

    public bool GetPoint(int get)
    {
        if (get > Point)
        {
            Debug.Log( get - Point + "포인트 필요!");
            return false;
        }

        Point -= get;
        return true;
    }

    public void DayTImer()
    {

    }
}
