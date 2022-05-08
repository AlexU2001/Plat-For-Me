using TMPro;
using UnityEngine;

public class PlatformCanvas : MonoBehaviour
{
    public static PlatformCanvas instance;

    public int canvasProgress = 0;
    [SerializeField] private TextMeshProUGUI text = null;

    private void OnEnable()
    {
        PlatformManager.PlatformCreated += UpdatePlatformCount;
    }

    private void OnDisable()
    {
        PlatformManager.PlatformCreated += UpdatePlatformCount;
    }

    private void Awake()
    {
        if (instance != null)
        {
            /*Debug.Log(instance.gameObject.name + " has " + instance.canvasProgress + " while " + gameObject.name + " has " + canvasProgress);*/
            if (instance.canvasProgress > canvasProgress)
            {
                Destroy(gameObject);
                /*Debug.Log(instance.gameObject.name + " which is why it won out");*/
            }
            else
            {
                /*Debug.Log(instance.gameObject.name + " which is why it lost out");*/
                Destroy(instance.gameObject);
                InstanceSelf();
            }
        }
        else
        {
            /*Debug.Log(gameObject.name + ": Hey, it was null dont blame me this is mine now!");*/
            InstanceSelf();
        }
    }

    private void Start()
    {
        if (text != null)
        {
            UpdatePlatformCount();
        }

    }

    private void InstanceSelf()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void UpdatePlatformCount(bool success)
    {
        if (success)
        {
            text.text = "x" + PlatformManager.instance.platformCount;
        }
    }
    private void UpdatePlatformCount()
    {
        /*Debug.Log("Ran with " + PlatformManager.instance.platformCount);*/
        text.text = "x" + PlatformManager.instance.platformCount;
    }
}
