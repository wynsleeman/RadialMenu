using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RadialMenu.Controls
{
    /// <summary>
    /// Interaction logic for RadialMenu.xaml
    /// </summary>
    public class RadialMenu : ContentControl
    {
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(RadialMenu),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty HalfShiftedItemsProperty =
            DependencyProperty.Register("HalfShiftedItems", typeof(bool), typeof(RadialMenu),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool HalfShiftedItems
        {
            get { return (bool)GetValue(HalfShiftedItemsProperty); }
            set { SetValue(HalfShiftedItemsProperty, value); }
        }

        public static readonly DependencyProperty CentralItemProperty =
            DependencyProperty.Register("CentralItem", typeof(RadialMenuCentralItem), typeof(RadialMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public RadialMenuCentralItem CentralItem
        {
            get { return (RadialMenuCentralItem)GetValue(CentralItemProperty); }
            set { SetValue(CentralItemProperty, value); }
        }

        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(List<RadialMenuItem>), typeof(RadialMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public new List<RadialMenuItem> Content
        {
            get { return (List<RadialMenuItem>)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public List<RadialMenuItem> Items
        {
            get { return Content; }
            set { Content = value; }
        }

        static RadialMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadialMenu), new FrameworkPropertyMetadata(typeof(RadialMenu)));
        }

        public override void BeginInit()
        {
            Content = new List<RadialMenuItem>();
            base.BeginInit();
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double maxRadius = 0.0;
            double gap = 10.0;
            for (int ring = 0; ring <= 3; ring++)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Ring == ring - 1)
                    {
                        maxRadius = System.Math.Max(maxRadius, Items[i].EdgeOuterRadius + gap);
                    }
                }
                int count = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Ring == ring)
                    {
                        count += 1;
                    }
                }
                int inx = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    RadialMenuItem item = Items[i];
                    if (item.Ring == ring)
                    {
                        item.Index = inx;
                        if(item.Count == 1)
                        {
                            item.Count = count;
                        }
                        if(item.InnerRadius < maxRadius)
                        {

                            item.HalfShifted = HalfShiftedItems;
                            item.ArrowRadius += maxRadius - item.InnerRadius;
                            item.ContentRadius += maxRadius - item.InnerRadius;
                            item.EdgeInnerRadius += maxRadius - item.InnerRadius;
                            item.EdgeOuterRadius += maxRadius - item.InnerRadius;
                            
                            item.OuterRadius += maxRadius - item.InnerRadius;
                            //Do this last !
                            item.InnerRadius = maxRadius;
                            //RadialMenuItem item = d as RadialMenuItem;
                            //if (item != null)
                            //{
                            var angleDelta = 360.0 / item.Count;
                            var angleShift = item.HalfShifted ? -angleDelta / 2 : 0;
                            var startAngle = angleDelta * item.Index + angleShift;
                            var rotation = startAngle + angleDelta / 2;
                            item.AngleDelta = angleDelta;
                            item.StartAngle = startAngle;
                            item.Rotation = rotation;
                        }
                        inx += 1;
                    }
                }
            }
            return base.ArrangeOverride(arrangeSize);
        }
    }
}
