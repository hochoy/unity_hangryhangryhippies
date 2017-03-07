var businesses;
var dataloaded = false;

document.getElementById('import').onclick = function() {
    var files = document.getElementById('selectFiles').files;
    if (files.length <= 0) {
        return false;
    }

    var fr = new FileReader();

    fr.onload = function(e) {

        var parseSuccess = true;

        try {
            businesses = JSON.parse(e.target.result);
        } catch (e) {
            alert("geoJSON not valid"); // error in the above string (in this case, yes)!
            parseSuccess = false;
        }

        if (parseSuccess) {
            dataloaded = true;
            attemptloadmapbox();
        }
        // var result = JSON.parse(e.target.result);
        //var formatted = JSON.stringify(result, null, 2);
        // businesses = result;
    };

    fr.readAsText(files.item(0));

};

// Doesn't work locally due to cross-origin restrictions
function loadsample() {
    $.getJSON("json/vanbiz_vancouver.geojson", function (data) {
        businesses = data;
        dataloaded = true;
        attemptloadmapbox();
    });

}
