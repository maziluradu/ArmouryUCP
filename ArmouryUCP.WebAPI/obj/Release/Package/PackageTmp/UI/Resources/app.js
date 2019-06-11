var app = angular.module('armouryPanel', []);

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

    var playerRequest = new XMLHttpRequest();
    playerRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (playerRequest.readyState == 4 && playerRequest.status == 200) {
                $scope.playerInfos = JSON.parse(playerRequest.responseText);
                $scope.adminLevelName = getAdminLevel($scope.playerInfos['AdminLevel']);

                houseRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house/" + $scope.playerInfos['Name']);
                houseRequest.send();
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