using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    [SerializeField]
    private Image bar;

    TransformWrapper barTransform;

    private void Awake()
    {
        barTransform = new TransformWrapper(bar.transform);
    }

    // Update is called once per frame
    private void Update()
    {
        Damageable player = Player.Instance.Damageable;
        Vector3 scale = barTransform.LocalScale;
        scale.x = player.CurrentLife / player.MaxLife;
        barTransform.LocalScale = scale;
    }
}
