var RegionHierarchyScript = {

    Columns:
        [

            { "data": "Id", "name": "Id" },
            { "data": "Requestedby", "name": "RegionRequestby" },
            { "data": "Requestedto", "name": "RegionRequestto" },
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
        CrudScript.makeAjaxRequest('GET', '/RegionsHierarchy/GetAll').then(function (data) {
            $("#addRowModal").modal("hide");
            var dt = $('.tblmanage').DataTable();
            dt.destroy();
            CrudScript.JqueryDataTableForReport('.tblmanage', data, RegionHierarchyScript.Columns);
        })

    },
    FillModal: function (data) {
        debugger
        $('#addRowModal').modal("show");
        $('#ModalTitle').text("Update Hierarchy");
        $('.fw-mediumbold').text('Update');
        $('#btnUpdate').css('display', 'block');
        $('#btnSubmit').css('display', 'none');
        $('#Id').val(data.Id);
        $('#RequestedBy').val(data.RegionRequestbyId);
       $('#RequestedTo').val(data.RegionRequesttoId);
        $('#RequestedTo').find("option[value=" + data.RegionRequesttoId + "]").prop("selected", "selected")
        $('#RequestedTo').selectpicker('refresh');
    },

   RegionHierarchyUpdate: function (data) {
        debugger
        if (RegionHierarchyScript.ValidationMessage()) {
            CrudScript.makeAjaxRequest('Post', '/RegionsHierarchy/Update', $("#form").serialize()).then(function (data) {
                debugger
                if (data == true) {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    RegionHierarchyScript.RenderDataTable();
                    ShowDivSuccess(" Updated Successfully !!!");
                }

                else {
                    ShowDivError(data.message);
                }
            })
        }
    },

   RegionHierarchyDelete: function (data) {
        var url = '/RegionsHierarchy/Delete';
        DeleteConfirm(url, data.Id, RegionHierarchyScript);
    },

   RegionHierarchySave: function () {
        debugger
        if (RegionHierarchyScript.ValidationMessage()) {
            CrudScript.makeAjaxRequest('Post', '/RegionsHierarchy/Add', $("#form").serialize()).then(function (data) {
                if (data == "Added Successfully!!!") {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    
                    RegionHierarchyScript.RenderDataTable();
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
        var namess = $("#RequestedBy").val();
        var requested = $("#RequestedTo").val();
        if (namess == "") {

            $('#RequestedBy').css('border-color', 'Red');
            $('#names').css('display', 'block');
            isValid = false;
        }
        else {
           
                $('#names').css('display', 'none');
            $('#RequestedBy').css('border-color', 'lightgrey');
            
        }

        if (requested == "") {

            $('#RequestedTo').css('border-color', 'Red');
            $('#namessssss').css('display', 'block');
            isValid = false;
        }
        else {
           
            $('#namessssss').css('display', 'none');
            $('#RequestedTo').css('border-color', 'lightgrey');
            
        }

        return isValid;
    },
}