using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int foodCount;
    public int waterCount;
    public int allFoodCount;
    public int allWaterCount;
    public int allHpCount;
    public int ThunderCount;

    public GameObject[] itemImage;
    public Text[] itemText;

    private void Update()
    {
        itemImage[0].SetActive(foodCount > 0);
        itemImage[1].SetActive(waterCount > 0);
        itemImage[2].SetActive(allFoodCount > 0);
        itemImage[3].SetActive(allWaterCount > 0);
        itemImage[4].SetActive(allHpCount > 0);
        itemImage[5].SetActive(ThunderCount > 0);

        itemText[0].text = "먹이\nx " + foodCount;
        itemText[1].text = "물\nx " + waterCount;
        itemText[2].text = "전체 포만도\nx " + allFoodCount;
        itemText[3].text = "전체 수분\nx " + allWaterCount;
        itemText[4].text = "전체 체력\nx " + allHpCount;
        itemText[5].text = "천둥 방어\nx " + ThunderCount;
    }

    public bool AddItem(ItemType item)
    {
        switch (item)
        {
            case ItemType.foodItem:
                if (foodCount >= 30) return false;
                foodCount++;
                break;
            case ItemType.waterItem:
                if (waterCount >= 30) return false;
                waterCount++;
                break;
            case ItemType.allfoodItem:
                if (allFoodCount >= 30) return false;
                allFoodCount++;
                break;
            case ItemType.allwaterItem:
                if (allWaterCount >= 30) return false;
                allWaterCount++;
                break;
            case ItemType.allHpItem:
                if (allHpCount >= 30) return false;
                allHpCount++;
                break;
            case ItemType.ThunderItem:
                if (ThunderCount >= 30) return false;
                ThunderCount++;
                break;
        }
        return true;
    }

    public void SetItem(int index)
    {
        switch (index)
        {
            case 1:
                foodCount--;
                GameManager.instance.FSMManager.AddButton(4);
                break;
            case 2:
                waterCount--;
                GameManager.instance.FSMManager.AddButton(5);
                break;
            case 3:
                allFoodCount--;
                StartCoroutine(AllAuto(ItemType.allfoodItem));
                break;
            case 4:
                allWaterCount--;
                StartCoroutine(AllAuto(ItemType.allwaterItem));
                break;
            case 5:
                allHpCount--;
                StartCoroutine(AllAuto(ItemType.allHpItem));
                break;
            case 6:
                ThunderCount--;
                break;
        }
    }

    public IEnumerator AllAuto(ItemType item)
    {
        // 60초 반복 실행
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(1f);
            var animals = FindObjectsByType<Animal>(FindObjectsSortMode.None);

            foreach (var an in animals)
            {
                switch (item)
                {
                    case ItemType.allfoodItem:
                        an.Animalfood = Mathf.Clamp(an.Animalfood + 1f, 0, 100f);
                        break;
                    case ItemType.allwaterItem:
                        an.Animalwater = Mathf.Clamp(an.Animalwater+ 1f, 0, 100f);
                        break;
                    case ItemType.allHpItem:
                        an.AnimalHp = Mathf.Clamp(an.AnimalHp+ 1f, 0, 100f);
                        break;
                }
            }
        }
    }
}
