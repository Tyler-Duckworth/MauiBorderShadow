namespace MauiBorderShadow.Controls;

public partial class Card : ContentView
{
    double defaultHeight;
    const string expandAnimationName = nameof(expandAnimationName);
    bool IsExpanded = false;
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(Card), "");
    public string Title
    {
        get => (string)GetValue(Card.TitleProperty);
        set => SetValue(Card.TitleProperty, value);
    }

    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(Card), "");
    public string Subtitle
    {
        get => (string)GetValue(Card.SubtitleProperty);
        set => SetValue(Card.SubtitleProperty, value);
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (!IsExpanded)
        {
            defaultHeight = contentView.Height;
            stackLayout.Add(new Label
            {
                Text = Subtitle,
                TextColor = Colors.Black
            });
        } else
        {
            stackLayout.RemoveAt(stackLayout.Count - 1);
        }

        double endSize = IsExpanded ? defaultHeight : (contentView.Measure(Width, double.PositiveInfinity).Request.Height);
        new Animation(v => contentView.HeightRequest = v, contentView.Height, endSize)
            .Commit(contentView, expandAnimationName, easing: Easing.Linear, finished: (value, isInterrupted) =>
            {
                if (isInterrupted)
                    return;
                IsExpanded = !IsExpanded;
            });
    }
    public Card()
	{
		InitializeComponent();
	}
}