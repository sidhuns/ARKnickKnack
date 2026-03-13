using UnityEngine;
using System;
using TMPro;
public class timecont : MonoBehaviour
{
    [SerializeField]
    public TMP_Text obj;

    void Start()
    {
        obj.text = System.DateTime.Now.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
