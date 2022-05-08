using UnityEngine;
using UnityEngine.SceneManagement;

enum HazardTarget { Player, Platform }

public class Hazard : MonoBehaviour
{
    [SerializeField] private HazardTarget hazardTarget = HazardTarget.Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (hazardTarget)
        {
            case HazardTarget.Player:
                if (collision.CompareTag("Player"))
                {
                    int index = SceneManager.GetActiveScene().buildIndex - 1;
                    try
                    {
                        SceneManager.GetSceneByBuildIndex(index);
                        SceneManager.LoadSceneAsync(index);
                    }
                    catch (System.Exception)
                    {

                        Debug.LogWarning("No Following Scene");
                        if (SceneManager.GetActiveScene().buildIndex != 0)
                        {
                            Debug.Log("Returning to 0");
                            SceneManager.LoadSceneAsync(0);
                        }
                        /*throw;*/
                    }
                }
                break;
            case HazardTarget.Platform:
                if (collision.CompareTag("Platform"))
                {
                    Destroy(collision.gameObject);
                }
                break;
            default:
                break;
        }

    }
}
