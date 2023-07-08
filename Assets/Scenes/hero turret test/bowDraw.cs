using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class heroAim : MonoBehaviour
{
public bool bowDrawn;
public GameObject bowDrawStart;
public GameObject bowDrawEnd;
public GameObject spawnArrowPosition;
public GameObject arrowTarget;
[SerializeField] private Transform pfArrow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aimDir = (arrowTarget.transform.position - spawnArrowPosition.transform.position).normalized;

        if (bowDrawn)
        {
           // var pullDistance = Vector3.Distance(bowDrawStart.transform.position, bowDraw)
            //transform.position += Vector3.back * (Time.deltaTime * 1f);
            transform.position = Vector3.Lerp(transform.position, bowDrawEnd.transform.position, Time.deltaTime * 20f);
            
            if(transform.position == bowDrawEnd.transform.position)
            {
                Instantiate(pfArrow, spawnArrowPosition.transform.position, Quaternion.LookRotation(aimDir,Vector3.up));
                bowDrawn = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, bowDrawStart.transform.position, Time.deltaTime * 20f);
            if(transform.position == bowDrawStart.transform.position)
            {
                bowDrawn = true;
            }
            
        }
        
    }
}
