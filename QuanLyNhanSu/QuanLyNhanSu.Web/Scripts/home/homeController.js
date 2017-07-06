var homeconfig = {
    pageSize: 3,
    pageIndex: 1,
}
var homeController = {
    init: function () {
        homeController.loadData();
        homeController.registerEvent();
    },
    registerEvent: function () {
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            homeController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            homeController.saveData();
        });
        $('.btnEdit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            homeController.loadDetail(id);
        });
        $('.btnDelete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.confirm("Are you sure to delete this user?", function (result) {
                homeController.deleteUser(id);
                setTimeout(homeController.loadData(true), 100);
            });
        });
        $('#btnSearch').off('click').on('click', function () {
            homeController.loadData(true);
        });
        $('#txtKeyword').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                homeController.loadData(true);
            }
        });
    },
    loadDetail: function (id) {
        $.ajax({
            url: '/Home/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#hidID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtEmail').val(data.Email);
                    $('#txtTel').val(data.Phone);
                    $('#txtUserName').val(data.UserName);
                    $('#txtPassword').val(data.Password);
                    $('#ckStatus').prop('checked', data.Status);
                }
                else {
                    bootbox.alert("Load failed!");  
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveData: function () {
        var name = $('#txtName').val();
        var email = $('#txtEmail').val();
        var tel = $('#txtTel').val();
        var user = $('#txtUserName').val();
        var password = $('#txtPassword').val();
        var status = $('#ckStatus').prop('checked');
        var id = parseInt($('#hidID').val());
        var employee= {
            Name: name,
            Email: email,
            Phone: tel,
            Status: status,
            UserName: user,
            Password: password,
            ID: id
        }
        $.ajax({
            url: '/Home/SaveData',
            data: {
                strEmployee: JSON.stringify(employee)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Save success", function () {
                        $('#modalAddUpdate').modal('hide');
                        homeController.loadData(true);
                    });                 
                }
                else {
                    bootbox.alert("Save failed!");  
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    },
    
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtName').val('');
        $('#txtEmail').val('');
        $('#txtTel').val('');
        $('#txtUserName').val('');
        $('#txtPassword').val('');
        $('#ckStatus').prop('checked', true);
    },
    deleteUser: function (id) {
        $.ajax({
            url: '/Home/Delete',
            type: 'POST',
            data: {
                id: id
            },
            dataType: 'json',
            success: function () {
                if (response.status) {
                    bootbox.alert("Delete success", function () {
                        homeController.loadData(true);
                    });   
                }
                else {
                    bootbox.alert("Delete failed!");
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadData: function (changePageSize) {
        var keyword = $('#txtKeyword').val();
        $.ajax({
            url: '/Home/LoadData',
            type: 'GET',
            data: {
                keyword: keyword,
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Email: item.Email,
                            UserName: item.UserName,
                            Phone: item.Phone,
                            Status: item.Status == true ? "<span class=\"label label-success\">Actived</span>" : "<span class=\"label label-danger\">Locked</span>"
                        });
                    });
                    $('#tblData').html(html);
                    homeController.paging(response.total, function () {
                        homeController.loadData();
                    }, changePageSize);
                    homeController.registerEvent();
                }
                else
                {
                    bootbox.alert("Search failed!");
                }
            }    
        })
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / homeconfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                homeconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
homeController.init();