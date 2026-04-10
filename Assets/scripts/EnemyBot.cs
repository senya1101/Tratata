using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBot : Entity
{
    [Header("Настройки ИИ (Курочки)")]
    public Transform targetCrystal; 
    public Transform targetPlayer; 
    public float aggroRadius = 5f; 

    public float attackRange = 2.5f; 
    public float damage = 10f;       
    public float attackCooldown = 1.5f; 

    private NavMeshAgent agent;
    private float lastAttackTime;
    private Animator animator;

    private Transform currentTarget; 

    protected override void Start()
    {
        base.Start(); 

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        
        if (targetCrystal == null)
        {
            GameObject crystalObj = GameObject.FindGameObjectWithTag("Crystal");
            if (crystalObj != null) targetCrystal = crystalObj.transform;
        }

        
        if (targetPlayer == null)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null) targetPlayer = player.transform;
        }
    }

    private void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        
        if (targetCrystal == null && targetPlayer == null)
        {
            agent.isStopped = true;
            return;
        }

        
        DetermineTarget();

        
        if (currentTarget != null && currentTarget.gameObject.activeInHierarchy)
        {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);

            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= attackRange)
            {
                Attack(currentTarget);
            }
        }
        else
        {
            agent.isStopped = true; 
        }
    }

    private void DetermineTarget()
    {
        
        currentTarget = targetCrystal;

        
        if (targetPlayer != null && targetPlayer.gameObject.activeInHierarchy)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
            
            if (distanceToPlayer <= aggroRadius)
            {
                currentTarget = targetPlayer;
            }
        }
    }

    private void Attack(Transform target)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack"); 
            }

            Entity targetEntity = target.GetComponent<Entity>();
            if (targetEntity != null)
            {
                targetEntity.TakeDamage(damage);
            }
            else 
            {
                Crystal crystalComponent = target.GetComponent<Crystal>();
                if (crystalComponent != null)
                {
                    crystalComponent.TakeDamage(damage);
                }
            }

            lastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}