using UnityEngine;

public class TreeEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();

        if (An.AnimalHp < 30f)
        {
            An.AnimalHp = 100f;
            An.AddExp(4);
        }
    }
}
