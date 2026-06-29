using System.Collections;
using UnityEngine;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander Area")]
    public float wanderWidth;
    public float wanderHeight;
    public Vector2 startingPosition;
    public float speed;
    public Vector2 target;
    public float pauseDuration;

    private Rigidbody2D rb;
    private bool isPaused;
    private Animator anim;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PauseAndPickNextDestination());
    }

    private void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(PauseAndPickNextDestination());
        }

        Move();

    }

    private void Move()
    {
        Vector3 direction = ((Vector3)target - transform.position).normalized;

        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        rb.linearVelocity = direction * speed;
    }

    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2;
        float halfHeight = wanderHeight / 2;
        int edge = Random.Range(0, 4);

        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),
            1 => new Vector2(startingPosition.x + halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),
            2 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y - halfHeight),
            _ => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y + halfHeight),

        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(PauseAndPickNextDestination());
    }

    IEnumerator PauseAndPickNextDestination()
    {
        isPaused = true;
        anim.SetBool("isWalking", false);
        yield return new WaitForSeconds(pauseDuration);

        target = GetRandomTarget();
        anim.SetBool("isWalking", true);
        isPaused = false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }
}
