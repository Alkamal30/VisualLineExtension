using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisualLineExtension
{
    internal sealed class VisualLineAdornment
    {
        private const double LeftMargin = 140d;
        private const double LineThickness = 1d;
        private const double LinePositionOffset = 2.5d;
        private static readonly Color LineColor = Color.FromRgb(46, 46, 46);

        private readonly IWpfTextView _textView;
        private readonly IAdornmentLayer _adornmentLayer;
        private readonly Line _verticalLine;

        public VisualLineAdornment(IWpfTextView view)
        {
            _textView = view ?? throw new ArgumentNullException("view");

            _verticalLine = new Line
            {
                Stroke = BuildColorBrush(),
                StrokeThickness = LineThickness,
            };

            _adornmentLayer = view.GetAdornmentLayer(nameof(VisualLineAdornment));

            _textView.LayoutChanged += OnSizeChanged;
        }

        private static Brush BuildColorBrush()
        {
            Brush colorBrush = new SolidColorBrush(LineColor);
            colorBrush.Freeze();

            return colorBrush;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            _adornmentLayer.RemoveAllAdornments();

            _verticalLine.Y2 = _textView.ViewportHeight;

            Canvas.SetLeft(_verticalLine, (LeftMargin - 1) * _textView.FormattedLineSource.ColumnWidth + LinePositionOffset);
            Canvas.SetTop(_verticalLine, _textView.ViewportTop);

            _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _verticalLine, null);
        }
    }
}
