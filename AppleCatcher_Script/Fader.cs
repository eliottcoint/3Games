using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public const float DURATION = 2f;
    protected SpriteRenderer my_rendere;

    // Start is called before the first frame update
    void Start()
    {
        my_rendere = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeSceneIn());
    }

    //Methode to request fade out of scene
    public void RequestSceneOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeSceneOut());
    }

    //Coroutine to fade scene in = fader fades out
    protected IEnumerator FadeSceneIn()
    {
        while (my_rendere.color.a > 0)
        {
            float alpha = my_rendere.color.a;
            alpha -= Time.deltaTime / DURATION;
            my_rendere.color = new Color(1, 1 ,1, alpha);
            yield return null;
        }
    }

    //Coroutine to fade scene out = fader fades in
    protected IEnumerator FadeSceneOut()
    {
        while (my_rendere.color.a < 1)
        {
            float alpha = my_rendere.color.a;
            alpha += Time.deltaTime / DURATION;
            my_rendere.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}
