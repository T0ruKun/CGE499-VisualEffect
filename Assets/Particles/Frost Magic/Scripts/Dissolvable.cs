using DG.Tweening;
using UnityEngine;

public sealed class Dissolvable : MonoBehaviour{
    public float minCutoff;
    public float maxCutoff;
    public float dissolveDuration;
        
    private static readonly int DissolveProperty = Shader.PropertyToID("_Cutoff");
        
    public void LerpDissolve(bool value){
        var particle = gameObject.GetComponent<ParticleSystem>();
        var render = particle.GetComponent<Renderer>();
        var material = render.material;
            
        var startValue = value ? minCutoff : maxCutoff;
        var endValue = value ? maxCutoff : minCutoff;
        material.DOFloat(endValue, DissolveProperty, dissolveDuration).From(startValue);
    }
}