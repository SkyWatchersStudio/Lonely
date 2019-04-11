using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float viewZone = 10;
    public float parallaxSpeed = -10;

    private Transform[] layers;
    private float[] imageDistance;
    private int rightIndex;
    private int leftIndex;
    private Transform cameraTransfrom;
    private float lastCameraX;

    private void Start()
    {
        cameraTransfrom = Camera.main.transform;
        //save the last position of the camera
        lastCameraX = cameraTransfrom.position.x;

        //get the layers in child and fixed position of the layers next to each other
        GetBackgroundsChilds();

        //left and right indexes used to know which position we are working with in layers
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }
    private void GetBackgroundsChilds()
    {
        /*it ignore first image in the child because its only use for getting the fixed image distance
         *for example if we have 3 image in the child it parallax 2 of them and use first one for
         *getting the distance and fix it to second one*/
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
        //use camera transition to move backgrounds that make background look you are moving
        float deltaX = cameraTransfrom.position.x - lastCameraX;
        //make a 3D visual. trigger lever are going to change the speed
        transform.position += Vector3.right * (deltaX * parallaxSpeed);
    }
    private void LateUpdate()
    {
        lastCameraX = cameraTransfrom.position.x;

        //if camera view reached a certain point with the background in the right of screen
        //scroll right if opposit scroll left
        if (cameraTransfrom.position.x > layers[rightIndex].position.x - viewZone)
            ScrollRight();
        else if (cameraTransfrom.position.x < layers[leftIndex].position.x + viewZone)
            ScrollLeft();
    }
    private void ScrollRight()
    {
        //we would translate so it will not change in y position...
        float xTarget = layers[rightIndex].position.x - layers[leftIndex].position.x;
        layers[leftIndex].Translate(new Vector2(xTarget + imageDistance[leftIndex], 0f));

        //the right most image become left most image
        rightIndex = leftIndex;
        //update new positions of the left most image and new right image
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
    private void ScrollLeft()
    {
        //opposite of the scroll right
        float xTarget = layers[leftIndex].position.x - layers[rightIndex].position.x;
        layers[rightIndex].Translate(new Vector2(xTarget - imageDistance[rightIndex], 0f));

        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }
}
