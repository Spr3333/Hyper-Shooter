using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletUIManager : MonoBehaviour
{
    public static BulletUIManager instance;

    [Header("Elements")]
    [SerializeField] private Transform bulletsParent;

    [Header("Settings")]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    private int bulletShot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        PlayerShooter.OnShot += OnShotCallBack;

        PlayerMovement.OnEnterdWarzone += EnteredWarzoneCallBack;
        PlayerMovement.OnExitWarzone += ExitWarzoneCallBack;
    }

    private void OnDestroy()
    {
        PlayerShooter.OnShot -= OnShotCallBack;
        PlayerMovement.OnEnterdWarzone -= EnteredWarzoneCallBack;
        PlayerMovement.OnExitWarzone -= ExitWarzoneCallBack;
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletsParent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EnteredWarzoneCallBack()
    {
        bulletsParent.gameObject.SetActive(true);
    }

    private void ExitWarzoneCallBack()
    {
        bulletsParent.gameObject.SetActive(false);
        Reload();
    }

    private void OnShotCallBack()
    {
        bulletShot++;

        //if (bulletShot > bulletsParent.childCount)
        //    bulletShot = bulletsParent.childCount;

        bulletShot = Mathf.Clamp(bulletShot, 0, bulletsParent.childCount);

        //bulletShot = Mathf.Min(bulletShot, bulletsParent.childCount);

        bulletsParent.GetChild(bulletShot - 1).GetComponent<Image>().color = inactiveColor;
    }
    private void Reload()
    {
        bulletShot = 0;
        for (int i = 0; i < bulletsParent.childCount; i++)
        {
            bulletsParent.GetChild(i).GetComponent<Image>().color = activeColor;
        }
    }

    public bool CanShoot()
    {
        return bulletShot < bulletsParent.childCount;
    }
}
