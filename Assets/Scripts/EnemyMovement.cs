using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    if (LevelManager.main == null || LevelManager.main.path == null || LevelManager.main.path.Length == 0)
        {
            Debug.LogError("Path not set in LevelManager.");
            Destroy(gameObject);
            return;
        }
    
    target = LevelManager.main.path[pathIndex];
}

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;
            
            if (pathIndex == LevelManager.main.path.Length) {
                EnemySpawner.OnEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
}
