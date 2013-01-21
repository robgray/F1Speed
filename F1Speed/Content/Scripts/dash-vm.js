var DashViewModel = function ($, ko) {

    var self = this;
    self.speedDelta = ko.observable();
    self.timeDelta = ko.observable();
    self.speedClass = ko.observable();
    self.timeClass = ko.observable();

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
            },
            success: function(data) {                
                self.speedDelta(data.SpeedDelta);
                self.timeDelta(data.TimeDelta);
                self.speedClass(data.IsSpeedDeltaPositive === "true" ? "positive" : "negative");
                self.timeClass(data.IsTimeDeltaPositive === "true" ? "positive" : "negative");

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