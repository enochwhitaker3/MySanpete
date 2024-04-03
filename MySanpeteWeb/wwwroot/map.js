var map;

function initializeMap(mapsKey, allOccasion) {
    // Initialize the Azure Maps
    atlas.setSubscriptionKey(mapsKey);
    // Create the map instance
    map = new atlas.Map("mapDiv", {
        view: "Auto",
        center: [-122.129, 47.640],
        zoom: 3
    });

    // Get user's location
    navigator.geolocation.getCurrentPosition(function (position) {
        var userLocation = [position.coords.longitude, position.coords.latitude];
        map.setCamera({
            center: userLocation,
            zoom: 13
        });
    });





    allOccasion.forEach((item) => {
        var marker = new atlas.HtmlMarker({
            color: 'DodgerBlue',
            text: 'O',
            position: []
            popup: new atlas.Popup({
                content: '<div style="padding:10px">BUUUUUUUNGER</div>',
                pixelOffset: [0, -30]
            })
        });
        map.markers.add(marker);

        // Do something with each item
        console.log(item);
    })

    

    //Add a click event to toggle the popup.
    map.events.add('click', marker, () => {
        marker.togglePopup();
    });

}

