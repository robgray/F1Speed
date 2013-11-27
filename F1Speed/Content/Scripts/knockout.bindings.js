// Custom knockout-bindings
ko.bindingHandlers.wheelspin = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        $(element).height(0);
    },
    update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var $wheelspin = $(element);
        var spinValue = ko.utils.unwrapObservable(valueAccessor());

        var maxHeight = 75;
        var scale = 20.0;

        spinValue = Math.min(Math.abs(spinValue / scale), 1);

        $wheelspin.height(Math.floor(maxHeight * spinValue));
    }
};

ko.bindingHandlers.pedal = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        $(element).height(0);
    },
    update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var $wheelspin = $(element);
        var spinValue = ko.utils.unwrapObservable(valueAccessor());

        var maxHeight = 80;

        $wheelspin.height(Math.floor(maxHeight * spinValue));
    }
};

ko.bindingHandlers.timeDelta = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {        
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var timeDelta = ko.utils.unwrapObservable(valueAccessor());
        var isPositive = viewModel.isTimePositive();

        var $bar = $(element);
        var parentWidth = $bar.parent().width();
        $bar.position({ top: 0, left: parentWidth / 2 });
        
        var maxTime = 6;
        var barWidth = ((timeDelta / maxTime) / 2) * parentWidth;
        $bar.width(barWidth);
        
        if (isPositive) {
            $bar.css("margin-left", "" + parentWidth / 2 + "px");            
        } else {            
            $bar.css("margin-left", "" + (parentWidth / 2) - barWidth + "px");
        }
    }
};