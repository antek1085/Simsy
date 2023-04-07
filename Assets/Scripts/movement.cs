using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.AI;

public class movement : MonoBehaviour
{
    [SerializeField] float hungerLoss = 0.5f;
    [SerializeField] float sleepLoss = 0.5f;
    [SerializeField] float moneyLoss = 0.5f;
    [SerializeField] float funLoss = 0.5f;
    [SerializeField] float hygieneLoss = 0.5f;

    private float hungerLossStop = 0.001f;
    private float funLossStop = 0.001f;
    private float energyLossStop = 0.001f;
    private float hygieneLossStop = 0.001f;

    private float hungerLossDefault = 0.4f;
    private float funLossDefault = 0.2f;
    private float energyLossDefault = 0.5f;

    public float hunger = 100f;
    public float sleep = 100f;
    public float money = 100f;
    public float fun = 100f;
    public float hygiene = 100f;

    public TextMeshProUGUI hunger_text;
    public TextMeshProUGUI sleep_text;
    public TextMeshProUGUI fun_text;
    public TextMeshProUGUI hygiene_text;

    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;
    public float idleDistance = 1f;

    public GameObject tarIdle;

    public RuntimeAnimatorController test;
    public RuntimeAnimatorController orc;
    public RuntimeAnimatorController walking;
    public RuntimeAnimatorController animIdle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Loss();
        // Hud();
        MinMax();

        if (hunger < 70f)
        {
            HungerAnim();
        }
        if ()
    }

    void Loss()
    {
        hunger -= Time.deltaTime * hungerLoss;
        sleep -= Time.deltaTime * sleepLoss;
        money -= Time.deltaTime * moneyLoss;
        fun -= Time.deltaTime * funLoss;
        hygiene -= Time.deltaTime * hygieneLoss;
    }

    void Hud()
    {
        hunger_text.text = Mathf.Round(hunger).ToString();
        sleep_text.text = Mathf.Round(sleep).ToString();
        money_text.text = Mathf.Round(money).ToString();
        fun_text.text = Mathf.Round(fun).ToString();
        hygiene_text.text = Mathf.Round(hygiene).ToString();
    }

    void HungerAdd()
    {
        hunger = 100f;
        GoToIdle();
    }

    void MinMax()
    {
        hunger = Mathf.Clamp(hunger, 0, 100);
        sleep = Mathf.Clamp(sleep, 0, 100);
        money = Mathf.Clamp(money, 0, 100);
        fun = Mathf.Clamp(fun, 0, 100);
        hygiene = Mathf.Clamp(hygiene, 0, 100);
    }

    public void GoToIdle()
    {
        agent.SetDestination(tarIdle.transform.position); //start
        sleepLoss = energyLossDefault;
        hungerLoss = hungerLossDefault;
        funLoss = funLossDefault;
        if (Vector3.Distance(transform.position, tarIdle.transform.position) > idleDistance)
            animator.runtimeAnimatorController = walking as RuntimeAnimatorController; //start walking animation
        else
        {
            animator.runtimeAnimatorController = animIdle as RuntimeAnimatorController; //animation 
        }
        
    }

    void HungerAnim()
    {
        agent.SetDestination(target.transform.position);
        //Invoke("HungerAdd",20f);
        if (Vector3.Distance(transform.position, target.transform.position) > idleDistance)
        {
            animator.runtimeAnimatorController = test as RuntimeAnimatorController; //walk to object
        }
        else
        {
            animator.runtimeAnimatorController = orc as RuntimeAnimatorController; //animation that will add Hunger
            hungerLoss = hungerLossStop;
            funLoss = funLossStop;
            sleepLoss = energyLossStop;
            hygieneLoss = hygieneLossStop;
            Invoke("HungerAdd", 20f);
        }
    }
}
