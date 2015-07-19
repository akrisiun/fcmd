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
        public static void Test(Gtk.Window gtkWindow)
        {
            var style = gtkWindow.StyleContext;

            // VBox vbox = new VBox(false, 0);
            // gtkWindow.Add(vbox);
            VBox vbox = (gtkWindow.Children[0]) as VBox;
            // -- gtkWindow.ContentArea.PackStart(vbox, true, true, 0);

            //VPaned vpaned = new VPaned { BorderWidth = 5 };
            //vbox.PackStart(vpaned, true, true, 0);      // expand, fill, padding

            HPaned hpaned = new HPaned();
            // vpaned.Add1(hpaned);
            vbox.Add(hpaned);

            Frame frame = new Frame() { ShadowType = ShadowType.In };
            frame.SetSizeRequest(60, 60);
            hpaned.Add1(frame);

            Frame frame2 = new Frame() { ShadowType = ShadowType.In };
            frame2.SetSizeRequest(160, 60);
            hpaned.Add2(frame2);

            Gtk.Button button = new Button("_Hi there GTK3");
            frame.Add(button);

            // gtkWindow.Resizable = true;
            // VBox vbox = new VBox(false, 5);
            // this.ContentArea.PackStart(vbox, true, true, 0);
            // vbox.BorderWidth = 5;

            var f2box = new HBox { BorderWidth = 3 };
            frame2.Add(f2box);

            //f2box.PackStart(new Label("Expander demo. Click on the triangle for details."), false, false, 0);

            //// Create the expander
            //Expander expander = new Expander("Details");
            //expander.Add(new Label("Details can be shown or hidden."));
            //f2box.PackStart(expander, false, false, 0);

            Gtk.Button button2 = new Button("_Hi there Side2");
            f2box.Add(button2);

            // .AddButton(Stock.Close, ResponseType.Close);
            // frame2.Add(new Button { Label = "Close.. " });

            gtkWindow.HeightRequest = 200;
            gtkWindow.WidthRequest = 400;
            // gtkWindow.ShowNow();
        }


        // Dictionary<ToggleButton, Widget> children = new Dictionary<ToggleButton, Widget>();

        public DemoPanes() : base("Panes")
        {
            VBox vbox = new VBox(false, 0);
            Add(vbox);

            VPaned vpaned = new VPaned();
            vbox.PackStart(vpaned, true, true, 0);
            vpaned.BorderWidth = 5;

            HPaned hpaned = new HPaned();
            vpaned.Add1(hpaned);

            Frame frame = new Frame();
            frame.ShadowType = ShadowType.In;
            frame.SetSizeRequest(60, 60);
            hpaned.Add2(frame);

            Gtk.Button button = new Button("_Hi there");
            frame.Add(button);

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
