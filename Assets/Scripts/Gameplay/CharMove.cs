using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharMove : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb2d;
    public float moveSpeed;
    public GameObject visuals;
    Vector2 moveAxis = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveAxis = ctx.ReadValue<Vector2>() * moveSpeed;
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
