using UnityEngine;

public class OneTimeInUpdate : MonoBehaviour
{
    public bool isDied = false;
    public bool IsDied
    {
        get { return isDied; }
        set { isDied = value; }
    }

    private void Update()
    {
        if (IsDied)
        {
            // buraya bir kez çalışacak methodları ekle
            isDied = false;
        }
    }
}
