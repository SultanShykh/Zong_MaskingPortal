var MenuScript = {

    Columns:
        [
            { "data": "Id", "name": "Id" },
            { "data": "MenuName", "name": "MenuName" },
            { "data": "MenuUrl", "name": "MenuUrl" },
            { "data": "Controller", "name": "Controller" },
            { "data": "Action", "name": "Action" },
            { "data": "IsActive", "name": "IsActive" },

            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {
                    return '<button type="button" class="btn btn-primary edit mr-2" id="' + data.Id + '" value="Edit"><i class="fa fa-edit"></i></button>'

                }
            }
        ],


    RenderDataTable: function () {
        CrudScript.makeAjaxRequest('GET', '/Menu/GetMenu').then(function (data) {

            var dt = $('.tblmanage').DataTable();
            dt.destroy();
            CrudScript.JqueryDataTableForReport('.tblmanage', data.data, MenuScript.Columns);
        })


    },

    SaveMenu: function () {

        CrudScript.makeAjaxRequest('Post', '/Menu/CreateMenu', $("form").serialize()).then(function (data) {
            $('#addRowModal').modal("hide");
            if (data.message == "Successfully Created!!!") {
                var mainTable = $('#mainTable').DataTable();
                mainTable.destroy();
                MenuScript.RenderDataTable();
                ShowDivSuccess(data.message);
            }

            else {
                ShowDivError(data.message);
            }
            
        })
    },

    FillModal: function (data) {
        $('#addRowModal').modal("show");
        $('.fw-mediumbold').text('Update');
        $('#btnUpdate').css('display', 'block');
        $('#btnSubmit').css('display', 'none');
        $('#menuId').val(data.Id);
        $('#menuName').val(data.MenuName);
        $('#menuURL').val(data.MenuUrl);
        $('#controller').val(data.Controller);
        $('#action').val(data.Action);
        $('[name="isactive"]').val([data.IsActive]);
    },

    UpdateMenu: function () {
        CrudScript.makeAjaxRequest('Post', '/Menu/UpdateMenu', $("form").serialize()).then(function (data) {
            $('#addRowModal').modal("hide");
            var mainTable = $('#mainTable').DataTable();
            mainTable.destroy();
            MenuScript.RenderDataTable();
            ShowDivSuccess("Updated Successfully");
        })
    },

    DeleteMenu: function (data) {
        var url = '/Menu/DeleteMenu';
        DeleteConfirm(url, data.Id, MenuScript);
    }


    }