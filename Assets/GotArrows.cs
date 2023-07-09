using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotArrows : MonoBehaviour
{
    public GameObject go, hero;
    public CameraFadeMain cfm;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        cfm = go.GetComponent<CameraFadeMain>();
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(hero.transform.position,transform.position) < 60.0f && done == false)
        {
            if (GameObject.FindGameObjectsWithTag("Hero").Length == 0)
            {
                cfm.beenToDungeon = true;
                cfm.forceFade = true;
                done = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //cfm.beenToDungeon = true;
    }
}
