using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private AudioClip levelMusic;

    private void Start()
    {

        if (cinemachineVirtualCamera == null)
        {
            FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoaded;
    }

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (levelMusic != null)
        {
            AudioManager.instance.PlayTarget(levelMusic);
        }
    }

    private void Update()
    {
        if (cinemachineVirtualCamera.Follow == null)
        {
            if (PlayerMovement.instance != null)
            {
                cinemachineVirtualCamera.Follow = PlayerMovement.instance.transform;
            }
        }

        if (PlayerMovement.instance.transform.position.y < -10)
        {
            PlayerMovement.instance.transform.position = Vector2.zero;
        }
    }

}
