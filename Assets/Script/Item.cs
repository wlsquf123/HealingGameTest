using UnityEngine;

public enum ItemType
{
    foodItem,
    waterItem,
    HpItem,
    allfoodItem,
    allwaterItem,
    allHpItem,
    ThunderItem
}

public class Item : MonoBehaviour
{
    public ItemType ItemState;

    private void Start()
    {
        ItemState = (ItemType)Random.Range(0, 6);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
