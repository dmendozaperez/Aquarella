using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace PanoramaControl
{
    [TemplatePart(Name = "PART_ScrollViewer", Type = typeof(ScrollViewer))]
    public class Panorama : ItemsControl
    {
        #region Data
        private ScrollViewer sv;
        private Point scrollTarget;
        private Point scrollStartPoint;
        private TouchPoint scrollTouchStartPoint;
        private Point scrollStartOffset;
        private Point previousPoint;
        private Vector velocity;
        private double friction;
        private DispatcherTimer animationTimer = new DispatcherTimer(DispatcherPriority.DataBind);
        private static int PixelsToMoveToBeConsideredScroll = 5;
        private static int PixelsToMoveToBeConsideredClick = 2;
        private IPanoramaTile tile = null;
        private Random rand = new Random(DateTime.Now.Millisecond);
        private TouchPoint currentTouchPoint;
        #endregion

        #region Ctor
        public Panorama()
        {
            friction = 0.85;

            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            animationTimer.Tick += new EventHandler(HandleWorldTimerTick);
            animationTimer.Start();

            TileColors = new Brush[] {
                new SolidColorBrush(Color.FromRgb((byte)111,(byte)189,(byte)69)),
                new SolidColorBrush(Color.FromRgb((byte)75,(byte)179,(byte)221)),
                new SolidColorBrush(Color.FromRgb((byte)65,(byte)100,(byte)165)),
                new SolidColorBrush(Color.FromRgb((byte)225,(byte)32,(byte)38)),
                new SolidColorBrush(Color.FromRgb((byte)128,(byte)0,(byte)128)),
                new SolidColorBrush(Color.FromRgb((byte)0,(byte)128,(byte)64)),
                new SolidColorBrush(Color.FromRgb((byte)0,(byte)148,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)0,(byte)199)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)135,(byte)15)),
                new SolidColorBrush(Color.FromRgb((byte)45,(byte)255,(byte)87)),
                new SolidColorBrush(Color.FromRgb((byte)127,(byte)0,(byte)55))
    
            };

            ComplimentaryTileColors = new Brush[] {
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255)),
                new SolidColorBrush(Color.FromRgb((byte)255,(byte)255,(byte)255))
            };

        }

        static Panorama()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Panorama), new FrameworkPropertyMetadata(typeof(Panorama)));
        }
        #endregion

        #region Properties
        public double Friction
        {
            get { return 1.0 - friction; }
            set { friction = Math.Min(Math.Max(1.0 - value, 0), 1.0); }
        }

        public List<Brush> TileColorPair
        {
            get 
            {
                int idx = rand.Next(TileColors.Length);
                return new List<Brush>() { TileColors[idx], ComplimentaryTileColors[idx] };
            }
        }

        #region DPs


        #region ItemBox

        public static readonly DependencyProperty ItemBoxProperty =
            DependencyProperty.Register("ItemBox", typeof(double), typeof(Panorama),
                new FrameworkPropertyMetadata((double)120.0));


        public double ItemBox
        {
            get { return (double)GetValue(ItemBoxProperty); }
            set { SetValue(ItemBoxProperty, value); }
        }

        #endregion

        #region GroupHeight

        public static readonly DependencyProperty GroupHeightProperty =
            DependencyProperty.Register("GroupHeight", typeof(double), typeof(Panorama),
                new FrameworkPropertyMetadata((double)640.0));


        public double GroupHeight
        {
            get { return (double)GetValue(GroupHeightProperty); }
            set { SetValue(GroupHeightProperty, value); }
        }

        #endregion



        #region HeaderFontSize

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(Panorama),
                new FrameworkPropertyMetadata((double)30.0));

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        #endregion


 
        #region HeaderFontColor

        public static readonly DependencyProperty HeaderFontColorProperty =
            DependencyProperty.Register("HeaderFontColor", typeof(Brush), typeof(Panorama),
                new FrameworkPropertyMetadata((Brush)Brushes.White));

        public Brush HeaderFontColor
        {
            get { return (Brush)GetValue(HeaderFontColorProperty); }
            set { SetValue(HeaderFontColorProperty, value); }
        }

        #endregion

        #region HeaderFontFamily

        public static readonly DependencyProperty HeaderFontFamilyProperty =
            DependencyProperty.Register("HeaderFontFamily", typeof(FontFamily), typeof(Panorama),
                new FrameworkPropertyMetadata((FontFamily)new FontFamily("Segoe UI")));

        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }

        #endregion

        #region TileColors

        public static readonly DependencyProperty TileColorsProperty =
            DependencyProperty.Register("TileColors", typeof(Brush[]), typeof(Panorama),
                new FrameworkPropertyMetadata((Brush[])null));

        public Brush[] TileColors
        {
            get { return (Brush[])GetValue(TileColorsProperty); }
            set { SetValue(TileColorsProperty, value); }
        }

        #endregion

        #region ComplimentaryTileColors

        public static readonly DependencyProperty ComplimentaryTileColorsProperty =
            DependencyProperty.Register("ComplimentaryTileColors", typeof(Brush[]), typeof(Panorama),
                new FrameworkPropertyMetadata((Brush[])null));

        public Brush[] ComplimentaryTileColors
        {
            get { return (Brush[])GetValue(ComplimentaryTileColorsProperty); }
            set { SetValue(ComplimentaryTileColorsProperty, value); }
        }

        #endregion

        #region UseSnapBackScrolling

        public static readonly DependencyProperty UseSnapBackScrollingProperty =
            DependencyProperty.Register("UseSnapBackScrolling", typeof(bool), typeof(Panorama),
                new FrameworkPropertyMetadata((bool)true));

        public bool UseSnapBackScrolling
        {
            get { return (bool)GetValue(UseSnapBackScrollingProperty); }
            set { SetValue(UseSnapBackScrollingProperty, value); }
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        private void DoStandardScrolling()
        {
            sv.ScrollToHorizontalOffset(scrollTarget.X);
            sv.ScrollToVerticalOffset(scrollTarget.Y);
            scrollTarget.X += velocity.X;
            scrollTarget.Y += velocity.Y;
            velocity *= friction;
        }

        

        private void HandleWorldTimerTick(object sender, EventArgs e)
        {
            var prop = DesignerProperties.IsInDesignModeProperty;
            bool isInDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop,
                typeof(FrameworkElement)).Metadata.DefaultValue;

            if (isInDesignMode)
                return;


            if (IsMouseCaptured)
            {
                Point currentPoint = Mouse.GetPosition(this);
                velocity = previousPoint - currentPoint;
                previousPoint = currentPoint;
            }
            else if (AreAnyTouchesOver)
            {
                Point currentPoint = new Point(currentTouchPoint.Position.X,currentTouchPoint.Position.Y);
                velocity = previousPoint - currentPoint;
                previousPoint = currentPoint;
            }
            else
            {
                if (velocity.Length > 1)
                {
                    DoStandardScrolling();
                }
                else
                {
                    if (UseSnapBackScrolling)
                    {
                        int mx = (int)sv.HorizontalOffset % (int)ActualWidth;
                        if (mx == 0)
                            return;
                        int ix = (int)sv.HorizontalOffset / (int)ActualWidth;
                        double snapBackX = mx > ActualWidth / 2 ? (ix + 1) * ActualWidth : ix * ActualWidth;
                        sv.ScrollToHorizontalOffset(sv.HorizontalOffset + (snapBackX - sv.HorizontalOffset) / 4.0);
                    }
                    else
                    {
                        DoStandardScrolling();
                    }
                }
            }
        }
        #endregion

        #region Overrides


        public override void OnApplyTemplate()
        {
            sv = (ScrollViewer)Template.FindName("PART_ScrollViewer", this);
            base.OnApplyTemplate();
        }

        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            currentTouchPoint = e.GetTouchPoint(this);

            tile = null;

            // Save starting point, used later when determining how much to scroll.
            scrollTouchStartPoint = e.GetTouchPoint(this);
            scrollStartOffset.X = sv.HorizontalOffset;
            scrollStartOffset.Y = sv.VerticalOffset;

            scrollStartPoint.X = scrollTouchStartPoint.Position.X;
            scrollStartPoint.Y = scrollTouchStartPoint.Position.Y;

            //store Control if one was found, so we can call its command later
            var x = TreeHelper.TryFindFromPoint<ListBoxItem>(this, scrollStartPoint);
            if (x != null)
            {
                x.IsSelected = true;
                ItemsControl tiles = ItemsControl.ItemsControlFromItemContainer(x);
                var data = tiles.ItemContainerGenerator.ItemFromContainer(x);
                if (data != null && data is IPanoramaTile)
                {
                    tile = (IPanoramaTile)data;
                    // JH Start - See IPanoramaTile for justification
                    tile.IsPressed = true;
                    // JH End - See IPanoramaTile for justification
                }
            }

            //this.CaptureTouch(TouchesCaptured.First<TouchDevice>()); 

            base.OnPreviewTouchDown(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            //if (sv.IsMouseOver)
			// JH Start - Belt & braces...
			if (sv.IsMouseOver && e.ChangedButton == MouseButton.Left)
			// JH End - Belt & braces...
			{
                tile = null;

                // Save starting point, used later when determining how much to scroll.
                scrollStartPoint = e.GetPosition(this);
                scrollStartOffset.X = sv.HorizontalOffset;
                scrollStartOffset.Y = sv.VerticalOffset;

				// JH Start - update cursor only once we move
                // Update the cursor if can scroll or not.
				//this.Cursor = (sv.ExtentWidth > sv.ViewportWidth) ||
				//    (sv.ExtentHeight > sv.ViewportHeight) ?
				//    Cursors.ScrollAll : Cursors.Arrow;
				// JH End - update cursor only once we move

                //store Control if one was found, so we can call its command later
                var x = TreeHelper.TryFindFromPoint<ListBoxItem>(this, scrollStartPoint);
                if (x != null)
                {
                    x.IsSelected = true;
                    ItemsControl tiles = ItemsControl.ItemsControlFromItemContainer(x);
                    var data = tiles.ItemContainerGenerator.ItemFromContainer(x);
                    if (data != null && data is IPanoramaTile)
                    {
                        tile = (IPanoramaTile)data;
						// JH Start - See IPanoramaTile for justification
						tile.IsPressed = true;
						// JH End - See IPanoramaTile for justification
					}
                }

                this.CaptureMouse();
            }

            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewTouchMove(TouchEventArgs e)
        {

            currentTouchPoint = e.GetTouchPoint(this);
            Point currentPoint = new Point(currentTouchPoint.Position.X, currentTouchPoint.Position.Y);

            // Determine the new amount to scroll.
            Point delta = new Point(scrollStartPoint.X - currentPoint.X, scrollStartPoint.Y - currentPoint.Y);

            if (Math.Abs(delta.X) < PixelsToMoveToBeConsideredScroll &&
                Math.Abs(delta.Y) < PixelsToMoveToBeConsideredScroll)
                return;

            // JH Start - Update the cursor to scrolling if it's moved and release the pressed state
            this.Cursor = (sv.ExtentWidth > sv.ViewportWidth) || (sv.ExtentHeight > sv.ViewportHeight) ? Cursors.ScrollAll : Cursors.Arrow;
            if (tile != null && tile.IsPressed)
                tile.IsPressed = false;
            // JH End - Update the cursor to scrolling if it's moved and release the pressed state


            scrollTarget.X = scrollStartOffset.X + delta.X;
            // JH Start - Don't want vertical scrolling
            //scrollTarget.Y = scrollStartOffset.Y + delta.Y;
            // JH End - Don't want vertical scrolling

            // Scroll to the new position.
            sv.ScrollToHorizontalOffset(scrollTarget.X);
            // JH Start - Don't want vertical scrolling
            //sv.ScrollToVerticalOffset(scrollTarget.Y);
            // JH End - Don't want vertical scrolling

            
            
            base.OnPreviewTouchMove(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                Point currentPoint = e.GetPosition(this);

                // Determine the new amount to scroll.
                Point delta = new Point(scrollStartPoint.X - currentPoint.X, scrollStartPoint.Y - currentPoint.Y);

                if (Math.Abs(delta.X) < PixelsToMoveToBeConsideredScroll &&
                    Math.Abs(delta.Y) < PixelsToMoveToBeConsideredScroll)
                    return;

				// JH Start - Update the cursor to scrolling if it's moved and release the pressed state
				this.Cursor = (sv.ExtentWidth > sv.ViewportWidth) || (sv.ExtentHeight > sv.ViewportHeight) ? Cursors.ScrollAll : Cursors.Arrow;
				if (tile != null && tile.IsPressed)
					tile.IsPressed = false;
				// JH End - Update the cursor to scrolling if it's moved and release the pressed state


                scrollTarget.X = scrollStartOffset.X + delta.X;
				// JH Start - Don't want vertical scrolling
                //scrollTarget.Y = scrollStartOffset.Y + delta.Y;
				// JH End - Don't want vertical scrolling

                // Scroll to the new position.
                sv.ScrollToHorizontalOffset(scrollTarget.X);
				// JH Start - Don't want vertical scrolling
				//sv.ScrollToVerticalOffset(scrollTarget.Y);
				// JH End - Don't want vertical scrolling
			}

            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewTouchUp(TouchEventArgs e)
        {
            
                
            this.ReleaseAllTouchCaptures();
            
            currentTouchPoint = e.GetTouchPoint(this);
            Point currentPoint = new Point(currentTouchPoint.Position.X, currentTouchPoint.Position.Y);

            // JH Start - If pressed, release
            if (tile != null && tile.IsPressed)
                tile.IsPressed = false;
            // JH End - If pressed, release

            // Determine the new amount to scroll.
            Point delta = new Point(scrollStartPoint.X - currentPoint.X, scrollStartPoint.Y - currentPoint.Y);

            if (Math.Abs(delta.X) < PixelsToMoveToBeConsideredClick &&
                Math.Abs(delta.Y) < PixelsToMoveToBeConsideredClick && tile != null)
            {
                //Ok, its a click ask the tile to do its job
                if (tile.TileClickedCommand.CanExecute(null))
                {
                    tile.TileClickedCommand.Execute(null);
                }
            }

            base.OnPreviewTouchUp(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.Cursor = Cursors.Arrow;
                this.ReleaseMouseCapture();
            }

            Point currentPoint = e.GetPosition(this);

			// JH Start - If pressed, release
			if (tile != null && tile.IsPressed)
				tile.IsPressed = false;
			// JH End - If pressed, release

            // Determine the new amount to scroll.
            Point delta = new Point(scrollStartPoint.X - currentPoint.X, scrollStartPoint.Y - currentPoint.Y);

            if (Math.Abs(delta.X) < PixelsToMoveToBeConsideredClick &&
                Math.Abs(delta.Y) < PixelsToMoveToBeConsideredClick && tile !=null)
            {
                //Ok, its a click ask the tile to do its job
                if (tile.TileClickedCommand.CanExecute(null))
                {
                    tile.TileClickedCommand.Execute(null);
                }
            }

            base.OnPreviewMouseUp(e);
        }


		// JH Start	- Support Mouse Wheel
		// this doesn't work so well with snap...switch it off if the wheel is used
		protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
		{
			// Arbitrary decision but holding the mouse button down whilst scrolling may end in tears
			if (this.IsMouseCaptured)
			{
				base.OnPreviewMouseWheel(e);
				return;
			}

			// switch off snap back if wheeling
			if (this.UseSnapBackScrolling)
				this.UseSnapBackScrolling = false;

			// get the change
			int delta = e.Delta;

			// and the current offset
			double offset = sv.HorizontalOffset;

			// and the total width
			double width = sv.ScrollableWidth;

			// remmeber the delta will be the 'wrong way around' unless you're a mac user...
			// check we've somewhere to go
			if (delta > 0 && offset > 0)
			{
				// calculate the change
				double newPos = offset - delta;
				if (newPos < 0)
					newPos = 0;
				// let everything else know we've changed
				scrollTarget.X = newPos;
				// do it
				sv.ScrollToHorizontalOffset(newPos);
			}
			else if (delta < 0 && sv.HorizontalOffset < sv.ScrollableWidth)
			{
				// calculate the change
				double newPos = offset - delta;
				if (newPos > width)
					newPos = width;
				// let everything else know we've changed
				scrollTarget.X = newPos;
				// do it
				sv.ScrollToHorizontalOffset(newPos);
			}
		}
		// JH End
        #endregion

    }
}
