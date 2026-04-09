using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Entity
{
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
}