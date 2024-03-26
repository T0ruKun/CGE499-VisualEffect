using DG.Tweening;
using UnityEngine;

namespace Learning{
    public sealed class Frostable : MonoBehaviour{
        public float frostDuration;
        
        private static readonly int   FrostProperty = Shader.PropertyToID("_IceSlider");

        public void LerpFrost(bool value){
            var material = gameObject.GetComponent<MeshRenderer>().material;
            
            var startValue = value ? 0f : 1f;
            var endValue = value ? 1f : 0f;
            material.DOFloat(endValue, FrostProperty, frostDuration).From(startValue);
        }
    }
}
