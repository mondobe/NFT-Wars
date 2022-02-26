using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : Interactable
{
    public StrokeBot bot;
    public bool outgoing;
    public LineRenderer myLine;
    public Wire connection;

    public new void Update()
    {
        base.Update();
        myLine.SetPositions(GetWirePoints());
        if (bot.NFTimer >= 0 && bot.NFTimer <= 1 && outgoing && bot.next != null)
            bot.newPrev.transform.position = Vector3.Lerp(connection.transform.position, transform.position, bot.NFTimer);
    }

    public override void OnInteract()
    {
        if(CharMove.wiring)
        {
            if (outgoing && !CharMove.wire.outgoing) 
            {
                bot.next = CharMove.wire.bot;
                CharMove.wire.bot.last = bot;
                connection = CharMove.wire;
                CharMove.wire.connection = this;
            }
            else if (!outgoing && CharMove.wire.outgoing)
            {
                bot.last = CharMove.wire.bot;
                CharMove.wire.bot.next = bot;
                connection = CharMove.wire;
                CharMove.wire.connection = this;
            }
            CharMove.wiring = false;
            CharMove.wire = null;
        }
        else
        {
            if (!outgoing)
            {
                if (connection != null)
                {
                    connection.bot.next = null;
                    connection.connection = null;
                }
                bot.last = null;
                connection = null;
            }
            else
            {
                if (connection != null)
                {
                    connection.bot.last = null;
                    connection.connection = null;
                }
                bot.next = null;
                connection = null;
            }
            CharMove.wiring = true;
            CharMove.wire = this;
        }
    }

    public Vector3[] GetWirePoints()
    {
        Vector3[] toRet = new Vector3[2];
        toRet[0] = transform.position;

        if (CharMove.wiring && CharMove.wire == this)
            toRet[1] = CharMove.player.transform.position + Vector3.up * 2;
        else if (connection != null)
            toRet[1] = connection.transform.position;
        else
            toRet[1] = transform.position;

        return toRet;
    }
}
