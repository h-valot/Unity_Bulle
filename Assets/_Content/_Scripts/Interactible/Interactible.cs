using UnityEngine;
using Sirenix.OdinInspector;

public class Interactible : MonoBehaviour
{
    [Title("External References")]
    [SerializeField] private RSO_CurrentItem m_currentItem;

    [SerializeField] private InteractType m_type;
    
    [ShowIf("m_type", InteractType.TRAVEL)]
    [SerializeField] private PanelType m_destination;

    [ShowIf("m_type", InteractType.SPAWN)]
    [SerializeField] private ItemType m_item;

    [ShowIf("m_type", InteractType.PLACE)]
    [SerializeField] private Transform m_placePosition;
}