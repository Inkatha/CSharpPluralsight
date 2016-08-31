// tripsController.js
(function () {

    "use strict";

    // Getting the existing module
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {
        var viewModel = this;

        viewModel.trips = [];

        viewModel.newTrip = {};

        viewModel.errorMessage = "";
        viewModel.isBusy = true;

        /* Get all trips */
        $http.get("/api/trips")
            .then(function (response) {
                // Success
                angular.copy(response.data, viewModel.trips);
            }, function (error) {
                // Failure
                viewModel.errorMessage = "Failed to load data";
            })
        .finally(function () {
            viewModel.isBusy = false;
        });

        /* Add a trip */
        viewModel.addTrip = function () {

            viewModel.isBusy = true;
            viewModel.errorMessage = "";

            $http.post("/api/trips", viewModel.newTrip)
                .then(function (response) {
                    // Success
                    viewModel.trips.push(response.data);
                    viewModel.newTrip = {};
                }, function (error) {
                    // Failure
                    viewModel.errorMessage = "Failed to save new trip";
                })
                .finally(function () {
                    viewModel.isBusy = false;
                });
        };


    }
})();