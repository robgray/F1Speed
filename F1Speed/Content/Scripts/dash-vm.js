function round(number, places) {

    if (isNaN(number))
        return number;   

    if (parseInt(number) === 0)
        return 0;
   
    var rounder = Math.pow(10, places);
    var result = (Math.round(number * rounder) / rounder);    
    return result.toFixed(places);
}

var DashViewModel = function ($, ko) {

    var self = this;
    self.speedDelta = ko.observable();
    self.timeDelta = ko.observable();
    self.speedClass = ko.observable();
    self.timeClass = ko.observable();
    self.circuitName = ko.observable();
    self.lapType = ko.observable();
    self.wheelspinRearLeft = ko.observable(0);
    self.wheelspinRearRight = ko.observable(0);
    self.wheelspinFrontLeft = ko.observable(0);
    self.wheelspinFrontRight = ko.observable(0);
    self.throttle = ko.observable(0);
    self.brake = ko.observable(0);
    
    function init() {
        getPacket();
    }
    
    function getPacket() {
        $.ajax({
            url: "/api/packet",
            dataType: "json",
            error: function() {
                self.speedDelta("ERR");
                self.timeDelta("ERR");
                self.speedClass("");
                self.timeClass("");
                
                setTimeout(function () { getPacket(); }, 2000);
            },
            success: function (data) {
                console.log(data);

                self.circuitName(data.CircuitName);
                self.lapType(data.LapType);
                self.speedDelta(round(data.SpeedDelta, 1));
                self.timeDelta(round(data.TimeDelta, 2));
                self.wheelspinRearLeft(data.WheelspinRearLeft);
                self.wheelspinRearRight(data.WheelspinRearRight);
                self.wheelspinFrontLeft(data.WheelspinFrontLeft);
                self.wheelspinFrontRight(data.WheelspinFrontRight);
                self.throttle(data.Throttle);
                self.brake(data.Brake);
                                
                self.speedClass(data.IsSpeedDeltaPositive ? "positive" : "negative");
                self.timeClass(data.IsTimeDeltaPositive ? "positive" : "negative");
                
                setTimeout(function() { getPacket(); }, 100);
            },
            contentType: "application/json"
        });
    }

    init();

    return {
        speedDelta: self.speedDelta,
        timeDelta: self.timeDelta
    };

}(jQuery, ko)
