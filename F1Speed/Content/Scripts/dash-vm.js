function round(number, places) {

    if (isNaN(number))
        return number;   

    if (parseInt(number) === 0)
        return 0;
   
    var rounder = Math.pow(10, places);
    var result = (Math.round(number * rounder) / rounder);    
    return result.toFixed(places);
}

var DashViewModel = function() {

    var self = this;

    self.isSpeedPositive = ko.observable(false);
    self.isTimePositive = ko.observable(false);
    self.lostConnection = ko.observable(true);
    self.speedDelta = ko.observable();
    self.timeDelta = ko.observable();
    self.circuitName = ko.observable();
    self.lapType = ko.observable();
    self.wheelspinRearLeft = ko.observable(0);
    self.wheelspinRearRight = ko.observable(0);
    self.wheelspinFrontLeft = ko.observable(0);
    self.wheelspinFrontRight = ko.observable(0);
    self.throttle = ko.observable(0);
    self.brake = ko.observable(0);

    self.speedClass = ko.computed(function() {
        if (self.speedDelta() === 0)
            return "";

        return self.isSpeedPositive() ? "positive" : "negative";
    });

    self.timeClass = ko.computed(function() {
        if (self.timeDelta() === 0)
            return "";

        return self.isTimePositive() ? "positive" : "negative";
    });

    self.speedDisplay = ko.computed(function() {
        if (self.lostConnection())
            return "ERR";

        if (self.speedDelta() === 0)
            return "0";

        return (self.isSpeedPositive() ? "+" : "-") + self.speedDelta();
    });

    self.timeDisplay = ko.computed(function() {
        if (self.lostConnection())
            return "ERR";

        if (self.timeDelta() === 0)
            return "0";

        return (self.isTimePositive() ? "-" : "+") + self.timeDelta();
    });

    self.connectionStatus = ko.computed(function() {
        return self.lostConnection() ? "Disconnected" : "Connected";
    });

    self.timebarCss = ko.computed(function() {
        return "timebar " + self.timeClass();
    });

    function init() {
        getPacket();
    }

    function getPacket() {
        $.ajax({
            url: "/api/packet",
            dataType: "json",
            error: function () {
                self.lostConnection(true);
                self.speedDelta(0);
                self.timeDelta(0);
                self.speedClass("");
                self.timeClass("");

                // Retry at a less frequent interval
                setTimeout(function () { getPacket(); }, 2000);
            },
            success: function (data) {
                self.lostConnection(false);
                self.isSpeedPositive(data.IsSpeedDeltaPositive);
                self.isTimePositive(data.IsTimeDeltaPositive);
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


                setTimeout(function () { getPacket(); }, 100);
            },
            contentType: "application/json"
        });
    }
    
    init();
};
