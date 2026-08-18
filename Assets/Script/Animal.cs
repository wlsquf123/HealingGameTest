using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum AnimalState
{
    None,
    Move,
    Food,
    Water,
    Rest,
    Idle
}
public class Animal : MonoBehaviour
{
    public AnimalState StateType;

    public string AnName;
    public int AnimalLevel = 1;
    public int Rating;
    public float AnimalExp;
    public float Animalfood = 50f;
    public float Animalwater = 50f;
    public float AnimalHp = 100f;
    public float AnimalSpeed;
    public float currentSpeed;

    [Header("번개 1회성무적")]
    public bool isThunder = false;
    
    [Header("타이머")]
    public float IdleTImer;
    private float FoodTImer = 0f;
    private float WaterAndTreeTimer = 0f;

    [Header("")]
    private Vector3 dir = Vector3.forward;

    [Header("UI")]
    public Text LvText;
    public Image ExpImage;
    public Image FoodImage;
    public Image WaterImage;
    public Image HpImage;
    public Text ExpText;
    public Text FoodText;
    public Text WaterText;
    public Text HpText;

    [Header("애니메이션")]
    public Animator AnimalAnimator;


    private void Update()
    {
        // state (상태 UI)
        LvText.text = AnimalLevel.ToString();
        ExpImage.fillAmount = AnimalExp / 10f;
        FoodImage.fillAmount = Animalfood / 100f;
        WaterImage.fillAmount = Animalwater / 100f;
        HpImage.fillAmount = AnimalHp / 100f;
        ExpText.text = AnimalExp + " / 10";
        FoodText.text = Animalfood + " / 100";
        WaterText.text = Animalwater + " / 100";
        HpText.text = AnimalHp + "/ 100";

        if (Animalwater < 30 && GameManager.instance.FSMManager.WaterList.Count > 0)
        {
            StateType = AnimalState.Water;
        }
        else if (Animalfood < 30 && GameManager.instance.FSMManager.FoodList.Count > 0)
        {
            StateType = AnimalState.Food;
        }
        else if (AnimalHp < 30 && GameManager.instance.FSMManager.TreeList.Count > 0)
        {
            StateType = AnimalState.Rest;
        }


        switch (StateType)
        {
            case AnimalState.Idle:
                IdleState();
                break;
            case AnimalState.Move:
                MoveState();
                break;
            case AnimalState.Food:
                FoodState();
                break;
            case AnimalState.Water:
                WaterState();
                break;
            case AnimalState.Rest:
                TreeState();
                break;
        }

        FoodTImer += Time.deltaTime * 2.4f;
        WaterAndTreeTimer += Time.deltaTime * 2.4f;
        if (FoodTImer >= 60f)
        {
            FoodTImer -= 60f;
            Animalfood = Mathf.Clamp(Animalfood - 10f, 0, 100f);
        }
        if (WaterAndTreeTimer >= 30f)
        {
            WaterAndTreeTimer -= 30f;
            Animalwater = Mathf.Clamp(Animalwater - 10f, 0, 100f);
            if (GameManager.instance.WeatherManager.WeatherType == WeatherState.비)
            {
                AnimalHp = Mathf.Clamp(AnimalHp - 10f, 0, 100f);
            }
            else
            {
                AnimalHp = Mathf.Clamp(AnimalHp - 5f, 0, 100f);
            }
        }
    }

    public void Change(AnimalState State) // 체인지 함수
    {
        StateType = State;

        switch (StateType)
        {
            case AnimalState.Idle:
                IdleTImer = 60f;
                AnimalAnimator.Play("Idle");
                break;
            case AnimalState.Move:
                dir.x = Random.Range(-10f, 10f);
                dir.z = Random.Range(-10f, 10f);
                IdleTImer = 3f;
                AnimalAnimator.Play("Move");
                break;
        }
    }

    public void AddExp(int add)
    {
        AnimalExp = Mathf.Clamp(AnimalExp + add, 0, 10f);
    }

    public void RandomAI()
    {
        int index = Random.Range(0, 2);

        if (index == 0)
        {
            Change(AnimalState.Idle);
        }
        else
        {
            if (Animalfood <= 0 || Animalwater <= 0 || AnimalHp <= 0)
            {
                Change(AnimalState.Idle);
                return;
            }

            Change(AnimalState.Move);
            AddExp(1);
        }
    }

    public void MoveToTarget(List<GameObject> targetList)
    {
        if (targetList == null || targetList.Count == 0)
        {
            RandomAI();
            return;
        }

        GameObject nearestTarget = targetList[0];
        float nearestDistance = 999999999999f;

        // 가장 가까운 타겟 찾기
        foreach (GameObject target in targetList)
        {
            float currentDistance = Vector3.SqrMagnitude(transform.position - target.transform.position);

            if (currentDistance < nearestDistance)
            {
                nearestDistance = currentDistance;
                nearestTarget = target;
            }
        }

        // 타겟을 향해 이동
        AnimalAnimator.Play("Move");
        transform.LookAt(nearestTarget.transform);
        transform.position = Vector3.MoveTowards(transform.position, nearestTarget.transform.position, currentSpeed * Time.deltaTime);
    }

    public void IdleState()
    {
        IdleTImer -= Time.deltaTime * 2.4f;

        if (IdleTImer <= 0)
        {
            RandomAI();
        }
    }

    public void MoveState()
    {

        if (GameManager.instance.WeatherManager.WeatherType == WeatherState.흐림)
        {
            currentSpeed = 1f;
        }
        else
        {
            currentSpeed = AnimalSpeed;
        }
        transform.rotation = Quaternion.LookRotation(dir);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        IdleTImer -= Time.deltaTime * 2.4f;
        if (IdleTImer <= 0)
        {
            RandomAI();
        }
    }

    public void FoodState()
    {
        if (Animalfood >= 30)
        {
            RandomAI();
            return;
        }
        MoveToTarget(GameManager.instance.FSMManager.FoodList);
    }

    public void WaterState()
    {
        if (Animalwater >= 30)
        {
            RandomAI();
            return;
        }
        MoveToTarget(GameManager.instance.FSMManager.WaterList);
    }

    public void TreeState()
    {
        if (AnimalHp >= 30)
        {
            RandomAI();
            return;
        }
        MoveToTarget(GameManager.instance.FSMManager.TreeList);
    }
}
