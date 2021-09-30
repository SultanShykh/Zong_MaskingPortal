var ZongRUScripts = {

    Columns:
        [

            { "data": "Id", "name": "Id" },
            { "data": "Username", "name": "Users" },
            { "data": "Regionname", "name": "ZongUsers" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {

                    return '<button type="button" class="btn btn-info edit" id="' + data.Id + '" value="Edit"><i class="fa fa-edit"></i></button>'

                }
            },
        ],



    RenderDataTable: function () {
        debugger
        CrudScript.makeAjaxRequest('GET', '/ZongRegionUsers/GetAll').then(function (data) {
            $("#addRowModal").modal("hide");
            var dt = $('.tblmanage').DataTable();
            dt.destroy();
            CrudScript.JqueryDataTableForReport('.tblmanage', data, ZongRUScripts.Columns);
        })

    },
    FillModal: function (data) {
        debugger
        $('#addRowModal').modal("show");
        $('#ModalTitle').text("Update User");
        $('.fw-mediumbold').text('Update');
        $('#btnUpdate').css('display', 'block');
        $('#btnSubmit').css('display', 'none');
        $('#Id').val(data.Id);
        $('#Users').val(data.UsersId);
        $('#ZongUsers').val(data.RegionId);
      
    },

    ZongRUUpdate: function (data) {
        debugger
        if (ZongRUScripts.ValidationMessage()) {
            CrudScript.makeAjaxRequest('Post', '/ZongRegionUsers/Update', $("#form").serialize()).then(function (data) {
                debugger
                if (data == true) {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    ZongRUScripts.RenderDataTable();
                    ShowDivSuccess(" Updated Successfully !!!");
                }

                else {
                    ShowDivError(data.message);
                }
            })
        }
    },

    ZongRUDelete: function (data) {
        var url = '/ZongRegionUsers/Delete';
        DeleteConfirm(url, data.Id, ZongRUScripts);
    },

    ZongRUSave: function () {
        debugger
        if (ZongRUScripts.ValidationMessage()) {
            CrudScript.makeAjaxRequest('Post', '/ZongRegionUsers/Add', $("#form").serialize()).then(function (data) {
                if (data == "Added Successfully!!!") {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    ZongRUScripts.RenderDataTable();
                    ShowDivSuccess(data);
                }

                else {
                    ShowDivError(data.message);
                }

            })
        }
    },

    ValidationMessage: function () {
        debugger
        var isValid = true;
        var namess = $("#Users").val();
        if (namess == "") {
            $('#Name').css('border-color', 'Red');
            $('#names').css('display', 'block');
            isValid = false;
        }
        else {
            var alpha = /^[0-9]+$/;
            if (!alpha.test(namess)) {
                $('#Name').css('border-color', 'Red');
                $('#names').css('display', 'block');
                isValid = false;
            }
            else {
                $('#names').css('display', 'none');
                $('#Name').css('border-color', 'lightgrey');
            }
        }

        return isValid;
    },
}