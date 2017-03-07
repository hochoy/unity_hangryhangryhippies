// Calls loadmapbox(input_token) from mapbox_map.js if token is valid

function checkToken(input_token) {

    var url = "https://api.mapbox.com/styles/v1/mapbox/streets-v9?access_token=" + input_token;

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET",url,true);
    xmlHttp.onload = function() {
        if (xmlHttp.status == 200){
            loadmapbox(input_token);
        } else {
            deanimateLoader();
            alert("Token invalid, please try again.");
            console.log("alert");
        }
    };

    xmlHttp.send();
}
