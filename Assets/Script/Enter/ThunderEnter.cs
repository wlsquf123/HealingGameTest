using UnityEngine;

public class ThunderEnter : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.AudioManager.SFX[3].Play();
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var An = other.GetComponent<Animal>();

        if (!other.CompareTag("Animal")) return;

        if (An.isThunder)
        {
            An.isThunder = false;
            return;
        }
        Destroy(other.gameObject);
    }
}