using System.Collections;
using UnityEngine;

public enum AsteroidType
{
    Asteroid,
    SuperAsteroid
}

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public AsteroidType type;
    public ParticleSystem breakParticles;
    public ParticleSystem explosionParticles;

    private float speed;
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;

	private void Awake ()
    {
        rigidBody = GetComponent<Rigidbody> ( );
        meshRenderer = GetComponent<MeshRenderer> ( );
        sphereCollider = GetComponent<SphereCollider> ( );
    }

    private void OnEnable ( )
    {
        speed = Random.Range ( minSpeed, maxSpeed );
        rigidBody.AddForce ( Vector3.down * speed, ForceMode.VelocityChange );
        rigidBody.AddTorque ( Random.insideUnitSphere, ForceMode.VelocityChange );
    }

    private void OnDisable ( )
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        breakParticles.gameObject.SetActive ( false );
        explosionParticles.gameObject.SetActive ( false );
        sphereCollider.enabled = true;
        meshRenderer.enabled = true;
    }

    private void OnMouseDown ( )
    {
        if (GameManager.GameState == GameStates.Game)
            OnClick ( );
    }

    public virtual void OnClick()
    {
        EventsManager.AsteroidDestroyed ( );
        StartCoroutine ( Explode ( ) );
    }

    protected IEnumerator Explode()
    {
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
        breakParticles.gameObject.SetActive ( true );
        explosionParticles.gameObject.SetActive ( true );
        yield return new WaitForSeconds ( 1 );
        gameObject.SetActive ( false );
    }

    private void LateUpdate ( )
    {
        if ( transform.position.y < GameManager.YMin - 2 * transform.localScale.y && meshRenderer.enabled)
        {
            EventsManager.LifeLost ( );
            StopAllCoroutines ( );
            gameObject.SetActive ( false );
        }
    }
}
