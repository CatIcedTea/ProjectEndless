using TMPro;
using UnityEngine;

public class TextPointFilter : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().fontSharedMaterial.mainTexture.filterMode = FilterMode.Point;
    }
}
