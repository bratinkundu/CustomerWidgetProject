

$("#FirstName").on('change keydown paste input', function () {
    $('#FirstName').css('border-color', '');
});

$("#LastName").on('change keydown paste input', function () {
    $('#LastName').css('border-color', '');
});

$("#Email").on('change keydown paste input', function () {
    $('#Email').css('border-color', '');

    var email = document.getElementById('DuplicateEmail');
    email.innerHTML = "";
});

$("#Password").on('change keydown paste input', function () {
    $('#Password').css('border-color', '');
});

$("#Address").on('change keydown paste input', function () {
    $('#Address').css('border-color', '');
});

$("#ContactNo").on('change keydown paste input', function () {
    $('#ContactNo').css('border-color', '');
});

$("#inputCity").on('change keydown paste input', function () {
    $('#inputCity').css('border-color', '');
});

$("#inputState").on('change keydown paste input', function () {
    $('#inputState').css('border-color', '');
});

$("#inputZip").on('change keydown paste input', function () {
    $('#inputZip').css('border-color', '');
});

$("#inputConfirmPassword").on('change keydown paste input', function () {
    $('#inputConfirmPassword').css('border-color', '');
});
function customValidate() {
    var isValid = true;
    if ($('#FirstName').val() == "") {
        $('#FirstName').css('border-color', 'red');
        isValid = false;
    }

    if ($('#LastName').val() == "") {
        $('#LastName').css('border-color', 'red');
        isValid = false;
    }
    if ($('#Email').val() == "") {
        $('#Email').css('border-color', 'red');
        isValid = false;
    }
    if ($('#Password').val() == "") {
        $('#Password').css('border-color', 'red');
        isValid = false;
    }
    if ($('#Address').val() == "") {
        $('#Address').css('border-color', 'red');
        isValid = false;
    }
    if ($('#ContactNo').val() == "") {
        $('#ContactNo').css('border-color', 'red');
        isValid = false;
    }
    if ($('#inputCity').val() == "") {
        $('#inputCity').css('border-color', 'red');
        isValid = false;
    }
    if ($('#inputState').val() == "") {
        $('#inputState').css('border-color', 'red');
        isValid = false;
    }
    if ($('#inputZip').val() == "") {
        $('#inputZip').css('border-color', 'red');
        isValid = false;
    }
    if ($('#inputConfirmPassword').val() == "") {
        $('#inputConfirmPassword').css('border-color', 'red');
        isValid = false;
    }

    return isValid;
}

$("#FirstName").keyup(function () {
    var userinput = $("#FirstName").val()
    var pattern = /^[a-zA-Z ]{2,30}$/;
    if (!pattern.test(userinput)) {
        $("#FirstNameError").remove();
        $("#FirstName").after('<span class="text-danger" id="FirstNameError">Please Enter Proper FirstName</span>');
    }
    else {
        $("#FirstNameError").remove();
    }
});

$("#LastName").keyup(function () {
    var userinput = $("#LastName").val()
    var pattern = /^[a-zA-Z ]{2,50}$/;
    if (!pattern.test(userinput)) {
        $("#LastNameError").remove();
        $("#LastName").after('<span class="text-danger" id="LastNameError">Please Enter Proper LastName</span>');
    }
    else {
        $("#LastNameError").remove();
    }
});

$("#ContactNo").keyup(function () {
    var userinput = $("#ContactNo").val()
    var pattern = /^([6-9]{1})([0-9]{9})$/;
    if (!pattern.test(userinput)) {
        $("#contactError").remove();
        $("#ContactNo").after('<span class="text-danger" id="contactError">Please enter valid 10 digit Contact Number</span>');
    }
    else {
        $("#contactError").remove();
    }
});

$("#Email").keyup(function () {
    var userinput = $("#Email").val();
    var pattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    if (!pattern.test(userinput)) {
        $("#EmailError").remove();
        $("#Email").after('<span class="text-danger" id="EmailError">Please enter valid Email Id</span>');
    }
    else {
        $("#EmailError").remove();
    }

});

$("#Password").keyup(function () {
    var userinput = $("#Password").val();
    var pattern = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;

    if (!pattern.test(userinput)) {
        $("#PasswordError").remove();
        $("#Password").after('<span class="text-danger" id="PasswordError">password contain 1 special character, Upper-Lower case, Number, minimum length 8</span > ');
    }
    else {
        $("#PasswordError").remove();
    }
});

$("#inputZip").keyup(function () {
    var contactLength = $("#inputZip").val().length;
    if (contactLength != 6) {
        $("#inputZipError").remove();
        $("#inputZip").after('<span class="text-danger" id="inputZipError">Please enter 6 digit ZipCode.</span > ');
    }
    else {
        $("#inputZipError").remove();
    }
});

$("#inputConfirmPassword").keyup(function () {
    var password = document.getElementById("Password").value;
    var confirmPassword = document.getElementById("inputConfirmPassword").value;
    if (password != confirmPassword) {
        $("#inputConfirmPasswordError").remove();
        $("#inputConfirmPassword").after('<span class="text-danger" id="inputConfirmPasswordError">Password do not match.</span > ');
    }
    else {
        $("#inputConfirmPasswordError").remove();
    }
});


function register() {

    var res = customValidate();
    if (res == false) {
        //alert('Please fill the required fields.','CustomerWidget');
        return false;
    }



    if ($('#inputAddress2').val("")) {
        var empObj = {
            FirstName: $('#FirstName').val(),
            LastName: $('#LastName').val(),
            Email: $('#Email').val(),
            Password: CryptoJS.MD5($('#Password').val()).toString(),
            //Password: $('#Password').val(),
            Address: $('#Address').val() + ", " + $('#inputCity').val() + ", " + $('#inputState').val() + " " + $('#inputZip').val(),
            ContactNo: parseInt($('#ContactNo').val())
        };
        console.log(empObj + "if");
    }
    else {
        var empObj = {
            FirstName: $('#FirstName').val(),
            LastName: $('#LastName').val(),
            Email: $('#Email').val(),
            Password: CryptoJS.MD5($('#Password').val()).toString(),
            //Password: $('#Password').val(),
            Address: $('#Address').val() + ", " + $('#inputAddress2').val() + ", " + $('#inputCity').val() + ", " + $('#inputState').val() + " " + $('#inputZip').val(),
            ContactNo: parseInt($('#ContactNo').val())
        };

    }
    console.log(empObj);
    $.ajax({
        url: 'http://localhost:59699/api/CreateCustomer',
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(empObj),
        dataType: "json",


        success: function () {
            alert("Congratulations! You are registered to Customer Widget.");
            $('#FirstName').val("");
            $('#LastName').val("");
            $('#Email').val("");
            $('#Password').val("");
            $('#Address').val("");
            $('#ContactNo').val("");
            $('#inputCity').val("");
            $('#inputState').val("");
            $('#inputZip').val("");
            $('#inputConfirmPassword').val("");
            $('#inputAddress2').val("");
            window.location.href = "Customer/Register";

        },
        beforeSend: function () {
            $("#loader").css("visibility", "visible");
        },

        complete: function () {
            $("#loader").css("visibility", "hidden");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var email = document.getElementById('DuplicateEmail');
            // alert(jqXHR.responseText);
            email.innerHTML = JSON.parse(jqXHR.responseText);
            $("#loader").css("visibility", "hidden");
            $('#Email').val("");
            $('#Email').css('border-color', 'red');
        }

    });




}

$(document).ready(function () {
    $("#loader").css("visibility", "hidden");
})

//code to retrive cookie 
//{
//    HttpCookie cookie = Request.Cookies["UserInfo"];
//    string Email = cookie["Email"];
//    string Password = cookie["Password"];
//}

//function GetCarList() {
//    $.ajax({
//        url: 'http://localhost:59699/api/CarList/GetCars',
//        type: 'GET',
//        success: function (data, textStatus, xhr) {
//            console.log(data);
//            alert(data);
//        },
//        error: function (error) {
//            console.log(error);
//            alert(error);
//        }
//    })
//}
/*Login*/

$("#Email1").on('change keydown paste input', function () {
    $('#Email1').css('border-color', '');
});

$("#Password1").on('change keydown paste input', function () {
    $('#Password1').css('border-color', '');
});
function customValidate1() {
    var isValid = true;
    if ($('#Email1').val() == "") {
        $('#Email1').css('border-color', 'red');
        isValid = false;
    }
    if ($('#Password1').val() == "") {
        $('#Password1').css('border-color', 'red');
        isValid = false;
    }
    return isValid;
}



function login() {

    // alert("fn called");
    var res = customValidate1();
    if (res == false) {
        alert("Please fill the required fields.");
        return false;
    }
    //if ($('#Email1').val().trim() == null && $('#Password1').val().trim() == null)
    //{
    //    alert("Enter Details");
    //}
    //var empObj = {
    //    Email: $('#Email1').val(),
    //    Password: CryptoJS.MD5($('#Password1').val()).toString()
    //};

    document.getElementById("passval").value = CryptoJS.MD5($('#Password1').val()).toString();
    document.getElementById("Email1").value();
    $('#Email1').val("");
    $('#Password1').val("");

    return true;
}


























