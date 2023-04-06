using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    public static bool endAnimation = false;

    public void EndConversation()
    {
        endAnimation = true;
    }
}
