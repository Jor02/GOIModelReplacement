using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GOIModelReplacement.Core
{
    public static class ExtensionMethods
    {
        public static Transform RecursiveFindChild(this Transform parent, string n)
        {
            foreach (Transform child in parent)
            {
                if (child.name == n)
                {
                    return child;
                }
                else
                {
                    Transform found = RecursiveFindChild(child, n);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }
    }
}
