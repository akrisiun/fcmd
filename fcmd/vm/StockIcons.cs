
	[Demo ("Stock Item and Icon Browser", "DemoStockBrowser.cs")]
	public class DemoStockBrowser : Gtk.Window
	{
		enum Column {
			Id,
			Name,
			Label,
			Accel,
		};

		Label typeLabel, nameLabel, idLabel, accelLabel;
		Image iconImage;

		public DemoStockBrowser () : base ("Stock Icons and Items")
		{
			SetDefaultSize (-1, 500);
			BorderWidth = 8;

			HBox hbox = new HBox (false, 8);
			Add (hbox);

			ScrolledWindow sw = new ScrolledWindow ();
			sw.SetPolicy (PolicyType.Never, PolicyType.Automatic);
			hbox.PackStart (sw, false, false, 0);

			ListStore model = CreateModel ();

			TreeView treeview = new TreeView (model);
			sw.Add (treeview);

			TreeViewColumn column = new TreeViewColumn ();
			column.Title = "Name";
			CellRenderer renderer = new CellRendererPixbuf ();
			column.PackStart (renderer, false);
			column.SetAttributes (renderer, "stock_id", Column.Id);
			renderer = new CellRendererText ();
			column.PackStart (renderer, true);
			column.SetAttributes (renderer, "text", Column.Name);

			treeview.AppendColumn (column);
			treeview.AppendColumn ("Label", new CellRendererText (), "text", Column.Label);
			treeview.AppendColumn ("Accel", new CellRendererText (), "text", Column.Accel);
			treeview.AppendColumn ("ID", new CellRendererText (), "text", Column.Id);

			Alignment align = new Alignment (0.5f, 0.0f, 0.0f, 0.0f);
			hbox.PackEnd (align, false, false, 0);

			Frame frame = new Frame ("Selected Item");
			align.Add (frame);

			VBox vbox = new VBox (false, 8);
			vbox.BorderWidth = 8;
			frame.Add (vbox);

			typeLabel = new Label ();
			vbox.PackStart (typeLabel, false, false, 0);
			iconImage = new Gtk.Image ();
			vbox.PackStart (iconImage, false, false, 0);
			accelLabel = new Label ();
			vbox.PackStart (accelLabel, false, false, 0);
			nameLabel = new Label ();
			vbox.PackStart (nameLabel, false, false, 0);
			idLabel = new Label ();
			vbox.PackStart (idLabel, false, false, 0);

			treeview.Selection.Mode = Gtk.SelectionMode.Single;
			treeview.Selection.Changed += new EventHandler (SelectionChanged);

			ShowAll ();
		}

		private ListStore CreateModel ()
		{
			ListStore store = new Gtk.ListStore (typeof (string), typeof(string), typeof(string), typeof(string), typeof (string));

			string[] stockIds = Gtk.Stock.ListIds ();
			Array.Sort (stockIds);

			// Use reflection to get the list of C# names
			Hashtable idToName = new Hashtable ();
			foreach (PropertyInfo info in typeof (Gtk.Stock).GetProperties (BindingFlags.Public | BindingFlags.Static)) {
				if (info.PropertyType == typeof (string))
					idToName[info.GetValue (null, null)] = "Gtk.Stock." + info.Name;
			}

			foreach (string id in stockIds) {
				Gtk.StockItem si;
				string accel;

				si = Gtk.Stock.Lookup (id);
				if (si.Keyval != 0)
					accel = Accelerator.Name (si.Keyval, si.Modifier);
				else
					accel = "";

				store.AppendValues (id, idToName[id], si.Label, accel);
			}

			return store;
		}

	}