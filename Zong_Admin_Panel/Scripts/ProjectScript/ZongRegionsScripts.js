var ZongRScripts = {

    Columns:
        [

            { "data": "Id", "name": "Id" },
            { "data": "Name", "name": "Name" },
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
        CrudScript.makeAjaxRequest('GET', '/ZongRegions/GetAll').then(function (data) {
            $("#addRowModal").modal("hide");
            var dt = $('.tblmanage').DataTable();
            dt.destroy();
            CrudScript.JqueryDataTableForReport('.tblmanage', data, ZongRScripts.Columns);
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
        $('#Name').val(data.Name);
        $('#URL').val(data.Url);
        if (data.IsActive == false) {
            $('#IsActive').prop('checked', false);
        }
        else {
            $('#IsActive').prop('checked', true);
        }
    },

   ZongRUpdate: function (data) {
        debugger
       if (ZongRScripts.ValidationMessage()) {
            CrudScript.makeAjaxRequest('Post', '/ZongRegions/Update', $("#form").serialize()).then(function (data) {
                debugger
                if (data == true) {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    ZongRScripts.RenderDataTable();
                    ShowDivSuccess(" Updated Successfully !!!");
                }

                else {
                    ShowDivError(data.message);
                }
            })
        }
    },

    ZongRDelete: function (data) {
        var url = '/ZongRegions/Delete';
        DeleteConfirm(url, data.Id, ZongRScripts);
    },

   ZongRSave: function () {
       debugger
       if (ZongRScripts.ValidationMessage()) {
            CrudScript.makeAjaxRequest('Post', '/ZongRegions/Add', $("#form").serialize()).then(function (data) {
                if (data == "Added Successfully!!!") {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    ZongRScripts.RenderDataTable();
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
        var namess = $("#Name").val();
         if (namess == "") {
            $('#Name').css('border-color', 'Red');
            $('#names').css('display', 'block');
            isValid = false;
        }
        else {
            var alpha = /^[a-zA-Z]+$/;
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