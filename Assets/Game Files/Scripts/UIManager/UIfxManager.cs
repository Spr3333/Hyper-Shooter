using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class UIfxManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float time = 1.0f;
    [SerializeField] private float bounceScale = 1.2f; // Adjust this value for the bounce scale
    public List<TextMeshProUGUI> bouncingTexts; // Assign your TextMeshProUGUI in the Inspector
    [SerializeField] private float flickerTime = 1.0f;
    [SerializeField] private float delay = 1.0f;




    void Start()
    {
        canvasGroup.alpha = 0.0f;
        FadeInOut();
        BounceTexts();
       
    }

    private void FadeInOut()
    {
        LeanTween.alphaCanvas(canvasGroup, 1, time).setLoopPingPong();
    }

    private void BounceTexts()
    {
        foreach (var text in bouncingTexts)
        {
            // Set the initial scale to zero
            text.transform.localScale = text.transform.localScale;

            // Bounce effect
            LeanTween.scale(text.gameObject, new Vector3(bounceScale, bounceScale, 1), time / 2)
                .setEase(LeanTweenType.easeOutQuad)
                .setLoopPingPong();
        }
    }


}
