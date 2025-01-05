using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fateDisplayinator : MonoBehaviour
{
    public fateCardDisplay[] fateDisplays;

    public fateSO fateData;

    public void newFate()
    {
        for(int i = 0; i < 3; i++)
        {
            string nextCard = fateData.getFateID(i);
            fateDisplays[i].newFateCard(nextCard);
        }
    }
}
