
    $("#FirstName").on('change keydown paste input', function () {
        $('#FirstName').css('border-color', '');
    });

    $("#LastName").on('change keydown paste input', function () {
        $('#LastName').css('border-color', '');
    });

$("#Email").on('change keydown paste input', function () {
        $('#Email').css('border-color', '');
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

    function ValidateConfirmPassword() {
        var password = document.getElementById("Password").value;
    var confirmPassword = document.getElementById("inputConfirmPassword").value;
        if (password != confirmPassword) {

            return false;
}
return true;
}

    function ValidateEmail() {
        var userinput = $("#Email").val();
        var pattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

        if (!pattern.test(userinput)) {
            return false;
}
return true;
}
    function ValidatePassword() {
        var userinput = $("#Password").val();
        var pattern = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;

        if (!pattern.test(userinput)) {
            return false;
}
return true;
}

    function ValidateContact() {
        var userinput = $("#ContactNo").val()
        var pattern = /^([6-9]{1})([0-9]{9})$/;
        if (!pattern.test(userinput)) {
            return false;
}
return true;
}

    function ValidateZipCode() {
        var contactLength = $("#inputZip").val().length;
        if (contactLength != 6) {
            return false;
}
return true;
}


function register() {
  
        var res = customValidate();
        if (res == false) {
            //  alert("Please fill the required fields.");
            return false;
}

var val = ValidateEmail();
        if (val == false) {
        alert("Please enter valid EmailID.");
    return false;
}

var val = ValidateContact();
        if (val == false) {
        alert("Please enter valid 10 digit Contact Number.");
    return false;
}

var val = ValidateZipCode();
        if (val == false) {
        alert("Please enter 6 digit ZipCode.");
    return false;
}

var val = ValidatePassword();
        if (val == false) {
        alert("password contain 1 special character, Upper-Lower case, Number, minimum length 8");
    //alert("Password must be of minimum 8 characters length and maximum 23 characters length. Password must contain at least 1 lowercase, 1 uppercase, 1 numeric character and at least one special character. ");
    return false;
}

var val = ValidateConfirmPassword();
        if (val == false) {
        alert("Passwords do not match.");
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
            console.log(empObj +"if");
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
            url: 'http://localhost:59699/api/Customers/PostCustomer',
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
                alert(jqXHR.responseText);
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

    //function login() {
    //    var res = customValidate1();
    //    if (res == false) {
    //        //  alert("Please fill the required fields.");
    //        return false;
    //    }
    //    if ($('#Email1').val().trim() == null && $('#Password1').val().trim() == null) {
    //        alert("Enter Details");
    //    }
    //    var empObj = {
    //        Email: $('#Email1').val(),
    //        Password: CryptoJS.MD5($('#Password1').val()).toString()

    //    };
    //    $('#Email1').val("");
    //    $('#Password1').val("");

    //    $.ajax({
    //        url: 'http://localhost:59699/api/LoginAPI/PostCustomer',
    //        type: 'POST',
    //        data: JSON.stringify(empObj),
    //        contentType: "application/json;charset=utf-8",
    //        dataType: "json",
    //        success: function (response) {
    //            debugger;
    //            alert("Congratulations! You are login succesfully.");
    //            alert(response.resp);
    //            window.location.href = "/Customer/CarList"
    //        },
    //        error: function () {
    //            alert("Your Login or Password is invalid");
    //        }

    //    });

    //}
