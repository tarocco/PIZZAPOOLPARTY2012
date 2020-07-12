using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFontNearest : MonoBehaviour
{
    [SerializeField]
    public Font Font;
    void Start()
    {
        var mat = Font.material;
        var txtr = mat.mainTexture;
        txtr.filterMode = FilterMode.Point;
    }
}
