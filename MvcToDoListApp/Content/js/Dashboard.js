angular.module("dashboardApp", [])
    .controller("dashboardCtrl",
        function ($scope, $http) { 
            $scope.Name = "";
            $scope.Add = function () {
                var dashboard = {};
                dashboard = {
                    Name: $scope.Name
                };
                $http.post("/Main/Add",
                {
                    dashboard
                }).then(function (res) {
                    var jsonResult = res.data;
                    if (jsonResult.Success) {
                        $scope.DashboardList.push(jsonResult.Data);
                        $scope.Name = "";
                    } else {
                        alert(jsonResult.Message);
                    }
                });
            }

             
            $scope.EditItem = {};
            $scope.Edit = function (index) {
                 
                $scope.DashboardList[index].EditMode = true;
                 
                $scope.EditItem.Name = $scope.DashboardList[index].Name; 
            };

            $scope.Cancel = function (index) {
                 $scope.DashboardList[index].Name = $scope.EditItem.Name; 
                 $scope.DashboardList[index].EditMode = false;
                $scope.EditItem = {};
            };
  
            $scope.Update = function (index) {
                var dashboard = $scope.DashboardList[index];
                $http.post("/Main/Update",
                {
                    dashboard
                }).then(function (res) {
                    var jsonResult = res.data;
                    if (jsonResult.Success) {
                        dashboard.EditMode = false;
                    } else {
                        alert(jsonResult.Message);
                    }
                });
            };

            $scope.Delete = function (id) {
                
                    $http.post("/Main/Delete", { Id: id })
                    .then(function (res) {
                        var jsonResult = res.data;
                        if (jsonResult.Success) {
                            $scope.DashboardList = $scope.DashboardList.filter(function (db) {
                                return db.ID !== id;
                            });
                        } else {
                            alert("Hata oldu");
                        }

                    });

                     
            };
            $scope.init = function () {
                $http.get("/Main/List", {}).then(function (res) {
                    var jsonResult = res.data;
                    if (jsonResult.Success) {
                        $scope.DashboardList = jsonResult.Data;
                    } else {
                        alert(jsonResult.Message)
                    }
                });
            }
});