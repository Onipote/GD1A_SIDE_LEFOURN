using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsCharacter : MonoBehaviour
{
    //Contrôles
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode leftKey1 = KeyCode.LeftArrow;

    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode rightKey1 = KeyCode.RightArrow;

    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode jumpKeyController = KeyCode.Joystick1Button0;

    //Autres
    [SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private BoxCollider2D boxCharacter;
    private bool isTouchingLayers;
    private int characterMask;
    private int groundMask;
    private int waterMask;
    [SerializeField] private LayerMask Traps;

    /*//Barre de vie (pas fini)
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    private HealthBar healthBar;*/

    Vector3 startingPosition;

    void Start()
    {
        startingPosition = gameObject.transform.position;
        characterMask = LayerMask.GetMask("Character");
        groundMask = LayerMask.GetMask("Ground");
        waterMask = LayerMask.GetMask("Water");

        /*currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);*/
    }
    void Update() // Update is called once per frame
    {
        //Déplacements de base (droite, gauche, saut)
        if (Input.GetKey(leftKey) || Input.GetKey(leftKey1) || Input.GetAxis("Horizontal")<0)
        {
            rgbd.AddForce(Vector2.left * 2);
        }
        if (Input.GetKey(rightKey) || Input.GetKey(rightKey1) || Input.GetAxis("Horizontal")>0)
        {
            rgbd.AddForce(Vector2.right * 2);
        }
        if ((boxCharacter.IsTouchingLayers(groundMask)|| (boxCharacter.IsTouchingLayers(waterMask))) && (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKeyController)))
        {
            rgbd.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
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
            Debug.Log("touche le sol");
        }
        //Obstacle causant la mort du joueur
        if (boxCharacter.IsTouchingLayers(Traps))
        {
            rgbd.transform.position = startingPosition;
        }
        /*//Test TakeDamage
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(25);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.IsTouchingLayers(characterMask))
        {
            rgbd.velocity = new Vector2(0, 0);
            Debug.Log("ralentissement");
        }
    }
    /*void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }*/
}