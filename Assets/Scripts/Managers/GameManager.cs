using System.Collections;
using UnityEngine;

public enum GameStates
{
    Menu,
    Game
}

public class GameManager : MonoBehaviour
{
    //Game variables
    public const float GAME_TIME = 60f;
    public const int MAX_LIVES = 5;
    private const float spawnRateMin = 0.4f;
    private const float spawnRateMax = 0.8f;
    private const float probabilitySuperAsteroid = 0.1f;
    private const float minDelayBeforeFirstSuperAsteroid = 15f;         //Delay to avoid Super Asteroids spawning at the beginning

    private static GameStates gameState;

    //Game state variables
    private static int lives;
    private static int score;

    //Spawning point variables
    private static float yMin;
    private float yMax;
    private float xMin;
    private float xMax;

    private float startTime;

    private IEnumerator gameLoopCoroutine;

    public static GameStates GameState { get { return gameState; } }
    public static int Score { get { return score; } }
    public static int Lives { get { return lives; } }

    private void Awake ( )
    {
        gameState = GameStates.Menu;
    }

    private void OnEnable ( )
    {
        CalculateSpawnRegion ( );
        EventsManager.GameStartEvent += EventsManager_GameStartedEvent;
        EventsManager.AsteroidDestroyedEvent += EventsManager_AsteroidDestroyedEvent;
        EventsManager.LifeLostEvent += EventsManager_LifeLostEvent;
    }

    private void OnDisable ( )
    {
        EventsManager.GameStartEvent -= EventsManager_GameStartedEvent;
        EventsManager.AsteroidDestroyedEvent -= EventsManager_AsteroidDestroyedEvent;
        EventsManager.LifeLostEvent -= EventsManager_LifeLostEvent;
    }

    private void EventsManager_LifeLostEvent ( )
    {
        lives--;

        if ( lives == 0 )
            GameOver ( );
    }

    private void EventsManager_AsteroidDestroyedEvent ( )
    {
        score++;
    }

    private void EventsManager_GameStartedEvent ( )
    {
        score = 0;
        lives = MAX_LIVES;
        startTime = Time.time;
        gameState = GameStates.Game;
        gameLoopCoroutine = GameLoop ( );
        StartCoroutine ( gameLoopCoroutine );
    }

    //Calculating screen bounds to spawn asteroids from an appropriate area
    private void CalculateSpawnRegion()
    {
        float screenTop = Camera.main.orthographicSize;
        float screenRight = Camera.main.orthographicSize * Camera.main.aspect;
        yMax = Camera.main.transform.position.y + screenTop;
        yMin = Camera.main.transform.position.y - screenTop;
        xMax = Camera.main.transform.position.x + screenRight;
        xMin = Camera.main.transform.position.x - screenRight;
    }

    public static float YMin { get { return yMin; } }

    private IEnumerator GameLoop()
    {
        Asteroid asteroid = null;
        float randomNum;

        //Keep instantiating asteroids while the game is on
        while( Time.time - startTime < GAME_TIME )
        {
            randomNum = Random.Range ( 0f, 1f );
            asteroid = AsteroidsPool.Instance.GetUnusedAsteroid ( Time.time - startTime > minDelayBeforeFirstSuperAsteroid && randomNum < probabilitySuperAsteroid ? AsteroidType.SuperAsteroid : AsteroidType.Asteroid);
            
            //Spawning asteroids making sure they are not close to the screen edges
            asteroid.transform.position = Vector3.up * (yMax + asteroid.transform.localScale.y) + Vector3.right * Random.Range ( xMin + 2 * asteroid.transform.localScale.x, xMax - 2 * asteroid.transform.localScale.x);
            asteroid.gameObject.SetActive ( true );
            yield return new WaitForSeconds ( Random.Range( spawnRateMin, spawnRateMax ) );
        }

        GameOver ( );

        yield break;
    }

    private void GameOver()
    {
        StopCoroutine ( gameLoopCoroutine );
        EventsManager.GameOver ( );
        gameState = GameStates.Menu;
    }
}
