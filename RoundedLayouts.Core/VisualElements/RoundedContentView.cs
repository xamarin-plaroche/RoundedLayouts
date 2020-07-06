using System;
using RoundedLayouts.Forms.Enums;
using Xamarin.Forms;

namespace RoundedLayouts.Forms.VisualElements
{
    public class RoundedContentView : ContentView
	{
		public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(RoundedContentView), 0);

		public int CornerRadius
		{
			get { return (int)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(RoundedContentView), 0);

		public int BorderThickness
		{
			get { return (int)GetValue(BorderThicknessProperty); }
			set { SetValue(BorderThicknessProperty, value); }
		}

		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RoundedContentView), Color.Transparent);

		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}

		public static readonly BindableProperty CornersTransformationProperty = BindableProperty.Create(nameof(CornersAttributes), typeof(CornerAttributes), typeof(RoundedStackLayout), CornerAttributes.AllRounded);

		public CornerAttributes CornersAttributes
		{
			get => (CornerAttributes)GetValue(CornersTransformationProperty);
			set => SetValue(CornersTransformationProperty, value);
		}
	}
}
