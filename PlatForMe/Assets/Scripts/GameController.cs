using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {

        if (cinemachineVirtualCamera == null)
        {
            FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
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
