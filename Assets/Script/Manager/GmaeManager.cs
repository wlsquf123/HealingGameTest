using UnityEngine;

public class GmaeManager : MonoBehaviour
{
    public static GmaeManager instance;
    public UIManager UIManager;
    public FSMManager FSMManager;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


}
