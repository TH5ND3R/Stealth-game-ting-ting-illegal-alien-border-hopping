using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMath : MonoBehaviour
{

    [SerializeField] Transform vect1;
    [SerializeField] Transform vect2;

    [SerializeField] Vector2 vector1;
    [SerializeField] Vector2 vector2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vect1.position = vector1.normalized;
        vect2.position = vector2;

        Debug.DrawLine(vector1.normalized, vector1.normalized * 2);
        Debug.DrawLine(vector2, vector2 + (vector1.normalized - vector2).normalized);
        
        float dotResult;
        dotResult = Vector3.Dot(vector1.normalized, (vector1.normalized - vector2).normalized);
        dotResult = dotResult * dotResult;// * Mathf.Sign(dotResult);
        print(dotResult);
        print(90 - dotResult * 90 < 45 ? "bagved" : "ikke bagved");

        print(Mathf.Atan2(vector1.y, vector1.x) * Mathf.Rad2Deg);
    }
}
