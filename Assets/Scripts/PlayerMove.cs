using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float pullCap; // longest distance for the pullback vector
    [SerializeField] GameObject projectile; // prefab
    [SerializeField] GameObject shootArrow; // arrow cursor

    Rigidbody2D rb;
    Vector2 pullback;
    Vector2 clickPos;

    AudioManager audioMgr;

    bool on; // currently holding left click
    bool release; // just released left click
    bool shoot; // currently holding space

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioMgr = FindObjectOfType<AudioManager>();
    }

    public void LeftClick(InputAction.CallbackContext info)
    {
        if (info.performed)
        {
            on = true;
            clickPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pullback = Vector2.zero;
        }
        else if (info.canceled)
        {
            on = false;
            release = true;
        }
    }

    public void Space(InputAction.CallbackContext info)
    {
        if (info.performed)
        {
            shoot = true;
        }
        else if (info.canceled)
        {
            shoot = false;
        }
    }

    private void Update()
    {
        if (on)
        {
            pullback = clickPos - (Vector2) Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (pullback.magnitude > pullCap)
            {
                pullback = pullback.normalized * pullCap;
            }
        }

        if (release)
        {
            if (shoot)
            {
                // create, aim, and shoot projectile
                GameObject newProj = Instantiate(projectile);
                newProj.transform.position = shootArrow.transform.position;
                newProj.transform.rotation = shootArrow.transform.rotation;
                newProj.GetComponent<Shoot>().ShootArrow();
                release = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (release)
        {
            if (!shoot)
            {
                // move player
                audioMgr.PlaySound("move");
                rb.AddForce(pullback.normalized * moveSpeed, ForceMode2D.Impulse);

            }
            release = false;
        }
    }

    // getters

    public bool GetOn()
    {
        return on;
    }

    public bool GetShoot()
    {
        return shoot;
    }

    public Vector2 GetPullback()
    {
        return pullback;
    }
}
