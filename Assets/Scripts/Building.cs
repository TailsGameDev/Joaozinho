using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] spriteRenderers = null;

    [SerializeField]
    private Color[] possibleColors = null;

    /*
    [SerializeField]
    private Transform spritesNode;

    private TransformWrapper transformWrapper;
    */

    private void Awake()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = (int) -(transform.position.y * 100.0f);
            spriteRenderer.color = possibleColors[Random.Range(0, possibleColors.Length)];
        }
        /*
        transformWrapper = new TransformWrapper(spritesNode);

        // Order z based on y
        Vector3 position = transformWrapper.Position;
        position.z = position.y;
        transformWrapper.Position = position;
        */
    }
}
