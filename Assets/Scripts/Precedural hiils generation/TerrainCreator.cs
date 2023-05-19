using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainCreator : MonoBehaviour
{
    public SpriteShapeController shape;
    public int scale = 1000;

    public int numOfPointsInEachSet = 20;

    float distanceBetweenPoints = 7;

    [SerializeField] FuelCollectiblesManager fuelCollectiblesManager;

    int currentlyLastAddedPoint;

    int LevelExtendedCounter;

    //Set End Trigger Box collider: we have to reposition it on x axis, when it is triggered and the the level is extended, now it should be at end of the newly extended part.
    [SerializeField] GameObject setEndTrigger;

    [SerializeField] BridgeBehaviour ourBridge;



    // Start is called before the first frame update
    void Start()
    {
        shape = GetComponent<SpriteShapeController>();

        currentlyLastAddedPoint = 0;

        ExtendNewGeneratedPath();
    }

    Vector3 startPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;

    [EasyButtons.Button]
    public void ExtendNewGeneratedPath()
    {
        LevelExtendedCounter++;

        currentlyLastAddedPoint = shape.spline.GetPointCount();

        scale = Mathf.CeilToInt(numOfPointsInEachSet * distanceBetweenPoints);
        shape.spline.SetPosition(currentlyLastAddedPoint - 2, shape.spline.GetPosition(currentlyLastAddedPoint - 2) + Vector3.right * scale);
        shape.spline.SetPosition(currentlyLastAddedPoint - 1, shape.spline.GetPosition(currentlyLastAddedPoint - 1) + Vector3.right * scale);


        //Vector3 startX = Vector3.zero;

        for (int i = (LevelExtendedCounter - 1) * numOfPointsInEachSet; i < LevelExtendedCounter * numOfPointsInEachSet; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBetweenPoints;

            if (i < 5)
            {              
                shape.spline.InsertPointAt(i + 2, new Vector3(xPos, shape.spline.GetPosition(i + 1).y, 0));
            }
            else if (i % 10 == 0)
            {               
                CreatePitFall(i);
            }
            else if((((i % 10)) == 1) )
            {
                shape.spline.InsertPointAt(i + 2, new Vector3(xPos, shape.spline.GetPosition(i).y, 0));
            }
          
            else
            {               
                shape.spline.InsertPointAt(i + 2, new Vector3(xPos, 10 * Mathf.PerlinNoise(i * Random.Range(5f, 15f), 0)));
            }

            

            if (((i % 10)) == 9)
            {
                startPos = shape.spline.GetPosition(i + 2);
            }

            if (((i % 10)) == 1)
            {
                endPos = shape.spline.GetPosition(i + 2);

                if (startPos != Vector3.zero && endPos != Vector3.zero)
                {

                    ourBridge.CreateDynamicBridge(startPos, endPos);
                }
            }        

            fuelCollectiblesManager.GenerateFuelCollectible(shape.spline.GetPosition(i + 2));
        }

        for (int i = ((LevelExtendedCounter - 1) * numOfPointsInEachSet) + 2; i < (LevelExtendedCounter * numOfPointsInEachSet) + 2; i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-2, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(2, 0, 0));
        }
        setEndTrigger.transform.position = shape.spline.GetPosition(shape.spline.GetPointCount() - 6);
    }

    public void CreatePitFall(int iteratorNum)
    {
        float xPos = shape.spline.GetPosition(iteratorNum + 1).x + distanceBetweenPoints;
        shape.spline.InsertPointAt(iteratorNum + 2, new Vector3(xPos, shape.spline.GetPosition(iteratorNum + 1).y - 10, 0));
    }


    void ExtendOldGeneratedPath()
    {
        scale = Mathf.CeilToInt(numOfPointsInEachSet * distanceBetweenPoints);
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * scale);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * scale);

        for (int i = 0; i < 150; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBetweenPoints;

            if (i < 5 || i > 145)
            {
                shape.spline.InsertPointAt(i + 2, new Vector3(xPos, shape.spline.GetPosition(i + 1).y, 0));
            }
            else
            {
                shape.spline.InsertPointAt(i + 2, new Vector3(xPos, 10 * Mathf.PerlinNoise(i * Random.Range(5f, 15f), 0)));
            }
            fuelCollectiblesManager.GenerateFuelCollectible(shape.spline.GetPosition(i + 2));

        }

        for (int i = 2; i < 152; i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-2, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(2, 0, 0));
        }
    }
}
