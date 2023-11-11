using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physicalButton;

    private void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        GameObject mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter");
    }

    private void AttachCallback(string buttonName)
    {
        if(buttonName.CompareTo("Skill1") == 0 )
        {

        }
        else if(buttonName.CompareTo("Skill2") == 0)
        {

        }
        else
        {

        }
    }
}
