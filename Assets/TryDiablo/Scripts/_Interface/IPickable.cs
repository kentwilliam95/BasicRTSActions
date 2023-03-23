using UnityEngine;

public interface IPickable
{
    Component Component{get;}
    InGameMaterialSO Pick();
}
