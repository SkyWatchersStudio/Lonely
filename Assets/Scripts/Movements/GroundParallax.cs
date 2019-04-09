using UnityEngine;

public sealed class GroundParallax : ParallaxBackground
{
    protected override void ScrollRight()
    {
        float xTarget = layers[rightIndex].position.x - layers[leftIndex].position.x;
        layers[leftIndex].Translate(new Vector2(xTarget + imageDistance[leftIndex], 0f));
        
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
    protected override void ScrollLeft()
    {
        float xTarget = layers[leftIndex].position.x - layers[rightIndex].position.x;
        layers[rightIndex].Translate(new Vector2(xTarget - imageDistance[rightIndex], 0f));
        
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }
}
