angular.module("TaskApp", [])
    .filter("dateformat", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(m[1]));
            else return null;
        };
    })
    .controller("TaskCtrl",
        function ($scope, $http) {
            $scope.reminderTypes = [ { type: "Email", id: "1" }, { type: "SMS", id: "2" }];
            $scope.TaskList = [];
            $scope.Description = "";
            $scope.DashboardID = document.getElementById("dashboardID").value;

            $scope.Add = function () {
                var task = {};
                task = {
                    DashboardID: $scope.DashboardID,
                    Description: $scope.Description
                };
                $http.post("/Tasks/Add",
                {
                    task
                }).then(function (res) {
                    var jsonResult = res.data;
                    if (jsonResult.Success) {
                        $scope.TaskList.push(jsonResult.Data);
                        $scope.Description = "";
                    } else {
                        alert(jsonResult.Message);
                    }
                });
            }

            $scope.EditItem = {};
            $scope.Edit = function (index) {
                $scope.TaskList[index].EditMode = true;
                $scope.EditItem.Description = $scope.TaskList[index].Description;
            };

            $scope.Cancel = function (index) {
                $scope.TaskList[index].Description = $scope.EditItem.Description;
                $scope.TaskList[index].EditMode = false;
                $scope.EditItem = {};
            };

            $scope.Update = function (index) {
                var task = $scope.TaskList[index];
                $http.post("/Tasks/Update",
                {
                    task
                }).then(function (res) {
                    var jsonResult = res.data;
                    if (jsonResult.Success) {
                        task.EditMode = false;
                    } else {
                        alert(jsonResult.Message);
                    }
                });
            };

            $scope.Delete = function (id) {

                $http.post("/Tasks/Delete", { Id: id })
                .then(function (res) {
                    var jsonResult = res.data;
                    if (jsonResult.Success) {
                        $scope.TaskList = $scope.TaskList.filter(function (db) {
                            return db.ID !== id;
                        });
                    } else {
                        alert("Hata oldu");
                    }
                });
            };

            $scope.TaskInit = function () {
                var id = $scope.DashboardID;
                $http.post("/Tasks/List", { id: id })
               .then(function (res) {
                   var jsonResult = res.data;
                   if (jsonResult.Success) {
                       $scope.TaskList = jsonResult.Data;
                   } else {
                       alert(jsonResult.Message)
                   }
               });
            };

            $scope.AddReminder = function () {
                $scope.TaskID = document.getElementById("taskID").value;
                $scope.ReminderDate = document.getElementById("date").value;
                console.log($scope.ReminderDate);
                var reminder = {};
                reminder = {
                    TaskID: $scope.TaskID,
                    Date: $scope.ReminderDate,
                    NotificationType: $scope.selectedReminderType,
                };
                $http.post("/Tasks/AddReminder",
                {
                    reminder
                }).then(function (res) {
                    var jsonResult = res.data;
                    if (jsonResult.Success) {
                        $("#reminderModal").modal('hide');
                        alert("Reminder created");
                        
                    } else {
                        alert(jsonResult.Message);
                    }
                }); 
            }

        });