using Microsoft.Maui.Handlers;

#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif

namespace MauiApp4;

public class WinUIRatingControl : View
{
    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(double), typeof(WinUIRatingControl), -1.0);

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}

#if WINDOWS
public class WinUIRatingControlHandler : ViewHandler<WinUIRatingControl, RatingControl>
{
    // mappers for properties
    public static IPropertyMapper<WinUIRatingControl, WinUIRatingControlHandler> Mapper =
        new PropertyMapper<WinUIRatingControl, WinUIRatingControlHandler>(ViewHandler.ViewMapper)
        {
            [nameof(WinUIRatingControl.Value)] = MapValue
        };

    // mappers for commands - not used here
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

    // creathe the WinUI control
    protected override RatingControl CreatePlatformView() => 
        new RatingControl
        {
            HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
        };

    // when the MAUI view is created and ready to be used
    protected override void ConnectHandler(RatingControl platformView)
    {
        base.ConnectHandler(platformView);

        // attach the event to watch for interesting WinUI control updates
        platformView.ValueChanged += OnPlatformValueChnaged;
    }

    // when the framework decides it is time to clean up the MAUI view
    protected override void DisconnectHandler(RatingControl platformView)
    {
        // disconnect the event we attached
        platformView.ValueChanged -= OnPlatformValueChnaged;

        base.DisconnectHandler(platformView);
    }

    // the WinUI control is updated, so let the MAUI view know
    private void OnPlatformValueChnaged(RatingControl sender, object args)
    {
        if (VirtualView is not null)
            VirtualView.Value = sender.Value;
    }

    // respond to MAUI view updates and pass it along to the WinUI control
    public static void MapValue(WinUIRatingControlHandler handler, WinUIRatingControl view) =>
        handler.PlatformView.Value = view.Value;
}
#endif
