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
    [SerializeField] private GameObject LimiteGauche;
    [SerializeField] private GameObject LimiteDroite;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    void Start()
    {
        /*leftLimitMask = LayerMask.GetMask("LeftLimit");
        rightLimitMask = LayerMask.GetMask("RightLimit");*/
        rb = GetComponent<Rigidbody2D>();
        currentPoint = LimiteDroite.transform;
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
}