using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator myAnim;
    public bool isAttacking = false;
    public bool isParrying = false;
    public static PlayerController instance;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Parry();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
        }
    }




      private void OnTriggerEnter2D(Collider2D enemy)
        {
            if(isAttacking == true)
            enemy.GetComponent<Health>().TakeDamage(1);
        }

    void Parry()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isParrying)
        {
            isParrying = true;
            myAnim.SetTrigger("doParry");
        }

    }


    }

