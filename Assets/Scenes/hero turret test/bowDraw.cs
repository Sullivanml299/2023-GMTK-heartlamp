using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class heroAim : MonoBehaviour
{
    public bool bowDrawn;
    public GameObject bowDrawStart;
    public GameObject bowDrawEnd;
    public GameObject spawnArrowPosition;
    public FindTarget arrowTarget;
    [SerializeField] private Transform pfArrow;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (arrowTarget.target == null)
        {
            return;
        }

        print(arrowTarget.gameObject.tag);
        print(arrowTarget.target.name);
        //print(Vector3.Distance(arrowTarget.transform.position, transform.position));
        var targetPosition = arrowTarget.target.gameObject.tag == "Boss" ? arrowTarget.transform.position + Vector3.up * 8f : arrowTarget.transform.position + Vector3.up * 3f;
        Vector3 aimDir = (targetPosition - spawnArrowPosition.transform.position).normalized;

        if (bowDrawn)
        {
            // var pullDistance = Vector3.Distance(bowDrawStart.transform.position, bowDraw)
            //transform.position += Vector3.back * (Time.deltaTime * 1f);
            transform.position = Vector3.Lerp(transform.position, bowDrawEnd.transform.position, Time.deltaTime * 20f);

            if (transform.position == bowDrawEnd.transform.position && Vector3.Distance(arrowTarget.transform.position, transform.position) < 60f)
            {
                Instantiate(pfArrow, spawnArrowPosition.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
                bowDrawn = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, bowDrawStart.transform.position, Time.deltaTime * 20f);
            if (transform.position == bowDrawStart.transform.position)
            {
                bowDrawn = true;
            }

        }

    }
}
