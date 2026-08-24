using UnityEngine;

public class WaterEnter : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.AudioManager.SFX[1].Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();

        if (An.Water <= 30f)
        {
            An.Water = 100f;
            An.AddExp(3);
            Destroy(gameObject);
        }
    }
}
