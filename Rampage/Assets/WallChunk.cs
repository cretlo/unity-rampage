using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChunk : MonoBehaviour
{
  List<GameObject> surroundingWallChunksList;
  List<GameObject> lowerWallChunksList;
  public bool isBroken;


  Rigidbody rb;


  void Await()
  {
    isBroken = false;
    surroundingWallChunksList = new List<GameObject>();
    lowerWallChunksList = new List<GameObject>();
  }

  // Start is called before the first frame update
  void Start()
  {
    rb = this.gameObject.GetComponent<Rigidbody>();

  }

  // Update is called once per frame
  void Update()
  {
    // BreakThisJoints();


  }

  void BreakThisJoints()
  {
    if (!isBroken && transform.localRotation.eulerAngles.x < -10 || transform.localRotation.eulerAngles.x > 10)
    {
      // print("BreakThisJoints Called");
      ConfigurableJoint[] joints = transform.GetComponents<ConfigurableJoint>();

      foreach (ConfigurableJoint joint in joints)
      {
        Destroy(joint);
      }
      isBroken = true;
    }
  }


  void OnTriggerEnter(Collider collider)
  {

    if (collider.transform.parent.tag == "WallChunk")
    {
      AddConfigJoint(collider);
      // surroundingWallChunksList.Add(collider.gameObject);

      // If the collided wall check is below add to below list
      // if (collider.gameObject.transform.position.y < this.transform.position.y)
      // {
      //   lowerWallChunksList.Add(collider.gameObject);
      // }

    }

  }

  void AddConfigJoint(Collider collider)
  {
    ConfigurableJoint configJoint = gameObject.AddComponent<ConfigurableJoint>();
    configJoint.xMotion = ConfigurableJointMotion.Locked;
    configJoint.yMotion = ConfigurableJointMotion.Locked;
    configJoint.zMotion = ConfigurableJointMotion.Locked;
    configJoint.zMotion = ConfigurableJointMotion.Locked;
    configJoint.angularXMotion = ConfigurableJointMotion.Locked;
    configJoint.angularYMotion = ConfigurableJointMotion.Locked;
    configJoint.angularZMotion = ConfigurableJointMotion.Locked;
    configJoint.projectionMode = JointProjectionMode.PositionAndRotation;

    // Test code
    // configJoint.autoConfigureConnectedAnchor = false;

    JointDrive newDrive = configJoint.xDrive;
    newDrive.positionSpring = Mathf.Infinity;

    configJoint.xDrive = newDrive;
    configJoint.yDrive = newDrive;
    configJoint.zDrive = newDrive;
    configJoint.angularXDrive = newDrive;

    configJoint.projectionAngle = 0;
    configJoint.projectionDistance = 0;


    configJoint.connectedBody = collider.GetComponentInParent<Rigidbody>();
    configJoint.breakForce = 300;

  }

  // void OnJointBreak(float breakForce)
  // {
  //   ConfigurableJoint[] thisJoints = this.GetComponents<ConfigurableJoint>();

  //   if (thisJoints.Length == 0)
  //   {
  //     // BreakSurroundingJoints();
  //     isBroken = true;
  //   }
  // }

  public void BreakSurroundingJoints()
  {
    foreach (GameObject wallChunk in surroundingWallChunksList)
    {
      ConfigurableJoint[] joints = wallChunk.GetComponents<ConfigurableJoint>();

      foreach (ConfigurableJoint joint in joints)
      {
        if (joint.connectedBody == rb)
        {
          Destroy(joint);
        }
      }
    }
  }
}

