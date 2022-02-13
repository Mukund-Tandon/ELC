using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
  [SerializeField] private float movespeed;
  [SerializeField] private float walkspeed;
  [SerializeField] private float runspeed;
  [SerializeField] private bool isGrounded;
  [SerializeField] private float groundCheckDistance;
  [SerializeField] private LayerMask groundmask;
  [SerializeField] private float gravity;
  private Vector3 moveDirection;
  private Vector3 vilocity;
  private CharacterController controller;

  private void Start()
  {
    controller = GetComponent<CharacterController>();
  }

  private void Update()
  {
    Movement();
  }

  private void Movement()
  {
    isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundmask);
    if(isGrounded && vilocity.y <0)
    {
      vilocity.y = -2f;
    }

    float moveZ = Input.GetAxis("Vertical");
    moveDirection = new Vector3(0,0,moveZ);
    bool ans = moveDirection != Vector3.zero;

    if(isGrounded)
    {
      if(ans && !Input.GetKey(KeyCode.LeftShift))
      {
        Walk();
      }
      else if(ans && Input.GetKey(KeyCode.LeftShift))
      {
        Run();
      }
      else if(moveDirection == Vector3.zero)
      {
        Idle();
      }
      moveDirection *=movespeed;
    }


    controller.Move(moveDirection*Time.deltaTime);
    vilocity.y += gravity*Time.deltaTime;
    controller.Move(vilocity*Time.deltaTime);
  }
  private void Idle()
  {

  }
  private void Walk()
  {
    movespeed = walkspeed;
  }
  private void Run()
  {
    movespeed = runspeed;
  }
}
