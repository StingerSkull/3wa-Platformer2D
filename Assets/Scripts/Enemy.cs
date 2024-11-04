using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int lifePoints;
    public Rigidbody2D rb2d;
    public float moveSpeed = 1f;
    public float range = 10f;
    public bool canFly = false;
    public AudioSource audioSound;

    private GameObject player;
    private GameObject gameManager;
    private bool facingRight = false;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, range);
    }
#endif

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerV2");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= range)
        {
            Vector2 angleVector = (player.transform.position - transform.position);
            if (canFly)
            {
                rb2d.velocity = angleVector.normalized * moveSpeed;
            }
            else
            {
                rb2d.velocity = new Vector2(angleVector.normalized.x * moveSpeed, rb2d.velocity.y);
            }
            if (rb2d.velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (rb2d.velocity.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            rb2d.velocity = Vector2.zero;
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
        gameManager.GetComponent<GameManager>().AddScore(600);
        AudioSource.PlayClipAtPoint(audioSound.clip,transform.position);
    }
}
