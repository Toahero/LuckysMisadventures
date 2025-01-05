using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fateinator : MonoBehaviour
{
    public fateSO fateScriptable;
    public bool newGame;
    
    

    // Start is called before the first frame update
    void Start()
    {
		
		//uncomment this to create a new fate scriptable object each time
        //fateScriptable = ScriptableObject.CreateInstance<fateSO>();
        //fateScriptable.name = "FateCards";
        fateScriptable.initFate();

        if (newGame)
        {
            fateScriptable.generateDeck();
        }

    }

    public void feedTheToad()
    {
        fateScriptable.feedTheToad();
    }
}
