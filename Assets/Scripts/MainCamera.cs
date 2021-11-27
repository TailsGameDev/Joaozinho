using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private TransformWrapper transformWrapper;

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);
    }

    private void Update()
    {
        if (Player.Instance != null)
        {
            transformWrapper.Position = Player.Instance.transform.position - Vector3.forward;
        }
    }
}
