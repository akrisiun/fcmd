using System;
using Gtk;

/*
 * from gi.repository import Gtk

class TestWindow(Gtk.Window):
    def __init__(self):
        Gtk.Window.__init__(self)
        self.resize(400, 400)
        self.connect("delete-event", Gtk.main_quit)

        ls = Gtk.ListStore(str)
        ls.append(["Testrow 1"])
        ls.append(["Testrow 2"])
        ls.append(["Testrow 3"])
        tv = Gtk.TreeView(ls)
        tr = Gtk.CellRendererText()
        col = Gtk.TreeViewColumn("Testcolumn", tr, text=0)
        tv.append_column(col)
        sel = tv.get_selection()
        sel.set_mode(Gtk.SelectionMode.MULTIPLE)

        self.add(tv)
        self.show_all()

if __name__ == "__main__":
    app = TestWindow()
    Gtk.main()
Note that you are using an incorrect treeselection mode in your question, the correct one is Gtk.SelectionMode.MULTIPLE.
*/


public partial class MainWindow: Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        Store = StoreSeal();
        // var view = this.nodeview1;
        var view = this.treeview1 as Gtk.TreeView;

        // var viewStore = new Gtk.NodeView(Store);
        view.Model = Store;
        view.Selection.Mode = SelectionMode.Multiple;
        
        // CellRendererText - Used to display text

        // Gtk.NodeView view = new Gtk.NodeView (Store);
        // Add (view);

        // Create a column with title Artist and bind its renderer to model column 0
        view.AppendColumn ("Artist", new Gtk.CellRendererText (), "text", 0);

        // Create a column with title 'Song Title' and bind its renderer to model column 1
        view.AppendColumn ("Song Title", new Gtk.CellRendererText (), "text", 1);
        view.ShowAll();

    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    public Gtk.ListStore Store { get; set; }

    Gtk.ListStore StoreSeal()
    {
        // var store = new Gtk.NodeStore (typeof (MyTreeNode));
        // var store = new Gtk.ListStore (typeof (MyTreeNode));
        // store.AddNode (new MyTreeNode ("The Beatles", "Yesterday"));
        var store = new Gtk.ListStore (typeof(string), typeof(string));

        store.AppendValues(new object[] { "The Beatles", "Yesterday" });
        store.AppendValues(new object[] { "Peter Gabriel", "In Your Eyes" });
        store.AppendValues(new object[] { "Rush", "Fly By Night"});
        return store;
    }

 /*
   private global::Gtk.HPaned hpaned1;
    private global::Gtk.ScrolledWindow GtkScrolledWindow;
    private global::Gtk.TreeView treeview1;

    protected virtual void Build ()
    {
        global::Stetic.Gui.Initialize (this);
        // Widget MainWindow
        this.Name = "MainWindow";
        this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
        this.WindowPosition = ((global::Gtk.WindowPosition)(4));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.hpaned1 = new global::Gtk.HPaned ();
        this.hpaned1.WidthRequest = 400;
        this.hpaned1.HeightRequest = 200;
        this.hpaned1.CanFocus = true;
        this.hpaned1.Name = "hpaned1";
        this.hpaned1.BorderWidth = ((uint)(2));

        // Container child hpaned1.Gtk.Paned+PanedChild
        
        this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
        this.GtkScrolledWindow.Name = "GtkScrolledWindow";
        this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
        // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
        
        this.treeview1 = new global::Gtk.TreeView();
        this.treeview1.CanFocus = true;
        this.treeview1.Name = "treeview1";

        this.GtkScrolledWindow.Add (this.treeview1);
        this.hpaned1.Add (this.GtkScrolledWindow);
        this.Add (this.hpaned1);
        if ((this.Child != null)) {
            this.Child.ShowAll ();
        }
        this.DefaultWidth = 802;
        this.DefaultHeight = 495;
        this.Show ();
        this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
    }
*/

}

// http://www.mono-project.com/docs/gui/gtksharp/widgets/treeview-tutorial/
// http://www.mono-project.com/docs/gui/gtksharp/widgets/nodeview-tutorial/
[Gtk.TreeNode (ListOnly=true)]
public class MyTreeNode : Gtk.TreeNode {

    string song_title;

    public MyTreeNode (string artist, string song_title)
    {
        Artist = artist;
        this.song_title = song_title;
    }

    [Gtk.TreeNodeValue (Column=0)]
    public string Artist;

    [Gtk.TreeNodeValue (Column=1)]
    public string SongTitle {get { return song_title; } }
}


