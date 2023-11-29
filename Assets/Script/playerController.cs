using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    Vector3 inputDir;
    [SerializeField]
    float playerSpeed = 5;
    float rotationSpeed;
    [SerializeField]
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        Debug.Log(inputDir);
    }

    private void FixedUpdate()
    {
        float neededRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        float playerAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,neededRotation,ref rotationSpeed,0.05f);
        transform.rotation = Quaternion.Euler(0,playerAngle,0);
        Vector3 moveDir = Quaternion.Euler(0,neededRotation,0) * Vector3.forward;
        moveDir = moveDir.normalized;
        rb.MovePosition(transform.position + (moveDir * playerSpeed * Time.deltaTime));
        rb.velocity = Vector3.zero;

    }
}
