using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float jumpHeight = 2f; 
    [SerializeField] private float gravity = 9.81f; 
    [SerializeField] private float fallMultiplier = 2.5f; // Makes falling faster
    [SerializeField] private float lowJumpMultiplier = 2f; // Makes short jumps feel better

    private Vector3 velocity; 
    private bool isGrounded; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if player is grounded
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Prevents floating issues
        }

        // Get movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        // 🔥 Apply faster gravity when falling
        if (velocity.y < 0)
        {
            velocity.y -= gravity * fallMultiplier * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            velocity.y -= gravity * lowJumpMultiplier * Time.deltaTime;
        }
        else 
        {
            velocity.y -= gravity * Time.deltaTime; // Normal gravity
        }

        controller.Move(velocity * Time.deltaTime);
    }
}