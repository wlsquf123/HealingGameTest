using UnityEngine;

public class AnimalEnter : MonoBehaviour
{
    public bool Merge = false;
    public Animal animal;

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var otherAn = other.GetComponent<AnimalEnter>();

        if (Merge || otherAn.Merge) return;
        if (animal.AnimalLevel != otherAn.animal.AnimalLevel) return;
        if (animal.AnName != otherAn.animal.AnName) return;
        if (animal.AnimalExp < 10f || otherAn.animal.AnimalExp < 10f) return;
        
        Merge = true;
        otherAn.Merge = true;

        if (animal.AnimalLevel < 3)
        {
            Destroy(other.gameObject);

            transform.localScale *= 1.25f;
            animal.AnimalLevel++;
            animal.AnimalHp = 100f;
            animal.Animalfood = 50f;
            animal.Animalwater = 50f;
            animal.AnimalExp = 0;

            animal.Change(AnimalState.Idle);

            Merge = false;
        }
        else
        {
            if (animal.Rating == 5) return;

            GameManager.instance.UIManager.OpenAnimalRating(animal.Rating + 1, transform.position);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
