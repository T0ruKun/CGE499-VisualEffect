using Cinemachine;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public sealed class SimpleParticleStage : MonoBehaviour{
    [SerializeField]
    private Locator particleA;
    [SerializeField]
    private Locator particleB;
    [SerializeField]
    private Locator particleC;
    [SerializeField]
    private Locator particleD;
    [SerializeField]
    private Locator particleE;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private Volume volume;

    private Bloom               bloom;
    private DepthOfField        depthOfField;
    private ChromaticAberration chromaticAberration;

    private readonly Vector3 FIXED_POS_1 = new (0.3f, 0, 34);
    private readonly Vector3 FIXED_POS_2 = new (26.8f, 0, 15.1f);
    private readonly Vector3 FIXED_POS_3 = new (8.5f, 0, -30.06f);
    private readonly Vector3 FIXED_POS_4 = new (-23.5f, 0, -27.8f);
    private readonly Vector3 FIXED_POS_5 = new (-21.62f, 0, 21.74f);

    // Start is called before the first frame update
    private void Start(){
        if (!volume.profile.TryGet(out bloom)){
            Debug.LogError("Bloom component not found in the URP Volume.");
            return;
        }
        
        if (!volume.profile.TryGet(out depthOfField)){
            Debug.LogError("DepthOfField component not found in the URP Volume.");
            return;
        }
        
        if (!volume.profile.TryGet(out chromaticAberration)){
            Debug.LogError("ChromaticAberration component not found in the URP Volume.");
            return;
        }
        
        particleA.parent.transform.position = FIXED_POS_1;
        particleB.parent.transform.position = FIXED_POS_2;
        particleC.parent.transform.position = FIXED_POS_3;
        particleD.parent.transform.position = FIXED_POS_4;
        particleE.parent.transform.position = FIXED_POS_5;

        StartCoroutine(DoSchedule());
    }
    
    private IEnumerator DoSchedule(){
        ChangeChromaticAberration(0, 0.65f, Ease.Flash);
        // Locator1
        virtualCamera.LookAt = particleA.parent;
        yield return new WaitForSeconds(2);
        StartCoroutine(particleA.PlayParticle());
        ChangeBloom(particleA.bloomIntensity, particleA.bloomTransitionTime, particleA.bloomEase);
        ChangeDepthOfField(particleA.depthOfFieldDistance, particleA.depthOfFieldTransitionTime, particleA.depthOfFieldEase);
        ChangeChromaticAberration(particleA.chromaticAberrationIntensity, particleA.chromaticAberrationTransitionTime, particleA.chromaticAberrationEase);
        yield return new WaitForSeconds(10);
        // Locator2
        virtualCamera.LookAt = particleB.parent;
        yield return new WaitForSeconds(2);
        StartCoroutine(particleB.PlayParticle());
        ChangeBloom(particleB.bloomIntensity, particleB.bloomTransitionTime, particleB.bloomEase);
        ChangeDepthOfField(particleB.depthOfFieldDistance, particleB.depthOfFieldTransitionTime, particleB.depthOfFieldEase);
        ChangeChromaticAberration(particleB.chromaticAberrationIntensity, particleB.chromaticAberrationTransitionTime, particleB.chromaticAberrationEase);
        yield return new WaitForSeconds(10);
        // Locator3
        virtualCamera.LookAt = particleC.parent;
        yield return new WaitForSeconds(2);
        StartCoroutine(particleC.PlayParticle());
        ChangeBloom(particleC.bloomIntensity, particleC.bloomTransitionTime, particleC.bloomEase);
        ChangeDepthOfField(particleC.depthOfFieldDistance, particleC.depthOfFieldTransitionTime, particleC.depthOfFieldEase);
        ChangeChromaticAberration(particleC.chromaticAberrationIntensity, particleC.chromaticAberrationTransitionTime, particleC.chromaticAberrationEase);
        yield return new WaitForSeconds(10);
        // Locator4
        virtualCamera.LookAt = particleD.parent;
        yield return new WaitForSeconds(2);
        StartCoroutine(particleD.PlayParticle());
        ChangeBloom(particleD.bloomIntensity, particleD.bloomTransitionTime, particleD.bloomEase);
        ChangeDepthOfField(particleD.depthOfFieldDistance, particleD.depthOfFieldTransitionTime, particleD.depthOfFieldEase);
        ChangeChromaticAberration(particleD.chromaticAberrationIntensity, particleD.chromaticAberrationTransitionTime, particleD.chromaticAberrationEase);
        yield return new WaitForSeconds(10);
        // Locator5
        virtualCamera.LookAt = particleE.parent;
        yield return new WaitForSeconds(2);
        StartCoroutine(particleE.PlayParticle());
        ChangeBloom(particleE.bloomIntensity, particleE.bloomTransitionTime, particleE.bloomEase);
        ChangeDepthOfField(particleE.depthOfFieldDistance, particleE.depthOfFieldTransitionTime, particleE.depthOfFieldEase);
        ChangeChromaticAberration(particleE.chromaticAberrationIntensity, particleE.chromaticAberrationTransitionTime, particleE.chromaticAberrationEase);
    }

    private void ChangeBloom(float intensity, float duration, Ease ease){
        DOTween.To(() => bloom.intensity.value, 
                x => bloom.intensity.value = x, 
                intensity,
                duration)
            .SetEase(ease);
    }
    
    private void ChangeDepthOfField(float gaussianEnd, float duration, Ease ease){
        DOTween.To(() => depthOfField.gaussianStart.value, 
                x => depthOfField.gaussianStart.value = x, 
                gaussianEnd,
                duration)
            .SetEase(ease);
    }
    
    private void ChangeChromaticAberration(float intensity, float duration, Ease ease){
        DOTween.To(() => chromaticAberration.intensity.value, 
                x => chromaticAberration.intensity.value = x, 
                intensity,
                duration)
            .SetEase(ease);
    }
}
