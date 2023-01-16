using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Vector2 moveInput;

    public float sneakSpeed;
    public float runSpeed;
    public float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = .1f;
    private float dashCounter;
    private float dashCooldownCounter;

    public static GameObject player
    {
        get { return _player; }
    }
    private static GameObject _player;

    public bool isMakingSound = true;

    public CircleCollider2D sneak;
    public CircleCollider2D walk;
    public CircleCollider2D run;
    public CircleCollider2D dash;

    [SerializeField] private float dashTime;

    void Awake()
    {
        _player = gameObject;
    }

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        sneak.gameObject.SetActive(false);
        walk.gameObject.SetActive(false);
        run.gameObject.SetActive(false);
        dash.gameObject.SetActive(false);
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        walk.gameObject.SetActive(false);
        sneak.gameObject.SetActive(false);
        run.gameObject.SetActive(false);
        switch (true)
        {
            case true when Input.GetKey(KeyCode.Space) && moveInput.magnitude > 0.1f:
                if (dashCooldownCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    dashTime = Time.time;
                    dash.gameObject.SetActive(true);
                }
                break;
            case true when Input.GetKey(KeyCode.LeftControl) && moveInput.magnitude > 0.1f:
                sneak.gameObject.SetActive(true);
                activeMoveSpeed = sneakSpeed;
                break;
            case true when Input.GetKey(KeyCode.LeftShift) && moveInput.magnitude > 0.1f:
                run.gameObject.SetActive(true);
                activeMoveSpeed = runSpeed;
                break;
            default:
                if (moveInput.magnitude > 0.1f)
                {
                    activeMoveSpeed = moveSpeed;
                    walk.gameObject.SetActive(true);
                }
                break;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                dash.gameObject.SetActive(false);
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
                activeMoveSpeed = moveSpeed;
            }
        }

        rb2d.velocity = moveInput * activeMoveSpeed;

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }
}
