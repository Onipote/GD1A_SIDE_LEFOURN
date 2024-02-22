using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float walkSpeed;
    private float moveInput;
    public float jumpSpeed;
    private bool isGrounded;

    private bool isTouchingLeft;
    private bool isTouchingRight;
    private bool wallJumping;
    private float touchingLeftOrRight;

    [Header("HP System")]
    //Jauge Points de vie (part 1)
    public float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    private int healMask;

    public BoxCollider2D snack;

    Vector3 startingPosition;
    void Start()
    {
        startingPosition = gameObject.transform.position;
        rgbd.gameObject.GetComponent<Rigidbody2D>();
        characterMask = LayerMask.GetMask("Character");
        groundMask = LayerMask.GetMask("Ground");
        waterMask = LayerMask.GetMask("Water");
        healMask = LayerMask.GetMask("Heal");

        health = maxHealth;
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

        //Obstacle causant la mort du joueur
        if (boxCharacter.IsTouchingLayers(Traps))
        {
            rgbd.transform.position = startingPosition;
        }

        //TEST TUTO WALL JUMP (pas fini)
        moveInput = Input.GetAxisRaw("Horizontal");

        if ((!isTouchingLeft && !isTouchingRight) || isGrounded)
        {
            rgbd.velocity = new Vector2(moveInput * walkSpeed, rgbd.velocity.y);
        }

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpSpeed);
        }

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
        new Vector2(0.9f, 0.2f), 0f, groundMask);

        isTouchingLeft = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.5f),
        new Vector2(0.9f, 0.2f), 0f, groundMask);

        isTouchingRight = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.5f),
        new Vector2(0.9f, 0.2f), 0f, groundMask);

        if (isTouchingLeft)
        {
            touchingLeftOrRight = 1;

        }
        else if (isTouchingRight)
        {
            touchingLeftOrRight = -1;
        }

        if (Input.GetKeyDown(jumpKey) && ((isTouchingRight) || (isTouchingLeft)) && !isGrounded)
        {
            wallJumping = true;
            Invoke("SetJumpingToFalse", 0.08f);
        }

        if (wallJumping)
        {
            rgbd.velocity = new Vector2(walkSpeed * touchingLeftOrRight, jumpSpeed);
        }
        //Wall jump
        /*if (boxCharacterWall.IsTouchingLayers(wallMask) && (Input.GetKeyDown(jumpKey) && (Input.GetKey(leftKey)) || (Input.GetKeyDown(jumpKeyController) && (Input.GetKey(leftKey1) || Input.GetKey(rightKey1)))))
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
        }*/

        //Jauge Points de vie (part 1)
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }

    //Jauge Points de vie (part 2)
    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxCharacter.IsTouchingLayers(3))
        {
            Destroy(snack.gameObject);
        }
        if (boxCharacter.IsTouchingLayers(Enemy1))
        {
            TakeDamage(25);
            Debug.Log("a pris des degats");
        }
        if (boxCharacter.IsTouchingLayers(healMask))
        {
            RestoreHealth(10);
        }

    }
}
    /*PlayerHealth()
    {
        if (boxCharacter.IsTouchingLayers(Enemy1))
        {
             health -= (Random.Range(5, 10);
        }
    }*/