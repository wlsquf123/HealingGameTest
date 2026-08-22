using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public enum AnimalState
{
    이동,
    배고픔,
    수분,
    휴식,
    대기
}
public class Animal : MonoBehaviour
{
    public AnimalState StateType;

    public string AnName;
    public int AnimalLevel = 1;
    public int Rating;
    public float AnimalExp;
    public float Food = 50f;
    public float Water = 50f;
    public float Hp = 100f;
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
    public GameObject target;

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
        FoodImage.fillAmount = Food / 100f;
        WaterImage.fillAmount = Water / 100f;
        HpImage.fillAmount = Hp / 100f;
        ExpText.text = AnimalExp + " / 10";
        FoodText.text = Food + " / 100";
        WaterText.text = Water + " / 100";
        HpText.text = Hp + "/ 100";

        if (Water < 30 && GameObject.FindGameObjectWithTag("Water") != null)
        {
            StateType = AnimalState.수분;
        }
        else if (Food < 30 && GameObject.FindGameObjectWithTag("Food") != null)
        {
            StateType = AnimalState.배고픔;
        }
        else if (Hp < 30 && GameObject.FindGameObjectWithTag("Tree") != null)
        {
            StateType = AnimalState.휴식;
        }


        switch (StateType)
        {
            case AnimalState.대기:
                IdleState();
                break;
            case AnimalState.이동:
                MoveState();
                break;
            case AnimalState.배고픔:
                FoodState();
                break;
            case AnimalState.수분:
                WaterState();
                break;
            case AnimalState.휴식:
                TreeState();
                break;
        }

        FoodTImer += Time.deltaTime * 2.4f;
        WaterAndTreeTimer += Time.deltaTime * 2.4f;
        if (FoodTImer >= 60f)
        {
            FoodTImer -= 60f;
            Food = Mathf.Clamp(Food - 10f, 0, 100f);
        }
        if (WaterAndTreeTimer >= 30f)
        {
            WaterAndTreeTimer -= 30f;
            Water = Mathf.Clamp(Water - 10f, 0, 100f);
            if (GameManager.instance.WeatherManager.WeatherType == WeatherState.비)
            {
                Hp = Mathf.Clamp(Hp - 10f, 0, 100f);
            }
            else
            {
                Hp = Mathf.Clamp(Hp - 5f, 0, 100f);
            }
        }
    }

    public void Change(AnimalState State) // 체인지 함수
    {
        StateType = State;

        switch (StateType)
        {
            case AnimalState.대기:
                IdleTImer = 60f;
                AnimalAnimator.Play("Idle");
                break;
            case AnimalState.이동:
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

    public void RandomState()
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            if (Food <= 0 || Water <= 0 || Hp <= 0)
            {
                Change(AnimalState.대기);
                return;
            }

            Change(AnimalState.이동);
            AddExp(1);
        }
        else
        {
            Change(AnimalState.대기);
        }
    }

    public void FindTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        target = null;
        float minDir = 999999999f;

        foreach (GameObject obj in objects)
        {
            float dis = Vector3.Distance(transform.position, obj.transform.position); // 내 동물거리랑 오브젝트의 거리를 구함.

            if (dis < minDir)
            {
                minDir = dis;
                target = obj;
            }
        }
        if (target == null) return;

        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime); // 거리 계산 
    }

    public void IdleState()
    {
        IdleTImer -= Time.deltaTime * 2.4f;

        if (IdleTImer <= 0)
        {
            RandomState();
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
            RandomState();
        }
    }

    public void FoodState()
    {
        FindTag("Food");

        if (Food >= 30 || target == null)
        {
            RandomState();
        }
    }

    public void WaterState()
    {
        FindTag("Water");

        if (Water >= 30 || target == null)
        {
            RandomState();
        }
    }

    public void TreeState()
    {
        FindTag("Tree");

        if (Hp >= 30 || target == null)
        {
            RandomState();
        }
    }
}