using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public int cardID = 0;
    
    public void cardClicked()
    {
        GetComponentInParent<DisplayPanel>().cardClicked(cardID);
    }
}
