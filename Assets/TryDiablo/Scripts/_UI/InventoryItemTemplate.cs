using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemTemplate : MonoBehaviour
{
    public Image iconMaterial;
    public TextMeshProUGUI textAmount;

    public void Initialize(Sprite sprite, string materialText)
    {
        iconMaterial.sprite = sprite;
        textAmount.text = materialText;
    }
}