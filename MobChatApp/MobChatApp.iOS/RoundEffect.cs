using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(MobChatApp.iOS.RoundEffect), nameof(MobChatApp.iOS.RoundEffect))]
namespace MobChatApp.iOS
{
    class RoundEffect : PlatformEffect
    {

        nfloat originalRadius;
        UIKit.UIView effectTarget;

        protected override void OnAttached()
        {
            try
            {
                effectTarget = Control ?? Container;
                originalRadius = effectTarget.Layer.CornerRadius;
                effectTarget.ClipsToBounds = true;
                effectTarget.Layer.CornerRadius = CalculateRadius();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set corner radius: {ex.Message}");
            }
        }

        protected override void OnDetached()
        {
            if (effectTarget != null)
            {
                effectTarget.ClipsToBounds = false;
                if (effectTarget.Layer != null)
                {
                    effectTarget.Layer.CornerRadius = originalRadius;
                }
            }
        }

        float CalculateRadius()
        {
            double width = (double)Element.GetValue(VisualElement.WidthRequestProperty);
            double height = (double)Element.GetValue(VisualElement.HeightRequestProperty);
            float minDimension = (float)Math.Min(height, width);
            float radius = minDimension / 2f;

            return radius;
        }
    }
}