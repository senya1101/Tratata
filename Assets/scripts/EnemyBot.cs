using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))] // Гарантирует, что на курочке точно будет AudioSource
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

    [Header("Визуальные эффекты")]
    public GameObject hitParticlesPrefab; // Префаб системы частиц при попадании

    [Header("Звуки")]
    public AudioClip hitSound; // Звук получения урона
    private AudioSource audioSource; // Ссылка на наш источник звука

    protected override void Start()
    {
        base.Start(); 

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
        // Находим AudioSource на самой курочке
        audioSource = GetComponent<AudioSource>();

        // Если кристалл не назначен в инспекторе, ищем его по тегу
        if (targetCrystal == null)
        {
            GameObject crystalObj = GameObject.FindGameObjectWithTag("Crystal");
            if (crystalObj != null) targetCrystal = crystalObj.transform;
        }

        // Если игрок не назначен, ищем его по скрипту PlayerController
        if (targetPlayer == null)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null) targetPlayer = player.transform;
        }
    }

    private void Update()
    {
        // Передача скорости в аниматор для переключения между Idle и Walk
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
        // По умолчанию цель - кристалл
        currentTarget = targetCrystal;

        // Если игрок рядом, переключаем агрессию на него
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

            // Наносим урон цели
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

    // Этот метод вызывается, когда курочка получает урон
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount); // Вычитает здоровье из скрипта Entity

        // 1. Воспроизведение звука с помощью PlayOneShot (чистый звук без обрываний)
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // 2. Создание системы частиц (визуального эффекта)
        if (hitParticlesPrefab != null)
        {
            GameObject particles = Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 2f); // Уничтожаем частицы через 2 секунды
        }
    }
}