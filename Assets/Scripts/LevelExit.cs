using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    private Player player;

    private void Start()
    {
        player = Player.GetPlayer();
    }

    private void Update()
    {
        if (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) <= 0.5f)
        {
            StartCoroutine(sceneFader.FadeAndLoadScene(SceneFader.FadeDirection.In, "Level " + (1 + SceneManager.GetActiveScene().buildIndex)));
        }
    }
}
