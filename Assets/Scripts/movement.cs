using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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

    private Animator animator;

    public RuntimeAnimatorController test;
    public RuntimeAnimatorController orc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Loss();
        Hud();

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Your Hunger level is " + hunger);
            Debug.Log("Your Sleep level is " + sleep);
            Debug.Log("Your Money level is " + money);
            Debug.Log("Your Fun level is " + fun);
            Debug.Log("Your Hygiene level is " + hygiene);
            
        }

        if (hunger < 70f)
        {
            Invoke("HungerAdd",20f);
            animator.runtimeAnimatorController = test as RuntimeAnimatorController;
        }
        else if (hunger > 70f)
        {
            animator.runtimeAnimatorController = orc as RuntimeAnimatorController;
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
    
    
    
    
}
