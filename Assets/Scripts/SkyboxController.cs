using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SkyboxController : MonoBehaviour
{
    [SerializeField] private float skyboxRotationSpeed = 1f;
    private Vector2 offset;
      void Update()
    {
       
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotationSpeed);
        
    }
}
