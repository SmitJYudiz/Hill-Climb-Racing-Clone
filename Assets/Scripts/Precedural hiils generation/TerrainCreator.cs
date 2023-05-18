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


    // Start is called before the first frame update
    void Start()
    {
        shape = GetComponent<SpriteShapeController>();      

        currentlyLastAddedPoint = 0;

        ExtendNewGeneratedPath();
    }

    [EasyButtons.Button]
    public void ExtendNewGeneratedPath()
    {
        LevelExtendedCounter++;

        currentlyLastAddedPoint = shape.spline.GetPointCount();

        scale = Mathf.CeilToInt(numOfPointsInEachSet * distanceBetweenPoints);
        shape.spline.SetPosition(currentlyLastAddedPoint - 2, shape.spline.GetPosition(currentlyLastAddedPoint - 2) + Vector3.right * scale);
        shape.spline.SetPosition(currentlyLastAddedPoint - 1, shape.spline.GetPosition(currentlyLastAddedPoint - 1) + Vector3.right * scale);

        for (int i = (LevelExtendedCounter-1)*numOfPointsInEachSet; i < LevelExtendedCounter *numOfPointsInEachSet; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBetweenPoints;

            if (i < 5)
            {
                shape.spline.InsertPointAt(i + 2, new Vector3(xPos, shape.spline.GetPosition(i + 1).y, 0));
            }
            else
            {
                shape.spline.InsertPointAt(i + 2, new Vector3(xPos, 10 * Mathf.PerlinNoise(i * Random.Range(5f, 15f), 0)));
            }
            //else if(i<10)
            //{
               
            //}
            //else
            //{
            //    shape.spline.InsertPointAt(i + 2, new Vector3(xPos, 15 * Mathf.PerlinNoise(i * Random.Range(5f, 15f), 0)));
            //}
            fuelCollectiblesManager.GenerateFuelCollectible(shape.spline.GetPosition(i + 2));
        }

        for (int i = ((LevelExtendedCounter - 1) * numOfPointsInEachSet) + 2; i < (LevelExtendedCounter * numOfPointsInEachSet) +2; i++)
        {         
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-2, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(2, 0, 0));
        }
        setEndTrigger.transform.position = shape.spline.GetPosition(shape.spline.GetPointCount()-6);
    }

    public void CreatePitFall()
    {
        //before end of each set, we will create a pitfall
        //        

        

        float xPos = shape.spline.GetPosition(GetCurrentPointCount()).x + distanceBetweenPoints;

        shape.spline.InsertPointAt(GetCurrentPointCount(), new Vector3(xPos, shape.spline.GetPosition(GetCurrentPointCount()).y, 0));
        shape.spline.SetTangentMode(GetCurrentPointCount(), ShapeTangentMode.Continuous);



        shape.spline.InsertPointAt(GetCurrentPointCount(), new Vector3(xPos, shape.spline.GetPosition(GetCurrentPointCount()).y - 5, 0));

        shape.spline.InsertPointAt(GetCurrentPointCount(), new Vector3(xPos, shape.spline.GetPosition(GetCurrentPointCount()).y, 0));
    }


    int GetCurrentPointCount()
    {
        return shape.spline.GetPointCount();
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
