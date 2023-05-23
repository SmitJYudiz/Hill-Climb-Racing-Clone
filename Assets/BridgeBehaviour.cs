using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BridgeBehaviour : MonoBehaviour
{
    [SerializeField] StepBehaviour Step;

    [SerializeField] GameObject startObject;
    [SerializeField] GameObject endObject;

    [SerializeField] List<Rigidbody2D> allRBOfBridge;


    int numberOfLoopsStepsToGenerate;


    //it should be same as width in pixels of the image used for bridge step divided by 100: as currently it is 64px: then gap is 0.64
    float stepWidth = 0.64f;


    [SerializeField] GameObject startObjectPrefab;
    [SerializeField] GameObject endObjectPrefab;

    private void Start()
    {
        allRBOfBridge = new List<Rigidbody2D>();

        //CreateBridge(startObject.transform.position, endObject.transform.position);        
    }


    [SerializeField]
    Vector3 bridgeStartPosition;

    [SerializeField]
    Vector3 bridgeEndPosition;


    Vector3 positionCounter = new Vector3();


    [EasyButtons.Button]
    public void TestBridge()
    {
        allRBOfBridge.Clear();
        foreach(Transform t in transform)
        {
            if(t.GetSiblingIndex()>1)
            {
                Destroy(t.gameObject);
            }
        }

        CreateBridge(startObject.transform.position, endObject.transform.position);
    }



    public  void CreateBridge(Vector3 startPosition, Vector3 endPosition)
    {
        startObject.transform.position = startPosition;
        endObject.transform.position = endPosition;

        
        StepBehaviour initialStep = Instantiate(Step, transform);
     
        initialStep.SetConnectedRB(startObject.GetComponent<Rigidbody2D>());

        initialStep.transform.position = startObject.transform.position + new Vector3(0.66f, 0, 0);

        positionCounter = initialStep.transform.position;
        
        allRBOfBridge.Add(initialStep.gameObject.GetComponent<Rigidbody2D>());
      
        for (int i=1; i<=numberOfLoopsStepsToGenerate; i++)
        {           
            StepBehaviour loopStep = Instantiate(Step, transform);

            loopStep.MyRB.bodyType = RigidbodyType2D.Static;

            loopStep.gameObject.transform.position = positionCounter + new Vector3(stepWidth, 0, 0);

            positionCounter = loopStep.gameObject.transform.position;

            loopStep.SetConnectedRB(allRBOfBridge[i - 1]);
            allRBOfBridge.Add(loopStep.MyRB);        
        }
        
        endObject.gameObject.GetComponent<HingeJoint2D>().connectedBody = allRBOfBridge[allRBOfBridge.Count-1];

        Debug.Log("Distance Between each step: "+  (allRBOfBridge[allRBOfBridge.Count-3].transform.position.x- allRBOfBridge[allRBOfBridge.Count - 4].transform.position.x));


        //foreach (Rigidbody2D item in allRBOfBridge)
        //{
        //    item.bodyType = RigidbodyType2D.Dynamic;
        //}
    }

    [EasyButtons.Button]
    public void CreateDynamicBridge(Vector3 startPos, Vector3 endPos)
    {

        allRBOfBridge.Clear();


        //startObject.transform.position = bridgeStartPosition;
        //endObject.transform.position = bridgeEndPosition;
        //startObject.transform.position = new Vector3(startPos.x + 0.5f, startPos.y - 0.5f, 0);
        //endObject.transform.position = new Vector3(endPos.x - 0.5f, endPos.y - 0.5f, 0);

        startObject = Instantiate(startObjectPrefab, transform);
        endObject = Instantiate(endObjectPrefab, transform);


        startObject.transform.position = new Vector3(startPos.x, startPos.y - 0.2f , 0);
        endObject.transform.position = new Vector3(endPos.x, endPos.y-0.2f, 0);

        StepBehaviour initialStep = Instantiate(Step, transform);

        initialStep.SetConnectedRB(startObject.GetComponent<Rigidbody2D>());

        initialStep.transform.position = startObject.transform.position + new Vector3(stepWidth, 0, 0);

        positionCounter = initialStep.transform.position;

        allRBOfBridge.Add(initialStep.gameObject.GetComponent<Rigidbody2D>());


        //Debug.Log("startPos: " + startObject.transform.position.x);
        //Debug.Log("endPosX: "+endObject.transform.position.x);

        //numberOfLoopsStepsToGenerate = Mathf.FloorToInt( ((endObject.transform.position.x - startObject.transform.position.x)/stepWidth) -1);
        numberOfLoopsStepsToGenerate = Mathf.FloorToInt(( Vector3.Distance(startObject.transform.position,endObject.transform.position) / stepWidth)-1);

        for (int i = 1; i <= numberOfLoopsStepsToGenerate; i++)
        {
            StepBehaviour loopStep = Instantiate(Step, transform);

            loopStep.MyRB.bodyType = RigidbodyType2D.Static;

            loopStep.gameObject.transform.position = positionCounter + new Vector3(stepWidth, 0, 0);

            positionCounter = loopStep.gameObject.transform.position;

            loopStep.SetConnectedRB(allRBOfBridge[i - 1]);
            allRBOfBridge.Add(loopStep.MyRB);
        }

        endObject.gameObject.GetComponent<HingeJoint2D>().connectedBody = allRBOfBridge[allRBOfBridge.Count - 1];

        foreach (Rigidbody2D item in allRBOfBridge)
        {
            item.bodyType = RigidbodyType2D.Dynamic;
        }

    }
}






//73-47 = 26 units in x: distance
//for that we had to put 38 steps
//with distance between each step = 0.66f

//original distance between 2 steps = 0.66f - 0.5f (as pivot in center and scale of step is 1: so 0.5f is for half step length) = 0.16f

//fixed things are: distance between steps = 0.66f

//26 (mainDistance) = n(number of steps:38) * 1

// (0.66*(38+1)

// 38 (number of steps to generate) =    Mathf.FloorToInt( 26{distance between two bridge end points - from sprite shape} /  0.66(distance between each step) - 1 );