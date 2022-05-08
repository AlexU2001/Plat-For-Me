using System;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    public float platformCount { get; private set; }

    public static PlatformManager instance;

    public static Action<bool> PlatformCreated;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        platformCount = 10;
    }

    private void OnEnable()
    {
        DragDrop.dropped += CreatePlatform;
    }

    private void OnDisable()
    {
        DragDrop.dropped -= CreatePlatform;
    }

    /* prototype code that spawned platform directly beneath player
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    */

    public void AddToCount(float sum)
    {
        float originalSum = platformCount;
        platformCount += sum;
        PlatformCreated?.Invoke(true);
        Debug.Log("Original: " + originalSum + " New: " + platformCount);
    }

    private void CreatePlatform(DragDrop.PlatformData data)
    {
        /*print("Create Platform call at " + data.positon);*/
        if (platformCount > 0)
        {
            GameObject newPlat = Instantiate(data.platformPrefab, data.positon, Quaternion.identity);
            newPlat.transform.SetParent(transform);
            AddToCount(-1);
        }
        else
        {
            PlatformCreated?.Invoke(false);
        }

    }
}
