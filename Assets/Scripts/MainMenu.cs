using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;

    public void Play()
    {
        StartCoroutine(sceneFader.FadeAndLoadScene(SceneFader.FadeDirection.In, "Game"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
