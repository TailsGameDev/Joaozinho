using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prototypesToClone = null;

    [SerializeField] private Transform helpSpawnPoint = null;

    private void Start()
    {
        SpawnManager.Instance.RegisterOnWaveChangedAction(SpawnHelpObject);
    }
    private void SpawnHelpObject()
    {
        GameObject prototypeToClone = prototypesToClone[Random.Range(0, prototypesToClone.Length)];
        GameObject instantiated = Instantiate(prototypeToClone, helpSpawnPoint.position, Quaternion.identity);
        instantiated.gameObject.SetActive(true);
    }
}
