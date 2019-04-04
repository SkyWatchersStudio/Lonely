using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float viewZone = 10;
    public float parallaxSpeed = 10;

    protected Transform[] layers;
    protected float[] imageDistance;
    protected int rightIndex;
    protected int leftIndex;
    private Transform cameraTransfrom;
    private float lastCameraX;

    private void Awake()
    {
        cameraTransfrom = Camera.main.transform;
        lastCameraX = cameraTransfrom.position.x;

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }
    private void Start()
    {
        layers = new Transform[transform.childCount - 1];
        int childCounter = 1;
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = transform.GetChild(childCounter); 
            childCounter++;
        }

        imageDistance = new float[transform.childCount - 1];
        for (int i = 0; i < imageDistance.Length; i++)
            imageDistance[i] = transform.GetChild(i + 1).position.x - transform.GetChild(i).position.x;
    }
    private void Update()
    {
        float deltaX = cameraTransfrom.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * parallaxSpeed);
    }
    private void LateUpdate() 
    {
        lastCameraX = cameraTransfrom.position.x;
        
        if (cameraTransfrom.position.x > layers[rightIndex].position.x - viewZone)
            ScrollRight();
        else if (cameraTransfrom.position.x < layers[leftIndex].position.x + viewZone)
            ScrollLeft();
    }
    protected virtual void ScrollRight()
    {
        layers[leftIndex].position =
                Vector2.right * (layers[rightIndex].position.x + imageDistance[leftIndex]);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
    protected virtual void ScrollLeft()
    {
        layers[rightIndex].position =
                Vector2.right * (layers[leftIndex].position.x - imageDistance[rightIndex]);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }
}
