public class MyToggle : ToggleMain
{
    public override void OnToggleValueChanged(bool newValue)
    {
        if (newValue)
        {
            print("a��k");
        }
        else
        {
            print("kapali");
        }
    }
}
