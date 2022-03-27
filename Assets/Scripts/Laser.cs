using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A laser
/// </summary>
public class Laser : MonoBehaviour
{
    // timer support
    const float LaserLifeSeconds = 2f;
    Timer deathTimer;

    /// <summary>
    /// Applies the force to the laser object
    /// </summary>
    /// <param name="laserDirection"></param>
    public void ApplyForce(Vector2 forceDirection)
    {
        const float Magnitude = 12.5f;
        GetComponent<Rigidbody2D>().AddForce(forceDirection * Magnitude, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = LaserLifeSeconds;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
