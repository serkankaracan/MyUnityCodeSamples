public class MyToggle : ToggleMain
{
    public override void OnToggleValueChanged(bool newValue)
    {
        if (newValue)
        {
            print("açýk");
        }
        else
        {
            print("kapali");
        }
    }
}
