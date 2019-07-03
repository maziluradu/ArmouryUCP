var app = angular.module('armouryPanel', ['ngRoute', 'ngSanitize', 'ngAnimate']);

app.config(function ($routeProvider) {

    $routeProvider.
        when('/', {
            templateUrl: 'main.html',
            controller: 'frontPageController'
        })
        .when('/player/:id', {
            templateUrl: 'profile.html',
            controller: 'playerController'
        })
        .when('/houses', {
            templateUrl: 'houses.html',
            controller: 'housesController'
        })
        .when('/houses/:id', {
            templateUrl: 'house.html',
            controller: 'houseController'
        })
        .when('/vehicles', {
            templateUrl: 'vehicles.html',
            controller: 'vehiclesController'
        })
        .when('/vehicles/:id', {
            templateUrl: 'vehicle.html',
            controller: 'vehicleController'
        })
        .when('/businesses', {
            templateUrl: 'businesses.html',
            controller: 'businessesController'
        })
        .when('/businesses/:id', {
            templateUrl: 'business.html',
            controller: 'businessController'
        })
        .otherwise({
            templateUrl: '404.html',
            controller: '404Controller'
        });

});

app.controller('mainController', ['$scope', function ($scope) {

}]);

app.controller('houseController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.loadingIconHeightOffset = $window.innerHeight;
    $scope.Math = $window.Math;

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house/" + $location.path().split('/').pop()
    }).then(function successCallback(response) {
        if (response.status == 200) {
            $scope.houseInfos = response.data;

            $http({
                method: 'GET',
                url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $scope.houseInfos['Owner'] + '/byname'
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    $scope.ownerInfos = response.data;
                }
            }, function errorCallback() {
            });
        }
    }, function errorCallback() {
        });

    $scope.getDifferenceInDays = function (date) {
        var parsedDate = new Date(date);
        return parseInt(Math.abs(parsedDate.getTime() - new Date().getTime()) / (1000 * 60 * 60 * 24));
    }
}]);

app.controller('vehicleController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.loadingIconHeightOffset = $window.innerHeight;
    $scope.Math = $window.Math;

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/vehicle/" + $location.path().split('/').pop()
    }).then(function successCallback(response) {
        if (response.status == 200) {
            $scope.vehicleInfos = response.data;

            $http({
                method: 'GET',
                url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $scope.vehicleInfos['Owner'] + '/byname'
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    $scope.ownerInfos = response.data;
                }
            }, function errorCallback() {
            });
        }
    }, function errorCallback() {
    });

    $scope.getDifferenceInDays = function (date) {
        var parsedDate = new Date(date);
        return parseInt(Math.abs(parsedDate.getTime() - new Date().getTime()) / (1000 * 60 * 60 * 24));
    }
}]);

app.controller('businessController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.loadingIconHeightOffset = $window.innerHeight;
    $scope.Math = $window.Math;

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/business/" + $location.path().split('/').pop()
    }).then(function successCallback(response) {
        if (response.status == 200) {
            $scope.businessInfos = response.data;

            $http({
                method: 'GET',
                url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $scope.businessInfos['Owner'] + '/byname'
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    $scope.ownerInfos = response.data;
                }
            }, function errorCallback() {
            });
        }
    }, function errorCallback() {
    });

    $scope.getDifferenceInDays = function (date) {
        var parsedDate = new Date(date);
        return parseInt(Math.abs(parsedDate.getTime() - new Date().getTime()) / (1000 * 60 * 60 * 24));
    }
}]);

app.controller('404Controller', ['$timeout', '$window', function ($timeout, $window) {
    $timeout(function () {
        $window.location.href = '/';
    }, 3000);
}]);

app.controller('businessesController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.mainLocation = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + '/#!/';
    $scope.loadingIconHeightOffset = $window.innerHeight;
    $scope.switchingBusinessPage = false;
    $scope.currentPage = 0;
    $scope.businessPages = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    $scope.Math = $window.Math;

    var loadBusinesses = function (data) {
        $businessIndex = 0;
        $scope.businessInfos = data;
        $scope.switchingBusinessPage = false;

        $scope.businessPages = [];
        for (var i = Math.max($scope.currentPage - 5, 0); i < Math.min($scope.currentPage + 5 + ($scope.currentPage < 5 ? 5 - $scope.currentPage : 0), $scope.businessInfos.information.Total / 10); i++) {
            $scope.businessPages.push(i);
        }
    }

    $scope.switchToPage = function ($page) {
        $http({
            method: 'GET',
            url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/business/paging/" + $page
        }).then(function successCallback(response) {
            if (response.status == 200) {
                loadBusinesses(response.data);
            }
        }, function errorCallback() {
        });

        $scope.switchingBusinessPage = true;
        $scope.currentPage = $page;
    }

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/business"
    }).then(function successCallback(response) {
        if (response.status == 200) {
            loadBusinesses(response.data);
        }
    }, function errorCallback() {
    });
}]);

app.controller('housesController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.mainLocation = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + '/#!/';
    $scope.loadingIconHeightOffset = $window.innerHeight;
    $scope.switchingHousePage = false;
    $scope.currentPage = 0;
    $scope.housePages = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    $scope.Math = $window.Math;

    var loadHouses = function (data) {
        $houseIndex = 0;
        $scope.houseInfos = data;
        $scope.switchingHousePage = false;

        $scope.housePages = [];
        for (var i = Math.max($scope.currentPage - 5, 0); i < Math.min($scope.currentPage + 5 + ($scope.currentPage < 5 ? 5 - $scope.currentPage : 0), $scope.houseInfos.information.Total / 10); i++) {
            $scope.housePages.push(i);
        }
    }

    $scope.switchToPage = function ($page) {
        $http({
            method: 'GET',
            url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house/paging/" + $page
        }).then(function successCallback(response) {
            if (response.status == 200) {
                loadHouses(response.data);
            }
        }, function errorCallback() {
        });

        $scope.switchingHousePage = true;
        $scope.currentPage = $page;
    }

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house"
    }).then(function successCallback(response) {
        if (response.status == 200) {
            loadHouses(response.data);
        }
    }, function errorCallback() {
    });
}]);

app.controller('vehiclesController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.mainLocation = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + '/#!/';
    $scope.loadingIconHeightOffset = $window.innerHeight;
    $scope.switchingVehiclePage = false;
    $scope.currentPage = 0;
    $scope.vehiclePages = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    $scope.Math = $window.Math;

    var loadVehicles = function (data) {
        $vehicleIndex = 0;
        $scope.vehicleInfos = data;
        $scope.switchingVehiclePage = false;

        $scope.vehiclePages = [];
        for (var i = Math.max($scope.currentPage - 5, 0); i < Math.min($scope.currentPage + 5 + ($scope.currentPage < 5 ? 5 - $scope.currentPage : 0), $scope.vehicleInfos.information.Total / 10); i++) {
            $scope.vehiclePages.push(i);
        }
    }

    $scope.switchToPage = function ($page) {
        $http({
            method: 'GET',
            url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/vehicle/paging/" + $page
        }).then(function successCallback(response) {
            if (response.status == 200) {
                loadVehicles(response.data);
            }
        }, function errorCallback() {
        });

        $scope.switchingVehiclePage = true;
        $scope.currentPage = $page;
    }

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/vehicle"
    }).then(function successCallback(response) {
        if (response.status == 200) {
            loadVehicles(response.data);
        }
    }, function errorCallback() {
    });
}]);

app.controller('frontPageController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.mainLocation = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + '/#!/';
    $scope.loadingIconHeightOffset = $window.innerHeight;

    $scope.onlinePlayersNumber = 0;

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/briefonline"
    }).then(function successCallback(response) {
        if (response.status == 200) {
            $scope.onlinePlayers = response.data;
            if ($scope.onlinePlayers.length > 0) {
                $scope.onlinePlayersNumber = $scope.onlinePlayers[0]['TotalPlayers'];

                $scope.onlinePlayers.forEach(function (element) {
                    element['profileUrl'] = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/#!/player/" + element['ID'];
                });
            }
        }
    }, function errorCallback() {
    });

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/server"
    }).then(function successCallback(response) {
        if (response.status == 200) {
            $scope.serverInfo = response.data;
        }
    }, function errorCallback() {
    });

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/server/news"
    }).then(function successCallback(response) {
        if (response.status == 200) {
            $scope.serverNews = response.data;
        }
    }, function errorCallback() {
    });
}]);

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

app.controller('secondaryNavController', ['$scope', '$location', function ($scope, $location) {
    var secondaryItems = this;

    secondaryItems.items = [
        [
            ['General', ''],
            ['Houses', '/#!/houses'],
            ['Vehicles', '/#!/vehicles'],
            ['Businesses', '/#!/businesses'],
            ['Clans', ''],
            ['Factions', ''],
            ['Wars', ''],
        ]
    ];
}]);

app.controller('playerController', ['$scope', '$location', '$window', '$http', function ($scope, $location, $window, $http) {
    $scope.loadingIconHeightOffset = $window.innerHeight;

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

    $http({
        method: 'GET',
        url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $location.path().split('/').pop()
    }).then(function successCallback(response) {
        if (response.status == 200) {
            $scope.playerInfos = response.data;
            $scope.modelPadded = ("00" + $scope.playerInfos.Model).slice(-3);
            $scope.adminLevelName = getAdminLevel($scope.playerInfos['AdminLevel']);

            $http({
                method: 'GET',
                url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house/" + $scope.playerInfos['Name'] + '/multiple'
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    $scope.houseInfos = response.data;

                    $scope.housesValue = 0;
                    $scope.housesCount = $scope.houseInfos.length;
                    $scope.houseInfos.forEach(function (element) {
                        $scope.housesValue += element['Value'];
                    });
                }
            }, function errorCallback() {
            });

            $http({
                method: 'GET',
                url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/vehicle/" + $scope.playerInfos['Name'] + "/multiple"
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    $scope.vehicleInfos = response.data;

                    $scope.vehiclesValue = 0;
                    $scope.vehiclesCount = $scope.vehicleInfos.length;
                    $scope.vehicleInfos.forEach(function (element) {
                        $scope.vehiclesValue += element['Value'];
                    });
                }
            }, function errorCallback() {
            });

            $http({
                method: 'GET',
                url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/business/" + $scope.playerInfos['Name'] + "/multiple"
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    $scope.businessInfos = response.data;

                    $scope.businessesValue = 0;
                    $scope.businessesCount = $scope.businessInfos.length;
                    $scope.businessInfos.forEach(function (element) {
                        $scope.businessesValue += element['Value'];
                    });
                }
            }, function errorCallback() {
            });

            $http({
                method: 'GET',
                url: $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $location.path().split('/').pop() + "/factionhistory"
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    $scope.factionHistory = response.data;
                }
            }, function errorCallback() {
            });
        }
    }, function errorCallback() {
    });
}]);