using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class TreeRandomizer : MonoBehaviour
{
    public Transform tree;

    [ContextMenu("Randomize")]
    void Randomize()
    {
        tree.localScale = new Vector3(Random.Range(5f, 7f), Random.Range(5f, 7f), Random.Range(5f, 10f));
        tree.Rotate(0, 0, Random.Range(0f, 180f), Space.Self);
    }


}
