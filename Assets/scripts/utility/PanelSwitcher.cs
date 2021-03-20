using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class PanelSwitcher : MonoBehaviour
{
  [System.Serializable]
  public struct Switch{
    public UnityEvent enter_callback;
    public UnityEvent exit_callback;
  }

  public Switch[] panels;
  private int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        panels[0].enter_callback.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SwitchPanel(int index){
      if (index==current) return;
      panels[index].enter_callback.Invoke();
      panels[current].exit_callback.Invoke();
      current = index;

    }
}
