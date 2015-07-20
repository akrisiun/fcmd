using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd
{

    //     [Demo("Paned Widget", "DemoPanes.cs")]
    public class DemoPanes : Gtk.Window
    {
        public static void Test(MainWindow window, Gtk.Window gtkWindow)
        {
            // var vbox = new VBox(false, 0);
            Gtk.VBox vbox = window.MainBox; 
            window.Padding = 0;

            // Backend.SetChild((IWidgetBackend)BackendHost.ToolkitEngine.GetSafeBackend(child));
            var back = window.BackEndGtk;
            var engine = Xwt.Toolkit.CurrentEngine;
            if (engine == null)
                return;

            VPaned vpaned = new VPaned { BorderWidth = 0 };
            // vbox.PackStart(vpaned, false, true, 0);      // expand, fill, padding
            vbox.Add(vpaned);
            vbox.PackStart(vpaned, true, true, 0);      // expand, fill, padding

            HPaned hpaned = new HPaned();
            // vbox.Add(hpaned);
            // vbox.PackStart(hpaned, true, true, 0);
            vpaned.Add(hpaned); // .PackStart(hpaned, true, true, 0);

            Frame frame = new Frame() { ShadowType = ShadowType.In };
            frame.SetSizeRequest(60, 60);
            hpaned.Add1(frame);

            Frame frame2 = new Frame() { ShadowType = ShadowType.In };
            frame2.SetSizeRequest(160, 60);
            hpaned.Add2(frame2);

            Gtk.Button button = new Button("_Hi there GTK3");
            frame.Add(button);

            var f2box = new HBox { BorderWidth = 3 };
            frame2.Add(f2box);
            // f2box.PackStart(new Label("Expander demo. Click on the triangle for details."), false, false, 0);

            //// Create the expander
            //Expander expander = new Expander("Details");
            //expander.Add(new Label("Details can be shown or hidden."));
            //f2box.PackStart(expander, false, false, 0);

            Gtk.Button button2 = new Button("_Hi there Side2");
            f2box.Add(button2);
            f2box.PackStart(button2, true, true, 0);

            //var pc = f2box[button2] as Paned.PanedChild;
            //if (pc != null)
            //    pc.Resize = true;
            // bool resize, bool shrink);

            //Paned paned = child.Parent as Paned;
            //Paned.PanedChild pc = paned[child] as Paned.PanedChild;
            //pc.Resize = toggle.Active;
            // .AddButton(Stock.Close, ResponseType.Close);

            gtkWindow.SetSizeRequest(800, 200);
            hpaned.Child1.SetSizeRequest(398, 200);
            hpaned.Child2.SetSizeRequest(398, 200);
        }


        // Dictionary<ToggleButton, Widget> children = new Dictionary<ToggleButton, Widget>();

        public DemoPanes() : base("Panes")
        {
            var vig1 = this.DefaultWidget;

            VBox vbox = new VBox(false, 0);
            Add(vbox);

            VPaned vpaned = new VPaned();
            vbox.PackStart(vpaned, true, true, 0);
            vpaned.BorderWidth = 2;

            HPaned hpaned = new HPaned();
            vpaned.Add2(hpaned);

            Frame frame = new Frame();
            frame.ShadowType = ShadowType.In;
            frame.SetSizeRequest(60, 60);
            // hpaned.Add2(frame);
            hpaned.Add1(frame);

            Gtk.Button button = new Button("_Hi there");
            frame.Add(button);

            Frame frame2 = new Frame();
            frame2.ShadowType = ShadowType.In;
            frame2.SetSizeRequest(60, 60);
            hpaned.Add2(frame2);

            Gtk.Button button2 = new Button("_Hi on Right side");
            frame2.Add(button2);

            // Now create toggle buttons to control sizing
            //vbox.PackStart(CreatePaneOptions(hpaned,
            //                   "Horizontal",
            //                   "Left",
            //                   "Right"),
            //        false, false, 0);

            //vbox.PackStart(CreatePaneOptions(vpaned,
            //                   "Vertical",
            //                   "Top",
            //                   "Bottom"),
            //        false, false, 0);

            // ShowAll();
            vbox.SetSizeRequest(400, 300);
            hpaned.Child1.SetSizeRequest(200 -3, 300);

            var h = vbox.Halign;  // Fill
            var v = vbox.Valign;
            var vexp = vbox.Vexpand;
            var veset = vbox.VexpandSet;
        }

        Frame CreatePaneOptions(Paned paned, string frameLabel,
             string label1, string label2)
        {
            Frame frame = new Frame(frameLabel);
            frame.BorderWidth = 4;
            return frame;
        }

    }
}

/*

    	Entry entry = new Entry ();
			vbox.PackStart (entry, false, true, 0);

			entry.Completion = new EntryCompletion ();
			entry.Completion.Model = CreateCompletionModel ();
			entry.Completion.TextColumn = 0;

			AddButton (Stock.Close, ResponseType.Close);

			ShowAll ();
			Run ();
			Destroy ();
		}

		ITreeModel CreateCompletionModel ()
		{
			ListStore store = new ListStore (typeof (string));

			store.AppendValues ("GNOME");
			store.AppendValues ("total");
			store.AppendValues ("totally");

			return store;
		}

    "  </menubar>" +
		"  <toolbar  name='ToolBar'>" +
		"    <toolitem name='open' action='Open'/>" +
		"    <toolitem name='quit' action='Quit'/>" +
		"    <separator action='Sep1'/>" +
		"    <toolitem name='logo' action='Logo'/>" +
		"  </toolbar>" +
		"</ui>";

		public DemoUIManager () : base ("UI Manager")
		{
			ActionEntry[] entries = new ActionEntry[] {
				new ActionEntry ("FileMenu", null, "_File", null, null, null),
				new ActionEntry ("PreferencesMenu", null, "_Preferences", null, null, null),
				new ActionEntry ("ColorMenu", null, "_Color", null, null, null),
				new ActionEntry ("ShapeMenu", null, "_Shape", null, null, null),
				new ActionEntry ("HelpMenu", null, "_Help", null, null, null),
				new ActionEntry ("New", Stock.New, "_New", "<control>N", "Create a new file", new EventHandler (ActionActivated)),
				new ActionEntry ("Open", Stock.Open, "_Open", "<control>O", "Open a file", new EventHandler (ActionActivated)),
				new ActionEntry ("Save", Stock.Save, "_Save", "<control>S", "Save current file", new EventHandler (ActionActivated)),
				new ActionEntry ("SaveAs", Stock.SaveAs, "Save _As", null, "Save to a file", new EventHandler (ActionActivated)),
				new ActionEntry ("Quit", Stock.Quit, "_Quit", "<control>Q", "Quit", new EventHandler (ActionActivated)),
				new ActionEntry ("About", null, "_About", "<control>A", "About", new EventHandler (ActionActivated)),
				new ActionEntry ("Logo", "demo-gtk-logo", null, null, "Gtk#", new EventHandler (ActionActivated))
			};

			ToggleActionEntry[] toggleEntries = new ToggleActionEntry[] {
				new ToggleActionEntry ("Bold", Stock.Bold, "_Bold", "<control>B", "Bold", new EventHandler (ActionActivated), true)
			};


			RadioActionEntry[] colorEntries = new RadioActionEntry[] {
				new RadioActionEntry ("Red", null, "_Red", "<control>R", "Blood", (int)Color.Red),
				new RadioActionEntry ("Green", null, "_Green", "<control>G", "Grass", (int)Color.Green),
				new RadioActionEntry ("Blue", null, "_Blue", "<control>B", "Sky", (int)Color.Blue)
			};

			RadioActionEntry[] shapeEntries = new RadioActionEntry[] {
				new RadioActionEntry ("Square", null, "_Square", "<control>S", "Square", (int)Shape.Square),
				new RadioActionEntry ("Rectangle", null, "_Rectangle", "<control>R", "Rectangle", (int)Shape.Rectangle),
				new RadioActionEntry ("Oval", null, "_Oval", "<control>O", "Egg", (int)Shape.Oval)
			};

			ActionGroup actions = new ActionGroup ("group");
			actions.Add (entries);
			actions.Add (toggleEntries);
			actions.Add (colorEntries, (int)Color.Red, new ChangedHandler (RadioActionActivated));
			actions.Add (shapeEntries, (int)Shape.Oval, new ChangedHandler (RadioActionActivated));

			UIManager uim = new UIManager ();
			uim.InsertActionGroup (actions, 0);
			AddAccelGroup (uim.AccelGroup);
			uim.AddUiFromString (uiInfo);

			VBox box1 = new VBox (false, 0);

    */
