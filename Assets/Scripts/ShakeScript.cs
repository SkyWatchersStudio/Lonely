using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeScript : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float amp;
    [SerializeField] private float frequency;

    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private float timer = 0.0f;
    private bool shake = false;

    private void Start()
    {
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if(timer > 0 && shake == true)
        {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0 && shake == true)
        {
            idle();
        }
    }

    public void shaker()
    {
        virtualCameraNoise.m_AmplitudeGain = amp;
        virtualCameraNoise.m_FrequencyGain = frequency;
        timer = duration;
        shake = true;
    }

    private void idle()
    {
        virtualCameraNoise.m_AmplitudeGain = 0.0f;
        virtualCameraNoise.m_FrequencyGain = 0.0f;
        timer = 0.0f;
        shake = false;
    }
}
