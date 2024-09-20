using UnityEngine;

public class AnimatedSprites : MonoBehaviour
{
    private Sprite[] currentSprites;
    public Sprite[] horizontalSprites;
    public Sprite[] jumpSprites;
    public Sprite[] idleSprites;
    public Sprite[] attackSprites;
    public Sprite[] takeHitSprites;
    public Sprite[] deathSprites;
    public Sprite[] gameOverSprite;
    public float defaultFramerate = 0.15f;
    public float gameOverFramerate = 0.2f; 
    private float animatedSpriteFramerate;
    private int frame;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animatedSpriteFramerate = defaultFramerate;
        InvokeRepeating(nameof(Animate), animatedSpriteFramerate, animatedSpriteFramerate);
    }

    private void Animate()
    {
        if (currentSprites == null || currentSprites.Length == 0)
            return;

        frame++;

        if (frame >= currentSprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < currentSprites.Length)
        {
            spriteRenderer.sprite = currentSprites[frame];
        }
    }

    public void SetHorizontalSprites()
    {
        currentSprites = horizontalSprites;
    }

    public void SetJumpSprites()
    {
        currentSprites = jumpSprites;
    }

    public void SetIdleSprites()
    {
        currentSprites = idleSprites;
    }

    public void SetAttackSprites()
    {
        currentSprites = attackSprites;
    }

    public void SetTakeHitSprites()
    {
        currentSprites = takeHitSprites;
        frame = 0;
        ChangeFramerate(0.30f);
        Invoke("RestoreDefaultFramerate", 1.3f);
    }

    public void SetDeathSprites()
    {
        frame = 0;
        ChangeFramerate(gameOverFramerate);
        currentSprites = deathSprites;
    }

    public void SetGameOverSprites()
    {
        frame = 0;
        currentSprites = gameOverSprite;
    }

    private void ChangeFramerate(float newFramerate)
    {
        CancelInvoke(nameof(Animate));
        animatedSpriteFramerate = newFramerate;
        InvokeRepeating(nameof(Animate), animatedSpriteFramerate, animatedSpriteFramerate);
    }

    private void RestoreDefaultFramerate()
    {
        ChangeFramerate(defaultFramerate);
    }
}
