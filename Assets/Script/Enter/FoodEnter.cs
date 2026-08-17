using UnityEngine;

public class FoodEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();

        if (An.Animalfood < 30f)
        {
            An.Animalfood = 100f;
            An.AddExp(5);
            Destroy(gameObject);
        }
    }
}
