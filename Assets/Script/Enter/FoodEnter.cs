using UnityEngine;

public class FoodEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();

        if (An.Food < 30f)
        {
            An.Food = 100f;
            An.AddExp(5);
            GameManager.instance.FSMManager.FoodList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
