using UnityEngine;
using UnityEngine.SceneManagement; // Needed for restarting the scene

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private EndZone endZone;

    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float jumpHeight = 2f; 
    [SerializeField] private float gravity = 9.81f; 
    [SerializeField] private float fallMultiplier = 2.5f; 
    [SerializeField] private float lowJumpMultiplier = 2f; 

    private Vector3 velocity; 
    private bool isGrounded; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        endZone = FindFirstObjectByType<EndZone>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        // Get movement input
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical"); 
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f) 
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            if (endZone.isLevelComplete == false)
            {
                GameTimer.Instance?.StartTimer();
            }

        }

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        // Apply gravity
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
            velocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    // Detects collision with WallOfDeath
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("WallOfDeath")) 
        {
            Debug.Log("You hit the Wall of Death! Restarting...");
            RestartGame();
        }
    }

    // Restarts the scene
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) // When the player reaches the final goal
        {
            Debug.Log("Goal Reached! Timer Stopped.");
            GameTimer.Instance?.StopTimer();
        }
    }

}
