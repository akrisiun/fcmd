using Xwt;

namespace pluginner.Widgets
{
    public interface IStatusBar : ILabelWidget
    {
        string Text { get; set; }
    }

    public interface ILabelWidget : IContent
    { }

}
