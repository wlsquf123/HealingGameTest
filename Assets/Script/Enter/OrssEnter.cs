using UnityEngine;

public class OrssEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();

        if (An.Water <= 30f)
        {
            An.Water = 100f;
            An.AddExp(3);
        }
    }
}
