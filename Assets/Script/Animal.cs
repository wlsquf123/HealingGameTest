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

    private float FoodTImer;
    private float WaterAndTreeTimer;

    [Header("타이머")]
    public float IdleTImer;

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


        switch (StateType)
        {
            case AnimalState.Idle:
                IdleState();
                break;
            case AnimalState.Move:
                MoveState();
                break;
            case AnimalState.Food:

                break;
            case AnimalState.Water:

                break;
            case AnimalState.Rest:

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
            AnimalHp = Mathf.Clamp(AnimalHp - 5f, 0, 100f);
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
        transform.rotation = Quaternion.LookRotation(dir);
        transform.Translate(Vector3.forward * AnimalSpeed * Time.deltaTime);

        IdleTImer -= Time.deltaTime * 2.4f;
        if (IdleTImer <= 0)
        {
            RandomAI();
        }
    }
}
