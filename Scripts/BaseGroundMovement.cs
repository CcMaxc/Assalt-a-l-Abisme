using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGroundMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool startMovingRight = true;
    public float attackColliderDelay = 0.8f;
    private bool moveRight;
    private bool isAttacking = false;
    public EdgeCollider2D attackCollider;
    private bool isStunned = false;
    private bool gameOver = false;
    public int lives = 3;
    public AudioSource audioSource;

    private Rigidbody2D rb;
    private AnimatedSprites animatedSprites;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatedSprites = GetComponent<AnimatedSprites>();

        moveRight = startMovingRight;

    }

    void Update()
    {
        if (!isAttacking && !gameOver && !isStunned)
        {
            if (moveRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Mathf.Abs(rb.velocity.x) > 0.01f)
            {
                animatedSprites.SetHorizontalSprites();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GroundEdge"))
        {
            moveRight = !moveRight;
        }

        if (collision.collider.CompareTag("PlayerAttacking"))
        {
            TakeHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null && collider.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animatedSprites.SetAttackSprites();
        Invoke("ActivateAttackCollider", attackColliderDelay);
        Invoke("DeactivateAttack", 1.3f);
    }

    private void ActivateAttackCollider()
    {
        attackCollider.enabled = true;
    }

    private void DeactivateAttack()
    {
        attackCollider.enabled = false;
        isAttacking = false;
    }

    private void TakeHit()
    {
        bool notGameOver = TakeLive();

        if (notGameOver)
        {
            animatedSprites.SetTakeHitSprites();
            isStunned = true;
            Invoke("EndStun", 1.3f);
        }
        else
        {
            animatedSprites.SetDeathSprites();
            isStunned = true;
            Invoke("GameOver", 1.3f);
        }
    }

    private void EndStun()
    {
        isStunned = false;
    }

    private void GameOver()
    {
        audioSource.enabled = true;
        animatedSprites.SetDeathSprites();
        gameOver = true;
        gameObject.layer = 14;
        Invoke("Deactivate", 0.3f);
    }

    private bool TakeLive()
    {
        lives--;

        if (lives <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Deactivate()
    {
        if (gameObject.tag == "Skeleton")
        {
            Invoke("Load2", 2f);
        }
        gameObject.SetActive(false);
    }

    private void Load2()
    {
        SceneManager.LoadScene(2);
    }
}
