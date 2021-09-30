var SenderScript = {

    Columns:
        [
            { "data": "Id", "name": "Id" },
            //{
            //    "class": "details-control",
            //    "orderable": false,
            //    "data": null,
            //    "defaultContent": ""
            //},
            { "data": "AccountNumber", "name": "AccountNumber" },
            { "data": "Username", "name": "Username" },
            { "data": "Masking", "name": "Masking" },
            { "data": "NTN_FTN", "name": "NTN_FTN" },
            { "data": "NTN_FTN_Statuses", "name": "NTN_FTN_Statuses" },
            { "data": "Operators", "name": "Operators" },
            { "data": "j", "name": "Jazz" },
            { "data": "t", "name": "Telenor" },
            { "data": "u", "name": "Ufone" },
            { "data": "Status", "name": "Status" },
            { "data": "RequestedToo", "name": "Requestedto" },
            { "data": "RequestedBY", "name": "RequestedBy" },
            { "data": "RevertedByyy", "name": "RevertedByyy" },
            { "data": "ApprovedByyy", "name": "ApprovedByyy" },
            { "data": "Reason", "name": "Reason" },
            { "data": "CurrentStatus", "name": "CurrentStatus" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full)
                {
        
                    return '<textarea disabled>' + data.Comments + '</textarea>'  
                }
            },
            { "data": "CreatedDate", "name": "CreatedDate" },
            { "data": "TatStatus", "name": "TatStatus" },
            { "data": "DaysElapsed", "name": "DaysElapsed" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {
               
                    //if (data.Usertypess == 3)
                    //{

                    //    return '<button type="button" class="btn btn-danger btn-sm edit" id="' + data.Id + '" value="Edit"><i class="fa fa-edit"></i></button> <button type="button" class="btn btn-danger btn-sm delete" id="' + data.Id + '" value="Delete"><i class="fa fa-trash"></i></button>'
                    //}
                    if (data.Status == "Approved")
                    {
                        if (data.Usertypess == 1) {
                            return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button> <button type="button" class="btn btn-danger btn-sm details" id="' + data.Id + '" value="Detail"><i class="fa fa-eye"></i></button>'
                        }
                        else {
                            return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button>'
                        }

                    }
                    else if (data.Status == "Rejected") {
                        return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button>'
                    }
                    else if (data.Status == "Complete") {
                        //if (data.Usertypess == 1)
                        //{
                        //    return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button> <button type="button" class="btn btn-danger btn-sm details" id="' + data.Id + '" value="Detail"><i class="fa fa-eye"></i></button>'
                        //}
                        //else

                        return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button>'


                    }
                    else if (data.user != data.userId) {
                        return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button> <button type="button" class="btn btn-danger btn-sm detail" id="' + data.Id + '" value="Detail"><i class="fa fa-eye"></i></button> '
                    }

                    else {
                        return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button> <button type="button" class="btn btn-danger btn-sm detail" id="' + data.Id + '" value="Detail"><i class="fa fa-eye"></i></button><button type="button" class="btn btn-danger btn-sm edit" id="' + data.Id + '" value="Edit"><i class="fa fa-edit"></i></button> '
                    }


                }
            }

        ],
    counter: -1,
    BulkColumns:
        [
            { "data": "AccountNumber", "name": "AccountNumber" },
            { "data": "MaskingID", "name": "MaskingID" },
            //{ "data": "Username", "name": "Username" },
            { "data": "NTN_FTN", "name": "NTN_FTN" },
            { "data": "Operator", "name": "Operator" },
            { "data": "NTN_FTN_Status", "name": "NTN_FTN_Status" },
            //{ "data": "op", "name": "ss" },
            //{ "data": "op", "name": "ss" },
            //{ "data": "op", "name": "ss" },
            //{ "data": "op", "name": "ss" },
            { "data": "Comments", "name": "Comments" },
            {

                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {
                
                    SenderScript.counter++;
                    return ' <input type="file" id="file' + SenderScript.counter + '"  multiple  name="file" required></input> <label id = "Filediv" style = "display:none; color: red" > File Is Missing </label >'


                    SenderScript.counter++;
                    return ' <input type="file" id="file' + SenderScript.counter + '"   name="file' + SenderScript.counter + '" multiple="multiple"   required></input> <label id = "Filediv" style = "display:none; color: red" > File Is Missing </label >'


                }
            }



        ],


    RenderDataTable: function (url) {

        CrudScript.makeAjaxRequest('GET', url).then(function (data) {
 
            var dt = $('.tblmanage').DataTable();
            $("#addRowModal").modal("hide");
            dt.destroy();
      
            var url = $(location).attr('href');

            if (url.toString().toUpperCase().includes('COMPLETE'))
            {

                CrudScript.JqueryDataTableWithExportApproved('.tblmanage', data, SenderScript.Columns);
            }
            else {
       
                CrudScript.JqueryDataTableWithExport('.tblmanage', data, SenderScript.Columns);
            }


            //var detailRows = [];

            //$('.tblmanage tbody').on('click', 'tr td.details-control', function () {
            //    debugger
            //    var tr = $(this).closest('tr');
            //    var row = dt.row(tr);
            //    var idx = $.inArray(tr.attr('id'), detailRows);

            //    if (row.child.isShown()) {
            //        tr.removeClass('details');
            //        row.child.hide();

            //        // Remove from the 'open' array
            //        detailRows.splice(idx, 1);
            //    }
            //    else {
            //        tr.addClass('details');
            //        row.child(SenderScript.format(row.data())).show();

            //        // Add to the 'open' array
            //        if (idx === -1) {
            //            detailRows.push(tr.attr('id'));
            //        }
            //    }
            //});

            //// On each draw, loop over the `detailRows` array and show any child rows
            //dt.on('draw', function () {
            //    $.each(detailRows, function (i, id) {
            //        $('#' + id + ' td.details-control').trigger('click');
            //    });
            //});
            debugger
            if (data != "") {
                //if (data[0].Status == "Approved")
                //{
                //    var dt = $('.tblmanage').DataTable();
                //    dt.column(16).visible(false);
                //}
                debugger
                if (data[0].Usertypess == 3 && data[0].Status != "Approved" && data[0].Status != "Complete" && data[0].Status != "Rejected")
                {
                    $(".detail").show();
                    $(".history").show();
                    //$("td:nth-last-child(19)", $(this)).remove();
                    //$("#user").hide();
                }

                else {
                    $(".detail").show();
                }
                if (data[0].Usertypess == 1)
                {
                    $(".edit").hide();
                    $(".create").hide();

                }
                else {
                    $(".edit").show();
                    $(".create").show();
                }
                if (data[0].Status == "Approved")
                {

                    var dt = $('.tblmanage').DataTable();
                    //dt.column(17).visible(false);
                    //dt.column(18).visible(false);
                }
                if (data[0].Status == "Complete")
                {

                    var dt = $('.tblmanage').DataTable();
                   
                    //dt.column(17).visible(false);
                    //dt.column(18).visible(false);
                }

                //if (data[0].Status == "Rejected") {

                //    var dt = $('.tblmanage').DataTable();
                //    dt.column(17).visible(false);
                //    dt.column(18).visible(false);
                //}


                //if (data[0].Status == "Completed") {

                //    var dt = $('.tblmanage').DataTable();
                //    dt.column(17).visible(true);
                //    dt.column(18).visible(true);
                //}

                //for (a = 0; a <= dt.row.c; a++)
                //{
                //    if (data[a].Usertypess == 3 && data[a].approved != null)
                //    {
                //        $(".edit").hide();
                //    }
                //    else {
                //        $(".edit").show();
                //    }
            }

            SenderScript.Roles();
        })

    },

    //    format: function (d) {
    //        return 'Zong : ' + d.ss + '<br>' +
    //            'Jazz: ' + d.ss + '<br>' +
    //            'Telenor: ' + d.ss + '<br>' +
    //            'Ufone: ' + d.ss + '<br>' +
    //            'Requested To: ' + d.Requestedto + '<br>' +
    //            'Requested By: ' + d.RequestedBy + '<br>' +
    //            'Reverted By: ' + d.RevertedByyy + '<br>' +
    //            'Approved By: ' + d.ApprovedByyy + '<br>';
    //            //'The child row can contain anyta you wish, including links, images, inner tables etc.';
    //},

    ValidateTable: function () 
    {
        var redcount = 0;
        var url;
        var i = 0;
   
        var data = $('.tblmanage').DataTable().rows().data().toArray();
      
        var formData = new FormData();
        var number = /^92[0-9]{10}/;
        var ntn = /^[A-Za-z0-9-\-\s]+$/;
        var numberss = /^[A-Za-z0-9-\-\s,.!#$%^&@\-\s]{3,11}$/;

      

        $('.tblmanage tbody tr').each(function () {
         

            var inp = document.getElementById('file' + i);
   
            Attachment = "";
            if (inp.files.length != 0) {
                for (var j = 0; j < inp.files.length; ++j) {
                
                    var name = inp.files.item(j).name;

                    formData.append('file', $('#file' + i)[0].files[j]);

                    if (document.getElementById("file" + i).value != "")
                    {
                  
                        $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:last').css('background-color', 'white');
                        Attachment += name + ",";

                    }
                    else
                    {
                        redcount++;
                        $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:last').css('background-color', 'red');
                    }
                    //alert("here is a file name: " + name);
                }

                data[i]["Attachment"] = Attachment;
              
                if (!number.test(data[i].AccountNumber))
                {
                    redcount++;
                    $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:first').css('background-color', 'red');
                    $("#btnclear").show();
                    $("#btnSave").hide();
                   
                }
                else
                {
                    $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:first').css('background-color', 'white');
                }
            
                if (!numberss.test(data[i].MaskingID))
                {
                    redcount++;
                    $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:nth-child(2)').css('background-color', 'red');
                    $("#btnclear").show();
                    $("#btnSave").hide();
                }
                else
                {
                    $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:nth-child(2)').css('background-color', 'white');
                }
                if (!ntn.test(data[i].NTN_FTN))
                {
                    redcount++;
                    $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:nth-child(3)').css('background-color', 'red');
                    $("#btnclear").show();
                    $("#btnSave").hide();
                }
                i++;
            }
            else
            {
                redcount++;
                $('.tblmanage tr:gt(' + i + '):lt(1)').find('td:last').css('background-color', 'red');
                i++;
            }
        });

        if (redcount == 0)
        {
            formData.append('rowdata', JSON.stringify(data));
            SenderScript.SaveData(formData);
        }
        else
        {
           
            return false;
           
        }
    },
    LoadHistory: function (id)
    {
        CrudScript.makeAjaxRequest('GET', '/History/GetById/', { Id: id }).then(function (data) {
            localStorage.setItem("Permissions", JSON.stringify(data));
            var url = "/History/Index";
            // do something with the url client side variable, for example redirect
            window.location.href = url;
        });

    },
    LoadHistory: function (id) {
        CrudScript.makeAjaxRequest('GET', '/History/GetById/', { Id: id }).then(function (data) {
            localStorage.setItem("Permissions", JSON.stringify(data));
            var url = "/History/Index";
            // do something with the url client side variable, for example redirect
            window.location.href = url;
        });

    },

    SaveData: function (formdata) {
        jQuery.ajaxSettings.traditional = true;
        // CrudScript.makeAjaxRequest('POST', '/Sender/SaveBulk/', { JSON.stringify(things) }).then(function (data) {

        //})
        //$.post('@Url.("/Sender/SaveBulk/")', { things: things },
        //    function () {
        //        $('#result').html('"PassThings()" successfully called.');
        //    });

        $.ajax({
            type: "POST",
            url: '/Sender/SaveBulkss/',
            dataType: "json",
            data: formdata,
            //data: JSON.stringify({ data }),
            processData: false,
            contentType: false,
            dataType: "json",
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (data) {
            
                if (data == "Added Successfully !!!") {
                    localStorage.removeItem("BulkList");
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    ShowDivSuccess(" Data Save ");
                    $("#loader").hide();


                }
                else {
                    console.log(d);

                    ShowDivError("Error");
                }
                window.location.reload();
            }
        })
        //$.ajax({
        //    type: 'POST',
        //    contentType: 'application/json; charset=utf-8',
        //    dataType: 'json',
        //    url: '/Sender/SaveBulk/',
        //    data: JSON.stringify({things}) ,
        //    success: function (e) {

        //    },
        //    error: function () {

        //        alert();
        //    }
        //});
        //
        //
    },


    UploadBulk: function () {
        if (SenderScript.ValidateFile()) {

            $.ajax({
                url: '/Sender/UploadBulk',
                type: 'POST',
                data: new FormData($('#bulkform')[0]),
              
                success: function (d) {
           
                    if (d != null) {
                        console.log(d);
                        localStorage.setItem("BulkList", d);
                        var mainTable = $('.tblmanage').DataTable();
                        $('#UploadModal').modal("hide");
                        mainTable.destroy();
                        CrudScript.JqueryPFDataTable('.tblmanage', JSON.parse(d), SenderScript.BulkColumns);
                        ShowDivSuccess(" Bulk Upload ");
                        window.location.href = "/Sender/Bulk";
                 
                    }


                    else {

                        ShowDivError(d.message);
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        }
    },

    ValidateFile: function () {

        if ($('#file').get(0).files.length === 0) {

            $('#Filediv').css('display', 'block');
            return false;
        }

        return true;
    },

    Roles: function () {
        let Cookies = SenderScript.getCookie('SMSCookies');


        var json = $.parseJSON(Cookies);
        if (json.UserGroup == "SUPER ADMIN") {
            $(".create").remove();
        }
        if (json.UserGroup == "USERS")
        {
            $("#approved").remove();
            $("#revert").remove();
            $("#rejected").remove();
        }

    },

    getCookie: function (name) {
        let cookie = {};
        document.cookie.split(';').forEach(function (el) {
            let [k, v] = el.split('=');
            cookie[k.trim()] = v;
        })
        return cookie[name];
    },
    FillModal: function (data) {



        $('#addRowModal').modal("show");
        $('#ModalTitle').text("Update Masking");
        $('.fw-mediumbold').text('Update');
        $('#btnUpdate').css('display', 'block');
        $('#btnSubmit').css('display', 'none');
        $('#mtitle').text("Update Masking");
        $('#Id').val(data.Id);
        $('#customFileLang').val("");
        $('#UserId').val(data.UserId);
        $('#input-account').val(data.AccountNumber);
        $('#input-sender').val(data.Masking);
        $('#input-ntn-ftn').val(data.NTN_FTN);
        $('[name="NTN_FTN_Status"]').val([data.NTN_FTN_Status]);
        $('[name="Priority"]').val([data.Priority]);
        $('#Comment').val(data.Comments);
        $('#CreatorType').val(data.CreatorType);
        $('#RevertedByyy').val(data.RevertedByyy);

        var array = data.Attachment.split(",");
        for (var i = 0; i < array.length; i++) {
            var filename = array[i];
            var ss = filename.split("\UploadedFiles\\").pop();
            $("#textboxDiv").append("<a href=''><label id='attached' style='color: blue!important;  title='Click to Download This File''>'" + ss + "'</label></a><br/>");
        }

        $("#textboxDiv  a #attached").click(function (ss) {
            debugger
            var aa = ss.target.innerHTML;
            var dd = aa.replace(/.*(\/|\\)/, '');
            var uu = dd.slice(1, -1);
            SenderScript.Download(uu);
        });

    },



    FillDetailModal: function (data) {
        debugger
        if (data.approved != data.user )
        {
            if (data.Usertypess == 2 && data.CurrentStatus == "Requested To ITSAdmin")
            {
                $("#approved").hide();
                $("#revert").hide();
                $("#rejected").hide();
            }
            else if (data.Usertypess == 2 && data.approved == null || data.approved == 0)
            {
                $("#approved").show();
                $("#revert").show();
                $("#rejected").show();
            }
            //else if (data.Usertypess == 2 && data.approved == 0)
            //{
            //    $("#approved").show();
            //    $("#revert").show();
            //    $("#rejected").show();
            //}
             else {
                $("#approved").hide();
                $("#revert").hide();
                $("#rejected").hide();
            }
        }
    
        else {
            $("#approved").hide();
            $("#revert").hide();
            $("#rejected").hide();
        }
        //if (data.Usertypess == 2 || data.RequestedToo = "Super Admin")
        //{
        //    $("#approved").hide();
        //    $("#revert").hide();
        //    $("#rejected").hide();
        //}
        if (data.Username == "ZongAdmin" && data.Usertypess == 2) {
            $("#revert").hide();
            $("#approved").hide();
            $("#rejected").hide();

        }

        else if (data.Usertypess == 1)
        {
            $("#revert").show();
            $("#approved").show();
            $("#rejected").show();
        }
        else if (data.Usertypess == 2 && data.RequestedToo == "Super Admin") {
            $("#approved").hide();
            $("#revert").hide();
            $("#rejected").hide();
        }

        if (data.RevertedByyy != "--") {
            $("#revert").hide();
            $("#approved").hide();
            $("#rejected").hide();
        }

        $('#detailmodal').modal("show");
        $("#filedetail").val("");
        $('#mtitles').text("Masking Detail");
        $("#accountdetailsssss").text(data.AccountNumber);
        $("#senderdetail").text(data.Masking);
        $("#ntndetail").text(data.NTN_FTN);
        $("#ntnstatusdetail").text(data.NTN_FTN_Statuses);
        $("#operatordetail").text(data.Operators);
        $("#prioritydetail").text(data.Priorities);
        $("#commentsdetail").text(data.Comments);
        $("#revertreason").text(data.Reason);
        var array = data.Attachment.split(",");
        for (var i = 0; i < array.length; i++) {
            var filename = array[i];
            var ss = filename.split("\UploadedFiles\\").pop();
            $("#textboxDivsssssss").append("<a href=''><label id='attached' style='color: red!important;'>'" + ss + "'</label></a><br/>");
        }

        $("#textboxDivsssssss  a #attached").click(function (ss) {
     
            var aa = ss.target.innerHTML;
            var dd = aa.replace(/.*(\/|\\)/, '');
            var uu = dd.slice(1, -1);
            SenderScript.Download(uu);
        });
    },

    FillRouteModal: function (data) {
   
        $('#detailmodal').modal("show");
        $('#mtitles').text("Telcos");
        //if (data.Zong == true) {
        //    $('#zong').attr('checked', true);
        //}
        //else { $('#zong').attr('checked', false); }
        if (data.Jazz == true) {
            $('#jazz').attr('checked', true)
        }
        else { $('#jazz').attr('checked', false); }
        //if (data.Warid == true) {
        //    $('#warid').attr('checked', true)
        //}
        //else { $('#warid').attr('checked', false) }

        if (data.Ufone == true) {
            $('#ufone').attr('checked', true)
        }
        else { $('#ufone').attr('checked', false) }
        if (data.Telenor == true) {
            $('#telenor').attr('checked', true)
        }
        else { $('#telenor').attr('checked', false) }
    },
    UpdateUser: function (formData) {

        var ss = $("#reason").val();
        $.ajax({
            type: 'POST',
            url: '/Sender/Update',
            data: formData,
            success: function (data)
            {
                if (data == true)
                {
                    ShowDivSuccess("Updated Successfully!!!");
                    var ss = "/Sender/GetAll";
                    SenderScript.RenderDataTable(ss);

                }

                else
                {
                    ShowDivError(data.message);
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });

    },




    Download: function (ff) {

        $.ajax({
            url: '/Sender/Download',
            type: 'GET',
            data: { Files: ff },

            success: function () {
           
                window.location.href = "/Sender/Download/?Files=" + ff;
            },
            error: function () {
                alert("File Not Found");
            }

        });
    },



    DeleteUser: function (data) {
        var url = '/Sender/Delete';
        DeleteConfirm(url, data.Id, SenderScript);
    },

    Validate: function () {
    
        var isValid = true;
        //var alphanumeric = "/^[A-Za-z0-9]{3,11}+$/";        

        //var nic =/^[[0-9]{5}-[0-9]{7}-[0-9]{1}]+$/;
        var ntn = /^[A-Za-z0-9-\-\s]+$/;
        var number = /^[0-9]{11,13}$/;
        var numberss = /^[A-Za-z0-9-,.!#$%^&@\-\s]{3,11}$/;
        //var numberss = /^[A-Za-z0-9]{3,11}$/;
        //var number = /^((\+92)|(0092))-[{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}]+$/;
        var inputaccount = $("#input-account").val();
        var inputsender = $("#input-sender").val();
        var comment = $("#Comment").val();
        var ntntf = $('#input-ntn-ftn').val();
        var ntnftn = $("[name='NTN_FTN_Status']").val();
        var priority = $("[name='Priority']").val();

        if (inputaccount == "") {
            $('#input-account').css('border-color', 'Red');
            $('#accounts').css('display', 'block');
            isValid = false;
        }
        else {

            if (!number.test(inputaccount))
            {
                $('#input-account').css('border-color', 'Red');
                $('#accounts').css('display', 'block');
                $('#accounts').text("Please Enter a Correct Format");
                isValid = false;
            }
            else {
                $('#accounts').css('display', 'none');
                $('#input-account').css('border-color', 'lightgrey');
            }
        }

        if (inputsender == "") {
            $('#input-sender').css('border-color', 'Red');
            $('#senders').css('display', 'block');

            isValid = false;
        }
        else {
            if (!numberss.test(inputsender)) {
                $('#input-sender').css('border-color', 'Red');
                $('#senders').css('display', 'block');
                $('#senders').text("Please Enter a Correct Format");
                isValid = false;
            }
            else {
                $('#senders').css('display', 'none');
                $('#input-sender').css('border-color', 'lightgrey');
            }
        }

        if (comment == "") {
            $('#Comment').css('border-color', 'Red');
            $('#comments').css('display', 'block');

            isValid = false;
        }

        else {
            $('#comments').css('display', 'none');
            $('#Comment').css('border-color', 'lightgrey');
        }


        if (ntntf == "") {
            $('#input-ntn-ftn').css('border-color', 'Red');
            $('#ntnftn').css('display', 'block');
            isValid = false;
        }
        else {
            if (!ntn.test(ntntf)) {
                $('#input-ntn-ftn').css('border-color', 'Red');
                $('#ntnftn').css('display', 'block');
                $('#ntnftn').text("Please Enter a Correct Format");
                isValid = false;
            }
            else {
                $('#ntnftn').css('display', 'none');
                $('#input-ntn-ftn').css('border-color', 'lightgrey');
            }
        }



        if (ntnftn == "") {

            $('#ntnstatus').css('display', 'block');

            isValid = false;
        }

        else {
            $('#ntnstatus').css('display', 'none');
        }

        if (priority == "") {

            $('#priorities').css('display', 'block');

            isValid = false;
        }

        else {
            $('#priorities').css('display', 'none');

        }

        return isValid;

    },


    SaveUser: function (formData) {
       
        $.ajax({
            url: '/Sender/Add',
            type: 'POST',
            data: formData,
            success: function (data) {
            
                if (data == "Added Successfully!!!")
                {
                    var ss = "/Sender/GetAll";
                    $("#form").trigger('reset');
                    SenderScript.RenderDataTable(ss);
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

    Reverted: function (form) {

      

        var ss = $("#reasona").val();

        CrudScript.makeAjaxRequest('Post', '/Sender/Reverted', { sender: form, Name: ss }).then(function (data) {
       
            if (data == true) {
               
                var ss = "/Sender/GetAll";
                SenderScript.RenderDataTable(ss);
                $("#form").trigger("reset");
                $("#revertform").trigger("reset");
                $("#revertedmodal").modal("hide");
                ShowDivSuccess(" Updated Successfully !!!");
            }

            else {
                ShowDivError(data.message);
            }
        })

    },

    TelcosComplete: function (form) {
      

        //var zong = $("#zong").val();
        //var warid = $("#warid").val();
        var ufone = $("#ufone").val();
        var jazz = $("#jazz").val();
        var telenor = $("#telenor").val();

        CrudScript.makeAjaxRequest('Post', '/Sender/TelcosComplete', { sender: form,  Ufone: ufone, Jazz: jazz, Telenor: telenor }).then(function (data) {
        
            if (data == true) {
               
                var data = '/Sender/CompleteGetAll';
                SenderScript.RenderDataTable(data);
                $("#TelcosComplete").trigger("reset");
                $("#TelcosComplete").trigger("reset");
                $("#detailmodal").modal("hide");
                ShowDivSuccess(" Updated Successfully !!!");
            }

            else {
                ShowDivError(data.message);
            }
        })

    },

    Approved: function (form) {

  
        var ss = $("#reasona").val();
        CrudScript.makeAjaxRequest('Post', '/Sender/Approved', { sender: form, Name: ss }).then(function (data) {
       
            if (data == true) {
              
                var ss = "/Sender/GetAll";
                SenderScript.RenderDataTable(ss);
                $("#form").trigger("reset");
                $("#revertform").trigger("reset");
                $("#revertedmodal").modal("hide");
                ShowDivSuccess(" Updated Successfully !!!");
            }

            else {
                ShowDivError(data.message);
            }
        })

    },


    Rejected: function (form) {

   
        var ss = $("#reasona").val();
        CrudScript.makeAjaxRequest('Post', '/Sender/Rejected', { sender: form, Name: ss }).then(function (data) {
         
            if (data == true) {
              
                var ss = "/Sender/GetAll";
                SenderScript.RenderDataTable(ss);
                $("#form").trigger("reset");
                $("#revertform").trigger("reset");
                $("#revertedmodal").modal("hide");
                ShowDivSuccess(" Updated Successfully !!!");
            }

            else {
                ShowDivError(data.message);
            }
        })

    },
}