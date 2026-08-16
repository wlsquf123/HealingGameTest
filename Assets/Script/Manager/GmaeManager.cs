using UnityEngine;

public class GmaeManager : MonoBehaviour
{
    public static GmaeManager instance;



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
