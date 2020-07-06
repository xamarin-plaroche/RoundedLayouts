using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using RoundedLayouts.Forms.Enums;
using RoundedLayouts.Forms.VisualElements;
using RoundedLayouts.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedGrid), typeof(RoundedGridRenderer))]
namespace RoundedLayouts.Renderers
{
    public class RoundedGridRenderer : ViewRenderer
	{
		Context _context;

		public RoundedGridRenderer(Context context) : base(context)
		{
			_context = context;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			this.SetWillNotDraw(false);

			this.Invalidate();

			if (this.Element == null)
			{
				return;
			}

			this.SetBackgroundColor(Android.Graphics.Color.Transparent);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
			{
				this.SetBackgroundColor(Android.Graphics.Color.Transparent);
			}

			if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
				e.PropertyName == VisualElement.HeightProperty.PropertyName ||
				e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName ||
				e.PropertyName == RoundedGrid.CornerRadiusProperty.PropertyName ||
				e.PropertyName == RoundedGrid.BorderColorProperty.PropertyName ||
				e.PropertyName == RoundedGrid.BorderThicknessProperty.PropertyName)
			{
				this.Invalidate();
			}
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			var boxView = (RoundedGrid)Element;

			double sourceWidth = canvas.Width;
			double sourceHeight = canvas.Height;

			double desiredWidth = sourceWidth;
			double desiredHeight = sourceHeight;

			var topLeftCornerSize = _context.ToPixels(boxView.CornerRadius);
			var topRightCornerSize = _context.ToPixels(boxView.CornerRadius);
			var bottomLeftCornerSize = _context.ToPixels(boxView.CornerRadius);
			var bottomRightCornerSize = _context.ToPixels(boxView.CornerRadius);

			using (Paint paint = new Paint())
			using (Matrix matrix = new Matrix())
			using (var path = new Path())
			{
				// TopLeft
				if (boxView.CornersAttributes.HasFlag(CornerAttributes.TopLeftRounded))
				{
					path.MoveTo(0, (float)topLeftCornerSize);
					path.QuadTo(0, 0, (float)topLeftCornerSize, 0);
				}
				else
				{
					path.MoveTo(0, 0);
				}

				// TopRight
				if (boxView.CornersAttributes.HasFlag(CornerAttributes.TopRightRounded))
				{
					path.LineTo((float)(desiredWidth - topRightCornerSize), 0);
					path.QuadTo((float)desiredWidth, 0, (float)desiredWidth, (float)topRightCornerSize);
				}
				else
				{
					path.LineTo((float)desiredWidth, 0);
				}

				// BottomRight
				if (boxView.CornersAttributes.HasFlag(CornerAttributes.BottomRightRounded))
				{
					path.LineTo((float)desiredWidth, (float)(desiredHeight - bottomRightCornerSize));
					path.QuadTo((float)desiredWidth, (float)desiredHeight, (float)(desiredWidth - bottomRightCornerSize), (float)desiredHeight);
				}
				else
				{
					path.LineTo((float)desiredWidth, (float)desiredHeight);
				}

				// BottomLeft
				if (boxView.CornersAttributes.HasFlag(CornerAttributes.BottomLeftRounded))
				{
					path.LineTo((float)bottomLeftCornerSize, (float)desiredHeight);
					path.QuadTo(0, (float)desiredHeight, 0, (float)(desiredHeight - bottomLeftCornerSize));
				}
				else
				{
					path.LineTo(0, (float)desiredHeight);
				}

				paint.Color = boxView.BackgroundColor.ToAndroid();

				path.Close();
				canvas.DrawPath(path, paint);
			}
		}
	}
}
