// Loads mapbox map using token and pre-defined parameters

function attemptloadmapbox() {
    animateLoader();
    var input_token = document.getElementsByName("input_token")[0].value;
    checkToken(input_token); // will call loadmapbox() if token is valid
}

function loadmapbox(input_token) {
    mapboxgl.accessToken = input_token;

    // var bounds = [
    //     [-74.04728500751165, 40.68392799015035], // Southwest coordinates
    //     [-73.91058699000139, 40.77764500765852] // Northeast coordinates
    // ];

    var map = new mapboxgl.Map({
        container: "map",
        style: "mapbox://styles/mapbox/streets-v9",
        minZoom: "1",
        maxZoom: "20",
        hash: "TRUE",
        center: [-123.1001, 49.2254],
        zoom: "10.47",
        interactive: "TRUE",
        bearing: "0",
        pitch: "34"
            // maxBounds: bounds
    });

    if(dataloaded) {
        map.on('load', function(e) {
            // Add a GeoJSON source containing place coordinates and information.
            map.addSource("places", {
                type: "geojson",
                data: businesses
            });

            map.addLayer({
                "id": "locations",
                "type": "symbol",
                "source": "places",
                "layout": {
                    "icon-image": "rocket-15",
                    "icon-allow-overlap": true
                }
            });

        });
    }

    document.getElementById('fly').addEventListener('click', function() {

        var homepoint = [-123.1001, 49.2254];
        var target = homepoint;
        map.flyTo({

            center: target,
            zoom: 10.47,
            bearing: 0,
            pitch: 34,

            // These options control the flight curve, making it move
            // slowly and zoom out almost completely before starting
            // to pan.
            speed: 1.5, // make the flying slow
            curve: 1, // change the speed at which it zooms out

            // This can be any easing function: it takes a number between
            // 0 and 1 and returns another number between 0 and 1.
            easing: function(t) {
                return t;
            }
        });
    });

    deanimateLoader();

    map.addControl(new MapboxGeocoder({
        accessToken: mapboxgl.accessToken
    }));

    map.addControl(new mapboxgl.NavigationControl());
}
