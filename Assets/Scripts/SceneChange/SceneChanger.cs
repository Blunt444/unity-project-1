using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator anim;
    public float fadeTime = 1;
    public Vector2 newPlayerPosition;
    private Transform playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            anim.Play("FadeToWhite");
            StartCoroutine(DelayFade());    
        }
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        playerTransform.position = newPlayerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}
