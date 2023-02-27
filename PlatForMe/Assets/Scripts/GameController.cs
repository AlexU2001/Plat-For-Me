using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private AudioClip generalMusic;
    [SerializeField] private AudioClip specialMusic;

    private AudioClip desiredMusic;

    private void Awake()
    {
        ColorOrb orb;
        orb = FindObjectOfType<ColorOrb>();
        if (orb != null)
        {
            desiredMusic = specialMusic;
        } else
        {
            desiredMusic = generalMusic;
        }
    }

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
        if (desiredMusic != null)
        {
            AudioManager.instance.PlayTarget(desiredMusic);
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
