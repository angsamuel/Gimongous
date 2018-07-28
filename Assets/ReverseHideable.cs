﻿using UnityEngine;

public class ReverseHideable : MonoBehaviour, IHideable {

    virtual public void OnFOVLeave() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr != null){
            sr.enabled = true;
        }
        
        foreach(Transform child in transform){
            sr = child.GetComponent<SpriteRenderer>();
            if(sr != null){
                sr.enabled = true;
            }
            if(child.tag == "weapon"){
                foreach(Transform blade in child){
                    blade.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
    virtual public void OnFOVEnter() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr != null){
            sr.enabled = false;
        }
        foreach(Transform child in transform){
            sr = child.GetComponent<SpriteRenderer>();
            if(sr != null){
                sr.enabled = false;
            }

            if(child.tag == "weapon"){
                foreach(Transform blade in child){
                    blade.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
}
