using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotation : MonoBehaviour
{
  public float rotationSpeed = 5f;

  Rigidbody self;




  void Start(){
    self = GetComponent<Rigidbody>();
  }

  public void OnMove(InputValue input)
  {
      Vector2 inputVec = input.Get<Vector2>();
      Debug.Log(inputVec);
      if (inputVec.y==0f){
        Vector3 angvel = self.angularVelocity;
        angvel.x = 0f;
        self.angularVelocity = angvel;
        Debug.Log("X should stop");
      }
      else{
        Vector3 torque = (new Vector3 (inputVec.y*rotationSpeed,0,0))*self.mass;
        self.AddTorque(torque);
      }
      if (inputVec.x==0f){
        Vector3 angvel = self.angularVelocity;
        angvel.z = 0f;
        self.angularVelocity = angvel;
      }
      else{
        Vector3 torque = (new Vector3 (0,0,inputVec.x*rotationSpeed))*self.mass;
        self.AddTorque(torque);
      }
  }
  public void OnRotate(InputValue input)
  {

      float inputF = input.Get<float>();
      Debug.Log(inputF);
      if(inputF==0f){
        Vector3 angvel = self.angularVelocity;
        angvel.y = 0f;
        self.angularVelocity = angvel;
      }
      else{
        Vector3 torque = (new Vector3 (0,inputF*rotationSpeed,0))*self.mass;
        self.AddTorque(torque);
      }
  }
  public void OnHalt(InputValue input){
    self.angularVelocity=Vector3.zero;
  }

}
