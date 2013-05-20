using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using WinRtBehaviors;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace VKClient.Behaviours
{
    public class VisibilityTransitionBehaviour : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register("Visibility", typeof(Visibility), typeof(VisibilityTransitionBehaviour), new PropertyMetadata(0, new PropertyChangedCallback(VisibilityTransitionBehaviour.PropertyChangedCallback)));
        public static readonly DependencyProperty AnimationOutProperty = DependencyProperty.Register("AnimationOut", typeof(Storyboard), typeof(VisibilityTransitionBehaviour), new PropertyMetadata(null));
        public static readonly DependencyProperty AnimationInProperty = DependencyProperty.Register("AnimationIn", typeof(Storyboard), typeof(VisibilityTransitionBehaviour), new PropertyMetadata(null));
        public Storyboard AnimationOut
        {
            get
            {
                return (Storyboard)base.GetValue(VisibilityTransitionBehaviour.AnimationOutProperty);
            }
            set
            {
                base.SetValue(VisibilityTransitionBehaviour.AnimationOutProperty, value);
            }
        }
        public Storyboard AnimationIn
        {
            get
            {
                return (Storyboard)base.GetValue(VisibilityTransitionBehaviour.AnimationInProperty);
            }
            set
            {
                base.SetValue(VisibilityTransitionBehaviour.AnimationInProperty, value);
            }
        }
        public Visibility Visibility
        {
            get
            {
                return (Visibility)base.GetValue(VisibilityTransitionBehaviour.VisibilityProperty);
            }
            set
            {
                base.SetValue(VisibilityTransitionBehaviour.VisibilityProperty, value);
            }
        }
        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisibilityTransitionBehaviour visibilityTransitionBehaviour = (VisibilityTransitionBehaviour)d;
            visibilityTransitionBehaviour.TransitionOut((Visibility)e.OldValue);
        }
        private void TransitionOut(Visibility oldValue)
        {
            if (this.AnimationOut == null || oldValue == Visibility.Visible)
            {
                this.TransitionIn();
                return;
            }
            this.AnimationOut.Stop();
            Storyboard.SetTarget(this.AnimationOut, base.AssociatedObject);
            Storyboard animationOut = this.AnimationOut;
            this.AnimationOut.Completed += AnimationOutCompleted;

            //WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(animationOut.Completed), new Action<EventRegistrationToken>(animationOut.remove_Completed), new EventHandler<object>(this.AnimationOutCompleted));
            this.AnimationOut.Begin();
        }
        private void TransitionIn()
        {
            if (base.AssociatedObject == null)
            {
                return;
            }
            base.AssociatedObject.Visibility=this.Visibility;
            if (this.AnimationIn != null)
            {
                this.AnimationIn.Stop();
                Storyboard.SetTarget(this.AnimationIn, base.AssociatedObject);
                this.AnimationIn.Begin();
            }
        }
        private void AnimationOutCompleted(object sender, object e)
        {
            this.AnimationOut.Completed -= AnimationOutCompleted;
            
            //WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>(this.AnimationOut.remove_Completed), new EventHandler<object>(this.AnimationOutCompleted));
            this.TransitionIn();
        }
    }
}
