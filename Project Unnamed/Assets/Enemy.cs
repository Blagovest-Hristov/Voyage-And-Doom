using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    bool dead = false;
    public Rigidbody2D rb;
   
    public Transform AggroArea;
    public  float aggroRange = 40f;
    public float aggroHeight = 10f;
    public LayerMask CharacterLayer;
    
    public Transform StrikePoint;
    public float HitDmg = 15f;
    public float nextAttackTime = 0f;
    public float AttackRate = 4f;
    // Update is called once per frame
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {

        
        animator.SetFloat("Speed", Math.Abs(rb.velocity.x));
        Collider2D[] AggroEnemies = Physics2D.OverlapBoxAll(AggroArea.position, new Vector2(aggroRange, aggroHeight),0, CharacterLayer);
        if (AggroEnemies.Length>0)
        {
            GetComponent<EnemyAI>().enabled = true;
        

        }
        else
        { 
            GetComponent<EnemyAI>().enabled = false;
            
        }
        if (Time.time >= nextAttackTime)
        {
            if (Physics2D.OverlapCircleAll(StrikePoint.position, 1f, CharacterLayer).Length > 0)
            {
                Attack();
                nextAttackTime = Time.time + 1f / AttackRate;

            }
        }
    }
    void Attack()
    {
        WaitAttack();
        animator.SetTrigger("Attack");
        foreach (var item in Physics2D.OverlapCircleAll(StrikePoint.position, 1f, CharacterLayer))
        {
            item.GetComponent<Movement>().Damage(HitDmg);
        }
    }

    IEnumerator WaitAttack()
    {
        
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            yield return new WaitForSeconds(0.35f);

            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(AggroArea.position, new Vector2(aggroRange, aggroHeight));
        Gizmos.DrawWireSphere(StrikePoint.position, 1f);
    }

    public void Damage(int damage)
    {
        if (!dead)
        {
            currentHealth -= damage;

            animator.SetTrigger("Damage");
            

            if (currentHealth <= 0 )
            {
                animator.SetTrigger("Dead");
                animator.SetBool("Dead 0", true);
                gameObject.layer = 9;
                dead = true;
                GetComponent<Enemy>().enabled = false;
                GetComponent<EnemyAI>().enabled = false;
                GetComponent<Seeker>().enabled = false;

                
            }
        }
    }
}