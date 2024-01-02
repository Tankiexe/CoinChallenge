using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlayerController : MonoBehaviour
{
    public static EndingPlayerController instance;
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
            if (isGrounded) animator.SetFloat("Jumping", 0);
            if (!isGrounded) animator.SetFloat("Jumping", 1);
        }
    }

    [SerializeField]
    float playerSpeed = 5;
    [SerializeField]
    float strafeSpeed;
    float rotationSpeed;
    [SerializeField]
    Camera cam;

    

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

        inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetAxis("Jump") > 0)
        {
            jumpKeyPressed = true;
        }
        

        
    }

    private void FixedUpdate()
    {
        MovePlayer();

        IsGrounded();
        if (jumpKeyPressed) { TryToJump(); }
        UpdateAnimation(inputDir);
    }

    void MovePlayer()
    {
        float vitesseRotation = 0;
        //Forward Dir
        Vector3 _moveDir = transform.forward * inputDir.z;
        _moveDir.Normalize();
        _moveDir *= playerSpeed;

        //Strafe Dir
        Vector3 _strafeDir = Vector3.Cross(Vector3.up, transform.forward) * inputDir.x; // Get the perpandicular from forward
        _strafeDir.Normalize();
        _strafeDir *= strafeSpeed;

        _moveDir += _strafeDir; // Combine to vectors

        rb.MovePosition(transform.position + (_moveDir * Time.deltaTime));

        //Rotate Player toward cam direction
        float _neededRotation = cam.transform.eulerAngles.y;
        float _playerAngleDamp = Mathf.SmoothDampAngle(transform.eulerAngles.y, _neededRotation, ref vitesseRotation, 0.05f);
        transform.rotation = Quaternion.Euler(0f, _playerAngleDamp, 0f);


        

    }

    void UpdateAnimation(Vector3 dir)
    {
        animator.SetFloat("Forward", dir.z);
        animator.SetFloat("SideMove", dir.x);
    }

    void TryToJump()
    {
        jumpKeyPressed = false;
        if (!ISGROUNDED) return;
        animator.SetFloat("Jumping", 1);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    bool IsGrounded()
    {

        RaycastHit hit;
        /*if (ISGROUNDED == false)
        {
            ISGROUNDED = Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, groundMask);
            if (ISGROUNDED == true)
            {
                animator.SetFloat("Jumping", 1f);
            }
        }*/
        return ISGROUNDED = Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, groundMask);


    }
}
