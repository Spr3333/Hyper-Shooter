using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUIManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Transform bulletsParent;

    [Header("Settings")]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    private int bulletShot;

    private void Awake()
    {
        PlayerShooter.OnShot += OnShotCallBack;
    }

    private void OnDestroy()
    {
        PlayerShooter.OnShot -= OnShotCallBack;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnShotCallBack()
    {
        bulletShot++;
        bulletsParent.GetChild(bulletShot - 1).GetComponent<Image>().color = inactiveColor;
    }
}
