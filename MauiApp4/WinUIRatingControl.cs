using Microsoft.Maui.Handlers;

#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif

namespace MauiApp4;

public class WinUIRatingControl : View
{
    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(double), typeof(WinUIRatingControl), 0.0);

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}

#if WINDOWS
public class WinUIRatingControlHandler : ViewHandler<WinUIRatingControl, RatingControl>
{
    public static IPropertyMapper<WinUIRatingControl, WinUIRatingControlHandler> Mapper =
        new PropertyMapper<WinUIRatingControl, WinUIRatingControlHandler>(ViewHandler.ViewMapper)
        {
            [nameof(WinUIRatingControl.Value)] = MapValue
        };

    public static CommandMapper<IPicker, IProgressBarHandler> CommandMapper =
        new CommandMapper<IPicker, IProgressBarHandler>(ViewHandler.ViewCommandMapper)
        {
        };

    public WinUIRatingControlHandler()
        : base(Mapper, CommandMapper)
    {
    }

    public WinUIRatingControlHandler(IPropertyMapper mapper, CommandMapper commandMapper = null)
        : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
    {
    }

    protected override RatingControl CreatePlatformView() => new();

    public static void MapValue(WinUIRatingControlHandler handler, WinUIRatingControl view) =>
        handler.PlatformView.Value = view.Value;
}
#endif
