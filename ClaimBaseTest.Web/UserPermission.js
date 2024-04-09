

    function BindGlobalLink() {
        debugger;
        var ProjectId = $('#ddlProjectLink').val();
        
        var data = {
            projId: ProjectId

        };

        $.ajax({
            type: 'GET',
            url: '@Url.Content("~/SetPermission/BindGlobalLinkByProjectId")',
            data: data,
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                debugger;
                if (response.length == 0) {

                } else {
                    //console.log(response);
                    $("#ddlGlobalLink").empty();
                    $("#ddlGlobalLink").append($('<option value="0">--Select Global Link--</option>'));
                    if (response.length > 0) {
                        for (var i = 0; i < response.length; i++) {
                            $("#ddlGlobalLink").append($('<option/>')
                                .val(response[i].intGLinkID)
                                .html(response[i].nvchGLinkName)
                            );
                        }

                    }

                }



            },
            error: function (error) {
                console.log(error);
            }
        });
        return true;
    }

    function BindPrimarylink() {
        debugger;
        var Globid = $('#ddlGlobalLink').val();
        var data = {
            Globid: Globid

        };

        $.ajax({
            type: 'GET',
            url: '@Url.Content("~/SetPermission/BindPrimaryLinkByGlobalID")',
            data: data,
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.length == 0) {

                } else {
                    debugger;
                    //console.log(response);
                    $("#ddlPrimaryLink").empty();
                    $("#ddlPrimaryLink").append($('<option value="0">--Select Primary Link--</option>'));
                    if (response.length > 0) {
                        for (var i = 0; i < response.length; i++) {
                            $("#ddlPrimaryLink").append($('<option/>')
                                .val(response[i].primaryid)
                                .html(response[i].primaryname)
                            );
                        }

                    }

                }
            },
            error: function (error) {
                console.log(error);
            }
        });
        return true;


    }
    function BindButton() {
        debugger;
        var Primid = $('#ddlPrimaryLink').val();
        var data = {
            Primid: Primid

        };

        $.ajax({
            type: 'GET',
            url: '@Url.Content("~/SetPermission/GetButtonByPrimaryLink")',
            data: data,
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.length == 0) {

                } else {
                    debugger;
                    //console.log(response);
                    $("#ddlbutton").empty();
                    $("#ddlbutton").append($('<option value="0">--Select Button--</option>'));
                    if (response.length > 0) {
                        for (var i = 0; i < response.length; i++) {
                            $("#ddlbutton").append($('<option/>')
                                .val(response[i].buttonid)
                                .html(response[i].buttonname)
                            );
                        }
                    }
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
        return true;
    }



    $(document).ready(function () {
        $("select.ddluser").each(function () {
            $(this).select2();
        });

        $("select.ddluserrole").each(function () {
            $(this).select2();
        });

        debugger;
        var urlParams = new URLSearchParams(window.location.search);
        var Projectid = urlParams.get('Projectid');
        var globalLinkId = urlParams.get('globalLinkId');
        var primaryLinkId = urlParams.get('primaryLinkId');
        var buttonId = urlParams.get('buttonId');

        $('#ddlProjectLink').val(Projectid);
        $('#ddlProjectLink').trigger('change');
        $('#ddlGlobalLink').val(globalLinkId);
        $('#ddlGlobalLink').trigger('change');
        $('#ddlPrimaryLink').val(primaryLinkId);
        $('#ddlPrimaryLink').trigger('change');
        $('#ddlbutton').val(buttonId);

      

        $("#btnupdate").off("click").on("click", function () {
            debugger;
            var formDataArray = [];
            var filedata = new FormData();
            var userid = '';
            // Loop through each row
            $("#tblbody tr").each(function () {
                var item = {};
                var $row = $(this);
                var hasZeroValue = false;

                // Collect data for this row
                item.INTPROJECTLINKID = $("#ddlProjectLink").val();
                item.INTGLOBALLINKID = $("#ddlGlobalLink").val();
                item.INTPRIMARYTLINKID = $("#ddlPrimaryLink").val();
                item.INTBUTTONLINKID = $("#ddlbutton").val();
                item.TabName = $row.find("td").eq(3).text();
                item.TABID = $row.find("input[name='tabId']").val();

                var selectedValues = [];
                $row.find('select[id^="ddluserupdate"]:first').each(function () {
                    var val = $(this).val();
                    selectedValues.push((val !== null && val !== undefined) ? val : ''); // Check for null or undefined values and replace them with '0'
                });
                console.log("Number of selected options role:", selectedValues.length);
                console.log(selectedValues);

                var selectedValuesrole = [];

                $row.find('select[id^="ddluserupdaterole"]:first').each(function () {
                    var val = $(this).val();
                    console.log("Selected value role:", val); // Debugging statement
                    selectedValuesrole.push(val !== null ? val : '0'); // If value is empty, set it to zero
                });
                console.log("Number of selected options role:", selectedValuesrole.length);



                // Check if any dropdown has data
                if (selectedValues.length === 0) {
                    return true; // Continue to next iteration
                }
                if (selectedValuesrole.length === 0) {
                    return true; // Continue to next iteration
                }
                var userid = selectedValues.join(',');
                item.UserIds = userid;

                var roleid = selectedValuesrole.join(',');
                item.RoleIds = roleid;

                formDataArray.push(item);
                
            });

            // If formDataArray is empty, no dropdown had data, so return false
            if (formDataArray.length === 0) {
                return false;
            }

            filedata.append("UserSet", JSON.stringify(formDataArray));
            UpdateData(filedata);
        });
    });


    
    function UpdateData(setPermissionModel) {
        debugger;
        $.ajax({
            type: "POST",
            url: '/SetPermission/UpdateUserdetail', // Assuming correct URL for the server endpoint
            data: setPermissionModel,
            contentType: false,
            processData: false,
            success: function (response) {
                // Handle success response
                alert("Permission given to the user  Successfully");
               
            },
            error: function (xhr, textStatus, errorThrown) {
                // Handle error response
                //console.error("Error sending data for row " + (index + 1) + ": " + textStatus);
            }
        });
    }

    $('#SearchData').click(function () {
        debugger;
        
        var $row = $(this);
        // Get values from dropdowns
        var Projectid = $('#ddlProjectLink').val();
        var globalLinkId = $('#ddlGlobalLink').val();
        var primaryLinkId = $('#ddlPrimaryLink').val();
        var buttonId = $('#ddlbutton').val();
        //var tabid = $row.find("input[name='tabId']").val();
        if (Projectid === '') {
            alert('Please select a Project ');
            return false;
        } else if (globalLinkId === '' || globalLinkId === '0') {
            alert('Please select a global link ');
            return false;
        } else if (primaryLinkId === '' || primaryLinkId === '0') {
            alert('Please select a primary link ');
            return false;
        } else if (buttonId === '' || buttonId === '0') {
            alert('Please select a button');
            return false;
        }
        // Prepare data for the AJAX request
        var requestData = {
            Projectid: Projectid,
            globalLinkId: globalLinkId,
            primaryLinkId: primaryLinkId,
            buttonId: buttonId,
            //tabid:tabid
        };
        location.href = "/SetPermission/UserPermission?Projectid=" + Projectid + "&globalLinkId=" + globalLinkId + "&primaryLinkId=" + primaryLinkId + "&buttonId=" + buttonId;
        // Make the AJAX request
       @* $.ajax({
            url: "/SetPermission/Binduserddl",
            method: "GET",
            data: requestData,
            dataType:'Json',
            success: function (response) {
                // Handle success response
                console.log("AJAX success:", response);

                // If the AJAX call was successful, proceed to the next step
                // Redirect to the desired location
                location.href = "/SetPermission/UserPermission?Projectid=" + Projectid + "&globalLinkId=" + globalLinkId + "&primaryLinkId=" + primaryLinkId + "&buttonId=" + buttonId;
            },
            error: function (xhr, status, error) {
               
                console.log("An error occurred while processing your request:\n" + xhr.responseText);
                // Display the error message returned by the server in an alert
                alert("An error occurred while processing your request:\n" + xhr.responseText);
            }
        });*@
    });
