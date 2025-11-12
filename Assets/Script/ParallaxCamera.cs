using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    public Renderer BG;
    public Renderer FG;

    public float BGSpeed = .02f;
    public float FGSpeed = .06f;

    public float offset = 0.0f;

    private void Update()
    {
        float BGOffset = offset * BGSpeed;
        float FGOffset = offset * FGSpeed;

        BG.material.mainTextureOffset = new Vector2(BGOffset, 0);
        FG.material.mainTextureOffset = new Vector2(FGOffset, 0);
    }
}
