using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    public bool IsMoving;
    public bool oldMovement;
    public bool newMove;
    public NavMeshAgent agent;
    public bool isOnFloor;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();

        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if ((newMove))
        {
            Move();
            return;
        }
        if (oldMovement)
            OldMovement();
        else
            NavMeshMovement();
    }

    private void OldMovement()
    {
        Vector2 moveDirection = inputHandler.MoveInput;
        rb.velocity = moveDirection * moveSpeed;
        if (rb.velocity.magnitude > 0)
        {
            IsMoving = true;
            SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
        }
        else
        { IsMoving = false; }
    }

    private void NavMeshMovement()
    {
        Vector2 moveDirection = inputHandler.MoveInput;
        Vector2 target = moveDirection + (Vector2)transform.position;
        agent.SetDestination(target);
    }
    private void Move()
    {
        if (!isOnFloor) // Игрок может двигаться только если не в вырезе
        {
            Vector2 moveDirection = inputHandler.MoveInput;
            Vector2 movement = moveDirection * moveSpeed;
            rb.velocity = movement;
        }
        else
        {
            rb.velocity = Vector2.zero;
            IsMoving = false;// Останавливаем движение внутри выреза
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.ToString() == "floor")
        {
            isOnFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.ToString() == "floor")
        {
            isOnFloor = false;
        }
    }
}
