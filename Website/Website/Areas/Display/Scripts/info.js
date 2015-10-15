function getWeather() {
    $.simpleWeather({
        location: '85419',
        unit: 'c',
        success: function (weather) {
            $("#city").text(weather.city);

            html = '<h2>' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.alt.temp + '&deg;C</li></ul>';

            html = showWeatherDay(weather.forecast[0]);
            html += showWeatherDay(weather.forecast[1]);
            html += showWeatherDay(weather.forecast[2]);

            $("#weather").html(html);
        },
        error: function (error) {
            $("#weather").html('<p>' + error + '</p>');
        }
    });
}

function showWeatherDay(forecast) {
    html = '<div class="col-md-4 weatherForecast">';
    html += forecast.day + '<br />';
    html += '<img src="' + forecast.image + '" /><br />';
    html += '<span class="weatherText">' + forecast.text + '</span><br />';
    html += forecast.low + ' / ' + forecast.high + ' °C';
    html += '</div>';
    return html;
}

function getCalendar() {
    //Next Events
    $.getJSON("http://www.feuerwehr-mauern.de/scripts/cal.php?callback=?", function (data) {
        var items = [];
        var i = 0;

        var countItems = 7;

        console.log(data);

        $.each(data, function (key, val) {
            var fakedate = new Date(val["UNIX_TIMESTAMP"] * 1000);
            var date = new Date((val["UNIX_TIMESTAMP"] - fakedate.getTimezoneOffset() * 60) * 1000); //iCal .. 

            console.log(i + ", Offset: " + fakedate.getTimezoneOffset());
            items.push("<tr><td>" + date.format('d M. H:i') + "</td><td>" + val["SUMMARY"] + "</td></tr>");

            i++;
            if (i >= countItems) return false;
        });

        $(".cal-content").html(
            $("<tbody/>", {
                "class": "tile-cal-list",
                html: items.join("")
            })
        );
    });
}

$(document).ready(function () {
    $("#clock").simpleClock();

    getCalendar();
    getWeather();

    setInterval(getCalendar, 600000); //Update the calendar every 10 minutes.
    setInterval(getWeather, 600000); //Update the Weather every 10 minutes.
});