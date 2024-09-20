using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public EdgeCollider2D attackCollider;
    public float knockbackForce = 5f;
    private bool isAttacking = false;
    private bool isStunned = false;
    private bool gameOver = false;
    private int lives = 5;
    public Text livesCount;

    private Rigidbody2D rb;
    private AnimatedSprites animatedSprites;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animatedSprites = GetComponent<AnimatedSprites>();
        lives = 5;
        UpdateLivesDisplay();
    }

    private void UpdateLivesDisplay()
    {
        if (livesCount != null)
        {
            livesCount.text =lives.ToString();
        }
    }

    private void Update()
    {
        if (!gameOver && !isStunned && !isAttacking)
        {
            float move = Input.GetAxis("Horizontal");

            transform.Translate(move * moveSpeed * Time.deltaTime, 0, 0);

            if (move > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (move < 0 && !isAttacking)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (move == 0)
            {
                animatedSprites.SetIdleSprites();
            }
            else if (!isAttacking)
            {
                animatedSprites.SetHorizontalSprites();
            }

            if (Mathf.Abs(rb.velocity.y) > 0.01f)
            {
                animatedSprites.SetJumpSprites();
            }

            if ((Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f) || (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.01f))
            {
                if(audioSource1.enabled == false)
                {
                    audioSource1.enabled = true;
                }
                else
                {
                    audioSource1.Play();
                }

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (Input.GetKeyDown(KeyCode.K) && !isAttacking && Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                if (audioSource2.enabled == false)
                {
                    audioSource2.enabled = true;
                }
                else
                {
                    audioSource2.Play();
                }

                Attack();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemies") && !isAttacking)
        {
            TakeHit();
        }

        if (collision.collider.CompareTag("Skeleton") && !isAttacking)
        {
            TakeHit();
        }

        if (collision.collider.CompareTag("DeathBarrier"))
        {
            GameOver();
        }
    }

    private void Attack()
    {
        gameObject.tag = "PlayerAttacking";
        attackCollider.enabled = true;
        isAttacking = true;
        animatedSprites.SetAttackSprites();
        Invoke("DeactivateAttack", 0.6f);
    }

    private void DeactivateAttack()
    {
        gameObject.tag = "Player";
        attackCollider.enabled = false;
        animatedSprites.SetIdleSprites();
        isAttacking = false;
    }

    private void TakeHit()
    {
        bool notGameOver = TakeLive();

        gameObject.tag = "getHit";
        gameObject.layer = 15;

        if (notGameOver)
        {
            if (audioSource3.enabled == false)
            {
                audioSource3.enabled = true;
            }
            else
            {
                audioSource3.Play();
            }

            animatedSprites.SetTakeHitSprites();
            Vector2 knockbackDirection = -transform.right.normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            isStunned = true;
            Invoke("EndStun", 1.3f);
        }
        else if (!notGameOver)
        {
            audioSource4.enabled = true;
            gameObject.layer = 14;
            animatedSprites.SetDeathSprites();
            Vector2 knockbackDirection = -transform.right.normalized;
            rb.AddForce(knockbackDirection * (knockbackForce + 2), ForceMode2D.Impulse);

            isStunned = true;
            Invoke("GameOver", 1.3f);
        }
    }

    private void EndStun()
    {
        isStunned = false;
        Invoke("EndInvencibility", 1f);
    }

    private void EndInvencibility()
    {
        gameObject.layer = 12;
        gameObject.tag = "Player";
    }

    private void GameOver()
    {
        animatedSprites.SetGameOverSprites();
        gameOver = true;
        Invoke("ReturnMenu", 3.0f);
    }

    private bool TakeLive()
    {
        lives = lives - 1;
        UpdateLivesDisplay();

        if (lives <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
