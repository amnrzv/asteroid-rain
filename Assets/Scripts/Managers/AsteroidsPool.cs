using System.Collections.Generic;
using UnityEngine;

public class AsteroidsPool : MonoBehaviour
{
    private static AsteroidsPool instance;
    private List<Asteroid> asteroids;

    public Asteroid asteroidPrefab;
    public SuperAsteroid superAsteroidPrefab;

    public static AsteroidsPool Instance
    {
        get
        {
            if ( instance == null )
                instance = FindObjectOfType<AsteroidsPool>();
            return instance;
        }
    }

    private void Awake ( )
    {
        asteroids = new List<Asteroid> ( );
    }

    private void OnEnable()
    {
        EventsManager.GameStartEvent += EventsManager_GameStartedEvent;
    }

    private void OnDisable ( )
    {
        EventsManager.GameStartEvent -= EventsManager_GameStartedEvent;
    }

    private void EventsManager_GameStartedEvent ( )
    {
        ClearPool ( );
        GetComponentsInChildren ( true, asteroids );
    }

    private void ClearPool()
    {
        for ( int i = 0 ; i < asteroids.Count ; i++ )
        {
            asteroids [ i ].gameObject.SetActive ( false );
        }
    }

    public Asteroid GetUnusedAsteroid(AsteroidType type)
    {
        Asteroid asteroid = null;
        for ( int i = 0 ; i < asteroids.Count ; i++ )
        {
            if ( !asteroids [ i ].gameObject.activeInHierarchy && asteroids [i].type == type)
            {
                asteroid = asteroids [ i ];
                break;
            }
        }

        if (asteroid == null)
        {
            switch(type)
            {
                case AsteroidType.Asteroid:
                asteroid = Instantiate ( asteroidPrefab, transform );
                break;
                case AsteroidType.SuperAsteroid:
                asteroid = Instantiate ( superAsteroidPrefab, transform );
                break;
            }
            asteroid.gameObject.SetActive ( false );
            asteroids.Add ( asteroid );
        }

        return asteroid;
    }
}
