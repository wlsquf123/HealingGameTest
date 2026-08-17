using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    float Timer = 0f;
    public GameObject Items;

    private void Update()
    {
        var TreesObj = FindObjectsByType<TreeEnter>(FindObjectsSortMode.None);
        if (TreesObj.Length == 0) return;

        int randomTree = Random.Range(0, TreesObj.Length);

        Timer += Time.deltaTime * 2.4f;
        if (Timer > 30f)
        {
            Timer -= 30f;
            Instantiate(Items, TreesObj[randomTree].transform.position + transform.forward * 3 + transform.up * 15f, TreesObj[randomTree].transform.rotation);
        }
    }
}