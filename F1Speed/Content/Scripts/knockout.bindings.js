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
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        $(element).height(0);
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var $wheelspin = $(element);
        var spinValue = ko.utils.unwrapObservable(valueAccessor());

        var maxHeight = 75;

        $wheelspin.height(Math.floor(maxHeight * spinValue));
    }
}