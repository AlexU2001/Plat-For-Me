using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    private void LoadTargetScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.buildIndex == 0 && currentScene.name != "Title")
            {

                Debug.LogError("That shouldnt have been 0, check the added scenes");
                return;
            }
            else
            {
                int index = currentScene.buildIndex + 1;
                try
                {
                    AudioManager.instance?.PlayTarget("Next");
                    SceneManager.GetSceneByBuildIndex(index);
                    LoadTargetScene(index);
                }
                catch (System.Exception)
                {

                    /*Debug.LogWarning("No Following Scene");*/
                    if (currentScene.buildIndex != 0)
                    {
                        /*Debug.Log("Returning to 0");*/
                        AudioManager.instance?.PlayTarget("Next");
                        LoadTargetScene(0);

                    }
                }
            }
        }
    }
}
