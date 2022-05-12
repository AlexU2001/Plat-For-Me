using System.Collections.Generic;
using UnityEngine;

public class ColorOrb : MonoBehaviour
{
    [SerializeField] private Color color;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private ParticleSystem.MainModule particleMain;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            particleMain = GetComponentInChildren<ParticleSystem>().main;

            spriteRenderer.color = color;
            particleMain.startColor = color;
        }
    }

    private void OnValidate()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;

            particleMain = GetComponentInChildren<ParticleSystem>().main;
            particleMain.startColor = color;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UpdateColor();

            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        PlatformManager manager = PlatformManager.instance;
        manager.ChangeColor(color);
    }
}
