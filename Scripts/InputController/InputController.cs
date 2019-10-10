using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
    public static class InputController
    {
        private static bool disabled;
        private static readonly List<string> axisControls = new List<string>() { "Horizontal", "Vertical" };
        private static readonly List<string> keyControls = new List<string>() { "Fire" };

        private readonly static Dictionary<int, Dictionary<string, string>> playerControls = new Dictionary<int, Dictionary<string, string>>();

        public static bool IsCancelButtonPressed { get => Input.GetButtonDown("Cancel"); }
        public static bool IsPauseButtonPressed { get => Input.GetButtonDown("Pause"); }
        public static bool IsSubmitButtonPressed { get => Input.GetButtonDown("Submit"); }

        public static void Disable()
        {
            disabled = true;
        }

        public static void Enable()
        {
            disabled = false;
        }

        public static void AssignPlayerControls(int id, Dictionary<string, string> controls)
        {
            foreach(KeyValuePair<string, string> kv in controls)
            {
                if(!(axisControls.Contains(kv.Key) || keyControls.Contains(kv.Key)))
                {
                    var values = new List<string>();
                    values.AddRange(axisControls);
                    values.AddRange(keyControls);

                    var msg = "controls key must be one of the following: " + string.Join(", ", values);
                    throw new ArgumentOutOfRangeException(nameof(controls), msg);
                }
            }
            playerControls[id] = controls;
        }

        public static bool GetButtonPressed(int id, string key)
        {
            bool pressed = false;
            if (!disabled)
            {
                if (playerControls.ContainsKey(id))
                {
                    pressed = Input.GetButtonDown(playerControls[id][key]);
                }
            }

            return pressed;
        }

        public static float GetAxis(int id, string axis)
        {
            var value = 0.0f;
            if(!disabled)
            {
                if (!axisControls.Contains(axis))
                {
                    var msg = "axis must be one of the following: " + string.Join(" ", axisControls);
                    throw new ArgumentOutOfRangeException(nameof(axis), msg);
                }

                if (playerControls.ContainsKey(id))
                    value = Input.GetAxis(playerControls[id][axis]);
            }

            return value;
        }
    }
}