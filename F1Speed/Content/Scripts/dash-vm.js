function round(number, places) {

    if (isNaN(number)) {        
        return number;
    }

    if (Number(number) === 0) {
        return 0;
    }
    
    var rounder = Math.pow(10, places);
    var result = (Math.round(number * rounder) / rounder);

    return result.toFixed(places);
}

var DashViewModel = function() {

    var self = this;

    var MAX_TIME_DELTA = 5;

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

    self.fastestLap = ko.observable('');
    self.currentLap = ko.observable('');
    self.lastLap = ko.observable('');

    self.sector1Current = ko.observable('');
    self.sector1Delta = ko.observable('');
    self.sector1Fastest = ko.observable('');
    self.sector1IsFastest = ko.observable(false);
    
    self.sector2Current = ko.observable('');
    self.sector2Current = ko.observable('');
    self.sector2Delta = ko.observable('');
    self.sector2Fastest = ko.observable('');
    self.sector2IsFastest = ko.observable(false);
    
    self.sector3Current = ko.observable('');
    self.sector3Delta = ko.observable('');
    self.sector3Fastest = ko.observable('');
    self.sector3IsFastest = ko.observable(false);

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

        return (self.isTimePositive() ? "+" : "") + self.timeDelta();
    });

    self.connectionStatus = ko.computed(function() {
        return self.lostConnection() ? "Disconnected" : "Connected";
    });

    self.timebarCss = ko.computed(function() {
        return "timebar " + self.timeClass();
    });

    self.positiveTimeStyle = ko.computed(function() {
        var width = 100;
        if (self.timeDelta() < MAX_TIME_DELTA) {
            var percent = (self.timeDelta() / MAX_TIME_DELTA) * 100;
            width =  Math.floor(percent * 100) / 100;
        }

        return "width: " + width + "%; background-color: green";
    });

    self.negativeTimeStyle = ko.computed(function() {
        if (self.timeDelta() <= -MAX_TIME_DELTA)
            return "width: 0%";
        else {
            var percent = (self.timeDelta() / -MAX_TIME_DELTA) * 100;                       

            return "width: " + (Math.floor((100 - percent) * 100) / 100) + "%";
        }
    });

    self.inverseNegativeTimeStyle = ko.computed(function() {
        if (self.timeDelta() <= -MAX_TIME_DELTA)
            return "width: 100%;background-color: #FF0000;";
        else {
            var percent = (self.timeDelta() / -MAX_TIME_DELTA) * 100;

            return "width: " + Math.floor(percent * 100) / 100 + "%; background-color: #FF0000;";
        }
    });

    self.inversePositiveTimeStyle = ko.computed(function() {
        var width = 0;
        if (self.timeDelta() < MAX_TIME_DELTA) {
            var percent = self.timeDelta() / MAX_TIME_DELTA;
            width = 100 - (percent * 100);
        }

        return "width: " + (Math.floor(width * 100) / 100) + "%;";
    });

    self.sector1Style = ko.computed(function() {
        return self.sector1IsFastest() ? "color: green;" : "color: red;";
    });
    
    self.sector2Style = ko.computed(function () {
        return self.sector2IsFastest() ? "color: green;" : "color: red;";
    });
    
    self.sector3Style = ko.computed(function () {
        return self.sector3IsFastest() ? "color: green;" : "color: red;";
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

                self.fastestLap(data.FastestLap);
                self.currentLap(data.CurrentLap);
                self.lastLap(data.LastLap);

                self.sector1Current(data.Sector1.CurrentTime);
                self.sector1Delta(data.Sector1.DeltaTime);
                self.sector1Fastest(data.Sector1.FastestTime);
                self.sector1IsFastest(data.Sector1.IsFaster);

                self.sector2Current(data.Sector2.CurrentTime);
                self.sector2Delta(data.Sector2.DeltaTime);
                self.sector2Fastest(data.Sector2.FastestTime);
                self.sector2IsFastest(data.Sector2.IsFaster);

                self.sector3Current(data.Sector3.CurrentTime);
                self.sector3Delta(data.Sector3.DeltaTime);
                self.sector3Fastest(data.Sector3.FastestTime);
                self.sector3IsFastest(data.Sector3.IsFaster);
                
                setTimeout(function () { getPacket(); }, 100);
            },
            contentType: "application/json"
        });
    }
    
    init();
};
