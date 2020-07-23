using UnityEngine;
using System.Collections.Generic;

public class ObjectPool: MonoBehaviour {
 public Transform[] Objects;
 List<Transform> inPoolObjects;
 List<Transform> outOfPoolObjects;

 public void Awake () {
  inPoolObjects = new List<Transform> (Objects);
  outOfPoolObjects = new List<Transform> ();
 }

 public Transform GetObject () { 
  Debug.Log ("Name: " + name);
   int index = Random.Range (0, inPoolObjects.Count);
   Transform t = inPoolObjects [index];
   inPoolObjects.RemoveAt (index);
   outOfPoolObjects.Add (t);
   return t;
 }

 public void ReturnToPool (Transform t) {
  t.parent = transform;
  t.localPosition = Vector3.zero;
  outOfPoolObjects.Remove (t);
  inPoolObjects.Add (t);
 }
}