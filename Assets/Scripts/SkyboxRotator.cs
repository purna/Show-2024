using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 1.0f;

    void Update()
    {
        // Rotate the skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
