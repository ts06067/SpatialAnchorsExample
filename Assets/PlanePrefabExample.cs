using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanePrefabExample : MonoBehaviour
{
    private const int GRAY_PLANE_QUEUE = 3001;
    private const int DEFAULT_PLANE_QUEUE = 3000;

    public static int Count { get; private set; } = 0;

    private void Start()
    {
        ColorClassify();
        Count++;
    }

    private void OnDestroy()
    {
        Count--;
    }

    private void ColorClassify()
    {
        var plane = GetComponent<ARPlane>();
        var color = plane.classification switch
        {
            PlaneClassification.Floor => Color.green,
            PlaneClassification.Ceiling => Color.red,
            PlaneClassification.Wall => Color.yellow,
            PlaneClassification.Table => Color.blue,
            _ => Color.gray
        };

        var mat = GetComponent<MeshRenderer>().material;
        mat.color = color;
        mat.renderQueue = color == Color.gray ? GRAY_PLANE_QUEUE : DEFAULT_PLANE_QUEUE;
    }
}
