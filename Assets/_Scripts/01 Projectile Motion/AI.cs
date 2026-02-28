using System.Net;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] Transform startPoint = null, endPoint = null;
    [SerializeField] int angle = 0, trajectory_num = 100;
    [SerializeField] float v = 0;
    float angle_Rad = 0;
    [SerializeField] float configDistance = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmosSelected()
    {
        calV();

        Gizmos.color = Color.blue;
        for (int i = 0; i < trajectory_num; i++)
        {
            float time = i * configDistance;
            float X = Mathf.Cos(angle_Rad) * v * time;
            float Y = Mathf.Sin(angle_Rad) * v * time - 0.5f * (10 * time * time);
            Vector3 pos1 = startPoint.transform.position + new Vector3(X, Y, 0);
            time = (i + 1) * configDistance;
            X = Mathf.Cos(angle_Rad) * v * time;
            Y = Mathf.Sin(angle_Rad) * v * time - 0.5f * (10 * time * time);
            Vector3 pos2 = startPoint.transform.position + new Vector3(X, Y, 0);
            Gizmos.DrawLine(pos1, pos2);
        }
    }
    private void calV()
    {
        float g = 9.81f;

        float Y = endPoint.position.y - startPoint.position.y;
        float X = endPoint.position.x - startPoint.position.x;

        if (X < 0)
        {
            angle_Rad = -Mathf.Abs(angle) * Mathf.Deg2Rad;
            configDistance = -Mathf.Abs(configDistance);
        }
        else
        {
            angle_Rad = Mathf.Abs(angle) * Mathf.Deg2Rad;
            configDistance = Mathf.Abs(configDistance);
        }

        float denominator = 2 * Mathf.Pow(Mathf.Cos(angle_Rad), 2) *
                            (X * Mathf.Tan(angle_Rad) - Y);

        if (Mathf.Abs(denominator) < 0.0001f)
            return;

        float v2 = (g * X * X) / denominator;

        v = Mathf.Sqrt(Mathf.Abs(v2));
    }
}
