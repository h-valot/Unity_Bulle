using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;
    [SerializeField] private TMP_Text text;
    [SerializeField] private string dialogue1;
    [SerializeField] private string dialogue2;
    [SerializeField] private bool isSatisfied = false;


    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.ToString());
        if (collision.gameObject.tag == "Player")
        {
            _bubble.SetActive(true);

            if (isSatisfied == true)
            {
                text.text = dialogue2;
            }

            else 
            {
                text.text = dialogue1;
            }
            }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _bubble.SetActive(false);
        }
    }
}
