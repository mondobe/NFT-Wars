using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public TextMeshProUGUI doorText;

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        doorText.enabled = close;
    }

    public override void OnInteract()
    {
        SceneManager.LoadScene("Ending");
    }
}
