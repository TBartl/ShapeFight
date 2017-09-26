using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour {
    public static ColorManager inst;
    public List<Material> teamColor;
    public Material selectedMat;

    void Awake()
    {
        inst = this;
    }
    public Material Get(TeamID id)
    {
        return teamColor[(int)id];
    }
}
