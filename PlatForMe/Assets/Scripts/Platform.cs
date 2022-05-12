using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    public bool levelSpecific = false;
    void Start()
    {

        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hit.collider != null)
            {
                if (hitObj.CompareTag("Platform") && hitObj != gameObject)
                {
                    print("oofing myself");
                    Destroy(hitObj);
                }
            }
            else
            {
                Debug.Log("Nothing to see here");
                break;
            }
        }
        if (levelSpecific)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            if (transform.parent != PlatformManager.instance.transform)
            {
                transform.SetParent(PlatformManager.instance.transform);
            }
        }
    }

    private void OnDestroy()
    {
        if (!levelSpecific)
        {
            PlatformManager.instance.AddToCount(1);
            PlatformManager.instance.playerPlatforms.Remove(gameObject);
        }
    }
}
