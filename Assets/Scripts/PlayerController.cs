using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    
    private Rigidbody Rigidbody;
    private PlayerController playerController;
    private bool isGrounded;
    private bool gameRunning;

    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    [SerializeField] private float dash;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashCooldownTime;




    void Start()
    {
        gameRunning = true;
        Rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        Physics.gravity *= gravityModifier;  
        dashCooldown= 0;
    }




    void Update()
    {
        if (Input.GetKey(KeyCode.W) && gameRunning)
        {
            Rigidbody.AddForce(Vector3.forward * velocity , ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S) && gameRunning)
        {
            Rigidbody.AddForce(Vector3.back * velocity , ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A) && gameRunning)
        {
            Rigidbody.AddForce(Vector3.left * velocity , ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D) && gameRunning)
        {
            Rigidbody.AddForce(Vector3.right * velocity , ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded && gameRunning)
        {
            Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && gameRunning && dashCooldown == 0)
        {
            Rigidbody.AddForce(Rigidbody.linearVelocity.normalized * dash, ForceMode.Impulse);
            dashCooldown = dashCooldown + dashCooldownTime;
        }

    }

    private void FixedUpdate()
    {
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 0;
        }
        Debug.Log("Dash Cooldown: " + dashCooldown);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameRunning = false;
            Debug.Log("Game Over!");
        }

        if (collision.gameObject.CompareTag("OutOfBounds"))
        {
          transform.position = new Vector3(0, 0, 0);
            Rigidbody.linearVelocity = Vector3.zero; //not the best but not the worst either maybe the worst but yolooooo

        }
    }


}
