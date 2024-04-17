var map;

function initializeMap(mapsKey, jsonOccasions, jsonBusinesses) {
    // Initialize the Azure Maps
    atlas.setSubscriptionKey(mapsKey);
    // Create the map instance
    try {

        map = new atlas.Map("mapDiv", {
            view: "Auto",
            center: [-111.584933, 39.359856],
            zoom: 13
        });

        // Get user's location
        navigator.geolocation.getCurrentPosition(function (position) {
            var userLocation = [position.coords.longitude, position.coords.latitude];
            map.setCamera({
                center: userLocation,
                zoom: 13
            });
        });

        var deserializedData = JSON.parse(jsonOccasions);

        deserializedData.forEach((item) => {
            var marker = new atlas.HtmlMarker({
                color: 'DodgerBlue',
                text: 'O',
                position: [item.YCoordinate, item.XCoordinate],
                popup: new atlas.Popup({
                    content: `<div style="padding:10px">${item.Title}</div>`,
                    pixelOffset: [0, -30]
                })
            });
            map.markers.add(marker);

            map.events.add('click', marker, () => {
                marker.togglePopup();
            });
        })

        //Add a click event to toggle the popup.
        var deserializedData = JSON.parse(jsonBusinesses);

        deserializedData.forEach((item) => {
            var marker = new atlas.HtmlMarker({
                color: 'DodgerBlue',
                text: 'B',
                position: [item.YCoordinate, item.XCoordinate],
                popup: new atlas.Popup({
                    content: `<div style="padding:10px">${item.BusinessName}</div>`,
                    pixelOffset: [0, -30]
                })
            });

            if (item.YCoordinate !== null || item.XCoordinate !== null) {
                map.markers.add(marker);

                map.events.add('click', marker, () => {
                    marker.togglePopup();
                });
            }
        })


    }
    catch
    {
        //Don't get mad heber
    }
}

function focusMap(YCoordinate, XCoordinate) {
    map.setCamera({
        center: [YCoordinate, XCoordinate],
        zoom: 17
    });
}

