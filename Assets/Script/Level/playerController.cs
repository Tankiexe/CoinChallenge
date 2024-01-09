using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int playerMaxLife = 10;
    public int playerLife = 10;
    public bool canTakeDamege = true;
    [SerializeField]
    public bool isDead = false;

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
        if (Input.GetAxis("Jump") > 0 && ISGROUNDED)
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


        /*float neededRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
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
        }*/
        //rb.MovePosition(transform.position + (moveDir * playerSpeed * Time.deltaTime));


    }

    void UpdateAnimation(Vector3 dir)
    {
        animator.SetFloat("Forward", dir.z);
        animator.SetFloat("SideMove", dir.x);
        animator.SetBool("Grounded", ISGROUNDED);
    }

    void TryToJump()
    {
        jumpKeyPressed = false;
        if (!ISGROUNDED) return;
        
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
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

    private void IsHeDead()
    {
        if (transform.position.y < -20) isDead = true;
        if (playerLife <= 0) isDead = true;
        if (isDead == true)
        {
            GameManager.instance.respawnNeeded = true;
            

        }
    }

    public void TakingDamage(int damage)
    {
        playerLife -= damage;
        AudioLevelManeger.Instance.ToPlaySound(AudioLevelManeger.Instance.hit);
        IHM.Instance.UpdateLifeBar();
        /*canTakeDamege = false;
        float index = 0;
        while (index < 2 )
        {
            index += Time.deltaTime;
        }
        canTakeDamege = true;*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTakeDamege) return;
        if (other.gameObject.CompareTag("anemy")) TakingDamage(1);
    }
}
