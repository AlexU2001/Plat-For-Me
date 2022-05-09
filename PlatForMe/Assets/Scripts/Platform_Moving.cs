using System.Collections;
using UnityEngine;

// TO DO: Improve the script by changing it to a lerp and using a vector2 starting point and vector2 end point
public class Platform_Moving : Platform
{

    [Header("Base Settings")]
    [SerializeField] private float time = 4f;
    [SerializeField] private float distance = 4f;

    [Header("Advanced Settings")]
    [Tooltip("How long the platform will idle for after reaching its destination")]
    [SerializeField] private float standby = 1f;
    [Tooltip("If the platform will travel horizontally or alternatively vertically")]
    [SerializeField] private bool Horizontal = true;
    [Tooltip("If the platform will add or substract from the initial position")]
    [SerializeField] private bool reverse = false;
    

    private Vector2 start;
    private Vector2 end;

    private WaitForSeconds waitTime;

    private void OnEnable()
    {
        DetermineDirection();
        StartCoroutine(MoveTowards(start, end, time));

        waitTime = new WaitForSeconds(standby);
    }

    void DetermineDirection()
    {
        start = transform.localPosition;
        if (Horizontal)
        {
            end = reverse ? new Vector2(start.x - distance, start.y) : new Vector2(start.x + distance, start.y);
        }
        else
        {
            end = reverse ? new Vector2(start.x, start.y - distance) : new Vector2(start.x, start.y + distance);
        }
    }

    void NewDestination()
    {
        if ((Vector2)transform.localPosition == start)
        {
            StartCoroutine(MoveTowards(start, end, time));
        }
        else if ((Vector2)transform.localPosition == end)
        {
            StartCoroutine(MoveTowards(end, start, time));
        }
        else
        {
            Debug.LogError("Platform has exited intended path");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Name: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }


    IEnumerator MoveTowards(Vector2 startPos, Vector2 endPos, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            transform.localPosition = Vector2.Lerp(startPos, endPos, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPos;
        yield return waitTime;
        NewDestination();
    }
}
