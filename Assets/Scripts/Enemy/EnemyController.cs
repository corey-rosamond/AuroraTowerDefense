using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    // The castle we are going to attack.
    public Castle castle;

    // The NavMeshAgent we will use to move around the map.
    private NavMeshAgent navMeshAgent;
    public Animator animator;

    public Slider healthBar;

    // These are the values associated with health and health Regen.
    public float healthCurrent;
    public float healthMaximum;
    public float healthRegenAmount;
    public float healthRegenRate;
    public float healthRegenTimeUntil;

    // These are the values associated with attacking.
    public float attackDamage;
    public float attackRate;
    public float attackRange;
    public float attackTimeUntil;

    // This is the monetary value you will get for killing this unit.
    public int bounty = 10;

    

    private bool isWalking = false;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        SetHealth(healthMaximum);
        healthBar.maxValue = healthMaximum;
        healthBar.value = healthCurrent;


        attackTimeUntil = attackRate;
        healthRegenTimeUntil = healthRegenRate;

        // Set our destination to the target castle.
        NavMeshHit hit;
        NavMesh.SamplePosition(castle.transform.position, out hit, 10f, NavMesh.AllAreas);
        navMeshAgent.SetDestination(hit.position);
        isWalking = true;
        animator.SetBool("Walk", isWalking);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.rotation = Camera.main.transform.rotation;
        if (LevelManager.IsGameInProgress())
        {
           
            ProcessRegen();

            if (isWalking) 
            {
                ProcessWalk();
            
            } else if(isAttacking)
            {
                ProcessAttack();
            }
        }
    }

    private void OnDeath()
    {
        // Pay the player for killing it.
        GoldManager.Give(bounty);
        

        Destroy(gameObject);
    }

    public void ApplyDamage(float amount)
    {
        animator.SetBool("Damage", true);
        if(!SetHealth(healthCurrent - amount))
        {
            animator.SetBool("Death", true);
            OnDeath();
        }
    }

    private bool SetHealth(float value)
    {
        if(value <= 0)
        {
            healthCurrent = 0;
            healthBar.value = healthCurrent;
            return false;
        }
        healthCurrent = value;
        if(healthCurrent > healthMaximum)
        {
            healthCurrent = healthMaximum;
        }
        healthBar.value = healthCurrent;
        return true;
    }

    private void ProcessRegen()
    {
        healthRegenTimeUntil -= Time.deltaTime;
        if(healthMaximum != healthCurrent && healthRegenTimeUntil <= 0)
        {
            SetHealth(healthCurrent + healthRegenAmount);
            healthRegenTimeUntil = healthRegenRate;
        }
    }

    private void ProcessWalk()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                isWalking = false;
                animator.SetBool("Walk", isWalking);
                isAttacking = true;
            }
        }
    }

    private void ProcessAttack()
    {
        if(attackTimeUntil > 0)
        {
            attackTimeUntil -= Time.deltaTime;
        }
        if(attackTimeUntil <= 0)
        {
            if (castle != null)
            {
                float distanceToCastle = Vector3.Distance(castle.transform.position, transform.position);
                if (distanceToCastle <= attackRange)
                {
                    animator.SetBool("Attack", true);
                    attackTimeUntil = attackRate;
                    castle.ApplyDamage(attackDamage);
                }
                else
                {
                    Debug.Log("Castle not in range.");
                }
            } else
            {
                //Debug.Log("Castle Dead");
            }
        }
    }

    public void OnVictory()
    {
        animator.SetBool("Victory", true);
    }

    public void Setup(Castle targetCastle)
    {
        castle = targetCastle;
    }
}
