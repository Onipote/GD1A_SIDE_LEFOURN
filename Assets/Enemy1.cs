using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    /*[SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private BoxCollider2D boxEnemy;

    [Header("Layers")]
    private bool isTouchingLayers;
    private int leftLimitMask;
    private int rightLimitMask;*/
    [Header("Enemy1's part")]
    [SerializeField] private GameObject LimiteGauche;
    [SerializeField] private GameObject LimiteDroite;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    private int enemy1Mask;

    [Header("Character's link")]
    [SerializeField] private BoxCollider2D boxCharacter;
    [SerializeField] PlayerHealth hpSystem;
    void Start()
    {
        /*leftLimitMask = LayerMask.GetMask("LeftLimit");
        rightLimitMask = LayerMask.GetMask("RightLimit");*/
        rb = GetComponent<Rigidbody2D>();
        currentPoint = LimiteDroite.transform;

        enemy1Mask = LayerMask.GetMask("Enemy1");
    }

    void Update()
    {
        /*if (boxEnemy.IsTouchingLayers(leftLimitMask))
        {
        
        }
        if (boxEnemy.IsTouchingLayers(rightLimitMask))
        {
            
        }*/
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == LimiteDroite.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == LimiteDroite.transform)
        {
            currentPoint = LimiteGauche.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == LimiteGauche.transform)
        {
            currentPoint = LimiteDroite.transform;
        }
    }
    /*void Attack()
    {
        if (boxCharacter.IsTouchingLayers(enemy1Mask))
        {
            health - 25;
        }
    }*/
}