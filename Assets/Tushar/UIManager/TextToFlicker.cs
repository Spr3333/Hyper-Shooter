using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextToFlicker : MonoBehaviour
{
    public float flickerDuration = 0.1f;

    public TextMeshProUGUI[] lettersToAnimate;

    void Start()
    {
        // Start the flicker effect
        FlickerText();
    }

    void FlickerText()
    {
        // Iterate through each letter TextMeshProUGUI
        foreach (TextMeshProUGUI letterText in lettersToAnimate)
        {
            // Generate a random alpha value for each letter
            float randomAlpha = Random.Range(0f, 1f);

            // Use LeanTween to flicker the alpha value randomly
            LeanTween.value(letterText.gameObject, letterText.color.a, randomAlpha, flickerDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnUpdate((float alpha) =>
                {
                    Color color = letterText.color;
                    color.a = alpha;
                    letterText.color = color;
                });
        }

        // Call the function recursively for looping flicker effect
        StartCoroutine(NextFlicker());
    }

    IEnumerator NextFlicker()
    {
        yield return new WaitForSeconds(flickerDuration);

        // Call the function recursively for looping flicker effect
        FlickerText();
    }
}