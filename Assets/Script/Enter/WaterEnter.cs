using UnityEngine;

public class WaterEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();

        if (An.Animalwater <= 30f)
        {
            An.Animalwater = 100f;
            An.AddExp(3);
            Destroy(gameObject);
        }
    }
}
