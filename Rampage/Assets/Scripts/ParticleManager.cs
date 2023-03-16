using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
  public ParticleSystem punchParticle;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  /*
      Be sure to set each particle system's "Stop Action" variable to "Destroy"
      so that the removal of the particle system is based off its end of 
      durations
  */

  public void SpawnPunchParticles(Vector3 hitPosition, Vector3 hitNormal)
  {
    ParticleSystem particles = Instantiate(punchParticle);
    particles.transform.position = hitPosition;
    particles.transform.rotation = Quaternion.FromToRotation(particles.transform.forward, hitNormal);
  }
}
