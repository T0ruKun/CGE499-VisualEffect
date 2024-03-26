using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public sealed class Locator{
	public bool           Stop;
	public Transform      parent;
	public ParticleSystem ParticleSystem;
	public float          bloomIntensity;
	public float          bloomTransitionTime = 2f;
	public Ease           bloomEase;
	
	public  float          depthOfFieldDistance;
	public  float          depthOfFieldTransitionTime = 2f;
	public  Ease           depthOfFieldEase;
	
	public  float          chromaticAberrationIntensity;
	public float chromaticAberrationTransitionTime = 2f;
	public Ease chromaticAberrationEase;

	public float               PlayDuration   = 6f;
	public List<LifetimeEvent> LifetimeEvents = new();
	
	public IEnumerator PlayParticle(){
		if (ParticleSystem == null)
			yield break;
		ParticleSystem.Play();

		var i = 0;
		if (LifetimeEvents.Count == 0)
			yield return new WaitForSeconds(PlayDuration);
		else
			while (i < LifetimeEvents.Count){
				yield return new WaitForSeconds(LifetimeEvents[i].delayTimeInSec);
				LifetimeEvents[i].events?.Invoke();
				i++;
			}
		
		
		if (Stop)
			ParticleSystem.Stop();
	}
	

	[Serializable]
	public sealed class LifetimeEvent{
		public float      delayTimeInSec;
		public UnityEvent events;
	}
}