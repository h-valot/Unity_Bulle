using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    [Title("External References")]
    [SerializeField] private RSO_CurrentItem _currentItem;

    [SerializeField] private InteractibleType _type;
    
    [ShowIf("_type"), InteractibleType.TRAVEL]
    [SerializeField] private PanelType _destination;

    [ShowIf("_type"), InteractibleType.SPAWN]
    [SerializeField] private ItemType _item;

    [ShowIf("_type"), InteractibleType.PLACE]
    [SerializeField] private Transform _placePosition;

    
    
}
