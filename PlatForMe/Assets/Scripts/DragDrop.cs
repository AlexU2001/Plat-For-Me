using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PlatformType { Normal, Moving };

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    [SerializeField] private Canvas canvas;

    [Header("Dragged Platform Settings")]
    [SerializeField] private Image platformImage;
    [SerializeField] private GameObject platformPrefab;

    [Tooltip("0: Base Color 1: Drag Color 2: Invalid Color")]
    [SerializeField] private Color[] ImageColors = new Color[1];

    public static Action<PlatformData> dropped;


    public struct PlatformData
    {
        public Vector2 positon { get; private set; }
        public GameObject platformPrefab { get; private set; }

        public PlatformData(Vector2 targetPosition, GameObject prefab)
        {
            positon = targetPosition;
            platformPrefab = prefab;
        }

    }

    void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        platformImage.rectTransform.position = eventData.position;

        if (PlatformManager.instance.platformCount > 0)
        {
            platformImage.color = ImageColors[1];
        }
        else
        {
            platformImage.color = ImageColors[2];
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        DragPlatform(eventData.delta);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        platformImage.color = ImageColors[0];
        platformImage.rectTransform.anchoredPosition = Vector2.zero;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        if (CanPlace(mousePos))
        {
            dropped?.Invoke(new PlatformData(mousePos, platformPrefab));
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

    }

    void DragPlatform(Vector2 position)
    {
        /* platformImage.transform.SetParent(canvas.transform);*/
        platformImage.rectTransform.anchoredPosition += position / canvas.scaleFactor;
    }

    bool CanPlace(Vector2 position)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.CompareTag("Hazard"))
                {
                    Debug.Log("Prvented Platform Destruction");
                    return false;
                }
            }
        }
        return true;
    }

}
