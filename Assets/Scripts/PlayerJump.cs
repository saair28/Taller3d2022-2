using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerMovement pMovement;
    [Header("Jumping")]
    [SerializeField] float jumpForce = 5f;
    bool canJump => (pMovement.isGrounded &&  Input.GetKeyDown(KeyCode.Space));

    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();

    }

    public void Jump()
    {
        if(canJump)
        {
            pMovement.rb.velocity = new Vector3(pMovement.rb.velocity.x, 0, pMovement.rb.velocity.z);
            pMovement.rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
