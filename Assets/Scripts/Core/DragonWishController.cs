using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWishController : MonoBehaviour
{
    [SerializeField] GameObject[] dragonBall;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
