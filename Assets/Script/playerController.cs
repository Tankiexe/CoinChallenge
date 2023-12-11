using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public static playerController instance;
    [SerializeField]
    Rigidbody rb;
    Animator animator;

    Vector3 inputDir;
    Vector3 forward = Vector3.zero;
    bool jumpKeyPressed = false;
    [SerializeField]
    float jumpForce = 10;
    [SerializeField]
    LayerMask groundMask;
    bool isGrounded = true;
    public bool ISGROUNDED
    {
        get { return isGrounded; }
        set
        {
            isGrounded = value;
            animator.SetBool("Jump", false);
            animator.SetBool("Grounded", isGrounded);
        }
    }

    [SerializeField]
    float playerSpeed = 5;
    float rotationSpeed;
    [SerializeField]
    Camera cam;

    public int playerLife = 10;
    [SerializeField]
    bool isDead = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        
        inputDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        if (Input.GetAxis("Jump") > 0)
        {
            jumpKeyPressed = true;
        }
        IsHeDead();
        /*if (inputDir.x != 0)
        {
            forward = Vector3.forward;
        }
        else if (inputDir.z != 0)
        {
            forward = Vector3.forward;
        }*/
    }

    private void FixedUpdate()
    {
        MovePlayer();
        IsGrounded();
        if (jumpKeyPressed ) { TryToJump();}
        
    }

    void MovePlayer()
    {
        
        float neededRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        float playerNeededAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, neededRotation, ref rotationSpeed, 0.05f);
        transform.rotation = Quaternion.Euler(0, playerNeededAngle, 0);
        
        if (inputDir == Vector3.zero)
        {
            animator.SetBool("Backward", false);
            animator.SetBool("Running", false);
            return;
        }

        Vector3 moveDir = Quaternion.Euler(0, neededRotation, 0) * inputDir;
        moveDir = moveDir.normalized;
        if (inputDir.z < 0)
        {
            animator.SetBool("Backward", true);
            transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
        }
        if (inputDir.z > 0)
        {
            animator.SetBool("Running", true);
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }
        //rb.MovePosition(transform.position + (moveDir * playerSpeed * Time.deltaTime));
        
        
    }

    void TryToJump()
    {
        jumpKeyPressed = false;
        if (!ISGROUNDED) return;
        animator.SetBool("Jump", true);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
    }

    bool IsGrounded()
    {
        if (rb.velocity.y != 0) return ISGROUNDED = false;
        RaycastHit hit;
        return ISGROUNDED = Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, groundMask);

        
    }

    private void IsHeDead()
    {
        if (transform.position.y < -20) isDead = true;
        if (playerLife <= 0) isDead = true;
        if (isDead == true)
        {
            GameManager.instance.respawnNeeded = true;
            Destroy(gameObject);
        }
    }

}
