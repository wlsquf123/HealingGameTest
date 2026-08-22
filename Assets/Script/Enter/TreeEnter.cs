using UnityEngine;

public class TreeEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();

        if (An.Hp < 30f)
        {
            An.Hp = 100f;
            An.AddExp(4);
        }
    }
}
