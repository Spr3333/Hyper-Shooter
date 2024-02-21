using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class Uimanager : MonoBehaviour
{
    [SerializeField] private float flickerTime = 0.1f;
    public TextMeshProUGUI[] childTexts;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float time = 1.0f;

    void Start()
    {
        canvasGroup.alpha = 0.0f;
        FadeInOut();
        FlickerTexts();
    }

    private void FadeInOut()
    {
        LeanTween.alphaCanvas(canvasGroup, 1, time).setLoopPingPong();
    }

    private void FlickerTexts()
    {
        foreach (var text in childTexts)
        {
            FlickerText(text);
        }
    }

    private void FlickerText(TextMeshProUGUI text)
    {
        // Get the text string
        string originalText = text.text;

        // Flicker each character
        foreach (int index in Enumerable.Range(0, originalText.Length))
        {
            int currentIndex = index; // Create a local variable to capture the correct index

            LeanTween.value(0, 1, flickerTime)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnUpdate((float val) =>
                {
                    // Update the text with the flickering character
                    string flickerText = originalText;
                    flickerText = flickerText.Substring(0, currentIndex) +
                                   RandomChar() +
                                   flickerText.Substring(currentIndex + 1);
                    text.text = flickerText;

                    // Update the alpha of the character
                    Color textColor = text.color;
                    textColor.a = val;
                    text.color = textColor;
                })
                .setOnComplete(() =>
                {
                    // Restore the original text and reset alpha
                    text.text = originalText;
                    Color textColor = text.color;
                    textColor.a = 1.0f;
                    text.color = textColor;
                });
        }
    }

    // Helper function to get a random character
    private char RandomChar()
    {
        return (char)Random.Range('A', 'Z' + 1);
    }
}
