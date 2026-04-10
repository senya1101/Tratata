using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Entity
{
    [Header("Боевая система")]
    public float attackRange = 2.5f;     
    public float attackDamage = 35f;     
    public float attackCooldown = 0.5f;  
    public GameObject hitEffectPrefab;   

    private float nextAttackTime = 0f;
    [Header("Настройки перемещения")]
    public float moveSpeed = 7f;
    public float turnSpeed = 720f;

    private Rigidbody rb;
    private Vector3 moveInput;

    private InputAction moveAction;

    private void Awake()
    {
        moveAction = new InputAction("Move");

        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    protected override void Start()
    {
        base.Start(); 
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();

        moveInput = new Vector3(inputVector.x, 0f, inputVector.y);
        moveInput.Normalize();

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Time.time >= nextAttackTime)
            {
                PerformAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = moveInput * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = targetVelocity;

        if (moveInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }
    void PerformAttack()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        
        bool hitSomeone = false;

        foreach (var hitCollider in hitColliders)
        {
            
            EnemyBot enemy = hitCollider.GetComponent<EnemyBot>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                
                
                if (hitEffectPrefab != null)
                {
                    Instantiate(hitEffectPrefab, hitCollider.transform.position + Vector3.up, Quaternion.identity);
                }
                
                hitSomeone = true;
            }
        }

        if (hitSomeone)
        {
            Debug.Log("Попал по курице! 💥");
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
protected override void Die()
{
    
    Debug.Log("Игрок погиб!");
    GameManager.Instance.EndGame(); 
}
}