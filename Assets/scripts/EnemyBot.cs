using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBot : Entity
{
    [Header("Настройки ИИ (Курочки)")]
    public Transform targetCrystal; 
    public float attackRange = 2.5f; 
    public float damage = 10f;       
    public float attackCooldown = 1.5f; 

    private NavMeshAgent agent;
    private float lastAttackTime;

    private Animator animator;

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
    }

    private void Update()
    {

        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);

            Debug.Log(agent.velocity.magnitude);

        }

        if (targetCrystal == null || !targetCrystal.gameObject.activeInHierarchy)
        {
            agent.isStopped = true;
            return;
        }

        agent.SetDestination(targetCrystal.position);

        float distanceToTarget = Vector3.Distance(transform.position, targetCrystal.position);

        if (distanceToTarget <= attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Курочка клюет ведро!");
            if (animator != null)
            {
                Debug.Log("attackAnim");

                animator.SetTrigger("Attack"); 
            }

            Crystal crystalComponent = targetCrystal.GetComponent<Crystal>();
            if (crystalComponent != null)
            {
                crystalComponent.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
        }
    }
}