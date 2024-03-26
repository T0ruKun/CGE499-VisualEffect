using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Learning.Personal{
	public class ForceFieldController : MonoBehaviour{
		public string targetTag;
		public GameObject ShieldRipple;
		private VisualEffect ShieldRippleVFX;

		private void OnCollisionEnter(Collision other){
			if (other.gameObject.CompareTag(targetTag)){
				var ripple = Instantiate(ShieldRipple, transform);
				ShieldRippleVFX = ripple.GetComponent<VisualEffect>();
				ShieldRippleVFX.SetVector3("FieldCenter", other.contacts[0].point);
            
				Destroy(ripple, 2);
			}
		}
	}
}
