using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChunkV2 : MonoBehaviour
{
  public List<GameObject> surroundingWallChunksList = new List<GameObject>();


  Rigidbody rb;


  void Await()
  {
    // surroundingWallChunksList = new List<GameObject>();
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

  public bool AttachConfigJoint()
  {
    for (int i = 0; i < surroundingWallChunksList.Count; i++)
    {
      // See if the wall chunk is already attached
      ConfigurableJoint[] wallChunkJoints = surroundingWallChunksList[i].GetComponents<ConfigurableJoint>();
      if (wallChunkJoints.Length > 0)
      {
        continue;
      }

      AddConfigJoint(surroundingWallChunksList[i]);
      bool attached = surroundingWallChunksList[i].GetComponent<WallChunkV2>().AttachConfigJoint();

      // if (attached)
      // {
      //   return true;
      // }
    }

    if (GetComponents<ConfigurableJoint>().Length == 0)
    {
      AddConfigJoint(surroundingWallChunksList[0]);
    }
    return false;
  }



  void OnTriggerEnter(Collider collider)
  {
    if (collider.transform.parent == null)
    {
      return;
    }

    if (collider.transform.parent.tag == "WallChunk")
    {
      // print(collider.transform.parent.tag);
      surroundingWallChunksList.Add(collider.transform.parent.gameObject);
      //   surroundingWallChunksList.Add(collider.gameObject);
    }

  }

  void OnJointBreak()
  {
    GameManager.gameManager.AddPoints(10);
  }

  void AddConfigJoint(GameObject obj)
  {

    // Sets up and adds a config join to eachother
    ConfigurableJoint configJoint = gameObject.AddComponent<ConfigurableJoint>();
    configJoint.xMotion = ConfigurableJointMotion.Locked;
    configJoint.yMotion = ConfigurableJointMotion.Locked;
    configJoint.zMotion = ConfigurableJointMotion.Locked;
    configJoint.zMotion = ConfigurableJointMotion.Locked;
    configJoint.angularXMotion = ConfigurableJointMotion.Locked;
    configJoint.angularYMotion = ConfigurableJointMotion.Locked;
    configJoint.angularZMotion = ConfigurableJointMotion.Locked;
    configJoint.projectionMode = JointProjectionMode.PositionAndRotation;

    // configJoint.enablePreprocessing = false;
    configJoint.enableCollision = false;
    // configJoint.autoConfigureConnectedAnchor = false;

    // Test code

    JointDrive newDrive = configJoint.xDrive;
    newDrive.positionSpring = Mathf.Infinity;

    configJoint.xDrive = newDrive;
    configJoint.yDrive = newDrive;
    configJoint.zDrive = newDrive;
    configJoint.angularXDrive = newDrive;

    configJoint.projectionAngle = 0;
    configJoint.projectionDistance = 0;


    configJoint.connectedBody = obj.GetComponent<Rigidbody>();
    configJoint.breakForce = 300;

  }
}

