using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;
    [SerializeField] SpriteRenderer moveRend;
    [SerializeField] SpriteRenderer shootRend;

    private void Start()
    {
        DOTween.Init();
    }

    private void Update()
    {
        if (playerMove.GetOn())
        {
            Vector2 pullback = playerMove.GetPullback();
            if (pullback != Vector2.zero)
            {
                if (playerMove.GetShoot())
                {
                    shootRend.enabled = true;
                    moveRend.enabled = false;
                }
                else
                {
                    moveRend.enabled = true;
                    shootRend.enabled = false;
                }
                float angle = Mathf.Atan2(pullback.y, pullback.x) * Mathf.Rad2Deg;
                transform.DORotate(new Vector3(0, 0, angle), .1f);
            }
        }
        else
        {
            moveRend.enabled = false;
            shootRend.enabled = false;
        }
    }
}
