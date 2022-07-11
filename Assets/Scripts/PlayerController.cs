using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    public float jumpForce;
    public float jumpForce2;

    public float dashValue;
    public float dashTime;
    private float dashTimeCounter;

    //public Rigidbody theRB;
    public CharacterController controller;

    private Vector3 moveDirection;
    private Vector3 dashDirection;

    public float gravityScale;

    public float jumpCoolDown;
    private float jumpCoolDownCount;

    public Animator anim;

    public Transform pivot;

    public float rotateSpeed;

    public float waitTime;

    public GameObject playerModel;

    public float knockBackForce;
    public float knockBackTime;
    public float stunTime;
    private float knockBackCounter;

    private bool canDJump;
    private bool canDash;

    public Vector3 lastGroundPosition;
    public Vector3 lastMovement;

    private bool isGrounded;
    private bool isDashing;
    private bool isWalking;
    private bool isStunned;
    private bool hasDb;


    // Start is called before the first frame update
    void Start()
    {  
        //theRB = GetComponent<Rigidbody>();
          controller = GetComponent<CharacterController>();
          canDJump = true;
          canDash = true;
          dashTimeCounter = 0;
          lastGroundPosition = transform.position;
          lastMovement = transform.forward * moveSpeed;
          isGrounded = true;
          isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        waitTime = waitTime + Time.deltaTime;

        //theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed,  moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
       

        if(knockBackCounter <= 0){
            isStunned = false;

            float yPreNormalizacao = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            if(moveDirection.x != 0 || moveDirection.z !=0) lastMovement = moveDirection;
            moveDirection.y = yPreNormalizacao;

            if(Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")) > 0) waitTime = 0;

            
            if(jumpCoolDownCount > 0)jumpCoolDownCount = jumpCoolDownCount - Time.deltaTime;

            if(controller.isGrounded){
                isGrounded = true;
                canDJump = true;
                canDash = true;
                moveDirection.y = 0;
                lastGroundPosition = transform.position;
                if(Input.GetButtonDown("Jump")){
                    //theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
                    moveDirection.y = jumpForce;
                    waitTime = 0;
                    jumpCoolDownCount = jumpCoolDown;
                    isGrounded = false;
                }
            }
            else if(Input.GetButtonDown("Jump") && canDJump && jumpCoolDownCount <= 0){
                canDJump = false;
                moveDirection.y = jumpForce2;
            }
        }
        else knockBackCounter = knockBackCounter - Time.deltaTime;

        moveDirection.y =  moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        if(Input.GetButtonDown("Fire1") && canDash){
            moveDirection.y = 0;
            if (moveDirection.x != 0 || moveDirection.z != 0){
                dashDirection = moveDirection;
            }
            else dashDirection = lastMovement;
            dashDirection.y = 0;
            canDash = false;
            dashTimeCounter = dashTime;
        }
        
        
        if(dashTimeCounter > 0){
            moveDirection.y = 0;
            moveDirection = dashDirection*dashValue;
            dashTimeCounter = dashTimeCounter - Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);

         if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
             transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
             Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
             playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
         }

        isDashing = dashTimeCounter > 0;
        hasDb = !canDJump;
        isWalking = (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))) != 0;


        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isStunned", isStunned);
        anim.SetBool("hasDb", hasDb);
        
    }


    public void Knockback(Vector3 direction){
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;

        isStunned = true;
        
    }

    public void Stun(){
        knockBackCounter = stunTime;
        moveDirection = new Vector3(0f,0f,0f);
    }
}
