using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mihyaz
{
    public class Converter
    {
        public class StringToVector2
        {
            public virtual Vector2 Rotation(string _commandRotation)
            {
                if (_commandRotation.StartsWith("(") && _commandRotation.EndsWith(")"))
                {
                    _commandRotation = _commandRotation.Substring(1, _commandRotation.Length - 2);
                }

                try
                {
                    // split the items
                    string[] sArray = _commandRotation.Split(',');
                    // store as a Vector3
                    Vector2 result = new Vector2(
                        float.Parse(sArray[0]),
                        float.Parse(sArray[1]));
                    return result;
                }
                catch (System.FormatException)
                {
                    return new Vector2(0, 0);
                }
            }

            public virtual Vector2 Movement(string _commandMovement)
            {
                if (_commandMovement.StartsWith("(") && _commandMovement.EndsWith(")"))
                {
                    _commandMovement = _commandMovement.Substring(1, _commandMovement.Length - 2);
                }
                try
                {
                    // split the items
                    string[] sArray = _commandMovement.Split(',');
                    // store as a Vector3
                    Vector2 result = new Vector2(
                        float.Parse(sArray[0]),
                        float.Parse(sArray[1]));
                    return result;
                }
                catch (System.FormatException)
                {
                    return new Vector2(0, 0);
                }
            }
        }
    }
}

