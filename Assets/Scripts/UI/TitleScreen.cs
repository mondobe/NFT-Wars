using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void OnPress()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
