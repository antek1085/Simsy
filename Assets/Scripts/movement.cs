using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.AI;

public class movement : MonoBehaviour
{[Header("Stats")]
    public float hunger = 100f;
    public float sleep = 100f;
    public float fun = 100f;
    public float hygiene = 100f;

    [SerializeField] float hungerLoss = 0.6f;
    [SerializeField] float sleepLoss = 0.2f;
    [SerializeField] float funLoss = 0.5f;
    [SerializeField] float hygieneLoss = 0.4f;

    private float hungerLossStop = 0.001f;
    private float funLossStop = 0.001f;
    private float energyLossStop = 0.001f;
    private float hygieneLossStop = 0.001f;

    private float hungerLossDefault = 0.6f;
    private float funLossDefault = 0.5f;
    private float energyLossDefault = 0.2f;
    private float hygieneLossDefault = 0.4f;

    [SerializeField] private float minHunger = 70f;
    [SerializeField] private float minSleep = 70f;
    [SerializeField] private float minFun = 70f;
    [SerializeField] private float minHygiene = 70f;
    
    [Header("HUD")]
    
    public TextMeshProUGUI hunger_Text_Hud;
    public TextMeshProUGUI sleep_Text_Hud;
    public TextMeshProUGUI fun_Text_Hud;
    public TextMeshProUGUI hygiene_Text_Hud;
    public TextMeshProUGUI dead;
    
    [Header("Targets")]
    
    public GameObject tarIdle;
    public GameObject tarHunger;
    public GameObject tarSleep;
    public GameObject tarFun;
    public GameObject tarHygiene;
    public Transform lookAtSleep;
    public float idleDistance = 1f;
    
    [Header("Animator Controllers")]
    
    public NavMeshAgent agent;
    public Animator animator;

    public RuntimeAnimatorController walking;
    public RuntimeAnimatorController animIdle;
    public RuntimeAnimatorController animHunger;
    public RuntimeAnimatorController animSleep;
    public RuntimeAnimatorController animFun;
    public RuntimeAnimatorController animHygiene;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        dead.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Loss();
        Hud();
        MinMax();

        if (hunger < minHunger)
        {
            HungerAnim();
        }

        if (sleep < minSleep)
        {
            SleepAnim();
        }

        if (fun < minFun)
        {
            FunAnim();
        }

        if (hygiene < minHygiene)
        {
            HygieneAnim();
        }

        if (hunger == 0)
        {
            Destroy(gameObject);
            dead.enabled = true;

        }
    }

    void Loss()
    {
        hunger -= Time.deltaTime * hungerLoss;
        sleep -= Time.deltaTime * sleepLoss;
        fun -= Time.deltaTime * funLoss;
        hygiene -= Time.deltaTime * hygieneLoss;
    }

    void Hud()
    {
        hunger_Text_Hud.text = Mathf.Round(hunger).ToString();
        sleep_Text_Hud.text = Mathf.Round(sleep).ToString();
        fun_Text_Hud.text = Mathf.Round(fun).ToString();
        hygiene_Text_Hud.text = Mathf.Round(hygiene).ToString();
    }

    void HungerAdd()
    {
        hunger = 100f;
        GoToIdle();
    }
    void SleepAdd()
    {
        sleep = 100f;
        GoToIdle();
    }
    void FunAdd()
    {
        fun = 100f;
        GoToIdle();
    }
    void HygieneAdd()
    {
        hygiene = 100f;
        GoToIdle();
    }

    void MinMax()
    {
        hunger = Mathf.Clamp(hunger, 0, 100);
        sleep = Mathf.Clamp(sleep, 0, 100);
        fun = Mathf.Clamp(fun, 0, 100);
        hygiene = Mathf.Clamp(hygiene, 0, 100);
    }

    void GoToIdle()
    {
        agent.SetDestination(tarIdle.transform.position); //start
        sleepLoss = energyLossDefault;
        hungerLoss = hungerLossDefault;
        funLoss = funLossDefault;
        hygieneLoss = hygieneLossDefault;
        if (Vector3.Distance(transform.position, tarIdle.transform.position) > idleDistance)
            animator.runtimeAnimatorController = walking as RuntimeAnimatorController; //start walking animation
        else
        {
            animator.runtimeAnimatorController = animIdle as RuntimeAnimatorController; //animation 
        }
        
    }

    void HungerAnim()
    {
        agent.SetDestination(tarHunger.transform.position);
        //Invoke("HungerAdd",20f);
        if (Vector3.Distance(transform.position, tarHunger.transform.position) > idleDistance)
        {
            animator.runtimeAnimatorController = walking as RuntimeAnimatorController; //walk to object
        }
        else
        {
            animator.runtimeAnimatorController = animHunger as RuntimeAnimatorController; //animation that will add Hunger
            hungerLoss = hungerLossStop;
            funLoss = funLossStop;
            sleepLoss = energyLossStop;
            hygieneLoss = hygieneLossStop;
            Invoke("HungerAdd", 10f);
        }
    }
    void SleepAnim()
    {
        agent.SetDestination(tarSleep.transform.position);
        //Invoke("HungerAdd",20f);
        if (Vector3.Distance(transform.position, tarSleep.transform.position) > idleDistance)
        {
            animator.runtimeAnimatorController = walking as RuntimeAnimatorController; //walk to object
        }
        else
        {
            transform.LookAt(lookAtSleep, Vector3.up);
            animator.runtimeAnimatorController = animSleep as RuntimeAnimatorController; //animation that will add Hunger
            hungerLoss = hungerLossStop;
            funLoss = funLossStop;
            sleepLoss = energyLossStop;
            hygieneLoss = hygieneLossStop;
            Invoke("SleepAdd", 15f);
        }
    }
    void FunAnim()
    {
        agent.SetDestination(tarFun.transform.position);
        //Invoke("HungerAdd",20f);
        if (Vector3.Distance(transform.position, tarFun.transform.position) > idleDistance)
        {
            animator.runtimeAnimatorController = walking as RuntimeAnimatorController; //walk to object
        }
        else
        {
            animator.runtimeAnimatorController = animFun as RuntimeAnimatorController; //animation that will add Hunger
            hungerLoss = hungerLossStop;
            funLoss = funLossStop;
            sleepLoss = energyLossStop;
            hygieneLoss = hygieneLossStop;
            Invoke("FunAdd", 8f);
        }
    }
    void HygieneAnim()
    {
        agent.SetDestination(tarHygiene.transform.position);
        //Invoke("HungerAdd",20f);
        if (Vector3.Distance(transform.position, tarHygiene.transform.position) > idleDistance)
        {
            animator.runtimeAnimatorController = walking as RuntimeAnimatorController; //walk to object
        }
        else
        {
            animator.runtimeAnimatorController = animHygiene as RuntimeAnimatorController; //animation that will add Hunger
            hungerLoss = hungerLossStop;
            funLoss = funLossStop;
            sleepLoss = energyLossStop;
            hygieneLoss = hygieneLossStop;
            Invoke("HygieneAdd", 9f);
        }
    }
}
