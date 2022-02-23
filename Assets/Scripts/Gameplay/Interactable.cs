using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public int interactDistance;
    public SpriteRenderer[] spriteRenderers;
    bool close = false;

    public void Start()
    {
        CharMove.interactEvent.AddListener(WhenRecievedInteractMessage);
    }

    public void Update()
    {
        close = (transform.position - CharMove.player.transform.position).magnitude < interactDistance;
        foreach (SpriteRenderer spr in spriteRenderers)
            spr.color = close ? Color.gray : Color.white;
    }

    public void WhenRecievedInteractMessage()
    {
        if (close)
            OnInteract();
    }

    public virtual void OnInteract() { }
}
