using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadAsset : MonoBehaviour {
    [ContextMenu("Test")]
    void Test(){
        var iconPath = "Icons/Plane_icon";
        var asset = Resources.Load<Sprite>(iconPath);
        GetComponent<Image>().sprite = asset;
    }
}
