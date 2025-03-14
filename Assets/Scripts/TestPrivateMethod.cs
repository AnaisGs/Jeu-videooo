using UnityEngine;

public class TestPrivateMethod : MonoBehaviour
{

    public string universityName = "CY Paris Université";
    private int nbYearsBUT = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"nbYearsBUT {nbYearsBUT}");
        MyPrivateMethod();
        
    }
    
    public void MyMethod() {
        nbYearsBUT = 7;
    }

    private void MyPrivateMethod() {
        nbYearsBUT = 5;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) {
            Debug.Log($"nbYears {nbYearsBUT}");
        }
    }
}
