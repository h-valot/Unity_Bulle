using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Interactible : MonoBehaviour
{

    [Title("External References")]
    [SerializeField] private RSO_CurrentItem _currentItem;

    [SerializeField] private InteractType _type;
    
    [ShowIf("_type", InteractType.TRAVEL)]
    [SerializeField] private PanelType _destination;

    [ShowIf("_type", InteractType.SPAWN)]
    [SerializeField] private ItemType _item;

    [ShowIf("_type", InteractType.PLACE)]
    [SerializeField] private Transform _placePosition;

    
    
}
