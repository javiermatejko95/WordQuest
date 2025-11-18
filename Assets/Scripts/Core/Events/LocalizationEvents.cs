using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalizationEvents
{
    public static Action<string> OnLanguageLoad;
    public static Action<string> OnLanguageChanged;
}
