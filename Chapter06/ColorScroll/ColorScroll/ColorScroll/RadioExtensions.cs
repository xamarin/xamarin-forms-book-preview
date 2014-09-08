using System;
using System.Collections.Generic; 
using Xamarin.Forms;

namespace FormsBook.Utilities
{
    static class RadioExtensions
    {
        class Info
        {
            public bool toggledState;
            public Action<View> toggledHandler;
        }

        static Dictionary<View, Info> instances = new Dictionary<View, Info>();

        public static void AddRadioToggler(this View view, Action<View> toggledHandler)
        {
            // Add View to dictionary.
            instances.Add(view, new Info
            {
                toggledState = false,
                toggledHandler = toggledHandler
            });

            // Add a gesture recognizer for tap.
            view.GestureRecognizers.Add(
                new TapGestureRecognizer((View tappedView) =>
                {
                    tappedView.SetRadioState(true);
                }));
        }

        public static void SetRadioState(this View view, bool isToggled)
        {
            // Check if the property is actually changing.
            if (instances[view].toggledState != isToggled)
            {
                // Set the new value.
                instances[view].toggledState = isToggled;

                // Fire the handler.
                instances[view].toggledHandler(view);

                // If being toggled, untoggle all the siblings.
                if (isToggled)
                {
                    Layout<View> parent = view.ParentView as Layout<View>;

                    if (parent != null)
                    {
                        foreach (View sibling in parent.Children)
                        {
                            if (sibling != view && instances.ContainsKey(sibling))
                            {
                                sibling.SetRadioState(false);
                            }
                        }
                    }
                }
            }
        }

        public static bool GetRadioState(this View view)
        {
            return instances[view].toggledState;
        }
    }
}
