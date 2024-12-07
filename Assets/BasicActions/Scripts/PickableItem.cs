using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Item, IPickable
{
    [SerializeField] private InGameMaterialSO inGameMaterialSO;
    public Component Component => this;

    public InGameMaterialSO Pick()
    {
        return inGameMaterialSO;
    }
}