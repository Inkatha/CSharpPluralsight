// tripEditorController.js
(function () {

    "use strict";
    
    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var viewModel = this;

        viewModel.tripName = $routeParams.tripName;
        viewModel.stops = [];
        viewModel.newStop = {};

        var url = "/api/trips/" + viewModel.tripName + "/stops";

        viewModel.errorMessage = "";
        viewModel.isBusy = true;

        $http.get(url)
            .then(function (response) {
                // Success
                angular.copy(response.data, viewModel.stops);
                _showMap(viewModel.stops);
            }, function (err) {
                // Failure
                viewModel.errorMessage = "Failed to load stops.";
            })
            .finally(function() {
                viewModel.isBusy = false;
            });

        viewModel.addStop = function () {

            viewModel.isBusy = true;

            $http.post(url, viewModel.newStop)
                .then(function (response) {
                    // Success
                    viewModel.stops.push(response.data);
                    _showMap(viewModel.stops);
                    viewModel.newStop = {};
                }, function (error) {
                    // Failure
                    viewModel.errorMessage = "Unable to create a new stop";
                })
                .finally(function () {
                    viewModel.isBusy = false;
                });
        };
    }

    function _showMap(stops) {

        if (stops && stops.length > 0) {

            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            });
        }
    }
})();