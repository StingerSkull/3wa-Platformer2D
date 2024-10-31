using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public int lifePoints;
    public Rigidbody2D rb2d;
    public float moveSpeed = 1f;
    public float range = 10f;
    

    private GameObject player;
    private bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerV2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= range)
        {
            Vector2 angleVector = (player.transform.position - transform.position);
            rb2d.velocity = new Vector2(angleVector.normalized.x * moveSpeed, rb2d.velocity.y);
            if (rb2d.velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (rb2d.velocity.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SwordSlash"))
        {
            lifePoints--;
            if (lifePoints <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("SwordStab"))
        {
            lifePoints-=2;
            if (lifePoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
