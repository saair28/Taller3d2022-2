using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{
    PlayerView View;
    PlayerModel playerModel;
    Vector3 moveInput = Vector3.zero;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        playerModel = GetComponent<PlayerModel>();
        View = GetComponent<PlayerView>();
    }
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    public void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * playerModel.runSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * playerModel.walkSpeed;
            }
            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(playerModel.jumpHeight * -2f * playerModel.gravityScale);
            }
        }

        moveInput.y += playerModel.gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            View.changelife(-1);
        }
        if (collision.gameObject.CompareTag("Botella"))
        {
            View.changelife(+1);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            View.changelife(-1);
            Destroy(collision.gameObject);
        }
    }
}
