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
        if (animal.Exp < 10f || otherAn.animal.Exp < 10f) return;
        
        Merge = true;
        otherAn.Merge = true;

        if (animal.AnimalLevel < 3)
        {
            Destroy(other.gameObject);

            transform.localScale *= 1.25f;
            animal.AnimalLevel++;
            animal.Hp = 100f;
            animal.Food = 50f;
            animal.Water = 50f;
            animal.Exp = 0;

            GameObject NewObj = Instantiate(GameManager.instance.Effect, transform.position + transform.up * 2f, Quaternion.identity);
            Destroy(NewObj, 3f);
            GameManager.instance.AudioManager.SFX[0].Play(); // 머지 발동 효과음.

            animal.Change(AnimalState.대기);

            Merge = false;
        }
        else
        {
            if (animal.Rating == 5) return;
            GameObject NewObj = Instantiate(GameManager.instance.Effect, transform.position + transform.up * 2f, Quaternion.identity);
            Destroy(NewObj, 3f);
            GameManager.instance.UIManager.OpenAnimalRating(animal.Rating + 1, transform.position);
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameManager.instance.AudioManager.SFX[0].Play(); // 머지 발동
        }
    }
}