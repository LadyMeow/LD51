using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameValueLabels : MonoBehaviour
{
    public TextMeshProUGUI LabelName;
    public TextMeshProUGUI LabelValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLables(string name, string value)
    {
        LabelName.text = name;
        LabelValue.text = value;
    }
}
