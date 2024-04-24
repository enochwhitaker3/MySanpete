var map;

function initializeMap(mapsKey, jsonOccasions, jsonBusinesses) {
    // Initialize the Azure Maps
    atlas.setSubscriptionKey(mapsKey);
    // Create the map instance
    map = new atlas.Map("mapDiv", {
        view: "Auto",
        center: [-111.58251375208721, 39.36257310977088],
        zoom: 13,
        showFeedbackLink: false,
        showLogo: false
    });



    map.setCamera({
        center: [-111.58251375208721, 39.36257310977088],
        zoom: 13
    });

    var deserializedOccasions = JSON.parse(jsonOccasions);

    deserializedOccasions.forEach((item) => {
        var marker = new atlas.HtmlMarker({
            color: 'DodgerBlue',
            text: 'O',
            position: [item.YCoordinate, item.XCoordinate],
            popup: new atlas.Popup({
                content: `<div style="padding:10px">${item.Description}</div>`,
                pixelOffset: [0, -30]
            })
        });
        map.markers.add(marker);

        const popupManager = map.popups;

        map.events.add('click', marker, () => {
            popupManager.clear();
            marker.togglePopup();
        });
    });

    var deserializedBusinesses = JSON.parse(jsonBusinesses);

    deserializedBusinesses.forEach((item) => {
        if (item.YCoordinate !== null || item.XCoordinate !== null) {
            var marker = new atlas.HtmlMarker({
                color: 'DodgerBlue',
                text: 'B',
                position: [item.YCoordinate, item.XCoordinate],
                popup: new atlas.Popup({
                    content: `<div style="padding:10px">${item.BusinessName}</div>`,
                    pixelOffset: [0, -30]
                })
            });

            map.markers.add(marker);


            const popupManager = map.popups;


            map.events.add('click', marker, () => {
                popupManager.clear();
                marker.togglePopup();
            });
        }
    });
}

function focusMap(YCoordinate, XCoordinate) {
    map.setCamera({
        center: [YCoordinate, XCoordinate],
        zoom: 13
    });
}
