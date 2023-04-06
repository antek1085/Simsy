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

    public float hunger = 100f;
    public float sleep = 100f;
    public float money = 100f;
    public float fun = 100f;
    public float hygiene = 100f;
    
    public TextMeshProUGUI hunger_text;
    public TextMeshProUGUI sleep_text;
    public TextMeshProUGUI money_text;
    public TextMeshProUGUI fun_text;
    public TextMeshProUGUI hygiene_text;

    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;
    public float idleDistance = 1f;

    public RuntimeAnimatorController test;
    public RuntimeAnimatorController orc;

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
            agent.SetDestination(target.transform.position);
            //Invoke("HungerAdd",20f);
            if (Vector3.Distance(transform.position, target.transform.position) > idleDistance)
            {
                animator.runtimeAnimatorController = test as RuntimeAnimatorController;//walk to object
            }
            else
            {
                animator.runtimeAnimatorController = orc as RuntimeAnimatorController;//animation that will add Hunger
                Invoke("HungerAdd",20f);
            }
        }
        else if (hunger > 70f)
        {
            animator.runtimeAnimatorController = orc as RuntimeAnimatorController; // back to idle
        }
        
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
    }

    void MinMax()
    {
        hunger = Mathf.Clamp(hunger, 0, 100);
        sleep = Mathf.Clamp(sleep, 0, 100);
        money = Mathf.Clamp(money, 0, 100);
        fun = Mathf.Clamp(fun, 0, 100);
        hygiene = Mathf.Clamp(hygiene, 0, 100);
    }
    
    
    
    
}
