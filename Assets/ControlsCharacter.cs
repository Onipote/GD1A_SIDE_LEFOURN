using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsCharacter : MonoBehaviour
{
    [SerializeField] private KeyCode leftKey = KeyCode.A, rightKey = KeyCode.D, jumpKey = KeyCode.Space;
    [SerializeField] private Rigidbody2D rgbd;
    private bool isTouching;
    [SerializeField] private BoxCollider2D boxCharacter;
    private int groundMask;
    void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey))
        {
            rgbd.AddForce(Vector2.left * 5);
        }
        if (Input.GetKey(rightKey))
        {
            rgbd.AddForce(Vector2.right * 5);
        }
        if (boxCharacter.IsTouchingLayers(groundMask) && Input.GetKeyDown(jumpKey))
        {
            rgbd.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
