using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public float moveSpeed = 3f;
    public LayerMask listObstacleLayers;
    public float groundCheckRadius = 0.15f;
    public bool isFacingRight = false;
    public float distanceDetection = 0.1f;

    void Awake()
    {
        // Assigne automatiquement les composants si non définis
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (bc == null) bc = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (rb == null || bc == null)
        {
            Debug.LogError("Rigidbody2D ou BoxCollider2D non assigné !");
            return;
        }

        if (rb.linearVelocity.y != 0) return; // Corrige l'erreur de `linearVelocity`

        rb.linearVelocity = new Vector2(
            moveSpeed * (isFacingRight ? 1 : -1), // Utilisation de `isFacingRight` pour une logique plus claire
            rb.linearVelocity.y
        );

        if (HasNotTouchedGround() || HasCollisionWithObstacles())
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight; // Met à jour la direction
        transform.Rotate(0, 180, 0);
    }

    bool HasNotTouchedGround()
    {
        if (bc == null) return false;

        Vector2 detectionPosition = new Vector2(
            bc.bounds.center.x + ((isFacingRight ? 1 : -1) * (bc.bounds.size.x / 2)),
            bc.bounds.min.y
        );

        return !Physics2D.OverlapCircle(
            detectionPosition,
            groundCheckRadius,
            listObstacleLayers
        );
    }

    bool HasCollisionWithObstacles()
    {
        if (bc == null) return false;

        RaycastHit2D hit = Physics2D.Linecast(
            bc.bounds.center,
            bc.bounds.center + new Vector3(
                distanceDetection * (isFacingRight ? 1 : -1),
                0,
                0
            ),
            listObstacleLayers
        );

        return hit.transform != null;
    }

    void OnDrawGizmos()
    {
        if (bc == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            bc.bounds.center,
            bc.bounds.center + new Vector3(
                distanceDetection * (isFacingRight ? 1 : -1),
                0,
                0
            )
        );

        Gizmos.color = Color.red;
        Vector2 detectionPosition = new Vector2(
            bc.bounds.center.x + ((isFacingRight ? 1 : -1) * (bc.bounds.size.x / 2)),
            bc.bounds.min.y
        );

        Gizmos.DrawWireSphere(
            detectionPosition,
            groundCheckRadius
        );
    }
}
