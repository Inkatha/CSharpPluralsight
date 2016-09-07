!function(){"use strict";function t(t,n){var e=this;e.tripName=t.tripName,e.stops=[],e.newStop={};var s="/api/trips/"+e.tripName+"/stops";e.errorMessage="",e.isBusy=!0,n.get(s).then(function(t){angular.copy(t.data,e.stops),o(e.stops)},function(t){e.errorMessage="Failed to load stops."}).finally(function(){e.isBusy=!1}),e.addStop=function(){e.isBusy=!0,n.post(s,e.newStop).then(function(t){e.stops.push(t.data),o(e.stops),e.newStop={}},function(t){e.errorMessage="Unable to create a new stop"}).finally(function(){e.isBusy=!1})}}function o(t){if(t&&t.length>0){var o=_.map(t,function(t){return{lat:t.latitude,long:t.longitude,info:t.name}});travelMap.createMap({stops:o,selector:"#map",currentStop:1,initialZoom:3})}}t.$inject=["$routeParams","$http"],angular.module("app-trips").controller("tripEditorController",t)}();