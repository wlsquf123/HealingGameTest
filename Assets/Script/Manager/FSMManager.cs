using System.Security.Cryptography;
using UnityEngine;

public class FSMManager : MonoBehaviour
{
    public GameObject foodObj;
    public GameObject WaterObj;
    public GameObject TreeObj;

    public int set = 0;

    public void AddButton(int index)
    {
        set = index;
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 pos = hit.point;
            pos.y += 0.5f;

            switch (set)
            {
                case 1:
                    if (GameManager.instance.GetPoint(10))
                    {
                        Instantiate(foodObj, pos, Quaternion.identity);
                    }
                    break;
                case 2:
                    if (GameManager.instance.GetPoint(10))
                    {
                        Instantiate(WaterObj, pos, Quaternion.identity);
                    }
                    break;
                case 3:
                    if (GameManager.instance.GetPoint(20))
                    {
                        Instantiate(TreeObj, pos, Quaternion.identity);
                    }
                    break;
                case 4:
                    Instantiate(foodObj, pos, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(WaterObj, pos, Quaternion.identity);
                    break;
            }
            set = 0;
        }


        
    }
}
