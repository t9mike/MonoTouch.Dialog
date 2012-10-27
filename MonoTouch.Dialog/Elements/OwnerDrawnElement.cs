using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

namespace MonoTouch.Dialog
{
	public abstract class OwnerDrawnElement : Element, IElementSizing
	{		
		public string CellReuseIdentifier
		{
			get;set;	
		}
		
		public UITableViewCellStyle Style
		{
			get;set;	
		}
		
		public OwnerDrawnElement (UITableViewCellStyle style, string cellIdentifier) : base(null)
		{
			this.CellReuseIdentifier = cellIdentifier;
			this.Style = style;
		}
		
		public float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			return Height(tableView.Bounds);
		}
		
		protected virtual void ElementUnassignedFromView(UITableViewCell old_cell_view)
		{
		}

        /// <summary>
        /// This is a good place to change the accessibility features for a cell.
        /// </summary>
		protected virtual void ElementAssignedToView(UITableViewCell new_cell_view, bool first_time)
		{
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			OwnerDrawnCell cell = tv.DequeueReusableCell(this.CellReuseIdentifier) as OwnerDrawnCell;
			
			if (cell == null)
			{
				cell = new OwnerDrawnCell(this, this.Style, this.CellReuseIdentifier);
				ElementAssignedToView(cell, true);
			}
			else
			{
				var old_element = cell.Element;
				cell.Element = this;
				if (old_element != null)
					old_element.ElementUnassignedFromView(cell);
				ElementAssignedToView(cell, false);
			}
			
			cell.Update();
			return cell;
		}	
		
		public abstract void Draw(RectangleF bounds, CGContext context, UIView view);
		
		public abstract float Height(RectangleF bounds);
		
		class OwnerDrawnCell : UITableViewCell
		{
			OwnerDrawnCellView view;
			
			public OwnerDrawnCell(OwnerDrawnElement element, UITableViewCellStyle style, string cellReuseIdentifier) : base(style, cellReuseIdentifier)
			{
				Element = element;
			}
			
			public OwnerDrawnElement Element
			{
				get {
					return view.Element;
				}
				set {
					if (view == null)
					{
						view = new OwnerDrawnCellView (value);
						ContentView.Add (view);
					}
					else
					{
						view.Element = value;
					}
				}
			}
				
			

			public void Update()
			{
				SetNeedsDisplay();
				view.SetNeedsDisplay();
			}		
	
			public override void LayoutSubviews()
			{
				base.LayoutSubviews();
				
				view.Frame = ContentView.Bounds;
			}
		}
		
		class OwnerDrawnCellView : UIView
		{				
			OwnerDrawnElement element;
			
			public OwnerDrawnCellView(OwnerDrawnElement element)
			{
				this.element = element;
			}
			
			
			public OwnerDrawnElement Element
			{
				get { return element; }
				set {
					element = value; 
				}
			}
			
			public void Update()
			{
				SetNeedsDisplay();
			
			}
			
			public override void Draw (RectangleF rect)
			{
				CGContext context = UIGraphics.GetCurrentContext();
				element.Draw(rect, context, this);
			}
		}
	}
}

