﻿using System;
using RoundedLayouts.Forms.VisualElements;
using RoundedLayouts.Renderers;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using RoundedLayouts.Forms.Enums;

[assembly: ExportRenderer(typeof(RoundedStackLayout), typeof(RoundedStackLayoutRenderer))]
namespace RoundedLayouts.Renderers
{
    public class RoundedStackLayoutRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
            {
                return;
            }

            var boxView = (RoundedStackLayout)Element;

            NativeView.Layer.BorderColor = boxView.BorderColor.ToCGColor();
            NativeView.Layer.BorderWidth = boxView.BorderThickness;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == RoundedStackLayout.CornerRadiusProperty.PropertyName)
            {
                NativeView.Layer.CornerRadius = ((RoundedStackLayout)sender).CornerRadius;
            }
            if (e.PropertyName == RoundedStackLayout.BorderColorProperty.PropertyName)
            {
                NativeView.Layer.BorderColor = ((RoundedStackLayout)sender).BorderColor.ToCGColor();
            }
            if (e.PropertyName == RoundedStackLayout.BorderThicknessProperty.PropertyName)
            {
                NativeView.Layer.BorderWidth = ((RoundedStackLayout)sender).BorderThickness;
            }
            SetNeedsDisplay();
        }

        public override void Draw(CGRect rect)
        {
            var boxView = (RoundedStackLayout)Element;

            double sourceWidth = rect.Size.Width;
            double sourceHeight = rect.Size.Height;

            double desiredWidth = sourceWidth;
            double desiredHeight = sourceHeight;

            var topLeftCornerSize = boxView.CornerRadius;
            var topRightCornerSize = boxView.CornerRadius;
            var bottomLeftCornerSize = boxView.CornerRadius;
            var bottomRightCornerSize = boxView.CornerRadius;

            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                context.BeginPath();

                using (var path = new UIBezierPath())
                {
                    // TopLeft
                    if (boxView.CornersAttributes.HasFlag(CornerAttributes.TopLeftRounded))
                    {
                        path.MoveTo(new CGPoint(0, topLeftCornerSize));
                        path.AddQuadCurveToPoint(new CGPoint(topLeftCornerSize, 0), new CGPoint(0, 0));
                    }
                    else
                    {
                        path.MoveTo(new CGPoint(0, 0));
                    }

                    // TopRight
                    if (boxView.CornersAttributes.HasFlag(CornerAttributes.TopRightRounded))
                    {
                        path.AddLineTo(new CGPoint(desiredWidth - topRightCornerSize, 0));
                        path.AddQuadCurveToPoint(new CGPoint(desiredWidth, topRightCornerSize), new CGPoint(desiredWidth, 0));
                    }
                    else
                    {
                        path.AddLineTo(new CGPoint(desiredWidth, 0));
                    }

                    // BottomRight
                    if (boxView.CornersAttributes.HasFlag(CornerAttributes.BottomRightRounded))
                    {
                        path.AddLineTo(new CGPoint(desiredWidth, desiredHeight - bottomRightCornerSize));
                        path.AddQuadCurveToPoint(new CGPoint(desiredWidth - bottomRightCornerSize, desiredHeight), new CGPoint(desiredWidth, desiredHeight));
                    }
                    else
                    {
                        path.AddLineTo(new CGPoint(desiredWidth, desiredHeight));
                    }

                    // BottomLeft
                    if (boxView.CornersAttributes.HasFlag(CornerAttributes.BottomLeftRounded))
                    {
                        path.AddLineTo(new CGPoint(bottomLeftCornerSize, desiredHeight));
                        path.AddQuadCurveToPoint(new CGPoint(0, desiredHeight - bottomLeftCornerSize), new CGPoint(0, desiredHeight));
                    }
                    else
                    {
                        path.AddLineTo(new CGPoint(0, desiredHeight));
                    }

                    boxView.BackgroundColor.ToUIColor().SetFill();
                    path.Fill();

                    path.ClosePath();
                    context.AddPath(path.CGPath);
                    context.Clip();
                }
            }
        }
    }
}
