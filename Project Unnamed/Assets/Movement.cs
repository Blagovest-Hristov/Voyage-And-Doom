using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public CharacterController2D Controller;
    public new Rigidbody2D rigidbody ;
    public Transform attackPoint;
    public float Speed = 40f;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int damage = 20;
    public float nextAttackTime = 0f;
    public float AttackRate = 2f;

    public GameObject popUp;

    public float MaxHealth = 100f;
    public float currentHealth;

    float horizontalMove = 0f;

    public Animator animator;

    public Slider healthbar;

    bool jump = false;
    bool dash = false;
    

    public ParticleSystem runningParticles;
    // Start is called before the first frame update
    private void Start()
    {
        healthbar = GameObject.Find("/Canvas/Health Bar(1)").GetComponent<Slider>();
        popUp = GameObject.Find("/Canvas/PopUp");
        currentHealth = MaxHealth;

        healthbar.GetComponent<SetHealth>().setMaxHealth(MaxHealth);
    }
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Mathf.Abs(horizontalMove) != 0  && Controller.m_Grounded == true)
        {
           runningParticles.Play();
        }
        else
        {
            runningParticles.Stop();
        }
        horizontalMove = Input.GetAxisRaw("Horizontal")*Speed;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
            jump = true;

        }



        if (rigidbody.velocity.y < -0.1)
        {
            animator.SetBool("Falling", true);
            animator.SetBool("Jump", false);
           

        }
        else 
        {
            animator.SetBool("Falling", false);
            jump = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
            animator.SetTrigger("Dash");
        }



        if (Time.time>=nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Attack();
                nextAttackTime = Time.time + 1f / AttackRate;
            }
        }

    }

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        healthbar.GetComponent<SetHealth>().setHealth(currentHealth);

        animator.SetTrigger("Damage");
        if (currentHealth<0)
        {
            currentHealth = 0;
        }
        if (currentHealth<=0)
        {
            popUp.SetActive(true);
           
        }
    }


    public void Attack()
    {
        if (jump == true)
        {
            AirAttack();
        }
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().Damage(damage);
            }
            animator.SetTrigger("Attack");
        
    }

    private void AirAttack()
    {
        
    }

    public void Heal(float healingAmount)
    {
        if (currentHealth+healingAmount>=MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        else
        {
            currentHealth += healingAmount;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump, dash);
        jump = false;
        dash = false ;
        Speed = 80f;
    }
}
