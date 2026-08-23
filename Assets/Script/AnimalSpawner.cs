using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] Animals;
    private int set = -1;

    public void SpawnerIndex(int Index)
    {
        set = Index;
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
                case 0:
                    if (GameManager.instance.GetPoint(10))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 1:
                    if (GameManager.instance.GetPoint(10))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 2:
                    if (GameManager.instance.GetPoint(50))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 3:
                    if (GameManager.instance.GetPoint(50))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 4:
                    if (GameManager.instance.GetPoint(50))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 5:
                    if (GameManager.instance.GetPoint(300))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 6:
                    if (GameManager.instance.GetPoint(300))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 7:
                    if (GameManager.instance.GetPoint(300))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 8:
                    if (GameManager.instance.GetPoint(2000))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;
                case 9:
                    if (GameManager.instance.GetPoint(7000))
                    {
                        Instantiate(Animals[set], pos, Quaternion.identity);
                    }
                    break;

            }
            set = -1;
        }
    }
}
