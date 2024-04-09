function validateForm() {
    if ($("#txthierarchy").val() == "") {
        $("#txthierarchy").focus();
        alert(" Hierarchy Name Can't be blank.");

        return false;
    }
    else if ($("#txtnooflevel").val() == "") {
        $("#txtnooflevel").focus();
        alert("No of Level Can't be blank.");

        return false;
    }
    else if (parseFloat($('#txtnooflevel').val()) == 0) {
        $("#txtnooflevel").focus();
        alert("No of Level should be greater than 0.");

        return false;
    }
   
    else if ($("#txtaliasname").val() == "") {
        $("#txtaliasname").focus();
        alert(" Alias Name Can't be blank.");

        return false;
    }


    else {
        return confirm('Are you sure you want to  create this?');
    }
}
////For Reset---------------

function ClearField() {
   
    $('#txthierarchy').val('');
    $('#txtnooflevel').val('');
    $('#txtaliasname').val('');
    $('#txtaddress').val('');
  
}
////For Update---------------
function UpdvalidateForm() {
    if ($("#txtupdhierarchy").val() == "") {
        $("#txtupdhierarchy").focus();
        alert(" Hierarchy Name Can't be blank.");

        return false;
    }
    else if ($("#txtupdnooflevel").val() == "") {
        $("#txtupdnooflevel").focus();
        alert("No of Level Can't be blank.");

        return false;
    }
    else if ($("#txtupdaliasname").val() == "") {
        $("#txtupdaliasname").focus();
        alert(" Alias Name Can't be blank.");

        return false;
    }


    else {
        return confirm('Are you sure you want to  update this?');
    }
}
////For Active---------------
function check_uncheck_checkbox(isChecked) {
    if (isChecked) {
        $('input[name="chkbox"]').each(function () {
            this.checked = true;
        });
    }
    else {
        $('input[name="chkbox"]').each(function () {
            this.checked = false;
        });
    }
}
function chkClearField() {
    $('input[type=checkbox]').prop('checked', false);
}
function validateInactiveForm() {
    if ($('#example1 :checked').length == 0) {
        alert("No record selected for Inactive!");
        return false;
    }
    else {
        return confirm('Are you sure you want to Mark As In-Active?');
    }
}



///-------For Inactive-----------------

function check_uncheck_checkbox_Inactive(isChecked) {
    if (isChecked) {
        $('input[name="chkbox"]').each(function () {
            this.checked = true;
        });
    }
    else {
        $('input[name="chkbox"]').each(function () {
            this.checked = false;
        });
    }
}
function chkClearField_Inactive() {
    $('input[type=checkbox]').prop('checked', false);
}
function validateActiveForm() {
    if ($('#example1 :checked').length == 0) {
        alert("No record selected for Active!");
        return false;
    }
    else {
        return confirm('Are you sure you want to Mark As Active?');
    }
}


function inputLimiter(e, allow) {
    var AllowableCharacters = '';

    if (allow == 'NameCharactersymbol') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz./&';
    }
    if (allow == 'NameCharacters') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-.\'';
    }
    if (allow == 'Email') {
        AllowableCharacters = '1234567890@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_.';
    }
    if (allow == 'Description') {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-.,()/';
    }
    if (allow == 'NameCharactersAndNumbers') {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-,/.\'';
    }
    if (allow == 'Numbers') {
        AllowableCharacters = '1234567890';
    }
    if (allow == 'Decimal') {
        AllowableCharacters = '1234567890.';
    }
    if (allow == 'Filename') {
        AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/';
    }
    var k;
    k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
    if (k != 13 && k != 8 && k != 0) {
        if ((e.ctrlKey == false) && (e.altKey == false)) {
            return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}