using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAction : MonoBehaviour {

    ParticleSystem myPat;

    void Start() {
        myPat = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (myPat) {
            if (!myPat.isPlaying) {
                Destroy(gameObject);
            }
        }
    }
}
