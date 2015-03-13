$(document).ready(function () {


    $("#editSelf").live('click', function () {

        var personid = $('#personid').val();

        var url = 'EditSelf.aspx'; /* +personid;*/
        $.colorbox({ href: url, width: 800, height: 700, opacity: 0.8,
            overlayClose: false, open: true,
            onClosed: function () { window.location.reload() }
        }, function () {
            $("#ButtonSave").live('click', function () {
                var formvalidated = true;
                var fname = $("*[id$='txtFirstName']").val();
                var lname = $("*[id$='txtLastName']").val();
                var country = $("*[id$='dropCountry']").val();
                var message = 'Please provide - ';

                if ((fname == '' && lname == '') || (fname == null && lname == null)) {
                    formvalidated = false;
                    message += "first name or last name<br/>";
                }

                else if (fname.length == 0 && lname.length == 0) {
                    formvalidated = false;
                    message += "first name or last name<br/>";
                }

                if (country <= 0) {
                    formvalidated = false;
                    message += "country<br/>";
                }

                if (formvalidated) {
                    $.colorbox.Close();
                    window.location.href = "Dashboard.aspx";
                }
                else {
                    $.notifyBar({
                        html: message,
                        close: true,
                        delay: 1000000,
                        cls: "error"
                    });
                    return false;
                }
                return false;
            });

            return false;
        });
        return false;
    });


    $(".editPeopleList").live('click', function () {

        var senderid = $('#senderid').val();
        var sendertype = $('#sendertype').val();
        var addtype = $(this).attr('id');
        
        var url = '../AddRemovePersonnelList.aspx?action=viewall&senderid=' + senderid + '&sendertype=' + sendertype + '&addtype=' + addtype;
        $.colorbox({ href: url, width: 800, height: 540, opacity: 0.8,
            overlayClose: false, open: true /*,
            onClosed: function () { window.location.reload() }*/
        }, function () {
            $("*[id$='btnUpdateList']").click(function () {
                var selectedIDs = '';
                var selectedValues = '';
                $("*[id$='chkBoxLst']").find("input[type=checkbox][checked]").each(function () {
                    if (selectedIDs.length == 0) {
                        selectedIDs = $(this).parent().attr('id');
                        /*selectedValues = $('label[for=' + this.id + ']').html();*/
                    }
                    else {
                        selectedIDs += "," + $(this).parent().attr('id');
                        /*selectedValues += ", " + $('label[for=' + this.id + ']').html();*/
                    }
                });

                var radSelId = '';
                $("*[id$='rdPersonnelList']").find("input[type=radio][checked]").each(function () {
                    /* radSelId = $(this).val();*/
                    radSelId = $(this).parent().attr('id');
                });

                if (addtype == 'FEBranch') {
                    if (radSelId <= 0) {
                        $.notifyBar({
                            html: "Atleast one Stockist should be selected",
                            close: true,
                            delay: 1000000,
                            cls: "error"
                        });
                        return false;
                    }
                }

                if (selectedIDs.length > 0 || radSelId > 0 || addtype == "MaxStation") {
                    $.ajax({
                        type: "POST",
                        url: "../AddRemovePersonnelList.aspx?action=update&ids=" + selectedIDs + "&radid=" + radSelId + "&senderid=" + senderid + "&sendertype=" + sendertype + "&addtype=" + addtype,
                        datatype: "html",
                        async: false,
                        success: function (response) {
                            var transferData_ = $(response).find('input#transdata').val();
                            var suggestions_ = $(response).find('input#suggestion').val();

                            $.colorbox.close();

                            if (transferData_ == 'yes') {
                                if (addtype == "Physician") {
                                    window.location.href = "Patientemail.aspx?mailType=PhysicianTransferEmailDelete&pcount=0&choice=" + senderid;
                                }
                                else if (addtype == "FEBranch") {
                                    window.location.href = "Patientemail.aspx?mailType=NOABranchAssignment&choice=" + senderid;
                                }
                                // window.location.href = "PatientInfo.aspx?addtype=" + addtype + "&choice=" + senderid + '&trans=yes&suggestions=' + suggestions_;
                            }
                            else {
                                window.location.href = "PatientInfo.aspx?choice=" + senderid;
                            }

                            //                                $.notifyBar({
                            //                                    html: "" + addtype + " list updated",
                            //                                    close: true,
                            //                                    delay: 1000000000,
                            //                                    cls: "success"
                            //                                });
                        },
                        error: function (response) {
                            $.notifyBar({
                                html: "error",
                                close: true,
                                delay: 1000000,
                                cls: "error"
                            });
                        }
                    });
                }
                else {
                    $.notifyBar({
                        html: "Atleast one " + addtype + " should be selected",
                        close: true,
                        delay: 1000000,
                        cls: "error"
                    });
                    return false;
                }
                return false;
            });
            return false;
        });
        return false;
    });


    /******************Add Contact********************/
    $("#addContact").live('click', function () {

        var patientid = $('#senderid').val();

        var url = '../Patient/EditContact.aspx?action=Add&choice=' + patientid;
        $.colorbox({ href: url, width: 800, height: 500, opacity: 0.8,
            overlayClose: false, open: true /*,
            onClosed: function () { window.location.reload() }*/
        }, function () {
            $("*[id$='ButtonCancel']").click(function () {
                $.colorbox.close();
                return false;
            });

            $("*[id$='ButtonSave']").click(function () {

                var formvalidated = true;
                var fname = $("*[id$='txtContFirstName']").val();
                var lname = $("*[id$='txtContLastName']").val();
                var country = $("*[id$='dropCountry']").val();
                var relation = $("*[id$='dropRelationship']").val();
                var message = 'Please provide - ';

                if ((fname == '' && lname == '') || (fname == null && lname == null)) {
                    formvalidated = false;
                    message += "First name or Last name<br/>";
                }

                else if (fname.length == 0 && lname.length == 0) {
                    formvalidated = false;
                    message += "First name or Last name<br/>";
                }

                if (country <= 0) {
                    formvalidated = false;
                    message += "Country<br/>";
                }

                if (relation <= 0) {
                    formvalidated = false;
                    message += "Relationship<br/>";
                }
                if (formvalidated) {
                    $.ajax({
                        type: "POST",
                        url: "../Patient/EditContact.aspx?cid=0&action=submit&choice=" + patientid,
                        data: $("#frmContact").serialize(),
                        async: false,
                        success: function (response) {
                            window.location.href = "PatientInfo.aspx?choice=" + patientid;
                        },
                        error: function (response) {
                            $.notifyBar({
                                html: "error",
                                close: true,
                                delay: 1000000,
                                cls: "error"
                            });
                        }
                    });
                    return false;
                }
                else {
                    $.notifyBar({
                        html: message,
                        close: true,
                        delay: 1000000,
                        cls: "error"
                    });
                    return false;
                }
                return false;
            });
            return false;
        });
        return false;
    });

    /*****************Add PINC Contacts***************/
    $("#addPINCContact").live('click', function () {

        var patientid = $('#senderid').val();
        var ppin = $('#senderpin').val();
        var url = '../Patient/AddPINCContact.aspx?choice=' + patientid + '&pin=' + ppin;

        $.colorbox({ href: url, width: 450, height: 300, opacity: 0.8,
            overlayClose: false, open: true /*,
            onClosed: function () { window.location.reload() }*/
        }, function () {
            $("*[id$='ButtonContact']").click(function () {
                var selectedIDs = 0;
                var selectedValues = '';
                $("*[id$='cbContact']").find("input[type=checkbox][checked]").each(function () {
                    selectedIDs = selectedIDs + 1;
                });

                $("*[id$='cbContact2']").find("input[type=checkbox][checked]").each(function () {
                    selectedIDs = selectedIDs + 1;
                });

                if (selectedIDs == 0) {
                    $.notifyBar({
                        html: "Atleast one type of contact should be selected",
                        close: true,
                        delay: 10000000,
                        cls: "error"
                    });
                    return false;
                }
                else {
                    alert("Contact Logged");
                    $.colorbox.Close();
                }
            });

            return false;
        });
        return false;
    });


    /*****************Add/Edit Contacts***************/
    $("*[id$='editContact']").live('click', function () {

        var contactid = $('#contactid').val();
        var patientid = $('#patientid').val();

        var url = '../Patient/EditContact.aspx?action=Edit&cid=' + contactid + '&choice=' + patientid;
        $.colorbox({ href: url, width: 800, height: 500, opacity: 0.8,
            overlayClose: false, open: true /*,
            onClosed: function () { window.location.reload() }*/
        }, function () {
            //            $("*[id$='btnUpdateList']").click(function () {
            $("*[id$='ButtonSave']").click(function () {
                var formvalidated = true;
                var fname = $("*[id$='txtContFirstName']").val();
                var lname = $("*[id$='txtContLastName']").val();
                var country = $("*[id$='dropCountry']").val();
                var relation = $("*[id$='dropRelationship']").val();
                var message = 'Please provide - ';

                if ((fname == '' && lname == '') || (fname == null && lname == null)) {
                    formvalidated = false;
                    message += "First name or Last name<br/>";
                }

                else if (fname.length == 0 && lname.length == 0) {
                    formvalidated = false;
                    message += "First name or Last name<br/>";
                }

                if (country <= 0) {
                    formvalidated = false;
                    message += "Country<br/>";
                }

                if (relation <= 0) {
                    formvalidated = false;
                    message += "Relationship<br/>";
                }

                if (formvalidated) {
                    $.ajax({
                        type: "POST",
                        url: "../Patient/EditContact.aspx?cid=" + contactid + "&action=submit&choice=" + patientid,
                        data: $("#frmContact").serialize(),
                        async: false,
                        success: function (response) {
                            window.location.href = "ContactInfo.aspx?cid=" + contactid + "&choice=" + patientid;
                        },
                        error: function (response) {
                            $.notifyBar({
                                html: "error",
                                close: true,
                                delay: 1000000,
                                cls: "error"
                            });
                        }
                    });
                    return false;
                }
                else {
                    $.notifyBar({
                        html: message,
                        close: true,
                        delay: 1000000,
                        cls: "error"
                    });
                    return false;
                }

                return false;
            });
            return false;
        });
        return false;
    });

});
