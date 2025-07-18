using UnityEngine;
using UnityEngine.AI;

public class RagdollToggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Rigidbody ragdollRB in GetComponentsInChildren<Rigidbody>())
        {
            ragdollRB.isKinematic = true;
        }
    }

    public void triggerRagdoll()
    {
        foreach (Rigidbody ragdollRB in GetComponentsInChildren<Rigidbody>())
        {
            ragdollRB.isKinematic = false;
        }


        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().enabled = false;
        
    }

}
