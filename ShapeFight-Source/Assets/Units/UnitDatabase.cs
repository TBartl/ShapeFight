using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum UnitType
{
    baseStructure, sphere, octahedron, cube, porcupine, pyramid, katamari
}

public class UnitDatabase : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public static UnitDatabase inst;

    void Awake()
    {
        inst = this;
    }

    public GameObject Get(UnitType type)
    {
        return gameObjects[(int)type];
    }
}
