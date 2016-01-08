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

function getWeatherTranslationText(text) {
    var ret = weatherTranslationArray[text];
    if (!ret)
        return text;
    else
        return ret;
}

function showWeatherDay(forecast) {
    html = '<div class="col-md-4 weatherForecast">';
    html += dayArray[forecast.day] + '<br />';
    html += '<img src="' + forecast.image + '" /><br />';
    html += '<span class="weatherText">' + getWeatherTranslationText(forecast.text) + '</span><br />';
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

var dayArray = {
    "Fri": "Freitag",
    "Sat": "Samstag",
    "Sun": "Sonntag",
    "Mon": "Montag",
    "Tue": "Dienstag",
    "Wed": "Mittwoch",
    "Thu": "Donnerstag"
};

var weatherTranslationArray = {
    "Severe thunderstorms": "Gewitter",
    "Thunderstorms": "Gewitter",
    "Mixed rain and snow": "Schneeregen",
    "Mixed rain and sleet": "Regen und Graupel",
    "Mixed snow and sleet": "Schnee und Graupel",
    "Freezing drizzle": "Nieselregen",
    "Drizzle": "Nieselregen",
    "Freezing rain": "Kalter Regen",
    "Showers": "Schauer",
    "Snow flurries": "Schneetreiben",
    "Light snow showers": "Schneeschauer",
    "Blowing snow": "Schneetreiben",
    "Snow": "Schnee",
    "Hail": "Hagel",
    "Sleet": "Schneeregen",
    "Dust": "Staub",
    "Foggy": "Neblig",
    "Fog": "Nebel",
    "AM Fog\/PM Sun": "Nebel \/ Sonne",
    "Mist": "Nebel",
    "Haze": "Dunst",
    "Smoky": "Verraucht",
    "Blustery": "Stürmisch",
    "Windy": "Windig",
    "Cold": "Kalt",
    "Cloudy": "Bewölkt",
    "Partly Cloudy": "Teilweise Bewölkt",
    "Clear (night)": "Klare Nacht",
    "Sunny": "Sonnig",
    "Fair (night)": "Schöne Nacht",
    "Fair (day)": "Schöner Tag",
    "Mixed rain and hail": "Regen und Hagel",
    "Hot": "Heiß",
    "Isolated thunderstorms": "vereinzelte Gewitter",
    "Scattered thunderstorms": "vereinzelte Gewitter",
    "Scattered showers": "vereinzelte Schauer",
    "Heavy snow": "schwerer Schnee",
    "Scattered snow showers": "vereinzelte Schneeschauer",
    "Thundershowers": "Gewitter",
    "Snow showers": "Schneeschauer",
    "Isolated thundershowers": "vereinzelte Gewitterschauer",
    "Not available": null,
    "Mostly Clear": "Großteils Klar",
    "Mostly Sunny": "Meistens Sonnig",
    "Mostly Cloudy": "Großteils Bewölkt",
    "PM Showers": "Regen am Abend",
    "AM Showers": "Regen am Morgen",
    "Rain": "Regen",
    "Heavy Rain": "Platzregen",
    "Light Rain": "leichter Regen",
    "Rain Shower": "Regen- schauer",
    "Light Rain Early": "vereinzelt Regen",
    "Clear": "Klar",
    "PM Thunderstorms": "Gewitter",
    "Cloudy\/Wind": "Bewölkt\/Wind",
    "Mostly Cloudy\/Wind": "Bewölkt\/Wind",
    "Thunderstorms Early": "Gewitter",
    "Isolated Thunderstorms": "vereinzelte Gewitter",
    "Light Drizzle": "leichter Niesel",
    "Showers Late": null,
    "AM Light Rain": "leichter Regen",
    "Scattered Thunderstorms": "vereinzelt Donner",
    "Light Rain Shower": "leichte Schauer",
    "Showers Early": null,
    "PM Light Rain": "leichter Regen",
    "PM Drizzle": "Nieselregen"
}