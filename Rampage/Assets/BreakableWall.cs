using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public Vector3 wallChunkColliderSize;
    private Vector3 previousColliderSize;
    // Start is called before the first frame update
    void Start()
    {
        setupWallChunks();

        // Removes the setup objects
        Invoke("RemoveComponents", 0.5f);



    }

    void setupWallChunks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {   if(transform.GetChild(i).tag != "LOD"){
                GameObject wallChunk = transform.GetChild(i).gameObject;

                // Make a duplicate wall chunk
                GameObject duplicate = Instantiate(wallChunk);

                // Add necessary components to the actual wall chunk
                wallChunk.AddComponent<Rigidbody>();
                wallChunk.AddComponent<MeshCollider>().convex = true;
                wallChunk.tag = "WallChunk";
                wallChunk.AddComponent<WallChunk>();

                // Nest the duplicate
                duplicate.transform.SetParent(wallChunk.transform);

                // Make the duplicate slightly bigger for overlapping collider triggers
                duplicate.transform.localScale += new Vector3(.1f, .1f, .1f);
                duplicate.GetComponent<MeshRenderer>().enabled = false;
                duplicate.AddComponent<MeshCollider>();
                duplicate.GetComponent<MeshCollider>().convex = true;
                duplicate.GetComponent<MeshCollider>().isTrigger = true;

            }
        }

    }

    void RemoveComponents()
    {
        Destroy(this);
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).GetChild(0).gameObject);
            // Destroy(transform.GetChild(i).GetComponent<WallChunk>());
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}