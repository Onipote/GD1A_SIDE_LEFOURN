using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsCharacter : MonoBehaviour
{
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode leftKey1 = KeyCode.LeftArrow;

    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode rightKey1 = KeyCode.RightArrow;

    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode jumpKeyController = KeyCode.Joystick1Button0;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth = 100;

    [SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private BoxCollider2D boxCharacter;
    private bool isTouching;
    private int groundMask;
    void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        currentHealth = maxHealth;
    }
    void Update() // Update is called once per frame
    {
        if (Input.GetKey(leftKey) || Input.GetKey(leftKey1) || Input.GetAxis("Horizontal")<0)
        {
            rgbd.AddForce(Vector2.left * 5);
        }
        if (Input.GetKey(rightKey) || Input.GetKey(rightKey1) || Input.GetAxis("Horizontal")>0)
        {
            rgbd.AddForce(Vector2.right * 5);
        }
        if (boxCharacter.IsTouchingLayers(groundMask) && (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKeyController)))
        {
            rgbd.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
