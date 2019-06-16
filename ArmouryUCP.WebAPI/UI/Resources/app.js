var app = angular.module('armouryPanel', ['ngSanitize']);

/*app.config(function ($routeProvider) {

    $routeProvider.

        when('/', {
            templateUrl: 'index.html',
            controller: 'playerController'
        });

});*/

app.controller('mainController', function ($scope) {
});

app.controller('mainNavController', ['$scope', function ($scope) {
    var mainItems = this;

    mainItems.items = [
        { name: 'Profile', faItem: 'user', link: 'test' },
        { name: 'Staff', faItem: 'crown', link: 'test' },
        { name: 'Economy', faItem: 'comment-dollar', link: 'test' },
        { name: 'Complaints', faItem: 'exclamation-triangle', link: 'test' },
        { name: 'Unban Requests', faItem: 'user-lock', link: 'test' }
    ];
}]);

app.controller('secondaryNavController', ['$scope', function ($scope) {
    var secondaryItems = this;

    secondaryItems.items = [
        [ 'General', 'Properties', 'Businesses', 'Vehicles', 'Clan', 'Faction History', 'War Info', 'Stats' ],
        [ 'General', 'Properties', 'Businesses', 'Vehicles', 'Clan', 'Faction History', 'War Info', 'Stats' ]
    ];
}]);

app.controller('playerController', ['$scope', '$location', function ($scope, $location) {
    var factionHistoryRequest = new XMLHttpRequest();
    factionHistoryRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (factionHistoryRequest.readyState == 4 && factionHistoryRequest.status == 200) {
                $scope.factionHistory = JSON.parse(factionHistoryRequest.responseText);
            }
        });
    }

    var houseRequest = new XMLHttpRequest();
    houseRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (houseRequest.readyState == 4 && houseRequest.status == 200) {
                $scope.houseInfos = JSON.parse(houseRequest.responseText);

                $scope.housesValue = 0;
                $scope.housesCount = $scope.houseInfos.length;
                $scope.houseInfos.forEach(function (element) {
                    $scope.housesValue += element['Value'];
                });
            }
        });
    }

    var vehicleRequest = new XMLHttpRequest();
    vehicleRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (vehicleRequest.readyState == 4 && vehicleRequest.status == 200) {
                $scope.vehicleInfos = JSON.parse(vehicleRequest.responseText);

                $scope.vehiclesValue = 0;
                $scope.vehiclesCount = $scope.vehicleInfos.length;
                $scope.vehicleInfos.forEach(function (element) {
                    $scope.vehiclesValue += element['Value'];
                });
            }
        });
    }

    var businessRequest = new XMLHttpRequest();
    businessRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (businessRequest.readyState == 4 && businessRequest.status == 200) {
                $scope.businessInfos = JSON.parse(businessRequest.responseText);

                $scope.businessesValue = 0;
                $scope.businessesCount = $scope.businessInfos.length;
                $scope.businessInfos.forEach(function (element) {
                    $scope.businessesValue += element['Value'];
                });
            }
        });
    }

    var playerRequest = new XMLHttpRequest();
    playerRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (playerRequest.readyState == 4 && playerRequest.status == 200) {
                $scope.playerInfos = JSON.parse(playerRequest.responseText);
                $scope.modelPadded = ("00" + $scope.playerInfos.Model).slice(-3);
                $scope.adminLevelName = getAdminLevel($scope.playerInfos['AdminLevel']);

                houseRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house/" + $scope.playerInfos['Name']);
                houseRequest.send();

                vehicleRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/vehicle/" + $scope.playerInfos['Name']);
                vehicleRequest.send();

                businessRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/business/" + $scope.playerInfos['Name']);
                businessRequest.send();

                factionHistoryRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $location.path().split('/').pop() + "/factionhistory");
                factionHistoryRequest.send();
            }
        });
    }

    function getAdminLevel($adminLevel) {
        switch ($adminLevel) {
            case 0:
                return 'None';
                break;
            case 6:
                return 'Coordonator';
                break;
            case 7:
                return 'Scripter si Fondator'
                break;
            case 8:
                return 'Fondator';
                break;
            default:
                return 'Admin ' + $adminLevel;
                break;
        }
    }

    playerRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $location.path().split('/').pop());
    playerRequest.send();
}]);