using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHighlight : MonoBehaviour
{
    private float _timer;

    private static GameObject lastHighlight = null;

    // Update is called once per frame
    void Update()
    {
        this._timer -= Time.deltaTime;
        if(this._timer <= 0f)
        {
            //Debug.Log("Highlight timed out");
            this.gameObject.SetActive(false);
        }
    }

    // Refreshes the highlight - it will fade if not refreshed
    public void TriggerHighlight()
    {    
        if(lastHighlight != null && lastHighlight != this.gameObject)
        {
            lastHighlight.SetActive(false);
        } 
        lastHighlight = this.gameObject;

        if(!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
        this._timer = .4f;
    }

}
