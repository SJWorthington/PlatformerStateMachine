using UnityEngine;

public class PlanetGravityTrigger : MonoBehaviour
{
    private Transform planetTransform;

    private void Start()
    {
        planetTransform = GetComponentInParent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("object has entered grav planet range");
        other.gameObject.GetComponent<Player>()?.AddToInRangeGravityPlanetTransforms(planetTransform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>()?.RemoveFromInRangeGravityPlanetTransforms(planetTransform);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 8);
    }
}