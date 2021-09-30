var SenderScript = {
    
    Columns:
        [
            { "data": "Id", "name": "Id" },
            //{
            //    "mData": null,
            //    "bSortable": false,
            //    "mRender": function (data) {
            //        return '<input type="checkbox" class="CheckBox" name="getvalues"  id="' + data.Id + '" value=" ' + data.IsSelected + ' "/>'
            //    }
            //},
            { "data": "AccountNumber", "name": "AccountNumber" },
            { "data": "Username", "name": "Username" },
          { "data": "Masking", "name": "Masking" },
            { "data": "NTN_FTN", "name": "NTN_FTN" },
            { "data": "NTN_FTN_Statuses", "name": "NTN_FTN_Statuses" },
           
            { "data": "Operators", "name": "Operators" },
            { "data": "Status", "name": "Status" },
            { "data": "Status", "name": "Status" },
            { "data": "Status", "name": "Status" },
            { "data": "Status", "name": "Status" },
            { "data": "Status", "name": "Status" },

            //{
            //    "mData": null,
            //    "bSortable": false,
            //    "mRender": function (data, type, full) {
            //        return '<button type="button" class="btn btn-danger delete" id="' + data.Id + '" value="Delete"><i class="fa fa-trash"></i></button> <button type="button" class="btn btn-danger edit" id="' + data.Id + '" value="Edit"><i class="fa fa-edit"></i></button>'

            //    }
            //}
        ],


    RenderDataTable: function () {
        
        CrudScript.makeAjaxRequest('GET', '/Sender/GetAll').then(function (data) {
            debugger
            var dt = $('.tblmanage').DataTable();
            dt.destroy();
            CrudScript.JqueryDataTableForReport('.tblmanage', data, SenderScript.Columns);
        })

    },

    //SaveUser: function () {
    //    if (MaskingUserScript.Validate()) {
    //        CrudScript.makeAjaxRequest('Post', '/MaskingUsers/Add', $("#form").serialize()).then(function (data) {
    //            $('#addRowModal').modal("hide");
    //            if (data == "Added Successfully !!!") {
    //                var mainTable = $('.tblmanage').DataTable();
    //                mainTable.destroy();
    //                MaskingUserScript.RenderDataTable();
    //                ShowDivSuccess(data);
    //            }

    //            else {
    //                ShowDivError(data.message);
    //            }

    //        })
    //    }

    //},

    FillModal: function (data)
    {
        debugger
       $('#addRowModal').modal("show");
       $('#ModalTitle').text("Update User");
       $('.fw-mediumbold').text('Update');
       $('#btnUpdate').css('display', 'block');
       $('#btnSubmit').css('display', 'none');
       $('#Id').val(data.Id);
       $('#Masking').val(data.Masking);
       $('#CompanyName').val(data.CompanyName);
       $('#Cnic').val(data.Cnic);
       $('#NTN').val(data.NTN);
       $('#Contact').val(data.Contact);
        var array = data.attached.split(",");
        for (var i = 0; i < array.length; i++)
        {
            var filename = array[i];
           var ss=filename.split("\UploadedFiles").pop();

           $("#textboxDiv").append("<label id='attached' style='color: blue!important;'>'" + ss + "'</label>");       
        }
     },

    UpdateUser: function (formData) {
        debugger
        $.ajax({
            url: '/MaskingUsers/Update',
            type: 'POST',
            data: formData,
            success: function (data) {
                if (data == true) {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    MaskingUserScript.RenderDataTable();
                    ShowDivSuccess(data);
                    setTimeout(function () { window.location.href = '/MaskingUsers' }, 2000);
                }

                else {
                    ShowDivError(data.message);
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });
       
    },

    //DownloadUsers: function () {
    //    debugger
    //    var data= $row()
    //   $.ajax({
    //       url: '/MaskingUsers/Download',
    //        type: 'POST',
    //       data: JSON.stringify(data),
    //       contentType: "application/json; charset=utf-8",
    //       dataType: "JSON",
    //        success: function (data) {
    //            alert("Sucess");
    //        },
    //        error: function (data) {
    //            alert("Failed");
    //        }

    //    });
    //  },

    DeleteUser: function (data) {
        var url = '/MaskingUsers/Delete';
        DeleteConfirm(url, data.Id, MaskingUserScript);
    },

    Validate: function ()
    {
        debugger
        var isValid = true;
        //var alphanumeric = "/^[A-Za-z0-9]{3,11}+$/";        
        
        var nic =/^[[0-9]{5}-[0-9]{7}-[0-9]{1}]+$/;
        var ntn = /^[0-9]+$/;
        var number = /^[0-9]+$/;
       //var number = /^((\+92)|(0092))-[{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}]+$/;
        var masking = $("#Masking").val();
        var companyname = $("#CompanyName").val();
        var cnic = $("#Cnic").val();
        var contact = $("#Contact").val();
        var ntns = $("#NTN").val();
        if (masking == "")
        {
            $('#Masking').css('border-color', 'Red');
            $('#mask').css('display', 'block');
            isValid = false;
        }
        else
        {
            var alphanumeric = /^[a-zA-Z0-9]+$/;
            if (!alphanumeric.test(masking))
            {
                $('#Masking').css('border-color', 'Red');
                $('#mask').css('display', 'block');
                isValid = false;
            }
            else
            {
                $('#mask').css('display', 'none');
                $('#Masking').css('border-color', 'lightgrey');
            }
        }

        if (companyname == "")
        {
            $('#CompanyName').css('border-color', 'Red');
            $('#company').css('display', 'block');

            isValid = false;
        }
        else
        {
            if (!alphanumeric.test(companyname)) {
                $('#CompanyName').css('border-color', 'Red');
                $('#company').css('display', 'block');

                isValid = false;
            }
            else {
                $('#company').css('display', 'none');
                $('#CompanyName').css('border-color', 'lightgrey');
            }
        }
        if ($('#Cnic').val().trim() == "")
        {
            $('#Cnic').css('border-color', 'Red');
            $('#cnic').css('display', 'block');

            isValid = false;
        }
        else
        {
            if (!number.test(cnic)) {

                $('#Cnic').css('border-color', 'Red');
                $('#cnic').css('display', 'block');

                isValid = false;
            }
            else {
                $('#cnic').css('display', 'none');
                $('#Cnic').css('border-color', 'lightgrey');
            }
        }
        if ($('#NTN').val().trim() == "" )
        {
            $('#NTN').css('border-color', 'Red');
            $('#ntn').css('display', 'block');
             isValid = false;
        }
        else
        {
            if (!ntn.test(ntns)) {
                $('#NTN').css('border-color', 'Red');
                $('#ntn').css('display', 'block');
                isValid = false;
            }
            else {
                $('#ntn').css('display', 'none');
                $('#NTN').css('border-color', 'lightgrey');
            }
        }
        if ($('#Contact').val().trim() == "")
        {
            $('#Contact').css('border-color', 'Red');
            $('#contact').css('display', 'block');

            isValid = false;
        }
        else
        {
            if (!number.test(contact)) {
                $('#Contact').css('border-color', 'Red');
                $('#contact').css('display', 'block');
                isValid = false;
            }
            else {
                $('#contact').css('display', 'none');
                $('#Contact').css('border-color', 'lightgrey');
            }
        }
        return isValid;
        
    },


    SaveUser: function (formData) {
        debugger
        $.ajax({
            url: '/Sender/Add',
            type: 'POST',
            data: formData,
            success: function (data)
            {
                if (data == "Added Successfully !!!")
                {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    SenderScript.RenderDataTable();
                    ShowDivSuccess(data);
                }

                else {
                    ShowDivError(data.message);
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });
    },


}