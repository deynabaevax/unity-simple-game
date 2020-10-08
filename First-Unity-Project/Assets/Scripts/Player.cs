using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining = 0;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if space key is pressed down 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal"); // check the Project Settings -> Input Manager for detailed info

    }
    // FixedUpdate is called once every physics update
    private void FixedUpdate()
    {
        //// the position and the redius; the length is checking how manu componets are we hitting the ground with
        //// and check if it's checking the ground so --> the length sould be at least one, otherwise fly
        //// if it's one then I'm colliding with myself, if it's two then I'm touching the ground 
        //if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1) 
        //{
        //    return;
        //}


        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0); // Vector3(x,y,z)

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed == true)
        {
            float jumpPower = 5;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange); // give the player superjump powers 
            jumpKeyWasPressed = false; // it won't fly like a rocket 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) // because Coin is in layer 9 
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }
}
