using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class ShopKeeper : MonoBehaviour
{

    public Animator anim;

    private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            anim.SetBool("PlayerInRange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            anim.SetBool("PlayerInRange", false);
        }
    }

}
