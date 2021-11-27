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
        for (int s = 0; s < spriteRenderers.Length; s++)
        {
            SpriteRenderer spriteRenderer = spriteRenderers[s];
            spriteRenderer.sortingOrder = (int) -((transform.position.y * 100.0f) - s);
        }
        spriteRenderers[0].color = possibleColors[Random.Range(0, possibleColors.Length)];
        /*
        transformWrapper = new TransformWrapper(spritesNode);

        // Order z based on y
        Vector3 position = transformWrapper.Position;
        position.z = position.y;
        transformWrapper.Position = position;
        */
    }
}
