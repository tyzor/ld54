using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public int volts;
    public bool isOn = false;

    [SerializeField]
    private TextMeshProUGUI voltLabel;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        this.isOn = _animator.GetBool("IsOn");
        
    }


    public void InitLever(int volts, bool state = false)
    {
        this.volts = volts;
        voltLabel.SetText(volts.ToString() + " V");
        this.isOn = state;
    }

    public void ToggleLever()
    {
        this.isOn = !this.isOn;
        _animator.SetBool("IsOn", this.isOn);
        
        if(this.isOn)
            voltLabel.color = Color.green;
        else
            voltLabel.color = Color.white;
    }

}
