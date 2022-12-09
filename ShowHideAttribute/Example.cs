using UnityEngine;

public class Example : MonoBehaviour
{
    public bool showHideList = false;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(showHideList))]
    public string aField = "item 1";

    //===================================

    public bool enableDisableList = false;

    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And,
    nameof(enableDisableList))]
    public string anotherField = "item 2";

    //====================================

    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(CalculateIsEnabled))]
    public string yetAnotherField = "one more";
    public bool CalculateIsEnabled()
    {
        return true;
    }

    //===================================

    public bool condition1;
    public bool condition2;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(condition1),
    nameof(condition2))]
    public string oneLastField = "last field";
}
