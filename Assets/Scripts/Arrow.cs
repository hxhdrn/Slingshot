using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


// Shows & rotates arrow cursors around player
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
        if (playerMove.GetOn()) // left click held
        {
            Vector2 pullback = playerMove.GetPullback(); // arrow direction
            if (pullback != Vector2.zero)
            {
                // swap cursors
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
                
                float angle = Mathf.Atan2(pullback.y, pullback.x) * Mathf.Rad2Deg; // find angle in degrees based on vector2
                transform.DORotate(new Vector3(0, 0, angle), .1f); // tween to new angle
            }
        }
        else
        {
            // cursors invisible
            moveRend.enabled = false;
            shootRend.enabled = false;
        }
    }
}
