using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BreakableWallsV2 : MonoBehaviour
{
  // Start is called before the first frame update
  void Awake()
  {
    setupWallChunks();




  }

  void Start()
  {

    // Removes the setup objects
    Invoke("RemoveComponents", 0.5f);
  }

  void setupWallChunks()
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      GameObject wallChunk = transform.GetChild(i).gameObject;

      // Make a duplicate wall chunk
      GameObject duplicate = Instantiate(wallChunk);


      // Nest the duplicate
      duplicate.transform.SetParent(wallChunk.transform);


      // Make the duplicate slightly bigger for overlapping collider triggers
      duplicate.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
      duplicate.transform.rotation = wallChunk.transform.rotation;
      duplicate.transform.position = wallChunk.transform.position;
      duplicate.GetComponent<MeshRenderer>().enabled = false;
      duplicate.AddComponent<MeshCollider>();
      duplicate.GetComponent<MeshCollider>().convex = true;
      duplicate.GetComponent<MeshCollider>().isTrigger = true;


    }

    for (int i = 0; i < transform.childCount; i++)
    {
      GameObject wallChunk = transform.GetChild(i).gameObject;
      // Add necessary components to the actual wall chunk
      Rigidbody rb = wallChunk.AddComponent<Rigidbody>();
      rb.isKinematic = true;
      wallChunk.tag = "WallChunk";
      wallChunk.AddComponent<MeshCollider>().convex = true;
      wallChunk.AddComponent<WallChunkV2>();
    }

  }

  void StartRecursion()
  {
    print("StartRecursion called");
    transform.GetChild(0).GetComponent<WallChunkV2>().AttachConfigJoint();
  }

  void RemoveComponents()
  {
    Destroy(this);
    for (int i = 0; i < transform.childCount; i++)
    {
      Destroy(transform.GetChild(i).GetChild(0).gameObject);
      // Destroy(transform.GetChild(i).GetComponent<WallChunk>());
    }
    StartRecursion();
  }


  // Update is called once per frame
  void Update()
  {

  }
}

