using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    [SerializeField] private Material _material;
    float shaderAmt = 0f;
    //public Shader _shader;


    // Start is called before the first frame update
    void Start()
    {
        //_material = new Material(_shader);
    }

    private void Update()
    {
        //_material.SetFloat("_GrayscaleAmount", 0.5f);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //_material.SetFloat("_GrayscaleAmount", 1.0f);
        if (PublicVars.xp <= PublicVars.xp_for_green_forest)
        {
            shaderAmt = (1f - ((float)(PublicVars.xp) / (float)(PublicVars.xp_for_green_forest)));
        }
        else
        {
            shaderAmt = 0f;
        }
        //float shaderAmt = (1f - ((float)(PublicVars.xp) / (float)(PublicVars.xp_for_green_forest)));
        _material.SetFloat("_GrayscaleAmount", shaderAmt);
        Graphics.Blit(source, destination, _material);
        
    }
}
