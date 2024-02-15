using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsCharacter : MonoBehaviour
{
    [Header("Contrôles Joueur")]
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode leftKey1 = KeyCode.LeftArrow;

    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode rightKey1 = KeyCode.RightArrow;

    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode jumpKeyController = KeyCode.Joystick1Button0;

    [Header("Colliders")]
    [SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private BoxCollider2D boxCharacter;
    [SerializeField] private BoxCollider2D boxCharacterWall;

    [Header("Layers")]
    private bool isTouchingLayers;
    private int characterMask;
    private int groundMask;
    private int waterMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private LayerMask Traps;
    [SerializeField] private LayerMask Enemy1;

    [Header("Enemy1's link")]
    [SerializeField] private BoxCollider2D boxEnemy1;
    [SerializeField] PlayerHealth hpSystem;

    Vector3 startingPosition;
    void Start()
    {
        startingPosition = gameObject.transform.position;
        characterMask = LayerMask.GetMask("Character");
        groundMask = LayerMask.GetMask("Ground");
        waterMask = LayerMask.GetMask("Water");
    }
    void Update() // Update is called once per frame
    {
        //Déplacements de base (droite, gauche, saut)
        if (Input.GetKey(leftKey) || Input.GetKey(leftKey1) || Input.GetAxis("Horizontal")<0)
        {
            rgbd.AddForce(Vector2.left * 1);
        }
        if (Input.GetKey(rightKey) || Input.GetKey(rightKey1) || Input.GetAxis("Horizontal")>0)
        {
            rgbd.AddForce(Vector2.right * 1);
        }
        if ((boxCharacter.IsTouchingLayers(groundMask)|| (boxCharacter.IsTouchingLayers(waterMask))) && (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKeyController)))
        {
            rgbd.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }

        //Wall jump
        if (boxCharacterWall.IsTouchingLayers(wallMask) && (Input.GetKeyDown(jumpKey) && (Input.GetKey(leftKey)) || (Input.GetKeyDown(jumpKeyController) && (Input.GetKey(leftKey1) || Input.GetKey(rightKey1)))))
        {
            rgbd.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            Debug.Log("wall jump");
        }
            //Altération déplacement (résistance de l'eau)
            if (boxCharacter.IsTouchingLayers(waterMask))
        {
            rgbd.mass = 2;
            Debug.Log("touche l'eau");
        }
        if (boxCharacter.IsTouchingLayers(groundMask) && !(boxCharacter.IsTouchingLayers(waterMask)))
        {
            rgbd.mass = 1;
            //Debug.Log("touche le sol");
        }

        //Obstacle causant la mort du joueur
        if (boxCharacter.IsTouchingLayers(Traps))
        {
            rgbd.transform.position = startingPosition;
        }
    }
    /*PlayerHealth()
    {
        if (boxCharacter.IsTouchingLayers(Enemy1))
        {
             health -= (Random.Range(5, 10);
        }
    }*/
}