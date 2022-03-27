using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Sprite whiteRockSprite;
    [SerializeField]
    Sprite magentaRockSprite;
    [SerializeField]
    Sprite greenRockSprite;

    // HUD score support
    int asteroidValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        // get a random sprite for an asteroid
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0,3);
        if (spriteNumber < 1)
        {
            spriteRenderer.sprite = whiteRockSprite;
        }
        else if (spriteNumber < 2)
        {
            spriteRenderer.sprite = magentaRockSprite;
        }
        else
        {
            spriteRenderer.sprite = greenRockSprite;
        }
    }

    /// <summary>
    /// Initializes asteroid moving in the given direction
    /// </summary>
    /// <param name="direction">direction for asteroid to move</param>
    /// <param name="position">position for asteroid</param>
    public void Initialize (Direction direction, Vector3 position)
    {
        // set asteroid position
        transform.position = position;

        // set random angle based on direction
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

        // get asteroid moving
        StartMoving(angle);
    }

    /// <summary>
    /// Destroys an asteroid on collision with laser
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Laser"))
        {
            AudioManager.Play(AudioClipName.AsteroidHit);
            Destroy(coll.gameObject);

            // destroy or split asteroid
            if (transform.localScale.x < 0.5f)
            {
                Destroy(gameObject);
                HUD hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
                hud.AddPoints(asteroidValue);
            }
            else
            {
                // shrink asteroid to half size
                Vector3 scale = transform.localScale;
                scale.x /= 2;
                scale.y /= 2;
                transform.localScale = scale;

                // cut collider radius in half
                CircleCollider2D collider = GetComponent<CircleCollider2D>();
                collider.radius /= 2;

                // clone asteroid twice and destroy original
                GameObject newAsteroid = Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
                newAsteroid.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));
                newAsteroid = Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
                newAsteroid.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Apply impulse force to get asteroid moving
    /// </summary>
    /// <param name="angle"></param>
    public void StartMoving(float angle)
    {
        const float MinImpulseForce = 0.5f;
        const float MaxImpulseForce = 2f;
        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
    }
}
