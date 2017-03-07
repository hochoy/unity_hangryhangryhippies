// changes display_token to reflect the user input for token.
// also saves the input_token as a js variable that can be used by mapbox_map.js

// function formChanged() {
//     var input_token = document.getElementsByName("input_token")[0].value;
//     // document.getElementsByName("display_token")[0].innerHTML = input_token;
// }

function searchKeyPress(e)
{
    // look for window.event in case event isn't passed in
    e = e || window.event;
    if (e.keyCode == 13)
    {
        attemptloadmapbox();
    }

}
