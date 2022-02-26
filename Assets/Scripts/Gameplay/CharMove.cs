using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharMove : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb2d;
    public float moveSpeed;
    public GameObject visuals;
    Vector2 moveAxis = Vector2.zero;
    public static UnityEvent interactEvent;
    public static GameObject player;
    public static bool placing, wiring;
    public static StrokeBot place;
    public static Wire wire;

    // Start is called before the first frame update
    void Awake()
    {
        interactEvent = new UnityEvent();
        player = gameObject;
        placing = false;
        wiring = false;
    }

    // Update is called once per frame
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if(!Easel.showing)
            moveAxis = ctx.ReadValue<Vector2>() * moveSpeed;
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        DrawTexture.MoveCursor(ctx.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.started)
            return;
        if (placing)
        {
            place.Place();
            placing = false;
            return;
        }
        interactEvent.Invoke();
    }

    void Update()
    {
        if(moveAxis != Vector2.zero)
            visuals.transform.localScale = new Vector2(Mathf.Sign(moveAxis.x), 1);
        anim.SetFloat("Velocity", moveAxis.magnitude);
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveAxis;
    }
}
