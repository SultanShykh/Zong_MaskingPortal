var InProcessScript = {
    Columns:
        [

            { "data": "Id", "name": "Id" },
            { "data": "Masking", "name": "Masking" },
            { "data": "CompanyName", "name": "Company" },
            { "data": "Cnic", "name": "Cnic" },
            { "data": "NTN", "name": "NTN" },
            { "data": "Contact", "name": "Contact" },
            { "data": "attached", "name": "attached" },
            { "data": "Status", "name": "Status" },


        ],

    InProcessSave: function (filename) {
        debugger

        if (InProcessScript.Validate()) {
            if (filename != "") {
                var form = $("#form").serialize();
                CrudScript.makeAjaxRequest('POST', '/Sender/InProcessAdd', form + "&Name=" + filename).then(function (data) {
                    if (data == "Added Successfully !!!") {
                        var mainTable = $('.tblmanage').DataTable();
                        mainTable.destroy();
                        var ss = "/Sender/InProcessGetAll";
                        SenderScript.RenderDataTable(ss);
                        $("#form").trigger("reset");
                        InProcessScript.readonlyprop();
                        links = [];
                        InProcessScript.RemoveDivs();
                        ShowDivSuccess(data);
                    }

                    else {
                        ShowDivError(data.message);
                    }

                })
            }
            else {
                alert("Please Select  a Row ");
            }

        }

    },

    //InProcessSave: function (filename) {
    //    debugger
    //    $.ajax({
    //        url: '/Masking/InProcessAdd',
    //        type: 'POST',
    //        data: {
    //            form: $("#form").serialize(),
    //            Name: filename,
    //        },
    //        success: function (data) {
    //            if (data == "Added Successfully !!!") {
    //                var mainTable = $('.tblmanage').DataTable();
    //                mainTable.destroy();
    //                var ss = '/Masking/GetAll';
    //                MaskingUserScript.RenderDataTable(ss);
    //                ShowDivSuccess(data);

    //            }

    //            else {
    //                ShowDivError(data.message);
    //            }
    //        },
    //        cache: false,
    //        contentType: false,
    //        processData: false
    //    });
    //},

    InProcessUpdate: function (form) {

        debugger
        CrudScript.makeAjaxRequest('Post', '/Sender/InProcessUpdate', form).then(function (data) {
                debugger
                if (data == "Error") {
                    var mainTable = $('.tblmanage').DataTable();
                    mainTable.destroy();
                    var ss = "/Sender/GetAll";
                    SenderScript.RenderDataTable(ss);
                    $("#form").trigger("reset");
                    ShowDivSuccess(" Download Successfully !!!");
                }

                else {
                    ShowDivError(data.message);
                }
            })
        
    },
    Validate: function () {
        debugger
        var isValid = true;
        var ss = $("#UfoneReciver").val();
        var zong = $("#ZongReciver").val();
        var warid = $("#WaridReciver").val();
        var mobilink = $("#MobilinkReciver").val();
        var messages = $("#Message").val();
        //////////Ufone Validatoion////////

        if (!$("#UfoneReciver").attr('readonly')) {
            if (ss == "") {
                $('#UfoneReciver').css('border-color', 'Red');
                $('#recives').css('display', 'block');
                $('#recives').text("Please Fill This Field");
                isValid = false;
            }
            else {
                var alphanumeric = /^923[0-9]{9,9}$/;
                var numbe = /^03[0-9]{9,9}$/;
                if (!alphanumeric.test(ss) && !numbe.test(ss)) {
                    $('#UfoneReciver').css('border-color', 'Red');
                    $('#recives').css('display', 'block');
                    $('#recives').text("Please Enter a Correct Number");
                    isValid = false;
                }
                else {
                    $('#recives').css('display', 'none');
                    $('#UfoneReciver').css('border-color', 'lightgrey');

                }
            }

        }
        else {
            $('#recives').css('display', 'none');
            $('#UfoneReciver').css('border-color', 'lightgrey');

        }

        ////////////////////Zong Validation////////

        if (!$("#ZongReciver").attr('readonly')) {
            if (zong == "") {
                $('#ZongReciver').css('border-color', 'Red');
                $('#zrecives').css('display', 'block');
                $('#zrecives').text("Please Fill This Field");
                isValid = false;
            }
            else {
                var alphanumeric = /^923[0-9]{9,9}$/;
                var numbe = /^03[0-9]{9,9}$/;
                if (!alphanumeric.test(zong) && !numbe.test(zong)) {
                    $('#ZongReciver').css('border-color', 'Red');
                    $('#zrecives').css('display', 'block');
                    $('#zrecives').text("Please Enter a Correct Number");
                    isValid = false;
                }
                else {
                    $('#zrecives').css('display', 'none');
                    $('#ZongReciver').css('border-color', 'lightgrey');

                }
            }

        }
        else {
            $('#zrecives').css('display', 'none');
            $('#ZongReciver').css('border-color', 'lightgrey');

        }

        ///////Warid Validation/////////////

        if (!$("#WaridReciver").attr('readonly')) {
            if (warid == "") {
                $('#WaridReciver').css('border-color', 'Red');
                $('#wrecives').css('display', 'block');
                $('#wrecives').text("Please Fill This Field");


                isValid = false;
            }
            else {
                var alphanumeric = /^923[0-9]{9,9}$/;
                var numbe = /^03[0-9]{9,9}$/;
                if (!alphanumeric.test(warid) && !numbe.test(warid)) {
                    $('#WaridReciver').css('border-color', 'Red');
                    $('#wrecives').css('display', 'block');
                    $('#wrecives').text("Please Enter a Correct Number");

                    isValid = false;
                }
                else {
                    $('#wrecives').css('display', 'none');
                    $('#WaridReciver').css('border-color', 'lightgrey');

                }
            }

        }
        else {
            $('#wrecives').css('display', 'none');
            $('#WaridReciver').css('border-color', 'lightgrey');

        }

        ///////////////Mobilink Validation////////////////

        if (!$("#MobilinkReciver").attr('readonly')) {
            if (mobilink == "") {
                $('#MobilinkReciver').css('border-color', 'Red');
                $('#mrecives').css('display', 'block');
                $('#mrecives').text("Please Fill This Field");
                isValid = false;
            }
            else {
                var alphanumeric = /^923[0-9]{9,9}$/;
                var numbe = /^03[0-9]{9,9}$/;
                if (!alphanumeric.test(mobilink) && !numbe.test(mobilink)) {
                    $('#MobilinkReciver').css('border-color', 'Red');
                    $('#mrecives').css('display', 'block');
                    $('#mrecives').text("Please Enter a Correct Number");
                    isValid = false;
                }
                else {
                    $('#mrecives').css('display', 'none');
                    $('#MobilinkReciver').css('border-color', 'lightgrey');

                }
            }

        }
        else {
            $('#mrecives').css('display', 'none');
            $('#MobilinkReciver').css('border-color', 'lightgrey');

        }

        //////////////Message Validation///////////

        if (messages == "") {
            $('#Message').css('border-color', 'Red');
            $('#messagess').css('display', 'block');
            isValid = false;
        }
        else {
            $('#messagess').css('display', 'none');
            $('#Message').css('border-color', 'lightgrey');
        }


        return isValid;
    },
    readonlyprop: function () {
        $("#UfoneReciver").attr("readonly", true);
        $("#WaridReciver").attr("readonly", true);
        $("#ZongReciver").attr("readonly", true);
        $("#MobilinkReciver").attr("readonly", true);
    },

    RemoveDivs: function () {
        $('#mrecives').css('display', 'none');
        $("#mobilink").css('display', 'none');
        $('#MobilinkReciver').css('border-color', 'lightgrey');
        $('#zrecives').css('display', 'none');
        $("#zong").css('display', 'none');
        $('#ZongReciver').css('border-color', 'lightgrey');
        $("#warid").css('display', 'none');
        $('#wrecives').css('display', 'none');
        $('#WaridReciver').css('border-color', 'lightgrey');
    }
}