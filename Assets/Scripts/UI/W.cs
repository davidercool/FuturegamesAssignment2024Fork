using UnityEngine;

public class W : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            Debug.Log("DAMN GG THAT WAS IMPRESSIVE YO, SICK KICKFLIPS");
        }
    }
}
