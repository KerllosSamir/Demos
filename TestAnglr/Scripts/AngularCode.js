/// <reference path="../angular.intellisense.js" />

var app = angular.module("myApp", ['angularUtils.directives.dirPagination']);

app.controller("myCtrl", function ($scope, $http) {
    $scope.employees = []; //declare an empty array
    $scope.pageno = 1; // initialize page no to 1
    $scope.total_count = 0;
    $scope.itemsPerPage = 2; //this could be a dynamic value from a drop down

    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.Employe = {};
            $scope.Employe.Emp_Name = $scope.EmpName;
            $scope.Employe.Emp_City = $scope.EmpCity;
            $scope.Employe.Emp_Age = $scope.EmpAge;
            $http({
                method: "post",
                url: "/Employee/Insert_Employee",
                datatype: "json",
                data: JSON.stringify($scope.Employe)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.EmpName = "";
                $scope.EmpCity = "";
                $scope.EmpAge = "";
            })
        } else {
            $scope.Employe = {};
            $scope.Employe.Emp_Name = $scope.EmpName;
            $scope.Employe.Emp_City = $scope.EmpCity;
            $scope.Employe.Emp_Age = $scope.EmpAge;
            $scope.Employe.Emp_Id = document.getElementById("EmpID_").value;
            $http({
                method: "post",
                url: "/Employee/Update_Employee",
                datatype: "json",
                data: JSON.stringify($scope.Employe)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.EmpName = "";
                $scope.EmpCity = "";
                $scope.EmpAge = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Employee";
            })
        }
    }
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "/Employee/Get_AllEmployee"
        }).then(function (response) {
            $scope.employees = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
    $scope.getData = function (pageno) { // This would fetch the data on page change.
        //In practice this should be in a factory.
        //$scope.pageno = pageno;
        $scope.users = [];
        //$http.get("/Employee/Get_AllEmployee", { params: { RowsPerPage: $scope.itemsPerPage, PageIndex: pageno } }).success(function (response) {
        //    //ajax request to fetch data into vm.data
        //    $scope.employees = response.data;  // data to be displayed on current page.
        //    $scope.total_count = response.total_count; // total data count.
        //});
        $http({
            method: "get",
            url: "/Employee/Search",
            datatype: "json",
            params: { RowsPerPage: $scope.itemsPerPage, PageIndex: pageno }
        }).then(function (response) {
            debugger;
            $scope.employees = response.data.data;  // data to be displayed on current page.
            $scope.total_count = response.data.total_count; // total data count.
        })
    };
    $scope.DeleteEmp = function (Emp) {
        $http({
            method: "post",
            url: "/Employee/Delete_Employee",
            datatype: "json",
            data: JSON.stringify(Emp)
        }).then(function (response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.UpdateEmp = function (Emp) {
        document.getElementById("EmpID_").value = Emp.Emp_Id;
        $scope.EmpName = Emp.Emp_Name;
        $scope.EmpCity = Emp.Emp_City;
        $scope.EmpAge = Emp.Emp_Age;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "Yellow";
        document.getElementById("spn").innerHTML = "Update Employee Information";
    }
})