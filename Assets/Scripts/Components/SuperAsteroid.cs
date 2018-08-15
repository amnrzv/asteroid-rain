using UnityEngine;

public class SuperAsteroid : Asteroid
{
    private float xOffset = 1.5f;

    public override void OnClick ( )
    {
        Split ( 2 );
        EventsManager.SuperAsteroidSplit ( );
        StartCoroutine ( base.Explode ( ) );
    }

    private void Split(int count)
    {
        Asteroid asteroid = null;
        for ( int i = 0 ; i < count ; i++ )
        {
            asteroid = AsteroidsPool.Instance.GetUnusedAsteroid (AsteroidType.Asteroid);
            asteroid.transform.position = transform.position + Mathf.Pow(-1, i) * Vector3.right * xOffset;
            asteroid.gameObject.SetActive ( true );
        }
    }
}
