using UnityEngine;

public class ElevationExit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] mountainBoundarys;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;
            }
            foreach (Collider2D boundary in mountainBoundarys)
            {
                boundary.enabled = false;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;

        }
        else if (collision.gameObject.CompareTag("Goblin1"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
