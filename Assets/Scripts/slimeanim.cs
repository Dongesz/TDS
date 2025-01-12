using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    // Adott mozg�sir�ny kezel�s�hez.
    private Vector3 lastPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }

        // A kezdeti poz�ci� ment�se.
        lastPosition = transform.position;
    }

    void Update()
    {
        AnimateSprite();
        AdjustSpriteDirection();
    }

    void AnimateSprite()
    {
        if (sprites.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer -= animationSpeed;
            currentFrame = (currentFrame + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentFrame];
        }
    }

    void AdjustSpriteDirection()
    {
        // Az aktu�lis mozg�sir�ny kisz�m�t�sa.
        Vector3 direction = (transform.position - lastPosition).normalized;

        if (direction != Vector3.zero)
        {
            // Ha a mozg�s jobb vagy bal ir�nyba t�rt�nik, t�kr�zz�k a sprite-ot a megfelel� tengely ment�n.
            if (direction.x > 0)
            {
                // Mozg�s jobbra, teh�t nem t�kr�zz�k.
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                // Mozg�s balra, t�kr�zz�k.
                spriteRenderer.flipX = true;
            }
        }

        // Az �j poz�ci� ment�se a k�vetkez� frame-hez.
        lastPosition = transform.position;
    }
}
