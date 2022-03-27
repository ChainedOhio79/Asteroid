using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns asteroids
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // save asteroid radius
        GameObject asteroid = Instantiate<GameObject>(asteroidPrefab);
        CircleCollider2D asteroidCollider = asteroid.GetComponent<CircleCollider2D>();
        float asteroidRadius = asteroidCollider.radius;
        Destroy(asteroid);

        // calculate screen width and height
        float screenWidth = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        float screenHeight = ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom;

        // right side asteroid
        asteroid = Instantiate<GameObject>(asteroidPrefab);
        Asteroid script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Left, new Vector2(ScreenUtils.ScreenRight + asteroidRadius,
            ScreenUtils.ScreenBottom + screenHeight / 2));

        // top side asteroid
        asteroid = Instantiate<GameObject>(asteroidPrefab);
        script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Down, new Vector2(ScreenUtils.ScreenLeft + screenWidth / 2, 
            ScreenUtils.ScreenTop + asteroidRadius));

        // left side asteroid
        asteroid = Instantiate<GameObject>(asteroidPrefab);
        script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Right, new Vector2(ScreenUtils.ScreenLeft - asteroidRadius,
            ScreenUtils.ScreenBottom + screenHeight / 2));

        // bottom side asteroid
        asteroid = Instantiate<GameObject>(asteroidPrefab);
        script = asteroid.GetComponent<Asteroid>();
        script.Initialize(Direction.Up, new Vector2(ScreenUtils.ScreenLeft + screenWidth / 2,
            ScreenUtils.ScreenBottom - asteroidRadius));
    }
}
