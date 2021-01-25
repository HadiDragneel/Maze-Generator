using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    // Script for the main menu background image. Shakes it up. Wanted to make this a more smooth animation. But since this wasn't a priority it got pushed until I ran out of time.

    private RectTransform image;


    private void Start()
    {
        image = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // Gets random floats and sets the new transform based on those values. Since this happens every frame it gives it a shaking effect.
        image.anchoredPosition = new Vector3(image.anchoredPosition.x + Random.Range(-0.2f, 0.2f), image.anchoredPosition.y + Random.Range(-0.2f, 0.2f));
    }
}
